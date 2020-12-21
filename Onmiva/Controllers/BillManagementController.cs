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
            return View(billRepository.GetAllBills());
        }

        public ActionResult CreateBill()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateBill(Bill bill)
        {



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
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult EditBill(int id)
        {
            Bill bill = billRepository.GetUserBill(id);
            return View(bill);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditBill(int NumberId, string PayComment, decimal Sum, int StateId, DateTime PayUntil)
        {

            Bill bill = new Bill();
            bill.NumberId = NumberId;
            bill.PayComment = PayComment;
            bill.Sum = Sum;
            bill.StateId = StateId;
            bill.PayUntil = PayUntil;
           

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
        public ActionResult DeleteBill(int BillId)
        {
            Bill bill = billRepository.GetUserBill(BillId);
            return View(bill);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteBill(Bill bill, int NumberId)
        {
            if (billRepository.DeleteBill(NumberId))
            {
                ViewBag.Status = true;
                ViewBag.Message = "Sąskaita " + NumberId + " ištrinta";
            }
            else
            {
                ViewBag.Status = false;
                ViewBag.Message = "Nepavyko ištrinti sąskaitos " + NumberId;
            }

            return View(bill);
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



        public List<Order> GetOrdersWithoutBill() {
            return billRepository.GetOrdersWithoutBill();
        }



        public List<string> GetBillStateList() {
            return billRepository.GetBillStateList();
        }

    }
}