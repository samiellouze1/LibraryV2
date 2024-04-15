using LIbrary.Models;

namespace LIbrary.Services.Profile
{
    public interface IProfileService
    {
        public Task<Reader> GetReaderByIdAsync(string id);
    }
}
