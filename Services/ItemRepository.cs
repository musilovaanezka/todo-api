using TODOApi.Interfaces;
using TODOApi.Models;
using TODOApi.Data;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace TODOApi.Services
{
    public class ItemRepository : IItemRepository
    {
        private readonly ApplicationDbContext _db;

        public ItemRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                var item = await GetByIdAsync(id);
                _db.Items.Remove(item);
                await _db.SaveChangesAsync();
            }
            catch (Exception ex) 
            {
                
            }
        }

        public async Task<List<Item>> GetAsync(
            int? id, 
            int? userId, 
            DateTime? fromCreateTime, 
            DateTime? toCreateTime, 
            DateTime? fromDeadline, 
            DateTime? toDeadline, 
            bool? isDone,
            string? name
        )
        {
            try
            {
                var query =  _db.Items.Where(x =>
                    (id.HasValue ? x.Id == id : true) &&
                    (userId.HasValue ? x.UserId == userId : true) &&
                    (fromCreateTime.HasValue ? x.CreatedDate >= fromCreateTime.Value : true) &&
                    (toCreateTime.HasValue ? x.CreatedDate <= toCreateTime.Value : true) &&
                    (fromDeadline.HasValue ? x.Deadline >= fromDeadline.Value : true) &&
                    (toDeadline.HasValue ? x.Deadline <= toDeadline.Value : true) &&
                    (name != null ? x.Name == name : true) &&
                    (!isDone.HasValue || x.Checked == isDone.Value));
                return await query.ToListAsync();
            } 
            catch ( Exception ex )
            {
                return null;
            }
        }

        public async Task<Item> GetByIdAsync(int id)
        {
            try { 
                return await _db.Items.FindAsync(id);
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public async Task PostAsync(Item item)
        {
            try
            {
                await _db.Items.AddAsync(item);
                await _db.SaveChangesAsync();

            } 
            catch (Exception ex) { }
        }

        public async Task PutAsync(Item item)
        {
            try 
            {
                _db.Items.Update(item);
                await _db.SaveChangesAsync();
            } 
            catch (Exception ex) { }
        }

        public async Task CheckAsync(int id)
        {
            try
            {
                var item = await _db.Items.FindAsync(id);
                if (item != null) { 
                    item.Checked = true; 
                    await _db.SaveChangesAsync();
                }
            }
            catch (Exception ex) { }
        }
    }
}
