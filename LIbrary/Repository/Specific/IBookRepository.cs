using LIbrary.Models;
using LIbrary.Repository.Generic;

namespace LIbrary.Repository.Specific
{
    public interface IBookRepository : IEntityBaseRepository<Book>
    {
        public Task<Book> GetEagerBookByIdAsync(string id);
        public Task<ICollection<Book>> GetAllEagerBooksAsync();
    }
}
