namespace NextPassword.MVVM.Models
{
    public class UserBase
    {
        public string Email { get; set; }
        public string Password { get; set; }

        // Constructeur avec les deux propriétés requises
        public UserBase(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}
