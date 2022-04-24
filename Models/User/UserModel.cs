namespace MarieSundbergAssignment.Models.User
{
    public class UserModel
    {
        public UserModel()
        {

        }

        public UserModel(string firstName, string lastName, string userPassword, string userEmail, string streetAddress, string zipCode, string city)
        {
            FirstName = firstName;
            LastName = lastName;
            UserPassword = userPassword;
            UserEmail = userEmail;
            StreetAddress = streetAddress;
            ZipCode = zipCode;
            City = city;
        }

        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string UserPassword { get; set; } = null!;
        public string UserEmail { get; set; } = null!;
        public string StreetAddress { get; set; } = null!;
        public string ZipCode { get; set; } = null!;
        public string City { get; set; } = null!;
    }
}
