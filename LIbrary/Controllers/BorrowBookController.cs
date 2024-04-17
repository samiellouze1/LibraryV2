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
                startDate = DateTime.Now,
                endDate= DateTime.Now.AddDays(7)
            };
            HttpContext.Session.SetString("BookId", bookId);
            return View(borrowVM);
        }
        [Authorize(Roles = "Reader")]
        [HttpPost]
        public async Task<IActionResult> BorrowBook(BorrowBookVM borrowBookVM)//string amount)
        {
            var bookId = HttpContext.Session.GetString("BookId");
            var book = await _bookCatalogueService.GetBookByIdAsync(bookId);
            var bookVM = _mapper.Map<BookReadVM>(book);
            borrowBookVM.bookReadVM = bookVM;
            if (!ModelState.IsValid)
            {
                ///check for goddamn errors

                return View(borrowBookVM);
            }
            else
            {
                var duration = (borrowBookVM.endDate - borrowBookVM.startDate).Days;
                var amount = duration * borrowBookVM.bookReadVM.price;
                var successUrl = Url.Action("BorrowedBooks", "BookCatalogue", null, Request.Scheme);
                var cancelUrl = Url.Action("BorrowedBooks", "BookCatalogue", borrowBookVM, Request.Scheme);
                var currency = "usd";
                var session = _paymentService.CreateCheckOutSession(amount.ToString(), currency, successUrl, cancelUrl);
                return Redirect(session);
            }
        }
        //[Authorize(Roles ="Reader")]
        //[HttpPost]
        //public async Task<IActionResult> BorrowBook(BorrowBookVM borrowVM)
        //{
        //    string Id = User.FindFirstValue("Id");
        //    await _borrowBookService.BorrowBook(borrowVM, Id);
        //    return RedirectToAction("Index", "Home");
        //}
    }
}
