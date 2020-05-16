namespace Auth
{
    public interface IUserService
    {
        bool IsValidUser(string username, string password);
    }

    public class UserService : IUserService
    {
        private IUserRepository repo;

        public UserService(IUserRepository r)
        {
            repo = r;
        }

        public bool IsValidUser(string username, string password)
        {
            UserCredentials testUser = repo.GetUser(username);
            
            return Password.CheckPassword(password, testUser.Salt, testUser.PasswordHash);
        }
    }
}