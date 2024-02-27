namespace Ecommerce.DataAccessLayer.Models
{
    public class CreateUserRequest
    {
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public int CountryId { get; set; }
        public int IndustryId { get; set; }
        public string ConfirmationToken { get; set; } = Guid.NewGuid().ToString();
        public string? CellPhone { get; set; }
        public DateTime BirthDate { get; set; }
    }
}