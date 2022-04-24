namespace MarieSundbergAssignment.Models.User
{
    public class User
    {
        public User()
        {

        }
        public User(int id, string firstName, string lastName, string userPassword, string email, string streetAddress, string zipCode, string city)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            UserPassword = userPassword;
            Email = email;
            StreetAddress = streetAddress;
            ZipCode = zipCode;
            City = city;
        }

        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string UserPassword { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string StreetAddress { get; set; } = null!;
        public string ZipCode { get; set; } = null!;
        public string City { get; set; } = null!;
    }
}
