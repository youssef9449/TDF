using System;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;
using TDF.Net.Classes;

namespace TDF.Net
{
    public static class Database
    {
        private static string connectionString;

        static Database()
        {
            connectionString = buildConnectionString();
        }

        private const string ConfigFileName = "config.ini";
        static string exeDirectory = AppDomain.CurrentDomain.BaseDirectory;
        static string iniFilePath = Path.Combine(exeDirectory, ConfigFileName);
        static IniFile iniFile = new IniFile(iniFilePath);

        static int startIP = 1;
        static int endIP = 255;
        static int Port = 1433;
        public static string serverIPAddress;


        public static string buildConnectionString()
        {
            // Read connection method from the config file
            string connectionMethod = iniFile.Read("Database", "ConnectionMethod", "").ToLower();
            if (string.IsNullOrWhiteSpace(connectionMethod) || (connectionMethod != "namedpipes" && connectionMethod != "tcp"))
            {
                MessageBox.Show("Connection method is missing or invalid in the config file. Please specify 'NamedPipes' or 'TCP'.",
                                "Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            // Check for server IP
            string serverIP = iniFile.Read("Database", "ServerIP", "");
            if (string.IsNullOrWhiteSpace(serverIP))
            {
                MessageBox.Show("Server IP is missing or invalid in the config file.",
                                "Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            serverIPAddress = serverIP;
            // Check for database name
            string databaseName = iniFile.Read("Database", "Database", "");
            if (string.IsNullOrWhiteSpace(databaseName))
            {
                MessageBox.Show("Database name is missing or invalid in the config file.",
                                "Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            // Check for trusted connection
            string trustedConnection = iniFile.Read("Database", "Trusted Connection", "").ToLower();
            if (string.IsNullOrWhiteSpace(trustedConnection) || (trustedConnection != "true" && trustedConnection != "false"))
            {
                MessageBox.Show("Trusted_Connection is missing or invalid in the config file. Please specify 'true' or 'false'.",
                                "Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            // Initialize connection string
            string connectionString = null;

            if (trustedConnection == "true")
            {
                if (connectionMethod == "namedpipes")
                {
                    connectionString = $"Data Source={serverIP};Initial Catalog={databaseName};Trusted_Connection=True;";
                }
                else if (connectionMethod == "tcp")
                {
                    string port = iniFile.Read("Database", "Port", "1433"); // Default port is 1433
                    connectionString = $"Data Source={serverIP},{port};Network Library=DBMSSOCN;Initial Catalog={databaseName};Trusted_Connection=True;";
                }
            }
            else
            {
                string userId = iniFile.Read("Database", "User Id", "");
                string password = iniFile.Read("Database", "Password", "");

                if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(password))
                {
                    MessageBox.Show("User ID or Password is missing or invalid in the config file.",
                                    "Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }

                if (connectionMethod == "namedpipes")
                {
                    connectionString = $"Data Source={serverIP};Initial Catalog={databaseName};User Id={userId};Password={password};";
                }
                else if (connectionMethod == "tcp")
                {
                    string port = iniFile.Read("Database", "Port", "1433"); // Default port is 1433
                    connectionString = $"Data Source={serverIP},{port};Network Library=DBMSSOCN;Initial Catalog={databaseName};User Id={userId};Password={password};";
                }
            }

            return connectionString;
        }
        public static void ensureConfigFileExists()
        {
            string configFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ConfigFileName);

            if (!File.Exists(configFilePath))
            {
                MessageBox.Show($"Configuration file '{ConfigFileName}' was not found, click 'OK' to start creating it.",
                 "Config File not found", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Create the configuration file with default values
                using (StreamWriter writer = new StreamWriter(configFilePath))
                {
                    writer.WriteLine("[Database]");
                    writer.WriteLine("ServerIP=" + NetworkUtils.findSqlServerIp(startIP, endIP, Port)); // Use the found IP address
                    writer.WriteLine("Database=Users");
                    writer.WriteLine("Trusted Connection=true");
                    writer.WriteLine("ConnectionMethod=namedpipes"); // Options: NamedPipes, TCP
                    writer.WriteLine("Port=1433"); // Used only for TCP
                    writer.WriteLine("User Id=admin"); // Used only if Trusted Connection=false
                    writer.WriteLine("Password=123"); // Used only if Trusted Connection=false
                }

                MessageBox.Show($"Configuration file '{ConfigFileName}' has been created with default values.",
                                 "Config File Created", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        public static SqlConnection getConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}
