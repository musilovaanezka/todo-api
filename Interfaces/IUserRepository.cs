using TODOApi.Models;

namespace TODOApi.Interfaces
{
    public interface IUserRepository
    {
        public Task<User> GetByIdAsync(int id);
        public Task<User> GetAsync(string logname);
        public Task PostAsync(string logname, string password);
        public Task DeleteAsync(int id);
        public Task PutAsync(string logname, string password);
    }
}
