using NodinSoft.Entities.Authentication;

namespace NodinSoft.Entities.ProductManagement
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime ProduceDate { get; set; }
        public string ManufacturePhone { get; set; }
        public string ManufactureEmail { get; set; }
        public bool IsAvailable { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}