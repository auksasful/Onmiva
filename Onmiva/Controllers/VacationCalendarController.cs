using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Onmiva.Models;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;

namespace Onmiva.Controllers
{



    public class VacationCalendarController : Controller
    {
        // GET: VacationCalendar
        public ActionResult VacationCalendar()
        {
            List<VacationCalendar> kalendoriai = new List<VacationCalendar>();
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            using (MySqlConnection mySqlConnection = new MySqlConnection(conn))
            {
                string query = "SELECT * FROM Vacationcalendar";
                using (MySqlCommand cmd = new MySqlCommand(query))
                {
                    cmd.Connection = mySqlConnection;
                    mySqlConnection.Open();
                    using (MySqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            kalendoriai.Add(new VacationCalendar
                            {
                                AtostogosId = Convert.ToInt32(sdr["id"]),
                                vardas = sdr["darb_vardas"].ToString(),
                                pavarde = sdr["darb_pavarde"].ToString(),
                                pradzios_data = Convert.ToDateTime(sdr["pradzios_data"]),
                                pabaigos_data = Convert.ToDateTime(sdr["pabaigos_data"])

                            });
                        }
                    }
                    mySqlConnection.Close();
                }
            }
            ViewBag.KontraktuSarasas = kalendoriai;
            return View(kalendoriai);
        }
        public ActionResult EditVacationCalendar()
        {
            return View();
        }

        public ActionResult FilterVacationCalendar(string sortOrder, string searchPavadinimas, string searchMiestas)
        {

            List<VacationCalendar> siuntejai = new List<VacationCalendar>();
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            using (MySqlConnection mySqlConnection = new MySqlConnection(conn))
            {
                string query = "SELECT * FROM Vacationcalendar";
                using (MySqlCommand cmd = new MySqlCommand(query))
                {
                    cmd.Connection = mySqlConnection;
                    mySqlConnection.Open();
                    using (MySqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            siuntejai.Add(new VacationCalendar
                            {
                                AtostogosId = Convert.ToInt32(sdr["id"]),
                                vardas = sdr["darb_vardas"].ToString(),
                                pavarde = sdr["darb_pavarde"].ToString(),
                                pradzios_data = Convert.ToDateTime(sdr["pradzios_data"]),
                                pabaigos_data = Convert.ToDateTime(sdr["pabaigos_data"])

                            });
                        }
                    }
                    mySqlConnection.Close();
                }
            }
            if (!String.IsNullOrEmpty(searchPavadinimas))
            {
                siuntejai = siuntejai.Where(s => s.vardas.Contains(searchPavadinimas)).ToList();
            }
            if (!String.IsNullOrEmpty(searchMiestas))
            {
                siuntejai = siuntejai.Where(s => s.pavarde.Contains(searchMiestas)).ToList();
            }
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "vardas" : "";
            ViewBag.CitySortParm = String.IsNullOrEmpty(sortOrder) ? "miestas" : "";
            switch (sortOrder)
            {
                case "vardas":
                    siuntejai = siuntejai.OrderBy(s => s.vardas).ToList();
                    break;
                case "miestas":
                    siuntejai = siuntejai.OrderBy(s => s.pavarde).ToList(); ;
                    break;
            }

            ViewBag.KontraktuSarasas = siuntejai;
            return View(siuntejai);
        }

        //public ActionResult Informacija(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    List<VacationCalendar> sarasas = Siunteju_Sarasas();
        //    VacationCalendar siuntejas = sarasas.Find(i => i.AtostogosId == id);
        //    if (siuntejas == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.siuntejas = siuntejas;

        //    ViewBag.siuntejoId = siuntejas.AtostogosId;
        //    ViewBag.atsiliepimai = atsiliepimai(ViewBag.siuntejoId);
        //    ViewBag.ivertinimai = ivertinimai(ViewBag.siuntejoId);
        //    return View("SenderInfo");
        //}

        public List<VacationCalendar> Siunteju_Sarasas()
        {
            List<VacationCalendar> siuntejai = new List<VacationCalendar>();
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            using (MySqlConnection mySqlConnection = new MySqlConnection(conn))
            {
                string query = "SELECT * FROM Vacationcalendar";
                using (MySqlCommand cmd = new MySqlCommand(query))
                {
                    cmd.Connection = mySqlConnection;
                    mySqlConnection.Open();
                    using (MySqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            siuntejai.Add(new VacationCalendar
                            {
                                AtostogosId = Convert.ToInt32(sdr["id"]),
                                vardas = sdr["darb_vardas"].ToString(),
                                pavarde = sdr["darb_pavarde"].ToString(),
                                pradzios_data = Convert.ToDateTime(sdr["pradzios_data"]),
                                pabaigos_data = Convert.ToDateTime(sdr["pabaigos_data"])

                            }); ;
                        }
                    }
                    mySqlConnection.Close();
                }
            }
            ViewBag.KontraktuSarasas = siuntejai;
            return siuntejai;
        }
    }
}