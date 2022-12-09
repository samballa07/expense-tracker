using System;
using BudgetTracker.Models;

namespace BudgetTracker.Repositories.Interfaces
{
    public interface IUserRepository
    {
        public IEnumerable<User> GetUsers();
        public Task<User?> GetUserByIDAsync(int userId);
        public Task<User?> GetUserByUsernameAsync(string username);
        public Task<bool> AddUserAsync(User user);
        public Task<bool> DeleteUserByIdAsync(int userId);
        public void UpdateUser(User user);
        public void Save();
    }
}

