using LIbrary.Data;
using LIbrary.Models;
using LIbrary.Repository.Generic;

namespace LIbrary.Repository.Specific
{
    public class BorrowItemRepository : EntityBaseRepository<BorrowItem>, IBorrowItemRepository
    {
        public BorrowItemRepository(AppDbContext context) : base(context)
        {

        }

        public Task<List<BorrowItem>> GetAllEagerBorrowItems()
        {
            throw new NotImplementedException();
        }

        public Task<BorrowItem> GetEagerBorrowItemByIdAsync(string id)
        {
            throw new NotImplementedException();
        }
    }
}
