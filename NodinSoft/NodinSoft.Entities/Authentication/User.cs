using NodinSoft.Entities.ProductManagement;

namespace NodinSoft.Entities.Authentication
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}