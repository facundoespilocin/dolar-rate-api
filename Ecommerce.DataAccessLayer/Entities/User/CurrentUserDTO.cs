namespace Ecommerce.DataAccessLayer.Entities.User
{
    public class CurrentUserDTO
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