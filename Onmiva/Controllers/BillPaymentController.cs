using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Onmiva.Controllers
{
    public class BillPaymentController : Controller
    {
        // GET: BillPayment
        public ActionResult BillPaymentList()
        {
            return View();
        }

        public ActionResult BillPaymentForm()
        {
            return View();
        }
    }
}