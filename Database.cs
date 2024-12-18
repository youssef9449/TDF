using System.Data.SqlClient;
using static TDF.Net.Program;
using System.Windows.Forms;

namespace TDF.Net
{
    public static class Database
    {
        private static string connectionString;

        static Database()
        {
            connectionString = "Server=TDF40\\SQLEXPRESS;Database=Users;Trusted_Connection=True;";
            //connectionString = "";
        }

        public static string BuildConnectionString()
        {

            // Check for missing or invalid values in the config file
            string serverIP = iniFile.Read("Database", "ServerIP", "");
            if (string.IsNullOrWhiteSpace(serverIP))
            {
                MessageBox.Show("Server IP is missing or invalid in the config file.", "Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null; // Or set a default IP: "127.0.0.1"
            }

            string databaseName = iniFile.Read("Database", "Database", "");
            if (string.IsNullOrWhiteSpace(databaseName))
            {
                MessageBox.Show("Database name is missing or invalid in the config file.", "Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            string trustedConnection = iniFile.Read("Database", "Trusted Connection", "").ToLower();
            if (string.IsNullOrWhiteSpace(trustedConnection) || (trustedConnection != "true" && trustedConnection != "false"))
            {
                MessageBox.Show("Trusted_Connection is missing or invalid in the config file. Please specify 'true' or 'false'.", "Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            // Build the connection string based on the Trusted_Connection setting
            string connectionString;
            if (trustedConnection == "true")
            {
                connectionString = $"Data Source={serverIP};Initial Catalog={databaseName};Trusted_Connection=True;";
            }
            else
            {
                string userId = iniFile.Read("Database", "User Id", "");
                string password = iniFile.Read("Database", "Password", "");

                if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(password))
                {
                    MessageBox.Show("User ID or Password is missing or invalid in the config file.", "Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }

                connectionString = $"Data Source={serverIP};Initial Catalog={databaseName};User Id={userId};Password={password};";
            }

            return connectionString;
        }

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}
