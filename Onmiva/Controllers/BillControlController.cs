using Onmiva.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Onmiva.Views.BillControl
{
    public class BillControlController : Controller
    {

        UserRepository userRepository = new UserRepository();
        BillRepository billRepository = new BillRepository();
        // GET: BillControl
        public ActionResult BillControl()
        {
            return View(billRepository.GetUserBills(User.Identity.Name));
        }

        public ActionResult BillView(int? billId)
        {
            if (billId == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(billRepository.GetUserBill(billId));
        }
    }
}