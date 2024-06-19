namespace DollarInfo.Utils
{
    public static class Enums
    {
        public enum TokenTypes
        {
            ConfirmToken = 1,
            ForgotToken = 2,
            InvitedToken = 3
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
    }
}