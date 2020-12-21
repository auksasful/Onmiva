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
    public class PaymentRepository
    {


        public bool AddPayment(Bill_Payment payment) {
            try
            {
                string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
                MySqlConnection mySqlConnection = new MySqlConnection(conn);
                string sqlquery = @"INSERT INTO `apmokejimai` (`data`, `id`, `suma`, `apmokejimo_metodas`,
`fk_Siuntu_imoneid`, `fk_Saskaitanr`, `fk_Klientasid_Klientas`) VALUES (?date, NULL, ?sum, ?payMethod, ?sendingCompany, ?bill, ?client);";
                MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
                mySqlCommand.Parameters.Add("?date", MySqlDbType.Datetime).Value = DateTime.Now;
                mySqlCommand.Parameters.Add("?sum", MySqlDbType.Decimal).Value = payment.Sum;
                if (payment.PaymentMethod == "banko pavedimu")
                {
                    mySqlCommand.Parameters.Add("?payMethod", MySqlDbType.Int32).Value = 1;
                }
                else {
                    mySqlCommand.Parameters.Add("?payMethod", MySqlDbType.Int32).Value = 2;
                }
                mySqlCommand.Parameters.Add("?sendingCompany", MySqlDbType.Int32).Value = payment.SenderCompany;
                mySqlCommand.Parameters.Add("?bill", MySqlDbType.Int32).Value = payment.Bill;
                mySqlCommand.Parameters.Add("?client", MySqlDbType.Int32).Value = payment.Client;

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

        public List<string> GetPaymentMethods() {
            List<string> methods = new List<string>();

            try
            {
                string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
                MySqlConnection mySqlConnection = new MySqlConnection(conn);
                string sqlquery = @"SELECT name as pav FROM apmokejimo_metodai";
                MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
                mySqlConnection.Open();
                MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
                DataTable dt = new DataTable();
                mda.Fill(dt);
                mySqlConnection.Close();

                foreach (DataRow item in dt.Rows)
                {
                    methods.Add(Convert.ToString(item["pav"]));

                }
                return methods;
            }
            catch (Exception)
            {
                return methods;
            }
        }


        public Bill_Payment GetPaymentByBill(int id) {
            Bill_Payment pay = new Bill_Payment();

            try
            {
                string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
                MySqlConnection mySqlConnection = new MySqlConnection(conn);
                string sqlquery = @"SELECT fk_Klientasid_Klientas as klientas,
fk_Siuntu_imoneid as siuntejas, suma, nr FROM saskaitos where saskaitos.nr = ?id";
                MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
                mySqlCommand.Parameters.Add("?id", MySqlDbType.Int32).Value = id;
                mySqlConnection.Open();
                MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
                DataTable dt = new DataTable();
                mda.Fill(dt);
                mySqlConnection.Close();

                foreach (DataRow item in dt.Rows)
                {
                    pay.Client = Convert.ToInt32(item["klientas"]);
                    pay.SenderCompany = Convert.ToInt32(item["siuntejas"]);
                    pay.Sum = Convert.ToDecimal(item["suma"]);
                    pay.Bill = Convert.ToInt32(item["nr"]);

                }
                return pay;
            }
            catch (Exception)
            {
                return pay;
            }
        }



        public decimal GetPaymentSum(int id) {
            decimal sum = 0;
            try
            {
                string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
                MySqlConnection mySqlConnection = new MySqlConnection(conn);
                string sqlquery = @"SELECT SUM(ap.suma) AS sum FROM apmokejimai ap WHERE ap.fk_Saskaitanr = ?id";
                MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
                mySqlCommand.Parameters.Add("?id", MySqlDbType.Int32).Value = id;
                mySqlConnection.Open();
                MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
                DataTable dt = new DataTable();
                mda.Fill(dt);
                mySqlConnection.Close();
                foreach (DataRow item in dt.Rows)
                {
                    sum = Convert.ToDecimal(item["sum"]);
                }
                return sum;
            }
            catch (Exception)
            {
                return sum;
            }

        }



    }
}