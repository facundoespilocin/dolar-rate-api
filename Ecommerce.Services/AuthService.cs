using AutoMapper;
using Ecommerce.DataAccessLayer.Models;
using Ecommerce.DataAccessLayer.Repositories.Interfaces;
using Ecommerce.Services.Interfaces;
using Ecommerce.Utils.EmailService;
using Ecommerce.Utils.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Security.Cryptography;
using Ecommerce.DataAccessLayer.Aspect;
using Ecommerce.Utils.Exceptions;

namespace Ecommerce.Services
{
    public class AuthService : IAuthService
    {
        private readonly ICurrentUserAspect _currentUser;
        private readonly AppSettings _appSettings;
        private readonly IMapper _mapper;
        private readonly IUsersRepository _usersRepository;
        private readonly IEmailService _emailService;
        private readonly IUsersService _usersService;

        public AuthService(ICurrentUserAspect currentUser, IOptions<AppSettings> appSettings, IMapper mapper, IUsersRepository usersRepository, IEmailService emailService, IUsersService usersService)
        {
            _currentUser = currentUser;
            _appSettings = appSettings.Value;
            _mapper = mapper;
            _usersRepository = usersRepository;
            _emailService = emailService;
            _usersService = usersService;
        }

        public async Task<AuthResponse> Authenticate(AuthRequest request)
        {
            var response = await GetByCredentials(request.UserName, request.Password);

            response.UserData = await _usersService.GetUserDataById(response.Id);

            response.Token = GenerateJwtToken(response);

            response.RefreshToken = GenerateRefreshToken(response.Id);

            return response;
        }

        public async Task<AuthResponse> RefreshToken(string refreshToken)
        {
            var userId = _currentUser.CurrentUserId;

            var user = await _usersService.GetById(Convert.ToInt64(userId));

            if (user is null)
            {
                throw new Exception($"Invalid UserId {userId}.");
            }

            //if (!user.RefreshToken.Equals(refreshToken))
            //{
            //    throw new WrongCredentials("Invalid Refresh Token.");
            //}
            //else if (user.TokenExpires < DateTime.Now)
            //{
            //    throw new WrongCredentials("Token expired.");
            //}

            var userData = await _usersService.GetUserDataById(user.Id);

            string token = GenerateJwtToken(new AuthenticatedUser()
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                //DefaultCompanyId = userData.DefaultCompany.Id
            });

            var newRefreshToken = GenerateRefreshToken(user.Id);

            //await UpdateRefreshToken(user.Id, newRefreshToken);

            return new AuthResponse()
            {
                Email = user.Email,
                FullName = user.FullName,
                Id = user.Id,
                Token = token,
                UserData = userData,
                RefreshToken = newRefreshToken
            };
        }

        public async Task<AuthResponse> GetByCredentials(string userName, string password)
        {
            var user = await _usersRepository.GetByCredentials(userName, password) ?? throw new Exception($"Username {userName} does not exist or Password is incorrect");

            if (!user.ConfirmedAccount)
                throw new Exception($"Account isn't confirmed");

            return _mapper.Map<AuthResponse>(user);
        }

        private string GenerateJwtToken(AuthenticatedUser user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.Secret));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            // Block used to define user data to drive business decisions
            List<Claim> claims = new()
            {
                new Claim(ClaimTypes.Sid, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.FullName),
                new Claim("Email", user.Email),
                //new Claim("DefaultCompanyId", user.DefaultCompanyId.ToString())
            };

            var token = new JwtSecurityToken(
               claims: claims,
               expires: DateTime.Now.AddDays(7),
               signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        private static RefreshToken GenerateRefreshToken(long userId)
        {
            var refreshToken = new RefreshToken
            {
                UserId = userId,
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                Expires = DateTime.Now.AddDays(7),
                Created = DateTime.Now
            };

            return refreshToken;
        }
    }
}