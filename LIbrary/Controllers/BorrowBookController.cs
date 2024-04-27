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
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));

                    // Log or handle the errors as needed
                    foreach (var error in errors)
                    {
                        // You can log the errors, display them to the user, or handle them in any other appropriate way
                        // For example, you can use TempData to display error messages on the next request
                        Console.WriteLine(error);
                    }
                    // Model validation failed, return the view with validation errors
                    return View(borrowBookVM);
                }
                else
                {
                    // Ensure that BookId is not null or empty
                    var bookId = HttpContext.Session.GetString("BookId");
                    var book = await _bookCatalogueService.GetBookByIdAsync(bookId);

                    // Calculate amount and proceed to payment
                    var duration = (borrowBookVM.EndDate - borrowBookVM.StartDate).Days;
                    var amount = duration * book.price;
                    var cancelUrl = Url.Action("SuccessBorrowBook", "BorrowBook",new { startDate= borrowBookVM.StartDate, endDate =borrowBookVM.EndDate, bookId= bookId }, Request.Scheme);
                    var successUrl = Url.Action("Index", "Home", null, Request.Scheme);
                    var currency = "usd";

                    // Create checkout session
                    var session = _paymentService.CreateCheckOutSession(amount.ToString(), currency, successUrl, cancelUrl,book.title);
                    return Redirect(session);
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                // Handle the exception gracefully, maybe display an error message to the user
                return RedirectToAction("Error", "Home"); // Redirect to an error page
            }
        }

        public async Task<IActionResult> SuccessBorrowBook(DateTime startDate, DateTime endDate, string bookId)
        {
            var Id = User.FindFirstValue("Id");
            var book = await _bookCatalogueService.GetBookByIdAsync(Id);
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
