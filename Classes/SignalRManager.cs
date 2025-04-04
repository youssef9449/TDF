﻿using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Threading.Tasks;
using System.Windows.Forms;
using TDF.Forms;
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
            HubProxy.On<int, string>("receiveGeneralNotification", (senderId, message) =>
            {
                Console.WriteLine($"Received general notification from {senderId}: {message}");

                if (!string.IsNullOrEmpty(message))
                {
                    var mainForm = Application.OpenForms.OfType<mainFormNewUI>().FirstOrDefault();
                    if (mainForm != null && !mainForm.IsDisposed && mainForm.IsHandleCreated)
                    {
                        mainForm.BeginInvoke(new Action(() =>
                        {
                            // Skip if this is the sender's own broadcast
                            if (loginForm.loggedInUser != null && loginForm.loggedInUser.userID == senderId)
                            {
                                //Console.WriteLine($"Skipping notification for sender {senderId}");
                                return;
                            }
                            mainFormNewUI.playSound("Notification");

                            // Show MessageBox after sound completes
                            MessageBox.Show(message, "Announcement", MessageBoxButtons.OK);
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
                else if (mainFormNewUI != null && !mainFormNewUI.isChatOpen(senderId))
                {
                    mainFormNewUI.BeginInvoke(new Action(async () =>
                    {
                        await mainFormNewUI.showMessageBalloons(senderId, null, new List<string> { message });
                        mainFormNewUI.updateMessageCounter(senderId, 1);
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
                        // Full refresh - only used when new users are added
                        mainForm.DisplayConnectedUsersAsync();
                    }));
                }
            });

            HubProxy.On<int, bool>("updateUserStatus", (userId, isConnected) =>
            {
                var mainForm = Application.OpenForms.OfType<mainFormNewUI>().FirstOrDefault();
                if (mainForm != null && !mainForm.IsDisposed && mainForm.IsHandleCreated)
                {
                    mainForm.BeginInvoke(new Action(() =>
                    {
                        // Just update the status of a single user
                        mainForm.UpdateUserConnectionStatus(userId, isConnected);
                    }));
                }
            });

            // Update message counts
            HubProxy.On<int, int, int>("updateMessageCounts", (receiverId, senderId, count) =>
            {
                var mainFormNewUI = Application.OpenForms.OfType<mainFormNewUI>().FirstOrDefault();
                mainFormNewUI?.BeginInvoke(new Action(() =>
                    mainFormNewUI.updateMessageCounter(senderId, count)));
            });

            HubProxy.On<int, string, string, string, string>("AddNewRequest", (requestId, userFullName, requestType, requestFromDay, requestStatus) =>
            {
                var requestsForm = Application.OpenForms.OfType<requestsForm>().FirstOrDefault();
                if (requestsForm != null && !requestsForm.IsDisposed && requestsForm.IsHandleCreated)
                {
                    requestsForm.BeginInvoke(new Action(() => requestsForm.AddRequestRow(requestId, userFullName, requestType, requestFromDay, requestStatus)));
                }
            });

            HubProxy.On("RefreshNotifications", (int requesterId) =>
            {
                if (!mainForm.hasHRRole && !mainForm.hasManagerRole)
                {
                    return; // Skip listener setup for non-HR/Manager users
                }

                // Skip notification if the current user is the requester
                if (loginForm.loggedInUser.userID == requesterId)
                {
                    return;
                }

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

            // Listener for incoming global chat messages
            HubProxy.On<string, int, string>("receiveGlobalChatMessage", (messageId, senderId, message) =>
            {
                var globalChatForms = Application.OpenForms.OfType<globalChatForm>().ToList();
                foreach (var form in globalChatForms)
                {
                    if (!form.IsDisposed && form.IsHandleCreated)
                    {
                        form.BeginInvoke(new Action(() =>
                        {
                            form.AppendGlobalChatMessage(senderId, message, messageId);
                        }));
                    }
                }
            });
            //HubProxy.On<string, int, string>("receiveDepartmentChatMessage", (messageId, senderId, message) =>
            //{
            //    var globalChatForm = Application.OpenForms.OfType<globalChatForm>().FirstOrDefault();
            //    if (globalChatForm != null && !globalChatForm.IsDisposed && globalChatForm.IsHandleCreated)
            //    {
            //        globalChatForm.BeginInvoke(new Action(() =>
            //        {
            //            globalChatForm.AppendDepartmentChatMessage(senderId, message, messageId, "Department");
            //        }));
            //    }
            //});

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