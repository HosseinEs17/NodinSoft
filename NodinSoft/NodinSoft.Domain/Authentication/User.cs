using NodinSoft.Domain.ProductManagement;

namespace NodinSoft.Domain.Authentication
{
    public class User
    {
        public int Id { get; private set; }
        public string? Email { get; set; } // Keep public set for UpdateContactInfo
        public string? PhoneNumber { get; set; } // Keep public set for UpdateContactInfo
        public string? Password { get; private set; }
        public ICollection<Product> Products { get; set; } = new List<Product>(); // Initialize

        // EF Core constructor
        private User() { }

        public User(string email, string password, string? phoneNumber) // Added phoneNumber
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email cannot be null or empty.", nameof(email));
            if (!email.Contains('@') || !email.Contains('.')) // Basic email format check
                throw new ArgumentException("Email is not in a valid format.", nameof(email));
            if (string.IsNullOrWhiteSpace(password)) // Assuming password here is the hash
                throw new ArgumentException("Password hash cannot be null or empty.", nameof(password));

            // Optional: Validate phoneNumber format if provided
            // if (!string.IsNullOrWhiteSpace(phoneNumber) && !IsValidPhoneNumber(phoneNumber)))
            //     throw new ArgumentException("Phone number is not in a valid format.", nameof(phoneNumber));

            Email = email;
            Password = password; // This is the hashed password
            PhoneNumber = phoneNumber;
        }

        public void ChangePassword(string currentPasswordHash, string newPasswordHash)
        {
            if (Password != currentPasswordHash) // Assuming Password stores the current hash
                throw new InvalidOperationException("Current password does not match.");
            if (string.IsNullOrWhiteSpace(newPasswordHash))
                throw new ArgumentException("New password hash cannot be null or empty.", nameof(newPasswordHash));

            Password = newPasswordHash;
        }

        public void UpdateContactInfo(string newEmail, string? newPhoneNumber)
        {
            if (string.IsNullOrWhiteSpace(newEmail))
                throw new ArgumentException("Email cannot be null or empty.", nameof(newEmail));
            if (!newEmail.Contains('@') || !newEmail.Contains('.')) // Basic email format check
                throw new ArgumentException("Email is not in a valid format.", nameof(newEmail));

            Email = newEmail;
            PhoneNumber = newPhoneNumber; // Allowed to be null or empty
        }
    }
}