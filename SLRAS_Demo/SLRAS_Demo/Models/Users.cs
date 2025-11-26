namespace SLRAS_Demo.Models
{
    public class Users
    {
        public int? Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required string MobileNumber { get; set; }
        public byte[]? Passwordhash { get; set; }
        public byte[]? PasswordSalt { get; set; }
        public int IsActive { get; set; } = 1;
        public required List<Roles> Roles { get; set; }

    }
}
