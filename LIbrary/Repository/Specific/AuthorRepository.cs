using LIbrary.Data;
using LIbrary.Models;
using LIbrary.Repository.Generic;

namespace LIbrary.Repository.Specific
{
    public class AuthorRepository : EntityBaseRepository<Author>, IAuthorRepository
    {
        public AuthorRepository(AppDbContext context) : base(context)
        {

        }
    }
}
