using LIbrary.Models;
using LIbrary.ViewModels.ReturnBook;

namespace LIbrary.Services.ReturnBook
{
    public interface IReturnBookService
    {
        public Task ReturnBook(ReturnBookVM returnBookVM, string readerId);
    }
}
