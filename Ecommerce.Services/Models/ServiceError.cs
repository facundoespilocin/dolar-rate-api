namespace Ecommerce.Services.Models
{
    public class ServiceError
    {
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public string ErrorDetail { get; set; }
        public object Reference { get; set; }
        public object ExternalReference { get; set; }

        public ServiceError()
        {
        }

        public ServiceError(Exception ex)
        {
            Exception ex2 = ex;
            if (ex.InnerException != null)
            {
                ex2 = ex.InnerException;
            }

            ErrorMessage = ex2.Message;
            ErrorDetail = ex2.ToString();
        }

        public ServiceError(string errorCode)
        {
            ErrorCode = errorCode;
        }

        public ServiceError(string errorCode, string errorMessage)
            : this(errorCode)
        {
            ErrorMessage = errorMessage;
        }

        public ServiceError(string errorCode, string errorMessage, string errorDetail)
            : this(errorCode, errorMessage)
        {
            ErrorDetail = errorDetail;
        }
    }
}