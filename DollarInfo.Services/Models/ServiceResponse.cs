using DollarInfo.Services.Models;

namespace DollarInfo.DAL.Models
{
    public class ServiceResponse<T>
    {
        public List<ServiceError> Errors { get; set; } = new List<ServiceError>();
        public T Data { get; set; }

        public bool Status => !Errors.Any();

        public void AddError(Exception ex)
        {
            Errors.Add(new ServiceError(ex));
        }

        public void AddError(string errorCode)
        {
            Errors.Add(new ServiceError(errorCode));
        }

        public void AddError(string errorCode, string errorMessage)
        {
            Errors.Add(new ServiceError(errorCode, errorMessage));
        }

        public void AddError(string errorCode, string errorMessage, string errorDetail)
        {
            Errors.Add(new ServiceError(errorCode, errorMessage, errorDetail));
        }

        public void AddError(ServiceError serviceError)
        {
            Errors.Add(serviceError);
        }

        public void AddErrors(List<ServiceError> serviceErrorList)
        {
            foreach (ServiceError serviceError in serviceErrorList)
            {
                Errors.Add(serviceError);
            }
        }
    }
}