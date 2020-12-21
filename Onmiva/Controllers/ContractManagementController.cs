using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Onmiva.Models;
using MySql.Data;
using MySql.Data.MySqlClient;
using Onmiva.Views;
using Onmiva.Models;

using System.Configuration;
using System.Net;

namespace Onmiva.Controllers
{
    public class ContractManagementController : Controller
    {
        // GET: ContractManagement
        public ActionResult CreateContract()
        {
            List<Sender> siuntejai = new List<Sender>();
            siuntejai = Siunteju_Sarasas();
            ViewBag.siuntejai = siuntejai;
            return View();
        }
        public ActionResult ContractList()
        {
            List<Sender> siuntejai = new List<Sender>();
            siuntejai = Siunteju_Sarasas();
            ViewBag.siuntejai = siuntejai;
            List<Contract> kontraktai = new List<Contract>();
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            using (MySqlConnection mySqlConnection = new MySqlConnection(conn))
            {
                string query = "SELECT * FROM Kontraktai";
                using (MySqlCommand cmd = new MySqlCommand(query))
                {
                    cmd.Connection = mySqlConnection;
                    mySqlConnection.Open();
                    using (MySqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            kontraktai.Add(new Contract
                            {
                                ContractId = Convert.ToInt32(sdr["id"]),
                                pasirasymo_data = Convert.ToDateTime(sdr["pasirasymo_data"]),
                                pabaigos_data = Convert.ToDateTime(sdr["galioja_iki"]),
                                imone = Convert.ToInt32(sdr["fk_Siuntu_imoneid"]),
                                siuntejas = Convert.ToInt32(sdr["fk_Siuntejasid"])

                            });
                        }
                    }
                    mySqlConnection.Close();
                }
            }
            ViewBag.KontraktuSarasas = kontraktai;
            
            return View(kontraktai);
        }
    
        public ActionResult ContractView()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateContract(Contract contract)
        {            
                List<Sender> siuntejai = new List<Sender>();
            siuntejai = Siunteju_Sarasas();
            ViewBag.siuntejai = siuntejai;
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
                MySqlConnection mySqlConnection = new MySqlConnection(conn);
                string sqlquery = @"INSERT INTO Kontraktai ( pasirasymo_data, galioja_iki, fk_Siuntu_imoneid, fk_Siuntejasid) 
            VALUES (?start_date, ?end_date, ?imone, ?siuntejas);";
                MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
                mySqlCommand.Parameters.Add("?imone", MySqlDbType.VarChar).Value = contract.imone;
                mySqlCommand.Parameters.Add("?siuntejas", MySqlDbType.VarChar).Value = contract.siuntejas;
                mySqlCommand.Parameters.Add("?start_date", MySqlDbType.Date).Value = contract.pasirasymo_data;
                mySqlCommand.Parameters.Add("?end_date", MySqlDbType.Date).Value = contract.pabaigos_data;
                mySqlConnection.Open();
                int i = mySqlCommand.ExecuteNonQuery();
                mySqlConnection.Close();
                if (i >= 1)
                {

                    ViewBag.Message = "Naujas kontraktas pridėtas sėkmingai";
                        ViewBag.Status = true;
            }
            return View();

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

        public List<Contract> KontraktuSarasas()
        {
            List<Contract> kontraktai = new List<Contract>();
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            using (MySqlConnection mySqlConnection = new MySqlConnection(conn))
            {
                string query = "SELECT * FROM Kontraktai";
                using (MySqlCommand cmd = new MySqlCommand(query))
                {
                    cmd.Connection = mySqlConnection;
                    mySqlConnection.Open();
                    using (MySqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            kontraktai.Add(new Contract
                            {
                                ContractId = Convert.ToInt32(sdr["id"]),
                                pasirasymo_data = Convert.ToDateTime(sdr["pasirasymo_data"]),
                                pabaigos_data = Convert.ToDateTime(sdr["galioja_iki"]),
                                imone = Convert.ToInt32(sdr["fk_Siuntu_imoneid"]),
                                siuntejas = Convert.ToInt32(sdr["fk_Siuntejasid"])

                            });
                        }
                    }
                    mySqlConnection.Close();
                }
            }
            ViewBag.KontraktuSarasas = kontraktai;
            return kontraktai;

        }

        public ActionResult UpdateContract(int? id)
        {
            List<Sender> siuntejai = new List<Sender>();
            siuntejai = Siunteju_Sarasas();
            ViewBag.siuntejai = siuntejai;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List<Contract> kontraktai = KontraktuSarasas();
            Contract kontraktas = kontraktai.Find(i => i.ContractId == id);
            if (kontraktas == null)
            {
                return HttpNotFound();
            }
            ViewBag.kontraktas = kontraktas;
            ViewBag.kontraktoid = kontraktas.ContractId;
            return View("EditKontraktas");
        }

        public ActionResult EditContract(Contract contract)
        {
            List<Sender> siuntejai = new List<Sender>();
            siuntejai = Siunteju_Sarasas();
            ViewBag.siuntejai = siuntejai;
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"UPDATE Kontraktai SET pasirasymo_data = ?start_date, galioja_iki = ?end_date, 
fk_Siuntu_imoneid = ?imone, fk_Siuntejasid = ?siuntejas
WHERE id=?kontraktoid;";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?imone", MySqlDbType.VarChar).Value = contract.imone;
            mySqlCommand.Parameters.Add("?siuntejas", MySqlDbType.VarChar).Value = contract.siuntejas;
            mySqlCommand.Parameters.Add("?start_date", MySqlDbType.Date).Value = contract.pasirasymo_data;
            mySqlCommand.Parameters.Add("?end_date", MySqlDbType.Date).Value = contract.pabaigos_data;
            mySqlCommand.Parameters.Add("?kontraktoid", MySqlDbType.Int32).Value = contract.ContractId ;
            mySqlConnection.Open();
            int i = mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();
            if (i >= 1)
            {

                ViewBag.Message = "Kontraktas redaguotas sėkmingai";
                ViewBag.Status = true;
            }
            return View("EditKontraktas");

        }

        public ActionResult UpdateContractDate(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List<Contract> kontraktai = KontraktuSarasas();
            Contract kontraktas = kontraktai.Find(i => i.ContractId == id);
            if (kontraktas == null)
            {
                return HttpNotFound();
            }
            ViewBag.kontraktas = kontraktas;
            ViewBag.kontraktoid = kontraktas.ContractId;
            return View("EditKontraktoData");
        }
        public ActionResult EditContractDate(Contract contract)
        {
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"UPDATE Kontraktai SET  galioja_iki = ?end_date WHERE id=?kontraktoid;";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?end_date", MySqlDbType.Date).Value = contract.pabaigos_data;
            mySqlCommand.Parameters.Add("?kontraktoid", MySqlDbType.Int32).Value = contract.ContractId;
            mySqlConnection.Open();
            int i = mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();
            if (i >= 1)
            {

                ViewBag.Message = "Kontrakto data redaguota sėkmingai";
                ViewBag.Status = true;
            }
            return View("EditKontraktoData");

        }

    }
}