namespace SLRAS_Demo.ViewModels
{
    public class UserViewModel
    {
        public int? Id { get; set; }
        public required  string FirstName { get; set; }
        public required string LastName { get; set; } 
        public required string Email { get; set; }        
        public required string MobileNumber { get; set; }
        public required List<RoleViewModel> roleViewModel { get; set; }
    }
}
