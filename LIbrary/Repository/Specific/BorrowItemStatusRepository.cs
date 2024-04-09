using LIbrary.Data;
using LIbrary.Models;
using LIbrary.Repository.Generic;

namespace LIbrary.Repository.Specific
{
    public class BorrowItemStatusRepository : EntityBaseRepository<BorrowItemStatus>, IBorrowItemStatusRepository
    {
        public BorrowItemStatusRepository(AppDbContext context) : base(context)
        {

        }
    }
}
