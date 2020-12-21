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
                    bill.SendingCompany = Convert.ToString(item["pavadinimas"]);

                }
                return bill;
            }
            catch (Exception)
            {
                return bill;
            }

        }



    }
}