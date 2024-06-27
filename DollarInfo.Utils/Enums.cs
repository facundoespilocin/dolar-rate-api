using System.Runtime.Serialization;

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

        public enum InflationIndexTypes
        {
            [EnumMember(Value = "MonthlyInflationIndexes")]
            Monthly = 1,
            [EnumMember(Value = "YearOnYearInflationIndexes")]
            YearOnYear = 2,
            [EnumMember(Value = "UvaInflationIndexes")]
            Uva = 3,
        }

        public enum InsertionTypes
        {
            Single = 1,
            Bulk = 2
        }

        public enum ExchangeRateType
        {
            Oficial = 1,
            Blue = 2,
            Bolsa = 3,
            Contadoconliquidación = 4,
            Mayorista = 5,
            Cripto = 6,
            Tarjeta = 7,
            Otro = 8
        }
    }
}