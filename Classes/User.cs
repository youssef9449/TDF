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

        public User()
        {

        }

        public User(string userName, string fullName, string passwordHash, string salt, string role, Image picture, string department)
        {
            UserName = userName;
            FullName = fullName;
            PasswordHash = passwordHash;
            Salt = salt;
            Role = role;
            Picture = picture;
            Department = department;
        }

        public void add()
        {
            using (SqlConnection conn = Database.GetConnection())
            {
                conn.Open();
                string query = "INSERT INTO Users (UserName, PasswordHash, Salt, FullName, Role, Department) VALUES (@UserName, @PasswordHash, @Salt, @FullName, @Role, @Department)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserName", UserName);
                    cmd.Parameters.AddWithValue("@PasswordHash", PasswordHash);
                    cmd.Parameters.AddWithValue("@Salt", Salt);
                    cmd.Parameters.AddWithValue("@FullName", FullName);
                    cmd.Parameters.AddWithValue("@Role", Role);
                    cmd.Parameters.AddWithValue("@Department", Department);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        /* public void Update(string newPasswordHash)
         {
             using (SqlConnection conn = Database.GetConnection())
             {
                 conn.Open();
                 string query = "UPDATE Users SET PasswordHash = @PasswordHash, Salt = @Salt WHERE UserName = @UserName";
                 using (SqlCommand cmd = new SqlCommand(query, conn))
                 {
                     cmd.Parameters.AddWithValue("@PasswordHash", newPasswordHash);
                     cmd.Parameters.AddWithValue("@Salt", Salt);
                     cmd.Parameters.AddWithValue("@UserName", UserName);
                     cmd.ExecuteNonQuery();
                 }
             }
         }*/
    }
}
