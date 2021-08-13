using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Models;

namespace Capstone.DAO
{
    public class ClaimFormSqlDao : IClaimFormDao
    {
        private readonly string connectionString;

        public ClaimFormSqlDao(string dbConnectionString)
        {
            this.connectionString = dbConnectionString;
        }

        public List<ClaimForm> GetAllClaims()
        {
            List<ClaimForm> allClaimForms = new List<ClaimForm>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string getAllClaimsSqlStatement = "SELECT claim_id, user_id, first_name, last_name, email, phone_number, date_of_claim, date_of_incident, location_of_incident_city, location_of_incident_state, image_link, description_of_damage, claim_status FROM claims;";

                    SqlCommand cmd = new SqlCommand(getAllClaimsSqlStatement, conn);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        ClaimForm claimForm = getClaimFormFromReader(reader);

                        allClaimForms.Add(claimForm);
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
                return null;
            }

            return allClaimForms;
        }

        public bool addClaimForm(ClaimForm newClaimForm)
        {
            bool addSuccessful = false;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string addNewClaim = "INSERT INTO claims (user_id, first_name, last_name, email, phone_number, date_of_claim, date_of_incident, location_of_incident_city, location_of_incident_state, image_link, description_of_damage)" +
                                         "VALUES (@user_id, @first_name, @last_name, @email, @phone_number, @date_of_claim, @date_of_incident, @location_of_incident_city, @location_of_incident_state, @image_link, @description_of_damage)";

                    SqlCommand cmd = new SqlCommand(addNewClaim, conn);

                    cmd.Parameters.AddWithValue("@user_id", newClaimForm.UserId);
                    cmd.Parameters.AddWithValue("@first_name", newClaimForm.FirstName);
                    cmd.Parameters.AddWithValue("@last_name", newClaimForm.LastName);
                    cmd.Parameters.AddWithValue("@email", newClaimForm.Email);
                    cmd.Parameters.AddWithValue("@phone_number", newClaimForm.PhoneNumber);
                    cmd.Parameters.AddWithValue("@date_of_claim", newClaimForm.DateOfClaim);
                    cmd.Parameters.AddWithValue("@date_of_incident", newClaimForm.DateOfIncident);
                    cmd.Parameters.AddWithValue("@location_of_incident_city", newClaimForm.LocationOfIncidentCity);
                    cmd.Parameters.AddWithValue("@location_of_incident_state", newClaimForm.LocationOfIncidentState);
                    if (string.IsNullOrEmpty(newClaimForm.ImageLink))
                    {
                        cmd.Parameters.AddWithValue("@image_link", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@image_link", newClaimForm.ImageLink);
                    }
                    cmd.Parameters.AddWithValue("@description_of_damage", newClaimForm.DescriptionOfDamage);

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

        public ClaimForm getClaimFormFromReader(SqlDataReader reader)
        {
            ClaimForm f = new ClaimForm()
            {
                ClaimId = Convert.ToInt32(reader["claim_id"]),
                UserId = Convert.ToInt32(reader["user_id"]),
                FirstName = Convert.ToString(reader["first_name"]),
                LastName = Convert.ToString(reader["last_name"]),
                Email = Convert.ToString(reader["email"]),
                PhoneNumber = Convert.ToString(reader["phone_number"]),
                DateOfClaim = Convert.ToDateTime(reader["date_of_claim"]),
                DateOfIncident = Convert.ToDateTime(reader["date_of_incident"]),
                LocationOfIncidentCity = Convert.ToString(reader["location_of_incident_city"]),
                LocationOfIncidentState = Convert.ToString(reader["location_of_incident_state"]),
                ImageLink = Convert.ToString(reader["image_link"]),
                DescriptionOfDamage = Convert.ToString(reader["description_of_damage"]),
                ClaimStatus = Convert.ToString(reader["claim_status"])
            };

            return f;
        }
        public bool UpdateClaim(ClaimForm claimForm)
        {
            bool updateSuccessful = false;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string updateClaimSqlStatement = "UPDATE claims " +
                        "SET claim_status = @claim_status " +
                        "WHERE claim_id = @claim_id";

                    SqlCommand cmd = new SqlCommand(updateClaimSqlStatement, conn);
                    cmd.Parameters.AddWithValue("@claim_status", claimForm.ClaimStatus);
                    cmd.Parameters.AddWithValue("@claim_id", claimForm.ClaimId);

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
