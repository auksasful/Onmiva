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

    public class SenderManagementController : Controller
    {
        // GET: SenderManagement
        
        public ActionResult SenderManagementAdmin(string sortOrder, string searchPavadinimas, string searchMiestas)
        {
            
                List<Sender> siuntejai = new List<Sender>();
                string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
                using (MySqlConnection mySqlConnection = new MySqlConnection(conn))
                {
                    string query = "SELECT * FROM Siuntejai";
                    using (MySqlCommand cmd = new MySqlCommand(query))
                    {
                        cmd.Connection = mySqlConnection;
                        mySqlConnection.Open();
                        using (MySqlDataReader sdr = cmd.ExecuteReader())
                        {
                            while (sdr.Read())
                            {
                                siuntejai.Add(new Sender
                                {
                                    SenderId = Convert.ToInt32(sdr["id"]),
                                    pavadinimas = sdr["pavadinimas"].ToString(),
                                    tel_nr = sdr["tel_nr"].ToString(),
                                    miestas = sdr["miestas"].ToString()

                                });
                            }
                        }
                        mySqlConnection.Close();
                    }
                }
                if (!String.IsNullOrEmpty(searchPavadinimas))
                {
                    siuntejai = siuntejai.Where(s => s.pavadinimas.Contains(searchPavadinimas)).ToList();
                }
                if (!String.IsNullOrEmpty(searchMiestas))
                {
                    siuntejai = siuntejai.Where(s => s.miestas.Contains(searchMiestas)).ToList();
                }
                ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "vardas" : "";
                ViewBag.CitySortParm = String.IsNullOrEmpty(sortOrder) ? "miestas" : "";
                switch (sortOrder)
                {
                    case "vardas":
                        siuntejai = siuntejai.OrderBy(s => s.pavadinimas).ToList();
                        break;
                    case "miestas":
                        siuntejai = siuntejai.OrderBy(s => s.miestas).ToList(); ;
                        break;
                }

                ViewBag.KontraktuSarasas = siuntejai;
                return View(siuntejai);
            }
           
        
        public ActionResult SenderManagement()
        {
            List<Sender> siuntejai = new List<Sender>();
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            using (MySqlConnection mySqlConnection = new MySqlConnection(conn))
            {
                string query = "SELECT * FROM Siuntejai";
                using (MySqlCommand cmd = new MySqlCommand(query))
                {
                    cmd.Connection = mySqlConnection;
                    mySqlConnection.Open();
                    using (MySqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            siuntejai.Add(new Sender
                            {
                                SenderId = Convert.ToInt32(sdr["id"]),
                                pavadinimas = sdr["pavadinimas"].ToString(),
                                tel_nr = sdr["tel_nr"].ToString(),
                                miestas = sdr["miestas"].ToString()

                            });
                        }
                    }
                    mySqlConnection.Close();
                }
            }
            ViewBag.KontraktuSarasas = siuntejai;
            return View(siuntejai);
   
        }
        public List<Sender> Siunteju_Sarasas()
        {
            List<Sender> siuntejai = new List<Sender>();
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            using (MySqlConnection mySqlConnection = new MySqlConnection(conn))
            {
                string query = "SELECT * FROM Siuntejai";
                using (MySqlCommand cmd = new MySqlCommand(query))
                {
                    cmd.Connection = mySqlConnection;
                    mySqlConnection.Open();
                    using (MySqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            siuntejai.Add(new Sender
                            {
                                SenderId = Convert.ToInt32(sdr["id"]),
                                pavadinimas = sdr["pavadinimas"].ToString(),
                                tel_nr = sdr["tel_nr"].ToString(),
                                miestas = sdr["miestas"].ToString()

                            }); ;
                        }
                    }
                    mySqlConnection.Close();
                }
            }
            ViewBag.KontraktuSarasas = siuntejai;
            return siuntejai;
        }

        public ActionResult Rating(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List<Sender> sarasas = Siunteju_Sarasas();
            Sender siuntejas = sarasas.Find(i => i.SenderId == id);
            if (siuntejas == null)
            {
                return HttpNotFound();
            }
            ViewBag.siuntejoPav = siuntejas.pavadinimas;
            ViewBag.siuntejoId = siuntejas.SenderId;
            return View("SenderReview");
        }
        public ActionResult InsertRating(string command, Sender_Rating rating, Sender_Rating review)
        {
            
                string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
                MySqlConnection mySqlConnection = new MySqlConnection(conn);
                string sqlquery = @"INSERT INTO Siuntejo_ivertinimai ( ivertinimas, fk_Siuntejasid) 
            VALUES (?ivertinimas, ?fk_Siuntejasid);";
                MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
                mySqlCommand.Parameters.Add("?ivertinimas", MySqlDbType.VarChar).Value = rating.ivertinimas;
                mySqlCommand.Parameters.Add("?fk_Siuntejasid", MySqlDbType.VarChar).Value = rating.SenderId;
                mySqlConnection.Open();
                int i = mySqlCommand.ExecuteNonQuery();
                mySqlConnection.Close();
                if (i >= 1)
                {

                    ViewBag.Message = "Naujas įvertinimas pridėtas sėkmingai";
                    ViewBag.Status = true;

                }
                return View("SenderReview");
              
        }

        public ActionResult InsertReview(Sender_Rating review)
        {
            var values = new[] { "kvailys", "gaidys", "kvailas", "durnas" };
            var str = review.atsiliepimas;
            if (values.Any(str.Contains))
            {
                ViewBag.Message = "Jūsų atsiliepime yra keiksmažodžių";
                ViewBag.Status = false;
                return View("SenderReview");
            }
            else
            {
                string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
                MySqlConnection mySqlConnection = new MySqlConnection(conn);
                string sqlquery = @"INSERT INTO Siuntejo_atsiliepimai ( atsiliepimas, fk_Siuntejasid) 
            VALUES (?atsiliepimas, ?fk_Siuntejasid);";
                MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
                mySqlCommand.Parameters.Add("?atsiliepimas", MySqlDbType.VarChar).Value = review.atsiliepimas;
                mySqlCommand.Parameters.Add("?fk_Siuntejasid", MySqlDbType.VarChar).Value = review.SenderId;
                mySqlConnection.Open();
                int i = mySqlCommand.ExecuteNonQuery();
                mySqlConnection.Close();
                if (i >= 1)
                {

                    ViewBag.Message = "Jūsų atsiliepimas pridėtas sėkmingai";
                    ViewBag.Status = true;

                }
                return View("SenderReview");
            }
        }
        public ActionResult Informacija(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List<Sender> sarasas = Siunteju_Sarasas();
            Sender siuntejas = sarasas.Find(i => i.SenderId == id);
            if (siuntejas == null)
            {
                return HttpNotFound();
            }
            ViewBag.siuntejas = siuntejas;
          
            ViewBag.siuntejoId = siuntejas.SenderId;
            ViewBag.atsiliepimai = atsiliepimai(ViewBag.siuntejoId);
            ViewBag.ivertinimai = ivertinimai(ViewBag.siuntejoId);
            return View("SenderInfo");
        }

        public List<string> atsiliepimai(int id)
        {
            int siuntejo_id = id;
            List<string> atsiliepimai = new List<string>();
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            using (MySqlConnection mySqlConnection = new MySqlConnection(conn))
            {
                string query = "SELECT atsiliepimas FROM Siuntejo_atsiliepimai WHERE fk_Siuntejasid = ?siuntejoid";

                using (MySqlCommand cmd = new MySqlCommand(query))
                {
                    cmd.Connection = mySqlConnection;
                    cmd.Parameters.Add("?siuntejoid", MySqlDbType.VarChar).Value = siuntejo_id;
                    mySqlConnection.Open();
                    using (MySqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            atsiliepimai.Add(sdr["atsiliepimas"].ToString()); 
                        }
                    }
                    mySqlConnection.Close();
                }
            }

            return atsiliepimai;
        }

        public List<int> ivertinimai(int id)
        {
            int siuntejo_id = id;
            List<int> ivertinimai = new List<int>();
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            using (MySqlConnection mySqlConnection = new MySqlConnection(conn))
            {
                string query = "SELECT ivertinimas FROM Siuntejo_ivertinimai WHERE fk_Siuntejasid = ?siuntejoid";

                using (MySqlCommand cmd = new MySqlCommand(query))
                {
                    cmd.Connection = mySqlConnection;
                    cmd.Parameters.Add("?siuntejoid", MySqlDbType.VarChar).Value = siuntejo_id;
                    mySqlConnection.Open();
                    using (MySqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            ivertinimai.Add(Convert.ToInt32(sdr["ivertinimas"]));
                        }
                    }
                    mySqlConnection.Close();
                }
            }

            return ivertinimai;
        }
    }
}