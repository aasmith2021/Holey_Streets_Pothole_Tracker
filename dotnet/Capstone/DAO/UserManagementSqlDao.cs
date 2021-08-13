using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Capstone.Models;

namespace Capstone.DAO
{
    public class UserManagementSqlDao : IUserManagementDao
    {
        private readonly string connectionString;

        public UserManagementSqlDao(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        //method to change user role
        public bool ChangeUserRole(int userId, string newRole)
        {
            bool changeSuccessful = false;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string changeUserRoleSqlStatement = "UPDATE users " +
                        "SET user_role = @user_role " +
                        "WHERE user_id = @user_id; ";

                    SqlCommand cmd = new SqlCommand(changeUserRoleSqlStatement, conn);
                    cmd.Parameters.AddWithValue("@user_role", newRole);
                    cmd.Parameters.AddWithValue("@user_id", userId);

                    cmd.ExecuteNonQuery();
                    changeSuccessful = true;
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
                return changeSuccessful;
            }

            return changeSuccessful;
        }

        public List<ReturnUser> ListUsersByRole(string role)
        {
            List<ReturnUser> allUsers = new List<ReturnUser>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string getAllUsersByRoleSqlStatement = "SELECT user_id, username, user_role FROM users WHERE user_role = @user_role;";

                    SqlCommand cmd = new SqlCommand(getAllUsersByRoleSqlStatement, conn);
                    cmd.Parameters.AddWithValue("@user_role", role);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        ReturnUser user = new ReturnUser();

                        user.UserId = Convert.ToInt32(reader["user_id"]);
                        user.Username = Convert.ToString(reader["username"]);
                        user.Role = Convert.ToString(reader["user_role"]);

                        allUsers.Add(user);
                    }
                }
            }
            catch (SqlException)
            {
                return null;
            }

            return allUsers;
        }

        public bool RequestEmployeeAccess(ReturnUser user)
        {
            bool requestAdded = false;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string submitEmployeeAccessRequestSqlStatement = "INSERT INTO requests (user_id, active_status) VALUES (@user_id, @active_status);";
                    
                    //All new requests are set to "active" (true/1) by default
                    int defaultActiveStatus = 1;

                    SqlCommand cmd = new SqlCommand(submitEmployeeAccessRequestSqlStatement, conn);
                    cmd.Parameters.AddWithValue("@user_id", user.UserId);
                    cmd.Parameters.AddWithValue("@active_status", defaultActiveStatus);

                    cmd.ExecuteNonQuery();

                    requestAdded = true;
                }
            }
            catch (SqlException)
            {
                return requestAdded;
            }

            return requestAdded;
        }
        //if user already has an active request, they should not submit a new request
        public bool CheckIfActiveRequest(ReturnUser user)
        {
            bool currentActiveRequest = true;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    int defaultActiveStatus = 1;
                    string submitEmployeeAccessRequestSqlStatement = "SELECT request_id, user_id, active_status FROM requests WHERE user_id = @user_id AND active_status = @active_status";

                    SqlCommand cmd = new SqlCommand(submitEmployeeAccessRequestSqlStatement, conn);
                    cmd.Parameters.AddWithValue("@user_id", user.UserId);
                    cmd.Parameters.AddWithValue("@active_status", defaultActiveStatus);

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (!reader.HasRows)
                    {
                        currentActiveRequest = false;
                    }
                }
            }
            catch (SqlException)
            {
                return currentActiveRequest;
            }

            return currentActiveRequest;
        }

        public bool SetRequestStatusToInactive(int userId)
        {
            bool requestSetToInactive = false;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string setRequestStatusToInactiveSqlStatement = "UPDATE requests SET active_status = @active_status WHERE user_id = @user_id;";

                    //Sets request status to "inactive" (false/0)
                    int inactiveStatus = 0;

                    SqlCommand cmd = new SqlCommand(setRequestStatusToInactiveSqlStatement, conn);
                    cmd.Parameters.AddWithValue("@user_id", userId);
                    cmd.Parameters.AddWithValue("@active_status", inactiveStatus);

                    cmd.ExecuteNonQuery();

                    requestSetToInactive = true;
                }
            }
            catch (SqlException)
            {
                return requestSetToInactive;
            }

            return requestSetToInactive;
        }

        public List<AccessChangeRequest> ListAllActiveChangeRequests()
        {
            List<AccessChangeRequest> allActiveChangeRequests = new List<AccessChangeRequest>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string getAllActiveChangeRequestsSqlStatement = "SELECT r.request_id AS 'request_id', r.user_id AS 'user_id', r.active_status AS 'active_status', u.username AS 'username', u.user_role AS 'user_role' " +
                                                                    "FROM requests r " +
                                                                    "JOIN users u ON r.user_id = u.user_id " +
                                                                    "WHERE active_status = @active_status;";

                    //A status is active if it has a value of true/1
                    int activeStatus = 1;

                    SqlCommand cmd = new SqlCommand(getAllActiveChangeRequestsSqlStatement, conn);
                    cmd.Parameters.AddWithValue("@active_status", activeStatus);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        AccessChangeRequest request = new AccessChangeRequest();

                        request.RequestId = Convert.ToInt32(reader["request_id"]);
                        request.UserId = Convert.ToInt32(reader["user_id"]);
                        request.Username = Convert.ToString(reader["username"]);
                        request.Role = Convert.ToString(reader["user_role"]);
                        request.ActiveStatus = Convert.ToBoolean(reader["active_status"]);

                        allActiveChangeRequests.Add(request);
                    }
                }
            }
            catch (SqlException)
            {
                return null;
            }

            return allActiveChangeRequests;
        }
    }
}
