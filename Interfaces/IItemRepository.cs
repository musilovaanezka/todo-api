using TODOApi.Models;

namespace TODOApi.Interfaces
{
    public interface IItemRepository
    {
        public Task<List<Item>> GetAsync(
            int? id, 
            int? userId, 
            DateTime? fromCreateTime, 
            DateTime? toCreateTime, 
            DateTime? fromDeadline, 
            DateTime? toDeadline, 
            bool? isDone,
            string? name
        );

        public Task<Item> GetByIdAsync( int id );

        public Task PostAsync( Item item );
        public Task DeleteAsync( int id );
        public Task PutAsync( Item item );
        public Task CheckAsync( int id );
    }
}
