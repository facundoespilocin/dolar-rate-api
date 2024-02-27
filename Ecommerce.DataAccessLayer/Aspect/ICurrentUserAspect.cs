using Ecommerce.DataAccessLayer.Dtos.User;

namespace Ecommerce.DataAccessLayer.Aspect
{
    public interface ICurrentUserAspect
    {
        long? CurrentUserId { get; }
        //long? DefaultCompanyId { get; }
        UserDataDTO CurrentUser { get; }
    }
}