using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Onmiva.Controllers
{
    public class SenderManagementController : Controller
    {
        // GET: SenderManagement
        public ActionResult SenderManagementAdmin()
        {
            return View();
        }
        public ActionResult SenderManagement()
        {
            return View();
        }
    }
}