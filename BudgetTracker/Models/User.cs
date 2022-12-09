using System;
namespace BudgetTracker.Models
{
    public class User
    {
        public int UserId { get; set; }

        public string Username { get; set; } = string.Empty;

        public byte[] PasswordHash { get; set; } = new byte[128];

        public byte[] PasswordSalt { get; set; } = new byte[16];

        //public User(int userid, string username, byte[] passwordhash, byte[] passwordsalt)
        //{
        //    this.UserId = userid;
        //    this.Username = username;
        //    this.PasswordHash = passwordhash;
        //    this.PasswordSalt = passwordsalt;
        //}
    }
}

