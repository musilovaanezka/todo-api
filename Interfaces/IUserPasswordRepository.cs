using TODOApi.Models;

namespace TODOApi.Interfaces
{
    public interface IUserPasswordRepository
    {
        public Task<UserPassword> GetByIdAsync(int id);
        public Task PostAsync(UserPassword userPassword);
        public Task DeleteAsync(int id);
        public Task PutAsync(UserPassword userPassword);
    }
}
