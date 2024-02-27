using Ecommerce.DataAccessLayer.Dtos.User;
using Ecommerce.Utils.Exceptions;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Ecommerce.DataAccessLayer.Aspect
{
    public class CurrentUserAspect : ICurrentUserAspect
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserAspect(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public long? CurrentUserId
        {
            get
            {
                var userId = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Sid)?.Value;

                return string.IsNullOrEmpty(userId) ? null : Convert.ToInt64(userId);
            }
        }

        // Constructor used to implement DefaultCompanyId value attribute 
        //public long? DefaultCompanyId
        //{
        //    get
        //    {
        //        var companyId = _httpContextAccessor.HttpContext.User?.FindFirst("DefaultCompanyId")?.Value;

        //        return string.IsNullOrEmpty(companyId) ? null : Convert.ToInt64(companyId);
        //    }
        //}

        public UserDataDTO CurrentUser
        {
            get
            {
                var userId = _httpContextAccessor.HttpContext.User?.FindFirst(ClaimTypes.Sid)?.Value;
                var fullName = _httpContextAccessor.HttpContext.User?.FindFirst(ClaimTypes.Name)?.Value;
                var email = _httpContextAccessor.HttpContext.User?.FindFirst(ClaimTypes.Email)?.Value;

                if (userId is null)
                {
                    throw new AuthException();
                }

                return new UserDataDTO()
                {
                    Id = Convert.ToInt64(userId),
                    FullName = fullName,
                    Email = email,
                };
            }
        }
    }
}