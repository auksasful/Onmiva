using Onmiva.Models;
using Onmiva.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Onmiva.Controllers
{
    public class BillManagementController : Controller
    {

        UserRepository userRepository = new UserRepository();
        BillRepository billRepository = new BillRepository();

        // GET: BillManagement
        public ActionResult BillManagement(int start = 0, int count = 20)
        {
            return View(userRepository.GetUsers(start, count));
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

        public ActionResult ViewBill(int billId)
        {
            return View(billRepository.GetUserBill(billId));
        }


        public ActionResult UserBillsList(int userId = 0)
        {
            return View(billRepository.GetUserBills(userId));
        }

        public List<User> GetUsers(int from, int count) {
            return userRepository.GetUsers(from, count);
        }


        public int GetUserCount() {
            return userRepository.GetUsersCount();
        }
    }
}