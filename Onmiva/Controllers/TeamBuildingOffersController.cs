using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MySql.Data.MySqlClient;
using Onmiva.Models;

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
            List<TeamBuildingOffers> teamOfferiai = new List<TeamBuildingOffers>();
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            using (MySqlConnection mySqlConnection = new MySqlConnection(conn))
            {
                string query = "SELECT * FROM Teambuildingoffer";
                using (MySqlCommand cmd = new MySqlCommand(query))
                {
                    cmd.Connection = mySqlConnection;
                    mySqlConnection.Open();
                    using (MySqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            teamOfferiai.Add(new TeamBuildingOffers
                            {
                                TeamBuildingId = Convert.ToInt32(sdr["id"]),
                                data = Convert.ToDateTime(sdr["data"]),
                                pasiulymas = sdr["pasiulymas"].ToString(),
                                islaidos = Convert.ToInt32(sdr["prel_islaidos"])

                            });
                        }
                    }
                    mySqlConnection.Close();
                }
            }
            ViewBag.KontraktuSarasas = teamOfferiai;
            return View(teamOfferiai);
        }




        [HttpPost]
        public ActionResult EditTeamBuildingOffers(TeamBuildingOffers teamBuildingOffers)
        {
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"INSERT INTO Teambuildingoffer ( data, pasiulymas, prel_islaidos) 
            VALUES (?data, ?pasiulymas, ?prelislaidos);";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?data", MySqlDbType.Date).Value = teamBuildingOffers.data;
            mySqlCommand.Parameters.Add("?pasiulymas", MySqlDbType.VarChar).Value = teamBuildingOffers.pasiulymas;
            mySqlCommand.Parameters.Add("?prelislaidos", MySqlDbType.Int32).Value = teamBuildingOffers.islaidos;
           
            mySqlConnection.Open();
            int i = mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();
            if (i >= 1)
            {

                ViewBag.Message = "Team building pasiulymas paredaguotas sekmingai";
                ViewBag.Status = true;
            }
            return View();

        }
    }
}