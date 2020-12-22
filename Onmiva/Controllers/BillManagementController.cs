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


            if (userRepository.GetUserRole(User.Identity.Name) != "Admin" && userRepository.GetUserRole(User.Identity.Name) != "Darbuotojas")
            {
                return RedirectToAction("Index", "Home");
            }

            return View(userRepository.GetUsers(start, count));
        }

        public ActionResult BillPaymentStateList(int? userId)
        {

            if (userId == null)
            {
                return RedirectToAction("Index", "Home");
            }

            if (userRepository.GetUserRole(User.Identity.Name) != "Admin" && userRepository.GetUserRole(User.Identity.Name) != "Darbuotojas")
            {
                return RedirectToAction("Index", "Home");
            }


            return View(billRepository.GetAllBills(userId));
        }

        public ActionResult CreateBill()
        {
            if (userRepository.GetUserRole(User.Identity.Name) != "Admin" && userRepository.GetUserRole(User.Identity.Name) != "Darbuotojas")
            {
                return RedirectToAction("Index", "Home");
            }


            return View();
        }

        [HttpPost]
        public ActionResult CreateBill(int? orderId, string PayComment, decimal? Sum, DateTime? PayUntil)
        {
            if (orderId == null || Sum == null || PayUntil == null)
            {
                return RedirectToAction("Index", "Home");
            }


            if (userRepository.GetUserRole(User.Identity.Name) != "Admin" && userRepository.GetUserRole(User.Identity.Name) != "Darbuotojas")
            {
                return RedirectToAction("Index", "Home");
            }
            Bill bill = new Bill();
            bill.OrderId = (int)orderId;
            bill.PayComment = PayComment;
            bill.Sum = (decimal)Sum;
            bill.PayUntil = (DateTime)PayUntil;
            Order_Bill order = billRepository.GetOrder((int)orderId);
            bill.SendingCompany = order.Company;
            bill.SendingCompanyId = order.CompanyId;

            if (billRepository.CreateBill(bill))
            {
                ViewBag.Status = true;
                ViewBag.Message = "Sąskaita užsakymui " + bill.Order + " sukurta";
            }
            else {
                ViewBag.Status = false;
                ViewBag.Message = "Nepavyko sukurti sąskaitos";
            }
            return View();
        }

        public ActionResult EditBill()
        {
            if (userRepository.GetUserRole(User.Identity.Name) != "Admin" && userRepository.GetUserRole(User.Identity.Name) != "Darbuotojas")
            {
                return RedirectToAction("Index", "Home");
            }


            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult EditBill(int? id)
        {

            if (id == null)
            {
                return RedirectToAction("Index", "Home");
            }

            if (userRepository.GetUserRole(User.Identity.Name) != "Admin" && userRepository.GetUserRole(User.Identity.Name) != "Darbuotojas")
            {
                return RedirectToAction("Index", "Home");
            }


            Bill bill = billRepository.GetUserBill(id);
            return View(bill);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditBill(int? NumberId, string PayComment, decimal? Sum, int? StateId, DateTime? PayUntil)
        {

            if (NumberId == null || Sum == null || StateId == null || PayUntil == null)
            {
                return RedirectToAction("Index", "Home");
            }


            if (userRepository.GetUserRole(User.Identity.Name) != "Admin" && userRepository.GetUserRole(User.Identity.Name) != "Darbuotojas")
            {
                return RedirectToAction("Index", "Home");
            }


            Bill bill = new Bill();
            bill.NumberId = (int)NumberId;
            bill.PayComment = PayComment;
            bill.Sum = (decimal)Sum;
            bill.StateId = (int)StateId;
            bill.PayUntil = (DateTime)PayUntil;
           

            if (billRepository.EditBill(bill))
            {
                ViewBag.Status = true;
                ViewBag.Message = "Sąskaita paredaguota";
            }
            else
            {
                ViewBag.Status = false;
                ViewBag.Message = "Nepavyko redaguoti sąskaitos";
            }


            return View(bill);
        }


        [HttpGet]
        public ActionResult DeleteBill(int? BillId)
        {

            if (BillId == null)
            {
                return RedirectToAction("Index", "Home");
            }


            if (userRepository.GetUserRole(User.Identity.Name) != "Admin" && userRepository.GetUserRole(User.Identity.Name) != "Darbuotojas")
            {
                return RedirectToAction("Index", "Home");
            }


            Bill bill = billRepository.GetUserBill(BillId);
            return View(bill);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteBill(Bill bill, int? NumberId)
        {
            if (NumberId == null)
            {
                return RedirectToAction("Index", "Home");
            }


            if (userRepository.GetUserRole(User.Identity.Name) != "Admin" && userRepository.GetUserRole(User.Identity.Name) != "Darbuotojas")
            {
                return RedirectToAction("Index", "Home");
            }


            if (billRepository.DeleteBill((int)NumberId))
            {
                ViewBag.Status = true;
                ViewBag.Message = "Sąskaita " + NumberId + " ištrinta";
            }
            else
            {
                ViewBag.Status = false;
                ViewBag.Message = "Nepavyko ištrinti sąskaitos - galimai ji jau apmokėta" + NumberId;
            }

            return View(bill);
        }


        public ActionResult ViewBill(int? billId)
        {
            if (billId == null)
            {
                return RedirectToAction("Index", "Home");
            }


            if (userRepository.GetUserRole(User.Identity.Name) != "Admin" && userRepository.GetUserRole(User.Identity.Name) != "Darbuotojas")
            {
                return RedirectToAction("Index", "Home");
            }

            return View(billRepository.GetUserBill(billId));
        }


        public ActionResult UserBillsList(DateTime? startDate, DateTime? endDate, int userId = 0)
        {
            if (userRepository.GetUserRole(User.Identity.Name) != "Admin" && userRepository.GetUserRole(User.Identity.Name) != "Darbuotojas")
            {
                return RedirectToAction("Index", "Home");
            }

            return View(billRepository.GetUserBills(startDate, endDate, userId));
        }

        public List<User> GetUsers(int from, int count) {

            return userRepository.GetUsers(from, count);
        }


        public int GetUserCount() {
            return userRepository.GetUsersCount();
        }



        public List<Order_Bill> GetOrdersWithoutBill() {
            return billRepository.GetOrdersWithoutBill();
        }



        public List<string> GetBillStateList() {
            return billRepository.GetBillStateList();
        }

    }
}