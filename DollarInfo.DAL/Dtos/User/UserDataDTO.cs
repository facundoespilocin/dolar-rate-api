// The difference between a DTO and a traditionally used class is that the DTO is used to implement
// the result of mapping classes by implementing AutoMapperProfile

// AutoMapperProfile inherits from the Profile class, which belongs to the AutoMapper library

// AutoMapper library is used on Services project
namespace DollarInfo.DAL.Dtos.User
{
    public class UserDataDTO
    {
        public long Id { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public int CountryId { get; set; }
        public int CurrencyId { get; set; }
        public string? CellPhone { get; set; }
        public DateTime BirthDate { get; set; }
    }
}