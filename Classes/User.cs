using System;
using System.Data.SqlClient;
using System.Drawing;

namespace TDF.Net.Classes
{
    public class User
    {
        public int userID { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string PasswordHash { get; set; }
        public string Salt { get; set; }
        public string Role { get; set; }
        public string Department { get; set; }
        public Image Picture { get; set; }
        public string Title { get; set; }


        public User()
        {

        }

        public User(string userName, string fullName, string passwordHash, string salt, string role, Image picture, string department, string title)
        {
            UserName = userName;
            FullName = fullName;
            PasswordHash = passwordHash;
            Salt = salt;
            Role = role;
            Picture = picture;
            Department = department;
            Title = title;
        }

        public void add()
        {
            using (SqlConnection conn = Database.GetConnection())
            {
                conn.Open();

                // Step 1: Insert into Users table and get the generated UserID
                string query = "INSERT INTO Users (UserName, PasswordHash, Salt, FullName, Role, Department, Title) " +
                               "VALUES (@UserName, @PasswordHash, @Salt, @FullName, @Role, @Department, @Title); " +
                               "SELECT SCOPE_IDENTITY();"; // Retrieve the last inserted identity (UserID)

                int userId = 0;
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserName", UserName);
                    cmd.Parameters.AddWithValue("@PasswordHash", PasswordHash);
                    cmd.Parameters.AddWithValue("@Salt", Salt);
                    cmd.Parameters.AddWithValue("@FullName", FullName);
                    cmd.Parameters.AddWithValue("@Role", Role);
                    cmd.Parameters.AddWithValue("@Department", Department);
                    cmd.Parameters.AddWithValue("@Title", Title);

                    // Execute the query and get the UserID
                    userId = Convert.ToInt32(cmd.ExecuteScalar());
                }

                // Step 2: Insert into AnnualLeave table using the UserID
                string secondQuery = "INSERT INTO AnnualLeave (UserID, FullName, Annual, CasualLeave, AnnualUsed, CasualUsed, Permissions, PermissionsUsed, UnpaidUsed) " +
                                     "VALUES (@UserID, @FullName, @Annual, @CasualLeave, @AnnualUsed, @CasualUsed, @Permissions, @PermissionsUsed, @UnpaidUsed)";

                using (SqlCommand cmd = new SqlCommand(secondQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", userId);  // Add the retrieved UserID
                    cmd.Parameters.AddWithValue("@FullName", FullName);
                    cmd.Parameters.AddWithValue("@Annual", 14); // Set default Annual leave balance
                    cmd.Parameters.AddWithValue("@CasualLeave", 7); // Set default Casual leave balance
                    cmd.Parameters.AddWithValue("@AnnualUsed", 0); // Set default Annual used
                    cmd.Parameters.AddWithValue("@CasualUsed", 0); // Set default Casual used
                    cmd.Parameters.AddWithValue("@Permissions", 2); // Set default Permissions used
                    cmd.Parameters.AddWithValue("@PermissionsUsed", 0); // Set default Permissions used
                    cmd.Parameters.AddWithValue("@UnpaidUsed", 0); // Set default Unpaid used
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
