using System.ComponentModel.DataAnnotations;

namespace Auth
{
    public class User
    {
        public User(string username, string password)
        {
            Username = username;
            _password = new Password(password);
        }

        private Password _password;

        //ругается без него
        public User() { }
       
        public int Id { get; set; }

        [Required(ErrorMessage = "Username is not set")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Недопустмая длина имени")]
        public string Username { get; set; }
        
        public string GetPasswordHash()
        {
            return _password.PasswordHash;
        }

        public string GetSalt()
        {
            return _password.Salt;
        }
    }
}