using NodinSoft.Domain.Authentication;

namespace NodinSoft.Domain.ProductManagement
{
    public class Product
    {
        public int Id { get; private set; }
        public string? Name { get; set; }
        public DateTime ProduceDate { get; private set; }
        public string? ManufacturePhone { get; set; }
        public string? ManufactureEmail { get; set; }
        public bool IsAvailable { get; private set; } // Changed to private set, managed by methods
        public int UserId { get; private set; }
        // User navigation property removed (Step 5)

        // EF Core constructor
        private Product() { }

        public Product(string name, DateTime produceDate, int userId, string manufactureEmail, string manufacturePhone)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Product name cannot be null or empty.", nameof(name));
            if (produceDate == default) // Using default to check against DateTime.MinValue
                throw new ArgumentException("Produce date must be a valid date.", nameof(produceDate));
            if (userId <= 0) // Assuming UserId should be positive
                throw new ArgumentException("User ID must be a positive integer.", nameof(userId));
            if (string.IsNullOrWhiteSpace(manufactureEmail))
                throw new ArgumentException("Manufacture email cannot be null or empty.", nameof(manufactureEmail));
            if (!manufactureEmail.Contains('@') || !manufactureEmail.Contains('.')) // Basic email format check
                throw new ArgumentException("Manufacture email is not in a valid format.", nameof(manufactureEmail));
            if (string.IsNullOrWhiteSpace(manufacturePhone)) // Assuming phone is required
                throw new ArgumentException("Manufacture phone cannot be null or empty.", nameof(manufacturePhone));

            Name = name;
            ProduceDate = produceDate;
            UserId = userId;
            ManufactureEmail = manufactureEmail;
            ManufacturePhone = manufacturePhone;
            IsAvailable = true; // Default state
        }

        public void MarkAsAvailable()
        {
            IsAvailable = true;
        }

        public void MarkAsUnavailable()
        {
            IsAvailable = false;
        }

        public void UpdateDetails(string newName, string newManufacturePhone, string newManufactureEmail)
        {
            if (string.IsNullOrWhiteSpace(newName))
                throw new ArgumentException("Product name cannot be null or empty.", nameof(newName));
            if (string.IsNullOrWhiteSpace(newManufactureEmail))
                throw new ArgumentException("Manufacture email cannot be null or empty.", nameof(newManufactureEmail));
            if (!newManufactureEmail.Contains('@') || !newManufactureEmail.Contains('.')) // Basic email format check
                throw new ArgumentException("Manufacture email is not in a valid format.", nameof(newManufactureEmail));
            if (string.IsNullOrWhiteSpace(newManufacturePhone)) // Assuming phone is required
                throw new ArgumentException("Manufacture phone cannot be null or empty.", nameof(newManufacturePhone));

            Name = newName;
            ManufacturePhone = newManufacturePhone;
            ManufactureEmail = newManufactureEmail;
        }
    }
}