﻿using Microsoft.AspNet.SignalR.Client;
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

    /// <summary>
    /// Initializes the SignalR connection, subscribes to events, and registers the current user.
    /// </summary>
    /// <param name="serverUrl">The URL of the SignalR server (e.g., "http://localhost:8080").</param>
    /// <param name="currentUserID">The ID of the current user.</param>
    public static async Task InitializeAsync(string serverUrl, int currentUserID)
    {
        if (Connection == null)
        {
            Connection = new HubConnection(serverUrl);
            HubProxy = Connection.CreateHubProxy("NotificationHub");

            // Global handler for receiving notifications.
            HubProxy.On<string>("receiveNotification", message =>
            {
                // Ensure UI updates occur on the UI thread.
                MessageBox.Show(message, "New Notification");
            });

            // Subscribe to the updateUserList event.
            HubProxy.On("updateUserList", () =>
            {
                // For example, log or raise an event to update the UI.
                Console.WriteLine("User list update event received from SignalR.");
            });
            HubProxy.On<int, string>("ReceivePendingMessage", (senderId, message) =>
            {
                var mainFormNewUI = Application.OpenForms.OfType<mainFormNewUI>().FirstOrDefault();
                if (mainFormNewUI != null && !mainFormNewUI.IsChatOpen(senderId))
                {
                    mainFormNewUI.BeginInvoke(new Action(async () =>
                    {
                        await mainFormNewUI.ShowSequentialBalloons(senderId, new List<string> { message });
                        mainFormNewUI.UpdateMessageCounter(senderId, 1);
                    }));
                }
            });
            HubProxy.On<int, int, int>("updateMessageCounts", (receiverId, senderId, count) =>
            {
                var mainFormNewUI = Application.OpenForms.OfType<mainFormNewUI>().FirstOrDefault();
                mainFormNewUI?.BeginInvoke(new Action(() =>
                    mainFormNewUI.UpdateMessageCounter(senderId, count)));
            });
        
    
            try
            {
                await Connection.Start();
                // Register this user's connection on the server.
                await HubProxy.Invoke("RegisterUserConnection", currentUserID);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error connecting to the Server; you won't receive messages nor notifications");
                //MessageBox.Show($"Error connecting to the Server;{ex}");

            }
        }
    }
}
