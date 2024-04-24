using LIbrary.Models;
using LIbrary.Repository.Specific;

namespace LIbrary.Services.FineReader
{
    public class FineService : IFineService
    {
        private readonly IFineRepository _fineRepository;

        public FineService(IFineRepository fineRepository)
        {
            _fineRepository = fineRepository;
        }

        public async Task<List<Fine>> GetFines(string readerId)
        {
            var fines = await _fineRepository.GetAllEagerFinesAsync();
            var myfines = fines.Where(f=>f.borrowItem.readerId == readerId).ToList();
            return myfines;
        }

        public async Task<Fine> GetFineByIdAsync(string fineId)
        {
            var fine = await _fineRepository.GetEagerFineByIdAsync(fineId);
            return fine;
        }

        public async Task DeleteFine(string fineId)
        {
            await _fineRepository.DeleteAsync(fineId);
        }
    }
}
