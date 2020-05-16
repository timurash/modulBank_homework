using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;


namespace Auth
{
    public interface IUserRepository
        {
            void Register(User user);
            void Update(User user);
            void DeleteUser(int id);
            User GetUser(int id);
            UserCredentials GetUser(string username);
            List<User> GetUsers();
        }

    public class UserRepository : IUserRepository
    {
        string connectionString;

        public UserRepository(IConfiguration configuration)
        {
            connectionString = configuration.GetValue<string>("DBInfo:ConnectionString");
        }

        internal IDbConnection Connection
        {
            get { return new NpgsqlConnection(connectionString); }
        }

        public UserRepository(string conn)
        {
            connectionString = conn;
        }

        public User GetUser(int id)
        {
            using (IDbConnection db = Connection)
            {
                return db.Query<User>("SELECT * FROM users WHERE id = @id", new { id }).FirstOrDefault();
            }
        }

        public UserCredentials GetUser(string username)
        {
            using (IDbConnection db = Connection)
            {
                return db.Query<UserCredentials>("SELECT * FROM users WHERE username = @username", new { username }).FirstOrDefault();
            }
        }

        public List<User> GetUsers()
        {
            using (IDbConnection db = Connection)
            {
                return db.Query<User>("SELECT * FROM users ORDER BY id").ToList();
            }
        }

        public void Register(User user)
        {
            using (IDbConnection db = Connection)
            {
                var sqlQuerry = "INSERT INTO users (username, passwordhash, salt) VALUES(@username, @passwordhash, @salt)";
                db.Execute(sqlQuerry, new
                {
                    username = user.Username,
                    passwordhash = user.GetPasswordHash(),
                    salt = user.GetSalt()
                });
            }
        }

        public void Update(User user)
        {
            using (IDbConnection db = Connection)
            {
                var sqlQuerry = "UPDATE users SET username = @username, passwordhash = @passwordhash, salt = @salt WHERE id = @Id";
                db.Execute(sqlQuerry, new
                {
                    username = user.Username,
                    passwordhash = user.GetPasswordHash(),
                    salt = user.GetSalt()
                });
            }
        }

        public void DeleteUser(int id)
        {
            using (IDbConnection db = Connection)
            {
                var sqlQuerry = "DELETE FROM users WHERE id = @Id";
                db.Execute(sqlQuerry, new { id });
            }
        }
    }
}