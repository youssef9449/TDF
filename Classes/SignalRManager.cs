using Microsoft.AspNet.SignalR.Client;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

public static class SignalRManager
{
    public static HubConnection Connection { get; private set; }
    public static IHubProxy HubProxy { get; private set; }
    public static bool IsConnected => Connection != null && Connection.State == ConnectionState.Connected;

    public static async Task InitializeAsync(string serverUrl, int currentUserID)
    {
        if (Connection == null)
        {
            Connection = new HubConnection(serverUrl);
            HubProxy = Connection.CreateHubProxy("NotificationHub");

            // Global handler for notifications (can be re-dispatched)
            HubProxy.On<string>("receiveNotification", message =>
            {
                // For example, display a global notification message
                MessageBox.Show(message, "New Notification");
            });
            // (Optional) Subscribe to the user list update event.
            // You can also subscribe for this event in the form that displays the user list.
            HubProxy.On("updateUserList", () =>
            {
                // Raise an event or simply log here.
                // It is often better to subscribe to this event directly in the form that owns the usersPanel.
                Console.WriteLine("User list update event received.");
            });

            try
            {
                await Connection.Start();
                // Register this user's connection on the server.
                await HubProxy.Invoke("RegisterUserConnection", currentUserID);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error connecting to the Server you won't receive messages nor notifications");
              //  MessageBox.Show("Error connecting to the Server: " + ex.Message);

            }
        }
    }
}
