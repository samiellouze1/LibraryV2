using AutoMapper;
using LIbrary.Services.BookCatalogue;
using LIbrary.Services.ReturnBook;
using LIbrary.ViewModels.BookCatalogue;
using LIbrary.ViewModels.BorrowBook;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LIbrary.Controllers
{
    public class BorrowBookController : Controller
    {
        private readonly IBorrowBookService _borrowBookService;
        private readonly IBookCatalogueService _bookCatalogueService;
        private readonly IMapper _mapper;
        public BorrowBookController(IBorrowBookService borrowBookService, IBookCatalogueService bookCatalogueService, IMapper mapper)
        {
            _borrowBookService = borrowBookService;
            _bookCatalogueService = bookCatalogueService;
            _mapper = mapper;
        }
        [Authorize(Roles ="Reader")]
        public async Task<IActionResult> BorrowBook(string bookId)
        {
            var book = await _bookCatalogueService.GetBookByIdAsync(bookId);
            var bookVM = _mapper.Map<BookReadVM>(book);
            var borrowVM = new BorrowBookVM() 
            { 
                bookReadVM=bookVM
            };
            return View(borrowVM);
        }
        [Authorize(Roles ="Reader")]
        [HttpPost]
        public async Task<IActionResult> BorrowBook(BorrowBookVM borrowVM)
        {
            string Id = User.FindFirstValue("Id");
            await _borrowBookService.BorrowBook(borrowVM, Id);
            return RedirectToAction("Index", "Home");
        }
    }
}
