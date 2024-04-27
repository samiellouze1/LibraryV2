using AutoMapper;
using LIbrary.Models;
using LIbrary.Services.FineReader;
using LIbrary.Services.Payment;
using LIbrary.ViewModels.BorrowBook;
using LIbrary.ViewModels.Fine;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LIbrary.Controllers
{
    public class FineController : Controller
    {
        private readonly IFineService _fineService;
        private readonly IPaymentService _paymentService;
        private readonly IMapper _mapper;

        public FineController(IFineService fineService, IPaymentService paymentService, IMapper mapper)
        {
            _fineService = fineService;
            _paymentService = paymentService;
            _mapper = mapper;
        }

        public async Task <IActionResult> Fines()
        {
            var Id = User.FindFirstValue("Id");
            var Fines = await _fineService.GetFines(Id);
            var fineReadVms = _mapper.Map<List<FineReadVM>>(Fines);
            ViewBag.Title = "Fines";
            return View(fineReadVms);
        }
        public async Task<IActionResult> PayFine(string fineId)
        {
            var fine = await _fineService.GetFineByIdAsync(fineId);
            var duration = (fine.borrowItem.endDate - fine.borrowItem.supposedEndDate).Days;
            var amount = duration * 2 * fine.borrowItem.bookCopy.book.price ;
            var cancelUrl = Url.Action("PayFineSuccess", "Fine", new {fineId = fineId}, Request.Scheme);
            var successUrl = Url.Action("Fines", "Fine", null, Request.Scheme);
            var currency = "usd";
            var session = _paymentService.CreateCheckOutSession(amount.ToString(), currency, successUrl, cancelUrl,"fine "+fineId.ToString());
            return Redirect(session);
        }
        public async Task<IActionResult> PayFineSuccess(string fineId)
        {
            await _fineService.DeleteFine(fineId);
            return RedirectToAction("Fines", "Fine");
        }
    }
}
