using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using TDF.Net;

namespace TDF.Forms
{
    public partial class globalChatForm : Form
    {
        private RichTextBox globalChatDisplay;
        private TextBox globalChatInput;
        private Button globalChatSendButton;
        private HashSet<string> displayedMessageIds = new HashSet<string>();
        private ComboBox chatChannelSelector;

        public globalChatForm()
        {
            InitializeComponent();
            Load += globalChatForm_Load;

            var globalChatPanel = new Panel { Dock = DockStyle.Fill };


            globalChatDisplay = new RichTextBox
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                BackColor = Color.White,
                Font = new Font("Segoe UI", 10)
            };
            globalChatInput = new TextBox
            {
                Dock = DockStyle.Bottom,
                Height = 30,
                Font = new Font("Segoe UI", 10)
            };
            globalChatSendButton = new Button
            {
                Dock = DockStyle.Right,
                Text = "Send",
                Width = 80,
                Height = 30,
                BackColor = Color.FromArgb(33, 150, 243),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            globalChatSendButton.Click += GlobalChatSendButton_Click;

            chatChannelSelector = new ComboBox
            {
                Dock = DockStyle.Left,
                Width = 150,
                Height = 30,
                Font = new Font("Segoe UI", 10),
                Items = { "Global Chat", "Department Chat" },
                SelectedIndex = 0
            };
            chatChannelSelector.SelectedIndexChanged += ChatChannelSelector_SelectedIndexChanged;

            globalChatPanel.Controls.Add(chatChannelSelector);
            globalChatPanel.Controls.Add(globalChatDisplay);
            globalChatPanel.Controls.Add(globalChatInput);
            globalChatPanel.Controls.Add(globalChatSendButton);
            Controls.Add(globalChatPanel);
        }

        private async void globalChatForm_Load(object sender, EventArgs e)
        {
            await LoadGlobalChatHistory();
        }
        private void ChatChannelSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            globalChatDisplay.Clear();
            displayedMessageIds.Clear();
            LoadChatHistoryBasedOnChannel();
        }
        private async Task LoadChatHistoryBasedOnChannel()
        {
            if (chatChannelSelector.SelectedIndex == 0) // Global Chat
            {
                await LoadGlobalChatHistory();
            }
            else // Department Chat
            {
                string department = GetUserDepartment(loginForm.loggedInUser.userID);
                await LoadDepartmentChatHistory(department);
            }
        }
        private void GlobalChatSendButton_Click(object sender, EventArgs e)
        {
            string message = globalChatInput.Text.Trim();
            if (!string.IsNullOrEmpty(message))
            {
                try
                {
                    string messageId = Guid.NewGuid().ToString();
                    SignalRManager.HubProxy.Invoke("SendGlobalChatMessage", messageId, message, loginForm.loggedInUser.userID);
                    globalChatInput.Clear();
                    AppendGlobalChatMessage(loginForm.loggedInUser.userID, message, messageId, true);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Failed to send message: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public void AppendGlobalChatMessage(int senderId, string message, string messageId, bool isLocal = false)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() => AppendGlobalChatMessage(senderId, message, messageId, isLocal)));
                return;
            }

            if (!displayedMessageIds.Contains(messageId))
            {
                string senderName = GetUserNameById(senderId);
                string formattedMessage = $"{senderName}: {message}\n";
                globalChatDisplay.AppendText(formattedMessage);
                globalChatDisplay.ScrollToCaret();
                displayedMessageIds.Add(messageId);
            }
        }

        private async Task LoadGlobalChatHistory()
        {
            try
            {
                using (SqlConnection conn = Database.getConnection())
                {
                    await conn.OpenAsync();
                    string query = "SELECT MessageID, SenderID, Message, Timestamp FROM GlobalChatMessages ORDER BY Timestamp ASC";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                string messageId = reader.GetString(0);
                                int senderId = reader.GetInt32(1);
                                string message = reader.GetString(2);
                                DateTime timestamp = reader.GetDateTime(3);
                                string senderName = GetUserNameById(senderId);
                                string formattedMessage = $"[{timestamp:T}] {senderName}: {message}\n";
                                globalChatDisplay.AppendText(formattedMessage);
                                displayedMessageIds.Add(messageId);
                            }
                        }
                    }
                }
                globalChatDisplay.ScrollToCaret();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load chat history: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GetUserNameById(int userId)
        {
            using (SqlConnection conn = Database.getConnection())
            {
                conn.Open();
                string query = "SELECT Username FROM Users WHERE UserID = @UserID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", userId);
                    object result = cmd.ExecuteScalar();
                    return result?.ToString() ?? $"User {userId}";
                }
            }
        }
        public void AppendDepartmentChatMessage(int senderId, string message, string messageId, string channel = "Department")
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() => AppendDepartmentChatMessage(senderId, message, messageId, channel)));
                return;
            }

            if (!displayedMessageIds.Contains(messageId))
            {
                string senderName = GetUserNameById(senderId);
                string formattedMessage = $"[{channel}] {senderName}: {message}\n";
                globalChatDisplay.AppendText(formattedMessage);
                globalChatDisplay.ScrollToCaret();
                displayedMessageIds.Add(messageId);
            }
        }

        private void DepartmentChatButton_Click(object sender, EventArgs e) // Add a button for department chat
        {
            string message = globalChatInput.Text.Trim();
            if (!string.IsNullOrEmpty(message))
            {
                string messageId = Guid.NewGuid().ToString();
                string department = GetUserDepartment(loginForm.loggedInUser.userID); // Implement this method
                SignalRManager.HubProxy.Invoke("SendDepartmentChatMessage", messageId, message, loginForm.loggedInUser.userID, department);
                globalChatInput.Clear();
                AppendDepartmentChatMessage(loginForm.loggedInUser.userID, message, messageId, "Department");
            }
        }

        private string GetUserDepartment(int userId)
        {
            using (SqlConnection conn = Database.getConnection())
            {
                conn.Open();
                string query = "SELECT Department FROM Users WHERE UserID = @UserID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", userId);
                    object result = cmd.ExecuteScalar();
                    return result?.ToString() ?? "";
                }
            }
        }
        private async Task LoadDepartmentChatHistory(string department)
        {
            try
            {
                using (SqlConnection conn = Database.getConnection())
                {
                    await conn.OpenAsync();
                    string query = "SELECT MessageID, SenderID, Message, Timestamp FROM DepartmentChatMessages WHERE Department = @Department ORDER BY Timestamp ASC";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Department", department);
                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                string messageId = reader.GetString(0);
                                int senderId = reader.GetInt32(1);
                                string message = reader.GetString(2);
                                DateTime timestamp = reader.GetDateTime(3);
                                string senderName = GetUserNameById(senderId);
                                string formattedMessage = $"[{department}] {senderName}: {message}\n";
                                globalChatDisplay.AppendText(formattedMessage);
                                displayedMessageIds.Add(messageId);
                            }
                        }
                    }
                }
                globalChatDisplay.ScrollToCaret();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load department chat history: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}