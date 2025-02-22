using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using TDF.Net;
using TDF.Net.Forms;

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

            // Handle general notifications with MessageBox
            HubProxy.On<string>("receiveGeneralNotification", message =>
            {
                Console.WriteLine($"Received general notification: {message}");
                if (!string.IsNullOrEmpty(message))
                {
                    var mainForm = Application.OpenForms.OfType<mainFormNewUI>().FirstOrDefault();
                    if (mainForm != null && !mainForm.IsDisposed && mainForm.IsHandleCreated)
                    {
                        mainForm.BeginInvoke(new Action(() =>
                        {
                            MessageBox.Show(message, "System Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }));
                    }
                }
            });

            // Handle pending chat messages
            HubProxy.On<int, string>("receivePendingMessage", (senderId, message) =>
            {
                Console.WriteLine($"SignalRManager received message from {senderId}: {message}");
                var mainFormNewUI = Application.OpenForms.OfType<mainFormNewUI>().FirstOrDefault();
                var chatForm = Application.OpenForms.OfType<chatForm>().FirstOrDefault(f => f.chatWithUserID == senderId);

                if (chatForm != null && !chatForm.IsDisposed && chatForm.IsHandleCreated)
                {
                    chatForm.BeginInvoke(new Action(async () =>
                    {
                        Console.WriteLine($"Refreshing chatForm for {chatForm.chatWithUserID}");
                        await chatForm.AppendMessageAsync(senderId, message);
                    }));
                }
                else if (mainFormNewUI != null && !mainFormNewUI.IsChatOpen(senderId))
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
                var mainForm = Application.OpenForms.OfType<mainFormNewUI>().FirstOrDefault();
                if (mainForm != null && !mainForm.IsDisposed && mainForm.IsHandleCreated)
                {
                    mainForm.BeginInvoke(new Action(() =>
                    {
                        Console.WriteLine("User list update event received from SignalR.");
                        mainForm.DisplayConnectedUsersAsync(true); // Sync call for compatibility
                    }));
                }
            });

            // Update message counts
            HubProxy.On<int, int, int>("updateMessageCounts", (receiverId, senderId, count) =>
            {
                var mainFormNewUI = Application.OpenForms.OfType<mainFormNewUI>().FirstOrDefault();
                mainFormNewUI?.BeginInvoke(new Action(() =>
                    mainFormNewUI.UpdateMessageCounter(senderId, count)));
            });

            HubProxy.On<int, string, string, string, string>("AddNewRequest", (requestId, userFullName, requestType, requestFromDay, requestStatus) =>
            {
                var requestsForm = Application.OpenForms.OfType<requestsForm>().FirstOrDefault();
                if (requestsForm != null && !requestsForm.IsDisposed && requestsForm.IsHandleCreated)
                {
                    requestsForm.BeginInvoke(new Action(() => requestsForm.AddRequestRow(requestId, userFullName, requestType, requestFromDay, requestStatus)));
                }
            });
            HubProxy.On("RefreshNotifications", () =>
            {
                var mainFormNewUI = Application.OpenForms.OfType<mainFormNewUI>().FirstOrDefault();
                if (mainFormNewUI != null && !mainFormNewUI.IsDisposed && mainFormNewUI.IsHandleCreated)
                {
                    mainFormNewUI.BeginInvoke(new Action(() => mainFormNewUI.LoadUnreadNotifications()));
                }
            });
            // Updated handler for status updates
            HubProxy.On<int, bool>("updateUserStatus", (userId, isConnected) =>
            {
                var mainForm = Application.OpenForms.OfType<mainFormNewUI>().FirstOrDefault();
                if (mainForm != null && !mainForm.IsDisposed && mainForm.IsHandleCreated)
                {
                    mainForm.BeginInvoke(new Action(() =>
                    {
                        Console.WriteLine($"Updating status for user {userId}: {isConnected}");
                        mainForm.UpdateUserStatus(userId, isConnected); // Synchronous call
                    }));
                }
            });

            try
            {
                await Connection.Start();
                await HubProxy.Invoke("RegisterUserConnection", currentUserID);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error connecting to the Server; you won't receive messages nor notifications", "Server Error");
            }
        }
    }

    public static void RegisterUser(int userId)
    {
        HubProxy.Invoke("RegisterUserConnection", userId);
    }
    // New method to reset SignalR state
    public static void ResetConnection()
    {
        if (Connection != null)
        {
            Connection.Stop();
            Connection.Dispose();
            Connection = null;
            HubProxy = null;
        }
    }
}