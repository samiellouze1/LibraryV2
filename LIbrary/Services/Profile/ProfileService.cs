using LIbrary.Models;
using LIbrary.Repository.Specific;

namespace LIbrary.Services.Profile
{
    public class ProfileService : IProfileService
    {
        private readonly IReaderRepository _readerRepository;

        public ProfileService(IReaderRepository readerRepository)
        {
            _readerRepository = readerRepository;
        }

        public async Task<Reader> GetReaderByIdAsync(string id)
        {
            var reader = await _readerRepository.GetByIdAsync(id);
            return reader;
        }
    }
}
