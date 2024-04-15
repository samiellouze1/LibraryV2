using LIbrary.Models;
using LIbrary.Repository.Generic;

namespace LIbrary.Repository.Specific
{
    public interface IBorrowItemRepository : IEntityBaseRepository<BorrowItem>
    {
        public Task<BorrowItem> GetEagerBorrowItemByIdAsync(string id);
        public Task<List<BorrowItem>> GetAllEagerBorrowItems();
    }
}
