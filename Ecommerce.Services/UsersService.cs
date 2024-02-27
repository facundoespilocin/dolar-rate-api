using AutoMapper;
using Ecommerce.DataAccessLayer.Models;
using Ecommerce.DataAccessLayer.Repositories.Interfaces;
using Ecommerce.Services.Interfaces;
using Ecommerce.Utils.EmailService;
using Ecommerce.Utils.Settings;
using Ecommerce.Utils.Extensions;
using Microsoft.Extensions.Options;
using System.Reflection;
using User = Ecommerce.DataAccessLayer.Entities.User.User;
using CreateUserRequest = Ecommerce.DataAccessLayer.Models.CreateUserRequest;
using Ecommerce.DataAccessLayer.Dtos.User;
using Ecommerce.DataAccessLayer.Entities.Customer;

namespace Ecommerce.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _usersRepository;
        private readonly AppSettings _appSettings;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;

        public UsersService(IUsersRepository usersRepository, IOptions<AppSettings> appSettings, IMapper mapper, IEmailService emailService)
        {
            _usersRepository = usersRepository;
            _appSettings = appSettings.Value;
            _mapper = mapper;
            _emailService = emailService;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            var result = await _usersRepository.GetAll();

            return result;
        }

        public async Task<User> GetById(long userId)
        {
            var user = await _usersRepository.GetById(userId);

            return user;
        }
        
        public async Task<CustomerDataDTO> GetCustomerByEmail(string email)
        {
            var user = await _usersRepository.GetCustomerByEmail(email);

            return user;
        }

        public async Task UpdateUser(User userRequest)
        {
            User user;

            user = await _usersRepository.GetById(userRequest.Id);

            await _usersRepository.Update(user);
        }

        public async Task<ServiceResponse> Create(CreateUserRequest request)
        {
            ServiceResponse response = new()
            {
                Metadata = new Metadata { },
                Data = new Data { }
            };

            try
            {
                response = await _usersRepository.Create(request);

                var directoryName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

                var htmlText = File.ReadAllText(Path.Combine(directoryName, "EmailFiles/Invitation.html"));

                string fullName = request.Name + ' ' + request.LastName;

                var textCopy = htmlText;

                textCopy = textCopy.Replace("#FULLNAME", fullName)
                    .Replace("#INVITELINK", _appSettings.BaseUrl.AppendToURL($"Register/Confirm?token={request.ConfirmationToken}"))
                    .Replace("#COMPANYNAME", "Ecommerce");

                var subject = "Welcome to Ecommerce";

                _emailService.Send(request.Email, subject, textCopy, request.Email);
            }
            catch (Exception e)
            {
                response.Metadata.Status = System.Net.HttpStatusCode.BadRequest;
                response.Metadata.Message = e.Message;
            }

            return response;
        }

        public async Task<ResetPasswordResponse> UpdateUserPassword(ResetPasswordRequest request)
        {
            var validation = await ValidateResetPasswordToken(request.Token);

            if (validation)
            {
                await _usersRepository.UpdateUserPassword(request.Token, request.NewPassword);

                return new ResetPasswordResponse { Succeded = true };
            }

            return new ResetPasswordResponse { Succeded = false };
        }

        public async Task<bool> ValidateResetPasswordToken(string token)
        {
            var user = await _usersRepository.GetUserByToken(token);

            if (user is not null)
            {
                if (!string.IsNullOrEmpty(user.PasswordResetToken))
                {
                    return true;
                }
            }

            return false;
        }

        public async Task PostForgotPassword(string email)
        {
            var user = await _usersRepository.GetUserByEmailWithPass(email) ?? throw new Exception($"Email {email} does not exist");

            user.PasswordResetToken = Guid.NewGuid().ToString();

            await _usersRepository.UpdateResetPasswordToken(email, user.PasswordResetToken);

            var directoryName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            var htmlText = File.ReadAllText(Path.Combine(directoryName, "EmailFiles/ForgotPassword.html"));
            
            var textCopy = htmlText;

            textCopy = textCopy
                .Replace("#INVITELINK", _appSettings.BaseUrl.AppendToURL($"ForgotPassword/Confirm?token={user.PasswordResetToken}"))
                .Replace("#COMPANYNAME", "Ecommerce")
                .Replace("#INVITELINK", user.PasswordResetToken);

            var subject = "You received a password reset request from Ecommerce";

            _emailService.Send(email, subject, textCopy, email);
        }

        public async Task<UserDataDTO> GetUserDataById(long userId)
        {
            var user = await _usersRepository.GetById(userId);

            return _mapper.Map<UserDataDTO>(user);
        }

        public async Task<ServiceResponse> ConfirmAccount(string token)
        {
            ServiceResponse response = new()
            {
                Metadata = new Metadata
                {
                    Message = "Success",
                    Status = System.Net.HttpStatusCode.OK
                },
                Data = new Data { }
            };

            try
            {
                await _usersRepository.ConfirmAccount(token);
            }
            catch (Exception e)
            {
                response.Metadata.Status = System.Net.HttpStatusCode.BadRequest;
                response.Metadata.Message = e.Message;
            }

            return response;
        }
    }
}