using Microsoft.EntityFrameworkCore;
using TODOApi.Data;
using TODOApi.Interfaces;
using TODOApi.Models;

namespace TODOApi.Services
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IUserPasswordRepository _userPasswordRepository;

        public UserRepository(
            ApplicationDbContext db, 
            IUserPasswordRepository userPasswordRepository)
        {
            _db = db;
            _userPasswordRepository = userPasswordRepository;
        }
        public async Task DeleteAsync(int id)
        {
            try
            {
                var user = await GetByIdAsync(id);
                if (user != null)
                {
                    var password = user.Password;
                    _db.Users.Remove(user);
                    _db.userPasswords.Remove(password);
                    await _db.SaveChangesAsync();
                }
            }
            catch { }
        }

        public async Task<User> GetAsync(string logname)
        {
            try
            {
                return await _db.Users
                    .Include(u => u.Password)
                    .FirstOrDefaultAsync(x => x.Login == logname);
            }
            catch (Exception ex) 
            {
                return null;
            }
        }

        public async Task<User> GetByIdAsync(int id)
        {
            try
            {
                return await _db.Users
                    .Include(u => u.Password)
                    .FirstOrDefaultAsync(u => u.Id == id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task PostAsync(string logname, string password)
        {
            try {
                string passwordHash = BCrypt.Net.BCrypt.HashPassword(password);
                var userPassword = new UserPassword() { Password = passwordHash };
                var user = new User() { Login = logname, Password = userPassword };

                await _db.Users.AddAsync(user);
                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
            }
        }

        public async Task PutAsync(string logname, string password)
        {
            try
            {
                string passwordHash = BCrypt.Net.BCrypt.HashPassword(password);
                var userPassword = new UserPassword() { Password = passwordHash };
                var user = await _db.Users.FirstOrDefaultAsync(x => x.Login == logname);

                if (user == null) 
                {
                    return;
                } 

                _db.Users.Update(user);
                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {

            }
        }
    }
}
