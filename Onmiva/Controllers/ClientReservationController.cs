using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Onmiva.Controllers
{
    public class ClientReservationController : Controller
    {
        // GET: ClientReservation
        public ActionResult AddClientReservation()
        {
            return View();
        }
        public ActionResult ClientReservation()
        {
            return View();
        }

        public ActionResult DeleteClientReservation()
        {
            return View();
        }
    }
}