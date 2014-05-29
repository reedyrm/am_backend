using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlServerCe;
using System.Diagnostics;
using System.Linq;
using System.Web;
using AM_Backend.Models;

namespace AM_Backend.DataService
{
    public static class UserDataService
    {
        private static readonly string CONNECTION_STRING = @"Data Source=|DataDirectory|\DB.sdf";

        public static User CreateUser(string username, string password)
        {
            SqlCeConnection con = new SqlCeConnection(CONNECTION_STRING);
            try
            {
                con.Open();
                SqlCeCommand comm = new SqlCeCommand("INSERT INTO users (username, password, salt, dateCreated) VALUES (@username, @password, @salt, @createdDate)", con);
                comm.Parameters.Add(new SqlCeParameter("@username", username));
                comm.Parameters.Add(new SqlCeParameter("@password", password));
                comm.Parameters.Add(new SqlCeParameter("@salt", String.Empty));
                comm.Parameters.Add(new SqlCeParameter("@createdDate", DateTime.UtcNow));


                int numberOfRows = comm.ExecuteNonQuery();
                if (numberOfRows > 0)
                {
                    return GetUser(username);
                }
            }
            catch (Exception ex)
            {
                Debug.Print("CreateUser Exception: " + ex);
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

            return null;
        }

        public static User GetUser(string username)
        {
            SqlCeConnection con = new SqlCeConnection(CONNECTION_STRING);
            try
            {
                con.Open();

                SqlCeCommand comm = new SqlCeCommand("SELECT id, username, password, salt, dateCreated FROM users u WHERE u.username = @username", con);
                comm.Parameters.Add(new SqlCeParameter("@username", username));


                User retrievedUser = new User();
                using (SqlCeDataReader reader = comm.ExecuteReader())
                {
                    reader.Read();

                    retrievedUser.Id = reader.GetInt64(reader.GetOrdinal("id"));
                    retrievedUser.Username = reader.GetString(reader.GetOrdinal("username"));
                    retrievedUser.Password = reader.GetString(reader.GetOrdinal("password"));
                    retrievedUser.Salt = reader.GetString(reader.GetOrdinal("salt"));
                    retrievedUser.DateCreated = reader.GetDateTime(reader.GetOrdinal("dateCreated"));

                    reader.Close();
                }

                return retrievedUser;

            }
            catch (Exception ex)
            {
                Debug.Print("GetUser Exception: " + ex);
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

            return null;
        }
    }
}