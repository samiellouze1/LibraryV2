using AutoMapper;
using LIbrary.Models;
using LIbrary.Repository.Specific;
using LIbrary.Services.BookCatalogue;
using LIbrary.Services.ReturnBook;
using LIbrary.ViewModels.BookCatalogue;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LIbrary.Controllers
{
    public class BookCatalogueController : Controller
    {
        private readonly IBookCatalogueService _bookCatalogueService;
        private readonly IMapper _mapper;
        public BookCatalogueController(IBookCatalogueService bookCatalogueService, IMapper mapper)
        {
            _bookCatalogueService = bookCatalogueService;
            _mapper = mapper;
        }
        public async Task<IActionResult> Books(string searchString)
        {
            string Id = User.FindFirstValue("Id");
            var books = await _bookCatalogueService.GetAllBooksAsync();
            var bookReadVms = new List<BookReadVM>();
            if (Id == null)
            {
                bookReadVms=_mapper.Map<List<BookReadVM>>(books);
            }
            else
            {
                foreach (Book book in books)
                {
                    var bookReadVm = _mapper.Map<BookReadVM>(book);
                    bookReadVm.isAlreadyBorrowed = _bookCatalogueService.IsAlreadyBorrowed(book, Id);
                    bookReadVm.isCurrentlyBorrowed = _bookCatalogueService.IsCurrentlyBorrowed(book, Id);
                    bookReadVms.Add(bookReadVm);
                }
            }
            return View(bookReadVms);
        }
        public async Task<IActionResult> Book(string bookId)
        {
            var book = await _bookCatalogueService.GetBookByIdAsync(bookId);
            var bookVM = _mapper.Map<BookReadVM>(book);
            string Id = User.FindFirstValue("Id");
            if (Id == null)
            {

            }
            else
            {
                bookVM.isAlreadyBorrowed = _bookCatalogueService.IsAlreadyBorrowed(book, Id);
                bookVM.isCurrentlyBorrowed = _bookCatalogueService.IsCurrentlyBorrowed(book, Id);
            }
            return View(bookVM);
        }
        [Authorize(Roles ="Reader")]
        public async Task<IActionResult> BorrowedBooks()
        {
            string Id = User.FindFirstValue("Id");
            var books = await _bookCatalogueService.GetBorrowedBooksByReaderIdAsync(Id);
            var bookReadVms = new List<BookReadVM>();
            if (Id == null)
            {
                bookReadVms = _mapper.Map<List<BookReadVM>>(books);
            }
            else
            {
                foreach (Book book in books)
                {
                    var bookReadVm = _mapper.Map<BookReadVM>(book);
                    bookReadVm.isAlreadyBorrowed = _bookCatalogueService.IsAlreadyBorrowed(book, Id);
                    bookReadVm.isCurrentlyBorrowed = _bookCatalogueService.IsCurrentlyBorrowed(book, Id);
                    bookReadVms.Add(bookReadVm);
                }
            }
            return View(bookReadVms);
        }
        [Authorize(Roles ="Reader")]
        public async Task<IActionResult> ReturnedBooks()
        {
            string Id = User.FindFirstValue("Id");
            var books = await _bookCatalogueService.GetReturnedBooksByReaderIdAsync(Id);
            var bookReadVms = new List<BookReadVM>();
            if (Id == null)
            {
                bookReadVms = _mapper.Map<List<BookReadVM>>(books);
            }
            else
            {
                foreach (Book book in books)
                {
                    var bookReadVm = _mapper.Map<BookReadVM>(book);
                    bookReadVm.isAlreadyBorrowed = _bookCatalogueService.IsAlreadyBorrowed(book, Id);
                    bookReadVm.isCurrentlyBorrowed = _bookCatalogueService.IsCurrentlyBorrowed(book, Id);
                    bookReadVms.Add(bookReadVm);
                }
            }
            return View(bookReadVms);
        }
    }
}
