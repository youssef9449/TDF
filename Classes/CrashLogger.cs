using System;
using System.IO;
using System.Windows.Forms;
using TDF.Net;
using static TDF.Net.loginForm;


namespace TDF.Classes
{
    public static class CrashLogger
    {
        // This should reflect your application's logged in user.
        // Ensure that loggedInUser is accessible here, for example, by making it public static in your main form or a session manager.
        //public static string LoggedInUserFullName { get; set; } = "UnknownUser";

        public static void LogException(Exception ex)
        {
            try
            {
                // Replace spaces with underscores to avoid issues in file names.
                string userName = (loggedInUser != null ? loggedInUser.FullName : "UnknownUser").Replace(" ", "_");

                // Get the application's startup location.
                string startLocation = Application.StartupPath;
                string logsFolder = Path.Combine(startLocation, "logs");

                // Create the logs folder if it doesn't exist.
                if (!Directory.Exists(logsFolder))
                {
                    Directory.CreateDirectory(logsFolder);
                }

                // Create a timestamp string (format: YYYYMMDD_HHMMSS).
                string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");

                // Build the file name.
                string fileName = $"{userName}_{timestamp}.txt";
                string filePath = Path.Combine(logsFolder, fileName);

                // Write the exception details to the file.
                File.WriteAllText(filePath, ex.ToString());
            }
            catch (Exception loggingEx)
            {
                // If logging fails, there's not much you can do. You might want to display a MessageBox or ignore it.
                MessageBox.Show($"Failed to log exception: {loggingEx.Message}", "Logging Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
