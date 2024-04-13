using LIbrary.Services.FineReader;
using LIbrary.Services.Payment;
using LIbrary.ViewModels.BorrowBook;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LIbrary.Controllers
{
    public class FineController : Controller
    {
        private readonly IFineService _fineService;
        private readonly IPaymentService _paymentService;

        public FineController(IFineService fineService, IPaymentService paymentService)
        {
            _fineService = fineService;
            _paymentService = paymentService;
        }

        public async Task <IActionResult> Fines()
        {
            var Id = User.FindFirstValue("Id");
            var Fines = await _fineService.GetFines(Id);
            ViewBag.Title = "Fines";
            return View();
        }
        public async Task<IActionResult> PayFine(string fineId)
        {
            var fine = await _fineService.GetFineByIdAsync(fineId);

            var duration = (fine.borrowItem.endDate - fine.borrowItem.supposedEndDate).Days;
            var amount = duration * 2 * fine.borrowItem.bookCopy.book.price ;
            var successUrl = Url.Action("Success", "Home", fine, Request.Scheme);
            var cancelUrl = Url.Action("Cancel", "Home", fine, Request.Scheme);
            var currency = "usd";
            var session = _paymentService.CreateCheckOutSession(amount.ToString(), currency, successUrl, cancelUrl);
            return Redirect(session);
        }

        public IActionResult Success()
        {
            return View();
        }
        public IActionResult Cancel()
        {
            return View();
        }
    }
}
