using System;
using System.Data;
using System.Data.Common;
using BudgetTracker.Models;
using BudgetTracker.Repositories.Interfaces;
using MySqlConnector;
using System.Threading.Tasks;


namespace BudgetTracker.Repositories
{
    public class UserRepository: IUserRepository
    {
        DbContext context;

        public UserRepository(DbContext context)
        {
            this.context = context;
        }

        private async Task<List<User>> ReadAllAsync(DbDataReader reader)
        {
            var users = new List<User>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    byte[] passwordsalt = new byte[16];
                    byte[] passwordhash = new byte[128];
                    var userid = reader.GetInt32(0);
                    var username = reader.GetString(1);
                    reader.GetBytes(2, 0, passwordhash, 0, 128);
                    reader.GetBytes(3, 0, passwordsalt, 0, 4);

                    var user = new User {
                        UserId = userid,
                        Username = username,
                        PasswordHash = passwordhash,
                        PasswordSalt = passwordsalt
                    };
                    users.Add(user);
                }
            }
            return users;
        }

        public IEnumerable<User> GetUsers()
        {
            return new List<User>();
        }

        async public Task<User?> GetUserByIDAsync(int id)
        {
            await context.Connection.OpenAsync();
            using (var cmd = context.Connection.CreateCommand())
            {
                cmd.CommandText = $"SELECT * FROM User WHERE user_id=\"{id}\";";
                var reader = await cmd.ExecuteReaderAsync();
                if (reader.HasRows)
                {
                    var users = await ReadAllAsync(reader);
                    return users.Count > 0 ? users[0] : null;
                }
            }

            return null;
        
        }

        async public Task<User?> GetUserByUsernameAsync(string username)
        {
            await context.Connection.OpenAsync();
            using (var cmd = context.Connection.CreateCommand())
            {
                cmd.CommandText = $"SELECT * FROM User WHERE username=\"{username}\";";
                var reader = await cmd.ExecuteReaderAsync();
                if (reader.HasRows)
                {
                    var users = await ReadAllAsync(reader);
                    return users.Count > 0 ? users[0] : null;
                }
            }

            return null;
        }

        public async Task<bool> AddUserAsync(User user)
        {
            try
            {
                await context.Connection.OpenAsync();
                using (var cmd = context.Connection.CreateCommand())
                {
                    cmd.CommandText = "INSERT INTO User (username, password_hash, password_salt) VALUES (@username, @password_hash, @password_salt)";
                    cmd.Parameters.AddWithValue("@username", user.Username);
                    cmd.Parameters.AddWithValue("@password_hash", user.PasswordHash);
                    cmd.Parameters.AddWithValue("@password_salt", user.PasswordSalt);
                    await cmd.ExecuteNonQueryAsync();
                    return true;
                }
            } catch(Exception e)
            {
                Console.WriteLine("Error inserting User into db,", e);
                return false;
            }
            
            
        }
        public async Task<bool> DeleteUserByIdAsync(int userId)
        {
            return false;
        }
        public void UpdateUser(User user)
        {

        }
        public void Save()
        {

        }

    
    }
}

