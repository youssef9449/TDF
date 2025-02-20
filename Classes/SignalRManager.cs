using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using TDF.Net;

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

            // Handler for general notifications (MessageBox)
            HubProxy.On<string>("receiveGeneralNotification", message =>
            {
                MessageBox.Show(message, "New Notification");
            });

            // Handler for pending messages (UI updates in mainFormNewUI)
            HubProxy.On<int, string>("receivePendingMessage", (senderId, message) =>
            {
                var mainFormNewUI = Application.OpenForms.OfType<mainFormNewUI>().FirstOrDefault();
                if (mainFormNewUI != null && !mainFormNewUI.IsChatOpen(senderId))
                {
                    mainFormNewUI.BeginInvoke(new Action(async () =>
                    {
                        await mainFormNewUI.ShowMessageBalloons(senderId, null, new List<string> { message });
                        mainFormNewUI.UpdateMessageCounter(senderId, 1);
                    }));
                }
            });

            // Subscribe to updateUserList
            HubProxy.On("updateUserList", () =>
            {
                Console.WriteLine("User list update event received from SignalR.");
            });

            // Update message counts
            HubProxy.On<int, int, int>("updateMessageCounts", (receiverId, senderId, count) =>
            {
                var mainFormNewUI = Application.OpenForms.OfType<mainFormNewUI>().FirstOrDefault();
                mainFormNewUI?.BeginInvoke(new Action(() =>
                    mainFormNewUI.UpdateMessageCounter(senderId, count)));
            });

            try
            {
                await Connection.Start();
                await HubProxy.Invoke("RegisterUserConnection", currentUserID);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error connecting to the Server; you won't receive messages nor notifications");
            }
        }
    }
}