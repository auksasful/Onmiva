using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Onmiva.Controllers
{
    public class BillManagementController : Controller
    {
        // GET: BillManagement
        public ActionResult BillManagement()
        {
            return View();
        }

        public ActionResult BillPaymentStateList()
        {
            return View();
        }

        public ActionResult CreateBill()
        {
            return View();
        }

        public ActionResult EditBill()
        {
            return View();
        }

        public ActionResult UserBillsList()
        {
            return View();
        }
    }
}