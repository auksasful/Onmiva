using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Onmiva.Controllers
{
    public class ContractManagementController : Controller
    {
        // GET: ContractManagement
        public ActionResult CreateContract()
        {
            return View();
        }
        public ActionResult ContractList()
        {
            return View();
        }
        public ActionResult ContractView()
        {
            return View();
        }
    }
}