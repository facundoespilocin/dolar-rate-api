using DollarInfo.DAL.Dtos.User;

namespace DollarInfo.DAL.Aspect
{
    public interface ICurrentUserAspect
    {
        long? CurrentUserId { get; }
        //long? DefaultCompanyId { get; }
        UserDataDTO CurrentUser { get; }
    }
}