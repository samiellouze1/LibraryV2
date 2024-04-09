using LIbrary.Data;
using LIbrary.Models;
using LIbrary.Repository.Generic;

namespace LIbrary.Repository.Specific
{
    public class BookCopyRepository : EntityBaseRepository<BookCopy>, IBookCopyRepository
    {
        public BookCopyRepository(AppDbContext context) : base(context)
        {

        }
    }
}
