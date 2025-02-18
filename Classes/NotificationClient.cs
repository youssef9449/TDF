using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TDF.Classes
{
    public class NotificationClient
    {
        private HubConnection connection;
        private IHubProxy hubProxy;

        public NotificationClient(string serverUrl)
        {
            connection = new HubConnection(serverUrl);
            hubProxy = connection.CreateHubProxy("NotificationHub");

            // Subscribe to receive notifications
            hubProxy.On<string>("receiveNotification", message =>
            {
                // Update the UI with the message (for example, show a message box)
                MessageBox.Show(message, "New Notification");
            });

            connection.Start().Wait();
        }
    }
}
