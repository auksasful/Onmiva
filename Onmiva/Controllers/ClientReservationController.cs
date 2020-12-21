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
    public class ClientReservationController : Controller
    {
        // GET: ClientReservation
        public ActionResult AddClientReservation()
        {
            return View();
        }

        public ActionResult DeleteClientReservation()
        {
            return View();
        }

        public ActionResult ClientReservation()
        {
            List<ClientReservation> rezervacijos = new List<ClientReservation>();
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            using (MySqlConnection mySqlConnection = new MySqlConnection(conn))
            {
                string query = "SELECT * FROM Consultation";
                using (MySqlCommand cmd = new MySqlCommand(query))
                {
                    cmd.Connection = mySqlConnection;
                    mySqlConnection.Open();
                    using (MySqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            rezervacijos.Add(new ClientReservation
                            {
                                rezervacijosId = Convert.ToInt32(sdr["id"]),
                                pavadinimas = sdr["pavadinimas"].ToString(),
                                klientoVardas = sdr["klient_vardas"].ToString(),
                                klientoPavarde = sdr["klient_pavarde"].ToString(),
                                rezervacijos_data = Convert.ToDateTime(sdr["data"])

                            });
                        }
                    }
                    mySqlConnection.Close();
                }
            }
            ViewBag.KontraktuSarasas = rezervacijos;
            return View(rezervacijos);
        }


        public List<ClientReservation> RezervacijuSarasas()
        {
            List<ClientReservation> rezervacijos = new List<ClientReservation>();
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            using (MySqlConnection mySqlConnection = new MySqlConnection(conn))
            {
                string query = "SELECT * FROM Consultation";
                using (MySqlCommand cmd = new MySqlCommand(query))
                {
                    cmd.Connection = mySqlConnection;
                    mySqlConnection.Open();
                    using (MySqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            rezervacijos.Add(new ClientReservation
                            {
                                rezervacijosId = Convert.ToInt32(sdr["id"]),
                                pavadinimas = sdr["pavadinimas"].ToString(),
                                klientoVardas = sdr["klient_vardas"].ToString(),
                                klientoPavarde = sdr["klient_pavarde"].ToString(),
                                rezervacijos_data = Convert.ToDateTime(sdr["data"])

                            });
                        }
                    }
                    mySqlConnection.Close();
                }
            }
            ViewBag.RezervacijuSarasas = rezervacijos;
            return rezervacijos;

        }

        [HttpPost]
        public ActionResult AddClientReservation(ClientReservation clientReservation)
        {
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"INSERT INTO Consultation ( pavadinimas, klient_vardas, klient_pavarde, data) 
            VALUES (?pavadinimas, ?vardas, ?pavarde, ?data);";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?pavadinimas", MySqlDbType.VarChar).Value = clientReservation.pavadinimas;
            mySqlCommand.Parameters.Add("?vardas", MySqlDbType.VarChar).Value = clientReservation.klientoVardas;
            mySqlCommand.Parameters.Add("?pavarde", MySqlDbType.VarChar).Value = clientReservation.klientoPavarde;
            mySqlCommand.Parameters.Add("?data", MySqlDbType.Date).Value = clientReservation.rezervacijos_data;
            mySqlConnection.Open();
            int i = mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();
            if (i >= 1)
            {

                ViewBag.Message = "Nauja rezervacija pridėta sėkmingai";
                ViewBag.Status = true;
            }
            return View();

        }

    }
}