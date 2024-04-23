using LIbrary.Data;
using LIbrary.Models;
using LIbrary.Repository.Generic;
using Microsoft.EntityFrameworkCore;

namespace LIbrary.Repository.Specific
{
    public class BorrowItemRepository : EntityBaseRepository<BorrowItem>, IBorrowItemRepository
    {
        public BorrowItemRepository(AppDbContext context) : base(context)
        {

        }

        public async Task<List<BorrowItem>> GetAllEagerBorrowItems()
        {
            return await _context.Set<BorrowItem>()
                .Include(bi => bi.bookCopy)
                    .ThenInclude(bc => bc.book)
                .Include(bi => bi.reader)
                .ToListAsync();
        }

        public async Task<BorrowItem> GetEagerBorrowItemByIdAsync(string id)
        {
            var borrowitem = await _context.Set<BorrowItem>()
                .Include(bi => bi.bookCopy)
                    .ThenInclude(bc => bc.book)
                .Include(bi => bi.reader)
                .FirstOrDefaultAsync(bi=>bi.Id==id);
            return borrowitem;
        }
    }
}
