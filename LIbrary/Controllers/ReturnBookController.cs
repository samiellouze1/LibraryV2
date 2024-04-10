using AutoMapper;
using LIbrary.Models;
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
            return View(returnBookVM);
        }
        public IActionResult ReturnBook()
        {
            return View();
        }
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> ReturnBook(ReturnBookVM returnBookVM)
        {
            string Id = User.FindFirstValue("Id");
            if (returnBookVM.confirmation)
            {
                await _returnBookService.ReturnBook(returnBookVM, Id);
                return RedirectToAction("BookCatalogue", "Books");
            }
            else
            {
                return RedirectToAction("BookCatalogue", "Books");
            }
        }
    }
}
