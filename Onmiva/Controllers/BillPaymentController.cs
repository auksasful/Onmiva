using Onmiva.Models;
using Onmiva.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Onmiva.Controllers
{
    public class BillPaymentController : Controller
    {

        UserRepository userRepository = new UserRepository();
        BillRepository billRepository = new BillRepository();
        PaymentRepository paymentRepository = new PaymentRepository();
        // GET: BillPayment
        public ActionResult BillPaymentList()
        {
            return View(billRepository.GetUserBills(User.Identity.Name));
        }

        [HttpGet]
        public ActionResult BillPaymentForm(int billId)
        {
            Payment pay = paymentRepository.GetPaymentByBill(billId);
            return View(pay);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BillPaymentForm(Payment payment)
        {
            if (paymentRepository.AddPayment(payment))
            {
                ViewBag.Message = "Apmokėjimas patvirtintas";
                ViewBag.Status = true;
            }
            else {
                ViewBag.Message = "Apmokėjimas nepavyko!";
                ViewBag.Status = false;
            }

            if (payment.PaymentMethod == "banko pavedimu" && ViewBag.Status == true)
            {
                string uri = Request.Url.GetLeftPart(UriPartial.Authority) + "/BillPayment/BillPaymentConfirmed?key=15368548";
                string itemName = "Užsakymas " + payment.Date;
                return Redirect("https://www.sandbox.paypal.com/cgi-bin/websrc?cmd=_xclick&amount=" + payment.Sum * (decimal)1.09885 + "&business=Onmiva@onmiva.lt&item_name=" + itemName + "&return=" + uri);
            }

            return View(payment);
        }

        [HttpGet]
        public ActionResult BillPaymentConfirmed(int key) {
            if (key == 15368548) {
                ViewBag.Message = "Apmokėjimas patvirtintas";
                ViewBag.Status = true;
            }

            return View();
        }


        public List<string> GetPaymentMethods() {
            return paymentRepository.GetPaymentMethods();
        }
    }
}