using MySql.Data.MySqlClient;
using Onmiva.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

namespace Onmiva.Repos
{
    public class BillRepository
    {

        public List<Bill> GetUserBills(int userId) {
            List<Bill> bills = new List<Bill>();

            try
            {
                string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
                MySqlConnection mySqlConnection = new MySqlConnection(conn);
                string sqlquery = @"SELECT * FROM Saskaitos sk INNER JOIN Siuntu_imones si ON sk.fk_Siuntu_imoneid = si.id
INNER JOIN Saskaitos_busenos sb ON sk.busena = sb.id_Saskaitos_busenos WHERE sk.fk_Klientasid_Klientas = " + userId + ";";
                MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
                mySqlConnection.Open();
                MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
                DataTable dt = new DataTable();
                mda.Fill(dt);
                mySqlConnection.Close();

                foreach (DataRow item in dt.Rows)
                {
                    Bill bill = new Bill();
                    bill.NumberId = Convert.ToInt32(item["nr"]);
                    bill.Sum = Convert.ToDecimal(item["suma"]);
                    bill.Date = Convert.ToDateTime(item["data"]);
                    bill.PayComment = Convert.ToString(item["mokejimo_paskirtis"]);
                    bill.PayUntil = Convert.ToDateTime(item["apmoketi_iki"]);
                    bill.State = Convert.ToString(item["name"]);
                    bill.SendingCompany = Convert.ToString(item["pavadinimas"]);
                    
                    bills.Add(bill);
                }
                return bills;
            }
            catch (Exception)
            {
                return bills;
            }

        }



        public List<Bill> GetUserBills(string email)
        {
            List<Bill> bills = new List<Bill>();

            try
            {
                string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
                MySqlConnection mySqlConnection = new MySqlConnection(conn);
                string sqlquery = @"SELECT * FROM Saskaitos sk INNER JOIN Siuntu_imones si ON sk.fk_Siuntu_imoneid = si.id
INNER JOIN Saskaitos_busenos sb ON sk.busena = sb.id_Saskaitos_busenos INNER JOIN Klientai kl ON
sk.fk_Klientasid_Klientas = kl.id_Klientas WHERE kl.el_pastas = '" + email + "';";
                MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
                mySqlConnection.Open();
                MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
                DataTable dt = new DataTable();
                mda.Fill(dt);
                mySqlConnection.Close();

                foreach (DataRow item in dt.Rows)
                {
                    Bill bill = new Bill();
                    bill.NumberId = Convert.ToInt32(item["nr"]);
                    bill.Sum = Convert.ToDecimal(item["suma"]);
                    bill.Date = Convert.ToDateTime(item["data"]);
                    bill.PayComment = Convert.ToString(item["mokejimo_paskirtis"]);
                    bill.PayUntil = Convert.ToDateTime(item["apmoketi_iki"]);
                    bill.State = Convert.ToString(item["name"]);
                    bill.SendingCompany = Convert.ToString(item["pavadinimas"]);

                    bills.Add(bill);
                }
                return bills;
            }
            catch (Exception)
            {
                return bills;
            }

        }

        public bool CreateBill(Bill bill) {
            try
            {

                Order order = GetOrder(bill.OrderId);

                string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
                MySqlConnection mySqlConnection = new MySqlConnection(conn);
                string sqlquery = @"INSERT INTO `Saskaitos` (`nr`, `suma`, `data`, `mokejimo_paskirtis`, `apmoketi_iki`,
`busena`, `fk_Klientasid_Klientas`, `fk_Uzsakymasid`, `fk_Siuntu_imoneid`) VALUES (NULL, ?sum, ?currDate, ?reason,
?payUntil, ?stateId, ?clientId, ?orderId, ?senderId);";
                MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
                mySqlCommand.Parameters.Add("?sum", MySqlDbType.Decimal).Value = bill.Sum;
                mySqlCommand.Parameters.Add("?currDate", MySqlDbType.Datetime).Value = DateTime.Now;
                mySqlCommand.Parameters.Add("?payUntil", MySqlDbType.Datetime).Value = bill.PayUntil;
                mySqlCommand.Parameters.Add("?reason", MySqlDbType.VarChar).Value = bill.PayComment;
                mySqlCommand.Parameters.Add("?stateId", MySqlDbType.Int32).Value = 2;
                mySqlCommand.Parameters.Add("?clientId", MySqlDbType.Int32).Value = order.ClientId;
                mySqlCommand.Parameters.Add("?orderId", MySqlDbType.Int32).Value = order.Id;
                mySqlCommand.Parameters.Add("?senderId", MySqlDbType.Int32).Value = order.CompanyId;
                mySqlConnection.Open();
                mySqlCommand.ExecuteNonQuery();
                mySqlConnection.Close();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<Order> GetOrdersWithoutBill() {
            List<Order> orders = new List<Order>();

            try
            {
                string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
                MySqlConnection mySqlConnection = new MySqlConnection(conn);
                string sqlquery = @"SELECT uz.id, uz.data, uz.suma, uz.svoris, bus.name AS busena, uz.busena AS busena_id, kl.vardas, kl.pavarde,
im.pavadinimas AS siuntu_imone FROM Uzsakymai uz LEFT JOIN Saskaitos sas ON uz.id = sas.fk_Uzsakymasid INNER JOIN
Uzsakymu_busenos bus ON uz.busena = bus.id_Užsakymo_busenos INNER JOIN Klientai kl ON kl.id_Klientas = uz.fk_Klientasid_Klientas
INNER JOIN Siuntu_imones im ON uz.fk_Siuntu_imoneid = im.id WHERE sas.fk_Uzsakymasid IS NULL";
                MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
                mySqlConnection.Open();
                MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
                DataTable dt = new DataTable();
                mda.Fill(dt);
                mySqlConnection.Close();

                foreach (DataRow item in dt.Rows)
                {
                    Order order = new Order();
                    order.Id = Convert.ToInt32(item["id"]);
                    order.Sum = Convert.ToDecimal(item["suma"]);
                    order.Date = Convert.ToDateTime(item["data"]);
                    order.Weight = Convert.ToDouble(item["svoris"]);
                    order.Client = Convert.ToString(item["vardas"]) + " " + Convert.ToString(item["pavarde"]);
                    order.State = Convert.ToString(item["busena"]);
                    order.StateId = Convert.ToString(item["busena_id"]);
                    order.Company = Convert.ToString(item["siuntu_imone"]);

                    orders.Add(order);
                }
                return orders;
            }
            catch (Exception)
            {
                return orders;
            }

        }

        public Bill GetUserBill(int billId)
        {
            Bill bill = new Bill();

            try
            {
                string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
                MySqlConnection mySqlConnection = new MySqlConnection(conn);
                string sqlquery = @"SELECT * FROM Saskaitos sk INNER JOIN Siuntu_imones si ON sk.fk_Siuntu_imoneid =
si.id INNER JOIN Saskaitos_busenos sb ON sk.busena = sb.id_Saskaitos_busenos WHERE sk.nr = " + billId + ";";
                MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
                mySqlConnection.Open();
                MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
                DataTable dt = new DataTable();
                mda.Fill(dt);
                mySqlConnection.Close();

                foreach (DataRow item in dt.Rows)
                {
                    bill.NumberId = Convert.ToInt32(item["nr"]);
                    bill.Sum = Convert.ToDecimal(item["suma"]);
                    bill.Date = Convert.ToDateTime(item["data"]);
                    bill.PayComment = Convert.ToString(item["mokejimo_paskirtis"]);
                    bill.PayUntil = Convert.ToDateTime(item["apmoketi_iki"]);
                    bill.State = Convert.ToString(item["name"]);
                    bill.StateId = Convert.ToInt32(item["busena"]);
                    bill.SendingCompany = Convert.ToString(item["pavadinimas"]);

                }
                return bill;
            }
            catch (Exception)
            {
                return bill;
            }

        }


        public Order GetOrder(int id)
        {
            Order order = new Order();

            try
            {
                string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
                MySqlConnection mySqlConnection = new MySqlConnection(conn);
                string sqlquery = @"SELECT * FROM Uzsakymai uz WHERE uz.id = ?id";
                MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
                mySqlCommand.Parameters.Add("?id", MySqlDbType.Int32).Value = id;
                mySqlConnection.Open();
                MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
                DataTable dt = new DataTable();
                mda.Fill(dt);
                mySqlConnection.Close();

                foreach (DataRow item in dt.Rows)
                {

                    order.Id = Convert.ToInt32(item["id"]);
                    order.Date = Convert.ToDateTime(item["data"]);
                    order.Sum = Convert.ToDecimal(item["suma"]);
                    order.Weight = Convert.ToDouble(item["svoris"]);
                    order.CompanyId = Convert.ToInt32(item["fk_Siuntu_imoneid"]);
                    order.StateId = Convert.ToString(item["busena"]);
                    order.ClientId = id;

                }
                return order;
            }
            catch (Exception)
            {
                return order;
            }

        }


        public bool EditBill(Bill bill) {
            try
            {


                string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
                MySqlConnection mySqlConnection = new MySqlConnection(conn);
                string sqlquery = @"UPDATE saskaitos SET suma = ?sum, mokejimo_paskirtis = ?reason, apmoketi_iki = ?payUntil, busena = ?stateId WHERE saskaitos.nr = ?billNum;";
                MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
                mySqlCommand.Parameters.Add("?sum", MySqlDbType.Double).Value = bill.Sum;
                mySqlCommand.Parameters.Add("?payUntil", MySqlDbType.Datetime).Value = bill.PayUntil;
                mySqlCommand.Parameters.Add("?reason", MySqlDbType.VarChar).Value = bill.PayComment;
                if (bill.StateId != 0)
                {
                    mySqlCommand.Parameters.Add("?stateId", MySqlDbType.Int32).Value = bill.StateId;
                }
                else {
                    mySqlCommand.Parameters.Add("?stateId", MySqlDbType.Int32).Value = GetUserBill(bill.NumberId).StateId;
                }
                mySqlCommand.Parameters.Add("?billNum", MySqlDbType.Int32).Value = bill.NumberId;
                mySqlConnection.Open();
                mySqlCommand.ExecuteNonQuery();
                mySqlConnection.Close();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }



        public bool DeleteBill(int id) {
            try
            {
                string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
                MySqlConnection mySqlConnection = new MySqlConnection(conn);
                string sqlquery = @"DELETE FROM saskaitos WHERE saskaitos.nr = ?id";
                MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
                mySqlCommand.Parameters.Add("?id", MySqlDbType.Double).Value = id;
                mySqlConnection.Open();
                mySqlCommand.ExecuteNonQuery();
                mySqlConnection.Close();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        public List<Bill> GetAllBills() {
            List<Bill> bills = new List<Bill>();

            try
            {
                string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
                MySqlConnection mySqlConnection = new MySqlConnection(conn);
                string sqlquery = @"SELECT * FROM Saskaitos sk INNER JOIN Siuntu_imones si ON sk.fk_Siuntu_imoneid = si.id
INNER JOIN Saskaitos_busenos sb ON sk.busena = sb.id_Saskaitos_busenos";
                MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
                mySqlConnection.Open();
                MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
                DataTable dt = new DataTable();
                mda.Fill(dt);
                mySqlConnection.Close();

                foreach (DataRow item in dt.Rows)
                {
                    Bill bill = new Bill();
                    bill.NumberId = Convert.ToInt32(item["nr"]);
                    bill.Sum = Convert.ToDecimal(item["suma"]);
                    bill.Date = Convert.ToDateTime(item["data"]);
                    bill.PayComment = Convert.ToString(item["mokejimo_paskirtis"]);
                    bill.PayUntil = Convert.ToDateTime(item["apmoketi_iki"]);
                    bill.State = Convert.ToString(item["name"]);
                    bill.SendingCompany = Convert.ToString(item["pavadinimas"]);

                    bills.Add(bill);
                }
                return bills;
            }
            catch (Exception)
            {
                return bills;
            }

        }

        public List<string> GetBillStateList() {
            List<string> states = new List<string>();

            try
            {
                string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
                MySqlConnection mySqlConnection = new MySqlConnection(conn);
                string sqlquery = @"SELECT name as pav, id_Saskaitos_busenos as id FROM saskaitos_busenos";
                MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
                mySqlConnection.Open();
                MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
                DataTable dt = new DataTable();
                mda.Fill(dt);
                mySqlConnection.Close();

                foreach (DataRow item in dt.Rows)
                {
                    states.Add(Convert.ToString(item["pav"]));
                   
                }
                return states;
            }
            catch (Exception)
            {
                return states;
            }
        }

    }
}