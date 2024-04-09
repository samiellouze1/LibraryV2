using LIbrary.Models;
using LIbrary.Repository.Generic;

namespace LIbrary.Repository.Specific
{
    public interface IReaderRepository : IEntityBaseRepository<Reader>
    {
        public Task<Reader> GetEagerReaderByIdAsync(string id);
    }
}
