using Dapper;
using Ecommerce.DataAccessLayer.Entities.Customer;
using Ecommerce.DataAccessLayer.Entities.User;
using Ecommerce.DataAccessLayer.Factory;
using Ecommerce.DataAccessLayer.Models;
using Ecommerce.DataAccessLayer.Repositories.Interfaces;
using Ecommerce.Utils.Extensions;

namespace Ecommerce.DataAccessLayer.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private IConnectionFactory _factory;

        public UsersRepository(IConnectionFactory factory)
        {
            _factory = factory;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            using var con = _factory.GetDbConnection;

            var query = "SELECT * FROM Users";

            var user = await con.QueryAsync<User>(query);

            return user;
        }

        public async Task<User> GetById(long userId)
        {
            using var con = _factory.GetDbConnection;

            var query = $"SELECT * FROM Users WHERE Id = {userId}";

            var user = await con.QueryFirstOrDefaultAsync<User>(query);

            return user;
        }

        public async Task<CustomerDataDTO> GetCustomerByEmail(string email)
        {
            using var con = _factory.GetDbConnection;

            var query = @$"SELECT c.*, u.Email 
                           FROM Customers c
                           INNER JOIN Users u ON u.Id = c.Id
                           WHERE u.Email = '{email}';";

            var user = await con.QueryFirstOrDefaultAsync<CustomerDataDTO>(query);

            return user;
        }

        public async Task<ServiceResponse> Create(CreateUserRequest request)
        {
            using var con = _factory.GetDbConnection;

            var query = @"INSERT INTO Users (Name, LastName, Email, Password, CountryId, IndustryId, CellPhone, ConfirmationToken, BirthDate, CreatedDate)
                          VALUES (@Name, @LastName, @Email, @Password, @CountryId, @IndustryId, @CellPhone, @ConfirmationToken, @BirthDate, @CreatedDate);
                          SELECT LAST_INSERT_ID();";

            var result = await con.ExecuteScalarAsync<int>(query, new
            {
                request.Name,
                request.LastName,
                request.Email,
                Password = request.Password?.GenerateHash(),
                request.CountryId,
                request.IndustryId,
                request.ConfirmationToken,
                request.CellPhone,
                request.BirthDate,
                CreatedDate = DateTime.Now
            });

            return new ServiceResponse
            {
                Metadata = new Metadata
                {
                    Message = "Success",
                    Status = System.Net.HttpStatusCode.OK
                },
                Data = new Data
                {
                    Id = result,
                    Name = request.Name
                }
            };
        }

        public async Task Update(User user)
        {
            using var con = _factory.GetDbConnection;

            var query = @"UPDATE Users SET Name=@Name, Height=@Height, Mass=@Mass, HairColor=@HairColor, 
                        SkinColor=@SkinColor, EyeColor=@EyeColor, BirthYear=@BirthYear, Gender=@Gender, 
                        Homeworld=@Homeworld, Created=@Created, Edited=@Edited, Url=@Url
                        WHERE Id=@Id";

            await con.ExecuteAsync(query, user);
        }

        public async Task Delete(int userId)
        {
            using var con = _factory.GetDbConnection;

            var query = $@"DELETE FROM Users WHERE Id = {userId}";

            await con.ExecuteAsync(query);
        }

        public async Task<User> GetByCredentials(string email, string password)
        {
            using var con = _factory.GetDbConnection;

            var passwordHash = password.GenerateHash();

            var user = await con.QueryFirstOrDefaultAsync<User>(@"SELECT * FROM Users WHERE Email = @Email", new
            {
                Email = email,
                Password = passwordHash
            });

            return user;
        }

        public async Task<User> GetUserByToken(string token)
        {
            using var con = _factory.GetDbConnection;

            var user = await con.QueryFirstOrDefaultAsync<User>(@"SELECT * FROM Users WHERE PasswordResetToken = @Token AND HasRequestedPasswordReset = 1;", new
            {
                Token = token
            });

            return user;
        }

        public async Task UpdateUserPassword(string token, string password)
        {
            using var con = _factory.GetDbConnection;

            var passwordHash = password?.GenerateHash();

            var result = await con.QueryAsync<int>(@"UPDATE Users SET Password = @Password, HasRequestedPasswordReset = 0, PasswordResetToken = NULL WHERE PasswordResetToken = @Token;", new
            {
                Password = passwordHash,
                Token = token
            });
        }

        public async Task<User> GetUserByEmailWithPass(string email)
        {
            using var con = _factory.GetDbConnection;

            var user = await con.QueryFirstOrDefaultAsync<User>(@"SELECT * FROM Users WHERE Email = @Email AND Password != '';", new
            {
                Email = email
            });

            return user;
        }

        public async Task UpdateResetPasswordToken(string email, string token)
        {
            using var con = _factory.GetDbConnection;

            var result = await con.QueryAsync<int>(@"UPDATE Users SET HasRequestedPasswordReset = 1, PasswordResetToken = @Token WHERE Email = @Email;", new
            {
                Email = email,
                Token = token
            });
        }
        
        public async Task ConfirmAccount(string token)
        {
            using var con = _factory.GetDbConnection;

            await con.ExecuteAsync(@"UPDATE Users SET ConfirmedAccount = IF(ConfirmationToken = @Token, 1, ConfirmedAccount);", new
            {
                Token = token
            });
        }
    }
}