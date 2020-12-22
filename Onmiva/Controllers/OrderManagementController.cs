using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Onmiva.Models;
using MySql.Data;
using MySql.Data.MySqlClient;
using Onmiva.Views;
using System.ComponentModel.DataAnnotations.Schema;

using System.Configuration;
using System.Net;

namespace Onmiva.Controllers
{
    public class OrderManagementController : Controller
    {
        // GET: OrderManagement
        public ActionResult OrderCreate()
        {
            return View();
        }

        // POST: OrderManagement
        [HttpPost]
        public ActionResult OrderCreate(Order order)
        {
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"INSERT INTO Uzsakymai ( id, data, suma, busena, fk_Siuntu_imoneid, fk_Klientasid_Klientas) 
            VALUES (?id, ?data, ?suma, ?busena, ?fk_Siuntu_imoneid, ?fk_Klientasid_Klientas);";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?id", MySqlDbType.Int32).Value = order.Id;
            mySqlCommand.Parameters.Add("?data", MySqlDbType.Date).Value = order.Date;
            mySqlCommand.Parameters.Add("?suma", MySqlDbType.Decimal).Value = order.Amount;
            mySqlCommand.Parameters.Add("?busena", MySqlDbType.VarChar).Value = order.OrderStatus;
            mySqlCommand.Parameters.Add("?fk_Siuntu_imoneid", MySqlDbType.VarChar).Value = order.ShipmentCompany;
            mySqlCommand.Parameters.Add("?fk_Klientasid_Klientas", MySqlDbType.VarChar).Value = order.Client;
            mySqlConnection.Open();
            int i = mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();
            if (i >= 1)
            {

                ViewBag.Message = "Naujas uzsakymas pridėtas sėkmingai";
                ViewBag.Status = true;
            }
            return View();
        }

        // GET
        public ActionResult OrderEdit(int Id)
        {
            List<Order> orders = new List<Order>();
            Order order = new Order();
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            using (MySqlConnection mySqlConnection = new MySqlConnection(conn))
            {
                string query = "SELECT * FROM Uzsakymai WHERE Uzsakymai.id = " + Id + ";";
                using (MySqlCommand cmd = new MySqlCommand(query))
                {
                    cmd.Connection = mySqlConnection;
                    mySqlConnection.Open();
                    using (MySqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            orders.Add(new Order
                            {
                                Id = Convert.ToInt32(sdr["id"]),
                                Date = Convert.ToDateTime(sdr["data"]),
                                Amount = Convert.ToDecimal(sdr["suma"]),
                                OrderStatus = sdr["busena"].ToString(),
                                ShipmentCompany = sdr["fk_Siuntu_imoneid"].ToString(),
                                Client = sdr["fk_Klientasid_Klientas"].ToString()
                            });
                        }
                    }
                    mySqlConnection.Close();
                }
            }
            order = orders.First();
            return View(order);
        }

        // POST
        [HttpPost]
        public ActionResult OrderEdit(Order order)
        {
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"UPDATE Uzsakymai SET data=?data, suma=?suma, busena=?busena, fk_Siuntu_imoneid=?fk_Siuntu_imoneid, fk_Klientasid_Klientas=?fk_Klientasid_Klientas WHERE id=?Id;";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?id", MySqlDbType.Int32).Value = order.Id;
            mySqlCommand.Parameters.Add("?data", MySqlDbType.Date).Value = order.Date;
            mySqlCommand.Parameters.Add("?suma", MySqlDbType.Decimal).Value = order.Amount;
            mySqlCommand.Parameters.Add("?busena", MySqlDbType.VarChar).Value = order.OrderStatus;
            mySqlCommand.Parameters.Add("?fk_Siuntu_imoneid", MySqlDbType.VarChar).Value = order.ShipmentCompany;
            mySqlCommand.Parameters.Add("?fk_Klientasid_Klientas", MySqlDbType.VarChar).Value = order.Client;
            mySqlConnection.Open();
            int i = mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();
            if (i >= 1)
            {

                ViewBag.Message = "Uzsakymas redaguotas sėkmingai";
                ViewBag.Status = true;
            }
            return View();

        }

        public ActionResult OrdersList()
        {
            List<Order> orders = new List<Order>();
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            using (MySqlConnection mySqlConnection = new MySqlConnection(conn))
            {
                string query = "SELECT * FROM Uzsakymai";
                using (MySqlCommand cmd = new MySqlCommand(query))
                {
                    cmd.Connection = mySqlConnection;
                    mySqlConnection.Open();
                    using (MySqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            orders.Add(new Order
                            {
                                Id = Convert.ToInt32(sdr["id"]),
                                Date = Convert.ToDateTime(sdr["data"]),
                                Amount = Convert.ToDecimal(sdr["suma"]),
                                OrderStatus = sdr["busena"].ToString(),
                                ShipmentCompany = sdr["fk_Siuntu_imoneid"].ToString(),
                                Client = sdr["fk_Klientasid_Klientas"].ToString()
                            });
                        }
                    }
                    mySqlConnection.Close();
                }
            }
            return View(orders);
        }

        public ActionResult OrderView(int Id)
        {
            List<Order> orders = new List<Order>();
            Order order = new Order();
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            using (MySqlConnection mySqlConnection = new MySqlConnection(conn))
            {
                string query = "SELECT * FROM Uzsakymai WHERE Uzsakymai.id = " + Id + ";";
                using (MySqlCommand cmd = new MySqlCommand(query))
                {
                    cmd.Connection = mySqlConnection;
                    mySqlConnection.Open();
                    using (MySqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            orders.Add(new Order
                            {
                                Id = Convert.ToInt32(sdr["id"]),
                                Date = Convert.ToDateTime(sdr["data"]),
                                Amount = Convert.ToDecimal(sdr["suma"]),
                                OrderStatus = sdr["busena"].ToString(),
                                ShipmentCompany = sdr["fk_Siuntu_imoneid"].ToString(),
                                Client = sdr["fk_Klientasid_Klientas"].ToString()
                            });
                        }
                    }
                    mySqlConnection.Close();
                }
            }
            return View(orders);
        }

        public ActionResult FilterOrders()
        {
            return View();
        }

        public ActionResult FilterOrders2(string searchKlientas)
        {
            List<Order> orders = new List<Order>();
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            using (MySqlConnection mySqlConnection = new MySqlConnection(conn))
            {
                string query = "SELECT * FROM Uzsakymai";
                using (MySqlCommand cmd = new MySqlCommand(query))
                {
                    cmd.Connection = mySqlConnection;
                    mySqlConnection.Open();
                    using (MySqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            orders.Add(new Order
                            {
                                Id = Convert.ToInt32(sdr["id"]),
                                Date = Convert.ToDateTime(sdr["data"]),
                                Amount = Convert.ToDecimal(sdr["suma"]),
                                OrderStatus = sdr["busena"].ToString(),
                                ShipmentCompany = sdr["fk_Siuntu_imoneid"].ToString(),
                                Client = sdr["fk_Klientasid_Klientas"].ToString()
                            });
                        }
                    }
                    mySqlConnection.Close();
                }
            }
            if (!String.IsNullOrEmpty(searchKlientas))
            {
                orders = orders.Where(s => s.Client.Contains(searchKlientas)).ToList();
            }

            ViewBag.OrdersList = orders;
            return View("OrdersList", orders);
        }

        public ActionResult AssignShipmentCompany(int Id)
        {
            List<Order> orders = new List<Order>();
            Order order = new Order();
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            using (MySqlConnection mySqlConnection = new MySqlConnection(conn))
            {
                string query = "SELECT * FROM Uzsakymai WHERE Uzsakymai.id = " + Id + ";";
                using (MySqlCommand cmd = new MySqlCommand(query))
                {
                    cmd.Connection = mySqlConnection;
                    mySqlConnection.Open();
                    using (MySqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            orders.Add(new Order
                            {
                                Id = Convert.ToInt32(sdr["id"]),
                                Date = Convert.ToDateTime(sdr["data"]),
                                Amount = Convert.ToDecimal(sdr["suma"]),
                                OrderStatus = sdr["busena"].ToString(),
                                ShipmentCompany = sdr["fk_Siuntu_imoneid"].ToString(),
                                Client = sdr["fk_Klientasid_Klientas"].ToString()
                            });
                        }
                    }
                    mySqlConnection.Close();
                }
            }
            order = orders.First();
            return View(order);
        }

        [HttpPost]
        public ActionResult AssignShipmentCompany(Order order)
        {
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"UPDATE Uzsakymai SET fk_Siuntu_imoneid=?fk_Siuntu_imoneid WHERE id=?Id;";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?id", MySqlDbType.Int32).Value = order.Id;
            mySqlCommand.Parameters.Add("?fk_Siuntu_imoneid", MySqlDbType.VarChar).Value = order.ShipmentCompany;
            mySqlConnection.Open();
            int i = mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();
            if (i >= 1)
            {

                ViewBag.Message = "Įmonė pakeista sėkmingai";
                ViewBag.Status = true;
            }
            return View();
        }

        public ActionResult ChangeOrderStatus(int Id)
        {
            List<Order> orders = new List<Order>();
            Order order = new Order();
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            using (MySqlConnection mySqlConnection = new MySqlConnection(conn))
            {
                string query = "SELECT * FROM Uzsakymai WHERE Uzsakymai.id = " + Id + ";";
                using (MySqlCommand cmd = new MySqlCommand(query))
                {
                    cmd.Connection = mySqlConnection;
                    mySqlConnection.Open();
                    using (MySqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            orders.Add(new Order
                            {
                                Id = Convert.ToInt32(sdr["id"]),
                                Date = Convert.ToDateTime(sdr["data"]),
                                Amount = Convert.ToDecimal(sdr["suma"]),
                                OrderStatus = sdr["busena"].ToString(),
                                ShipmentCompany = sdr["fk_Siuntu_imoneid"].ToString(),
                                Client = sdr["fk_Klientasid_Klientas"].ToString()
                            });
                        }
                    }
                    mySqlConnection.Close();
                }
            }
            order = orders.First();
            return View(order);
        }

        [HttpPost]
        public ActionResult ChangeOrderStatus(Order order)
        {
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"UPDATE Uzsakymai SET busena=?busena WHERE id=?Id;";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?id", MySqlDbType.Int32).Value = order.Id;
            mySqlCommand.Parameters.Add("?busena", MySqlDbType.VarChar).Value = order.OrderStatus;
            mySqlConnection.Open();
            int i = mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();
            if (i >= 1)
            {

                ViewBag.Message = "Būsena atnaujinta sėkmingai";
                ViewBag.Status = true;
            }
            return View();
        }
    }
}