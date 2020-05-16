namespace Auth
{
    public class UserCredentials
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Salt { get; set; }

        public string PasswordHash { get; set; }

        public UserCredentials() { }
    }
}