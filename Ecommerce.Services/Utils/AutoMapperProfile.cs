using AutoMapper;
using Ecommerce.DataAccessLayer.Dtos.User;
using Ecommerce.DataAccessLayer.Entities.User;
using Ecommerce.DataAccessLayer.Models;

namespace Ecommerce.Services.Utils
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //var hasher = new PasswordHasher<User>();

            CreateMap<User, AuthResponse>();

            CreateMap<User, UserDataDTO>();
        }
    }
}