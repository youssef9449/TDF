using System;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;

public static class NetworkUtils
{
    // Custom Form to simulate a message box
    private class ScanningForm : Form
    {
        public ScanningForm()
        {
            Text = "Scanning";
            Label label = new Label
            {
                Text = "Scanning for SQL Server, this might take a while ...",
                AutoSize = true,
                Location = new System.Drawing.Point(50, 50)
            };
            Controls.Add(label);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            StartPosition = FormStartPosition.CenterScreen;
            Size = new System.Drawing.Size(250, 150);
            BackColor = System.Drawing.Color.White;
            TopMost = true;
            //ShowInTaskbar = false;
            MaximizeBox = false;
        }

        public void CloseForm()
        {
            Invoke(new Action(() => Close()));
        }
    }

    public static string findSqlServerIp(int startOctet, int endOctet, int port)
    {
        // Create and show the custom scanning form in a separate thread
        ScanningForm scanningForm = new ScanningForm();
        Thread messageThread = new Thread(() =>
        {
            scanningForm.ShowDialog();
        });
        messageThread.Start();

        // Define the IP address range to scan
        string ipRangeStart = $"192.168.1.{startOctet}";

        // Iterate through the IP addresses in the range
        for (int i = startOctet; i <= endOctet; i++)
        {
            string ipAddress = $"192.168.1.{i}";

            try
            {
                // Create a TCP client to attempt a connection
                using (TcpClient client = new TcpClient())
                {
                    client.Connect(ipAddress, port);
                    // Close the scanning form if the SQL server is found
                    scanningForm.CloseForm();
                    return ipAddress;
                }
            }
            catch (Exception)
            {
                // Ignore any exceptions (e.g., connection refused)
            }
        }

        // If no SQL Server instance is found, close the scanning form and show a message
        scanningForm.CloseForm();
        MessageBox.Show("SQL Server not found on the scanned network.", "Scan Result", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        return null;
    }
}
