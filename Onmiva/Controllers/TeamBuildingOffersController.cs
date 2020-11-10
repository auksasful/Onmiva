using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Onmiva.Controllers
{
    public class TeamBuildingOffersController : Controller
    {
        // GET: TeamBuildingOffers
        public ActionResult EditTeamBuildingOffers()
        {
            return View();
        }

        public ActionResult TeamBuildingOffers()
        {
            return View();
        }
    }
}