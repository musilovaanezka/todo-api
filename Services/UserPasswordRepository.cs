using TODOApi.Data;
using TODOApi.Interfaces;
using TODOApi.Models;

namespace TODOApi.Services
{
    public class UserPasswordRepository : IUserPasswordRepository
    {
        private readonly ApplicationDbContext _db;

        public UserPasswordRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task DeleteAsync(int id)
        {
            try
            {
                var item = await GetByIdAsync(id);
                _db.userPasswords.Remove(item);
                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {

            }
        }

        public async Task<UserPassword> GetByIdAsync(int id)
        {
            try
            {
                return await _db.userPasswords.FindAsync(id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task PostAsync(UserPassword userPassword)
        {
            try
            {
                await _db.userPasswords.AddAsync(userPassword);
                await _db.SaveChangesAsync();

            }
            catch (Exception ex) { }
        }

        public async Task PutAsync(UserPassword userPassword)
        {
            try
            {
                _db.userPasswords.Update(userPassword);
                await _db.SaveChangesAsync();
            }
            catch (Exception ex) { }
        }
    }
}
