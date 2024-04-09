using LIbrary.Data;
using LIbrary.Models;
using LIbrary.Repository.Generic;
using Microsoft.EntityFrameworkCore;

namespace LIbrary.Repository.Specific
{
    public class ReaderRepository : EntityBaseRepository<Reader>, IReaderRepository
    {
        public ReaderRepository(AppDbContext context) : base(context)
        {

        }

        public async Task<Reader> GetEagerReaderByIdAsync(string id)
        {
            var reader = await _context.Set<Reader>()
                .Include(r => r.borrowItems)
                .ThenInclude(bi => bi.bookCopy)
                .ThenInclude(bc => bc.book)
                .FirstOrDefaultAsync(r=>r.Id==id);
            return reader;
        }
    }
}
