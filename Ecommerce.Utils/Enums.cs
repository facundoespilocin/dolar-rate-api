namespace Ecommerce.Utils
{
    public static class Enums
    {
        public enum TokenTypes
        {
            ConfirmToken = 1,
            ForgotToken = 2,
            InvitedToken = 3
        }

        public enum DataSourceTypes
        {
            Excel = 1,
            Connector = 2,
        }

        public enum StatusTypes
        {
            NotActive = 1,
            Active = 2,
            All = 3,
        }

        public enum DocumentTypes
        {
            DNI = 1,
            LCE = 2,
            Passport = 3,
        }

        public enum GenderTypes
        {
            Male = 1,
            Female = 2,
            Other = 3
        }

        public enum OrderTypes
        {
            Initialized = 1,
            Finished = 2,
            Error = 3,
        }
    }
}