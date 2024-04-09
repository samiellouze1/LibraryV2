using LIbrary.Services.Payment;
using Microsoft.AspNetCore.Mvc;
using Stripe.BillingPortal;

namespace LIbrary.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        public IActionResult Success()
        {
            return View();
        }
        public IActionResult Cancel() 
        {
            return View();
        }
        //public IActionResult CreateCheckoutSession()//string amount)
        //{
        //    var amount = "5";
        //    var successController = "Payment";
        //    var cancelController = "Payment";
        //    var successAction = "Success";
        //    var cancelAction = "Cancel";
        //    var successUrl = Url.Action(successAction, successController, null, Request.Scheme);
        //    var cancelUrl = Url.Action(cancelAction, cancelController, null, Request.Scheme);
        //    var currency = "usd";
        //    var session = _paymentService.CreateCheckOutSession(amount,currency, successUrl, cancelUrl);
        //    return Redirect(session);
        //}
    }
}
