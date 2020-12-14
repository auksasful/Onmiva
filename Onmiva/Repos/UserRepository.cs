﻿using MySql.Data.MySqlClient;
using Onmiva.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

namespace Onmiva.Repos
{
    public class UserRepository
    {

        public bool Register(User user)
        {
            try
            {
                string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
                MySqlConnection mySqlConnection = new MySqlConnection(conn);
                string sqlquery = @"INSERT INTO Klientai ( vardas, pavarde, el_pastas, slaptazodis, role, patvirtintas, patvirtinimo_kodas) 
            VALUES (?name, ?surname, ?email, ?password, ?role, ?mailVerified, ?activationCode);";
                MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
                mySqlCommand.Parameters.Add("?name", MySqlDbType.VarChar).Value = user.FirstName;
                mySqlCommand.Parameters.Add("?surname", MySqlDbType.VarChar).Value = user.LastName;
                mySqlCommand.Parameters.Add("?email", MySqlDbType.VarChar).Value = user.EmailID;
                mySqlCommand.Parameters.Add("?password", MySqlDbType.VarChar).Value = user.Password;
                mySqlCommand.Parameters.Add("?role", MySqlDbType.VarChar).Value = user.Role;
                mySqlCommand.Parameters.Add("?mailVerified", MySqlDbType.Byte).Value = 0;
                mySqlCommand.Parameters.Add("?activationCode", MySqlDbType.VarChar).Value = user.ActivationCode;
                //mySqlCommand.Parameters.Add("?id", MySqlDbType.Int32).Value = gamintojas.id;
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


        public string Login(UserLogin user) {
            try
            {
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"SELECT usr.slaptazodis FROM Klientai as usr WHERE usr.el_pastas = ?email;";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?email", MySqlDbType.VarChar).Value = user.EmailID;
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();

            foreach (DataRow item in dt.Rows)
            {
                string pw = Convert.ToString(item["slaptazodis"]);
                return pw;
            }
            return "";
        }
        catch (Exception)
        {
            return "";
        }


        }


        public bool CheckUserExists(User user)
        {
            try
            {
                string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
                MySqlConnection mySqlConnection = new MySqlConnection(conn);
                string sqlquery = @"SELECT COUNT(*) AS count FROM Klientai usr WHERE usr.el_pastas = ?email;";
                MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
                mySqlCommand.Parameters.Add("?email", MySqlDbType.VarChar).Value = user.EmailID;
                mySqlConnection.Open();
                MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
                DataTable dt = new DataTable();
                mda.Fill(dt);
                mySqlConnection.Close();

                foreach (DataRow item in dt.Rows)
                {
                    int count = Convert.ToInt32(item["count"]);
                    if (count == 0)
                    {
                        return false;
                    }
                    else {
                        return true;
                    }
                }
                    return true;
            }
            catch (Exception)
            {
                return true;
            }
        }


        public bool CheckEmailExists(string email)
        {
            try
            {
                string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
                MySqlConnection mySqlConnection = new MySqlConnection(conn);
                string sqlquery = @"SELECT COUNT(*) AS count FROM Klientai usr WHERE usr.el_pastas = ?email;";
                MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
                mySqlCommand.Parameters.Add("?email", MySqlDbType.VarChar).Value = email;
                mySqlConnection.Open();
                MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
                DataTable dt = new DataTable();
                mda.Fill(dt);
                mySqlConnection.Close();

                foreach (DataRow item in dt.Rows)
                {
                    int count = Convert.ToInt32(item["count"]);
                    if (count == 0)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return true;
            }
        }


        public int GetUsersCount()
        {
            try
            {
                string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
                MySqlConnection mySqlConnection = new MySqlConnection(conn);
                string sqlquery = @"SELECT COUNT(*) AS count FROM Klientai;";
                MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
                mySqlConnection.Open();
                MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
                DataTable dt = new DataTable();
                mda.Fill(dt);
                mySqlConnection.Close();

                foreach (DataRow item in dt.Rows)
                {
                    int count = Convert.ToInt32(item["count"]);
                    return count;
                }
                return 1;
            }
            catch (Exception)
            {
                return 1;
            }
        }


    }
}