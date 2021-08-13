using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Capstone.Models;


namespace Capstone.DAO
{
    public class PotholeSqlDao : IPotholeDao
    {
        private readonly string connectionString;

        public PotholeSqlDao(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public bool AddPothole(Pothole pothole)
        {
            bool addSuccessful = false;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string addNewPotholeSqlStatement = "INSERT INTO potholes (latitude, longitude, image_link, reported_date, reporting_user_id, repair_status) " +
                                                        "VALUES(@latitude, @longitude, @image_link, @reported_date, @reporting_user_id, @repair_status);";

                    SqlCommand cmd = new SqlCommand(addNewPotholeSqlStatement, conn);
                    cmd.Parameters.AddWithValue("@latitude", pothole.Latitude);
                    cmd.Parameters.AddWithValue("@longitude", pothole.Longitude);
                    if (string.IsNullOrEmpty(pothole.ImageLink))
                    {
                        cmd.Parameters.AddWithValue("@image_link", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@image_link", pothole.ImageLink);
                    }
                    if (pothole.ReportedDate == DateTime.Parse("01/01/0001 00:00:00"))
                    {
                        cmd.Parameters.AddWithValue("@reported_date", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@reported_date", pothole.ReportedDate);
                    }

                    cmd.Parameters.AddWithValue("@reporting_user_id", pothole.ReportingUserId);
                    cmd.Parameters.AddWithValue("@repair_status", pothole.RepairStatus);

                    cmd.ExecuteNonQuery();
                    addSuccessful = true;
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }

            return addSuccessful;
        }
        public List<Pothole> ListPotholes()
        {
            List<Pothole> allPotholes = new List<Pothole>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string getAllPotholesSqlStatement = "SELECT pothole_id, latitude, longitude, image_link, reported_date, reporting_user_id, inspected_date, repaired_date, repair_status, severity FROM potholes;";

                    SqlCommand cmd = new SqlCommand(getAllPotholesSqlStatement, conn);
                    
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Pothole pothole = GetPotholeFromReader(reader);

                        allPotholes.Add(pothole);
                    }
                }
            }
            catch (SqlException)
            {
                return null;
            }

            return allPotholes;
        }

        private Pothole GetPotholeFromReader(SqlDataReader reader)
        {
            Pothole pothole = new Pothole();
            pothole.Latitude = Convert.ToDecimal(reader["latitude"]);
            pothole.Longitude = Convert.ToDecimal(reader["longitude"]);
            if (reader["image_link"] != DBNull.Value)
            {
                pothole.ImageLink = Convert.ToString(reader["image_link"]);
            }

            pothole.ReportedDate = Convert.ToDateTime(reader["reported_date"]);
            pothole.ReportingUserId = Convert.ToInt32(reader["reporting_user_id"]);
            pothole.RepairStatus = Convert.ToString(reader["repair_status"]);
            pothole.Id = Convert.ToInt32(reader["pothole_id"]);
            if (reader["inspected_date"] != DBNull.Value)
            {
                pothole.InspectedDate = Convert.ToDateTime(reader["inspected_date"]);
            }
            if (reader["repaired_date"] != DBNull.Value)
            {
                pothole.RepairedDate = Convert.ToDateTime(reader["repaired_date"]);
            }
            if (reader["severity"] != DBNull.Value)
            {
                pothole.Severity = Convert.ToInt32(reader["severity"]);
            }

            return pothole;
        }

        public bool DeletePothole(int potholeId)
        {
            bool deleteSuccessful = false;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string deletePotholeSqlStatement = "DELETE FROM potholes WHERE pothole_id = @pothole_id;";

                    SqlCommand cmd = new SqlCommand(deletePotholeSqlStatement, conn);
                    cmd.Parameters.AddWithValue("@pothole_id", potholeId);

                    cmd.ExecuteNonQuery();

                    deleteSuccessful = true;
                }
            }
            catch (SqlException)
            {
                return deleteSuccessful;
            }

            return deleteSuccessful;
        }

        public bool UpdatePothole(Pothole pothole)
        {
            bool updateSuccessful = false;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string updatePotholeSqlStatement = "UPDATE potholes " +
                        "SET latitude = @latitude, longitude = @longitude, image_link = @image_link, reported_date = @reported_date, reporting_user_id = @reporting_user_id, inspected_date = @inspected_date, repaired_date = @repaired_date, repair_status = @repair_status, severity = @severity " +
                        "WHERE pothole_id = @pothole_id;";

                    SqlCommand cmd = new SqlCommand(updatePotholeSqlStatement, conn);
                    cmd.Parameters.AddWithValue("@pothole_id", pothole.Id);
                    cmd.Parameters.AddWithValue("@latitude", pothole.Latitude);
                    cmd.Parameters.AddWithValue("@longitude", pothole.Longitude);
                    if (string.IsNullOrEmpty(pothole.ImageLink))
                    {
                        cmd.Parameters.AddWithValue("@image_link", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@image_link", pothole.ImageLink);
                    }

                    cmd.Parameters.AddWithValue("@reported_date", pothole.ReportedDate);
                    
                    if (pothole.InspectedDate == null)
                    {
                        cmd.Parameters.AddWithValue("@inspected_date", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@inspected_date", pothole.InspectedDate);
                    }
                    if (pothole.RepairedDate == null)
                    {
                        cmd.Parameters.AddWithValue("@repaired_date", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@repaired_date", pothole.RepairedDate);
                    }

                    cmd.Parameters.AddWithValue("@reporting_user_id", pothole.ReportingUserId);
                    cmd.Parameters.AddWithValue("@repair_status", pothole.RepairStatus);
                    if (pothole.Severity == 0)
                    {
                        cmd.Parameters.AddWithValue("@severity", DBNull.Value);
                    } 
                    else
                    {
                        cmd.Parameters.AddWithValue("@severity", pothole.Severity);
                    }
                    
                    cmd.ExecuteNonQuery();

                    updateSuccessful = true;
                }
            }
            catch (SqlException e)
            {
                return updateSuccessful;
            }

            return updateSuccessful;
        }
    }
}
