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
        // GET: BillPayment
        public ActionResult BillPaymentList()
        {
            return View(billRepository.GetUserBills(User.Identity.Name));
        }

        public ActionResult BillPaymentForm()
        {
            return View();
        }
    }
}