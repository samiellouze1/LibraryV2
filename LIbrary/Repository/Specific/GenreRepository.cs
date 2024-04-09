using LIbrary.Data;
using LIbrary.Models;
using LIbrary.Repository.Generic;

namespace LIbrary.Repository.Specific
{
    public class GenreRepository : EntityBaseRepository<Genre>, IGenreRepository
    {
        public GenreRepository(AppDbContext context) : base(context)
        {

        }
    }
}
