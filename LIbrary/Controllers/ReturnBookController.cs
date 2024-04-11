using AutoMapper;
using LIbrary.Services.BookCatalogue;
using LIbrary.Services.ReturnBook;
using LIbrary.ViewModels.BookCatalogue;
using LIbrary.ViewModels.ReturnBook;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LIbrary.Controllers
{
    public class ReturnBookController : Controller
    {
        private readonly IReturnBookService _returnBookService;
        private readonly IBookCatalogueService _bookCatalogueService;
        private readonly IMapper _mapper;
        public ReturnBookController(IReturnBookService returnBookService, IBookCatalogueService bookCatalogueService, IMapper mapper)
        {
            _returnBookService = returnBookService;
            _bookCatalogueService = bookCatalogueService;
            _mapper = mapper;
        }
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> ReturnBook(string bookId)
        {
            var book = await _bookCatalogueService.GetBookByIdAsync(bookId);
            var bookReadVM = _mapper.Map<BookReadVM>(book);
            ReturnBookVM returnBookVM = new ReturnBookVM() { bookReadVM=bookReadVM,confirmation=false};
            HttpContext.Session.SetString("BookId", bookId);
            return View(returnBookVM);
        }

        [Authorize(Roles = "Reader")]
        [HttpPost]
        public async Task<IActionResult> ReturnBook(ReturnBookVM returnBookVM)
        {
            var bookId = HttpContext.Session.GetString("BookId");
            var book = await _bookCatalogueService.GetBookByIdAsync(bookId);
            var bookVm = _mapper.Map<BookReadVM>(book);
            returnBookVM.bookReadVM = bookVm;
            if (!ModelState.IsValid)
            {
                return View(returnBookVM);
            }
            else
            {
                string Id = User.FindFirstValue("Id");
                if (returnBookVM.confirmation)
                {
                    await _returnBookService.ReturnBook(returnBookVM, Id);
                    return RedirectToAction("BorrowedBooks", "BookCatalogue");
                }
                else
                {
                    return View(returnBookVM);
                }
            }

        }
    }
}
