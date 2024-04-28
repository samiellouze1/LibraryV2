using AutoMapper;
using LIbrary.Services.BookCatalogue;
using LIbrary.Services.Payment;
using LIbrary.Services.ReturnBook;
using LIbrary.ViewModels.BookCatalogue;
using LIbrary.ViewModels.BorrowBook;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Security.Claims;

namespace LIbrary.Controllers
{
    public class BorrowBookController : Controller
    {
        private readonly IBorrowBookService _borrowBookService;
        private readonly IBookCatalogueService _bookCatalogueService;
        private readonly IPaymentService _paymentService;
        private readonly IMapper _mapper;
        public BorrowBookController(IBorrowBookService borrowBookService, IBookCatalogueService bookCatalogueService, IMapper mapper, IPaymentService paymentService)
        {
            _borrowBookService = borrowBookService;
            _bookCatalogueService = bookCatalogueService;
            _mapper = mapper;
            _paymentService = paymentService;
        }
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> BorrowBook(string bookId)
        {
            var book = await _bookCatalogueService.GetBookByIdAsync(bookId);
            var bookVM = _mapper.Map<BookReadVM>(book);
            var borrowVM = new BorrowBookVM()
            {
                bookReadVM = bookVM,
                StartDate = DateTime.Today,
                EndDate= DateTime.Today.AddDays(7)
            };
            HttpContext.Session.SetString("BookId", bookId);
            return View(borrowVM);
        }
        [Authorize(Roles = "Reader")]
        [HttpPost]
        public async Task<IActionResult> BorrowBook(BorrowBookVM borrowBookVM)
        {
            var bookId = HttpContext.Session.GetString("BookId");
            var book = await _bookCatalogueService.GetBookByIdAsync(bookId);
            borrowBookVM.bookReadVM=_mapper.Map<BookReadVM>(book);
            if (!ModelState.IsValid)
            {
                var errorMessage = string.Join(" | ", ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage));
                ViewData["ErrorMessage"] = errorMessage;
                return View(borrowBookVM);
            }
            else
            {
                var duration = (borrowBookVM.EndDate - borrowBookVM.StartDate).Days;
                var amount = duration * book.price;
                var successUrl = Url.Action("SuccessBorrowBook", "BorrowBook",new { startDate= borrowBookVM.StartDate, endDate =borrowBookVM.EndDate, bookId= bookId }, Request.Scheme);
                var cancelUrl = Url.Action("Index", "Home", null, Request.Scheme);
                var currency = "usd";
                var session = _paymentService.CreateCheckOutSession(amount.ToString(), currency, successUrl, cancelUrl,book.title);
                return Redirect(session);
            }

        }

        public async Task<IActionResult> SuccessBorrowBook(DateTime startDate, DateTime endDate, string bookId)
        {
            var Id = User.FindFirstValue("Id");
            var book = await _bookCatalogueService.GetBookByIdAsync(bookId);
            var bookReadVM = _mapper.Map<BookReadVM>(book); 
            var borrowBookVM = new BorrowBookVM()
            {
                StartDate = startDate,
                EndDate = endDate,
                bookReadVM = bookReadVM
            };
            await _borrowBookService.BorrowBook(borrowBookVM, Id);
            return RedirectToAction("BorrowedBooks","BookCatalogue");
        }
    }
}
