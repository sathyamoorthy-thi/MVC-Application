using System;
using System.Data.SqlClient;

namespace ReimbursementClaim
{
    public class LoginUser
    {
        // Method to perform user login and role validation
        public static string GetLogin(UserCredentials user)
        {
            string username = user.username;
            string mailId = user.emailAddress;
            string password = user.password;

            // Create a connection to the database
            using (SqlConnection sqlConnection = new SqlConnection("data source=DESKTOP-LV9VD8I\\SQLEXPRESS;initial catalog=Reimbursement;trusted_connection=true"))
            {
                sqlConnection.Open();

                // Check if the user is an admin
                using (SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM AdminLogin WHERE username=@username AND mailid=@mailid AND password=@password;", sqlConnection))
                {
                    command.Parameters.AddWithValue("@username", username);
                    command.Parameters.AddWithValue("@mailid", mailId);
                    command.Parameters.AddWithValue("@password", password);
                    int adminCount = Convert.ToInt32(command.ExecuteScalar());

                    if (adminCount == 1)
                    {
                        return "AdminSuccess";  // Admin login success
                    }
                }

                // Check if the user is an employee
                using (SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM EmployeeLogin WHERE username=@username AND mailid=@mailid AND password=@password;", sqlConnection))
                {
                    command.Parameters.AddWithValue("@username", username);
                    command.Parameters.AddWithValue("@mailid", mailId);
                    command.Parameters.AddWithValue("@password", password);
                    int employeeCount = Convert.ToInt32(command.ExecuteScalar());

                    if (employeeCount == 1)
                    {
                        return "EmpSuccess";  // Employee login success
                    }
                }
            }

            return "Fails";  // Login failed for both admin and employee
        }
    }
}
