using LIbrary.Models;
using LIbrary.Repository.Generic;

namespace LIbrary.Repository.Specific
{
    public interface IFineRepository : IEntityBaseRepository<Fine>
    {
        public Task<List<Fine>> GetAllEagerFinesAsync();
        public Task<Fine> GetEagerFineByIdAsync(string fineId);
    }
}
