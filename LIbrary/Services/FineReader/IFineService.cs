using LIbrary.Models;

namespace LIbrary.Services.FineReader
{
    public interface IFineService
    {
        public Task<List<Fine>> GetFines(string readerId);
        public Task<Fine> GetFineByIdAsync(string id);
        Task DeleteFine(string fineId);
    }
}
