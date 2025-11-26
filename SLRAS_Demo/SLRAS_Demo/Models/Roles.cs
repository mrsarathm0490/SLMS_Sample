namespace SLRAS_Demo.Models
{
    public class Roles
    {
        public int? Id { get; set; }
        public required string Name { get; set; }
        public int IsActive { get; set; } = 1;
    }
}
