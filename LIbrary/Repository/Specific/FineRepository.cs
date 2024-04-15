using LIbrary.Data;
using LIbrary.Models;
using LIbrary.Repository.Generic;
using Microsoft.EntityFrameworkCore;

namespace LIbrary.Repository.Specific
{
    public class FineRepository : EntityBaseRepository<Fine>, IFineRepository
    {
        public FineRepository(AppDbContext context) : base(context)
        {

        }

        public async Task<List<Fine>> GetAllEagerFinesAsync()
        {
            var fines = await _context.Set<Fine>()
                .Include(f => f.fineStatus)
                .Include(f => f.borrowItem)
                    .ThenInclude(bi => bi.bookCopy)
                        .ThenInclude(bc => bc.book)
                .Include(f => f.borrowItem)
                    .ThenInclude(bi => bi.reader)
                .ToListAsync();
            return fines;
        }

        public async Task<Fine> GetEagerFineByIdAsync(string fineId)
        {
            var fine = await _context.Set<Fine>()
                                .Include(f => f.borrowItem)
                    .ThenInclude(bi => bi.bookCopy)
                        .ThenInclude(bc => bc.book)
                        .FirstOrDefaultAsync(f=>f.Id== fineId);
            return fine;
        }
    }
}
