using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using TDF.Net;
using TDF.Net.Classes;
using static TDF.Net.Program;

namespace TDF.Forms
{
    public partial class globalChatForm : Form
    {
        private HashSet<string> displayedMessageIds = new HashSet<string>();

        public globalChatForm()
        {
            InitializeComponent();

        }


        private async void globalChatForm_Load(object sender, EventArgs e)
        {
            applyTheme(this);

            await LoadChatHistoryBasedOnChannel();
        }

        public void AppendGlobalChatMessage(int senderId, string message, string messageId, bool isLocal = false)
        {
            AppendChatMessage(senderId, message, messageId, "Global Chat", isLocal);
        }

        //public void AppendDepartmentChatMessage(int senderId, string message, string messageId, string department, bool isLocal = false)
        //{
        //    AppendChatMessage(senderId, message, messageId, department, isLocal);
        //}

        private void AppendChatMessage(int senderId, string message, string messageId, string channel, bool isLocal = false)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() => AppendChatMessage(senderId, message, messageId, channel, isLocal)));
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

        private async Task LoadChatHistoryBasedOnChannel()
        {
                await LoadGlobalChatHistory();
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
                                string formattedMessage = $"[Global Chat] {senderName}: {message}\n";
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
                MessageBox.Show($"Failed to load global chat history: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //private async Task LoadDepartmentChatHistory(string department)
        //{
        //    try
        //    {
        //        using (SqlConnection conn = Database.getConnection())
        //        {
        //            await conn.OpenAsync();
        //            string query = "SELECT MessageID, SenderID, Message, Timestamp FROM DepartmentChatMessages WHERE Department = @Department ORDER BY Timestamp ASC";
        //            using (SqlCommand cmd = new SqlCommand(query, conn))
        //            {
        //                cmd.Parameters.AddWithValue("@Department", department);
        //                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
        //                {
        //                    while (await reader.ReadAsync())
        //                    {
        //                        string messageId = reader.GetString(0);
        //                        int senderId = reader.GetInt32(1);
        //                        string message = reader.GetString(2);
        //                        DateTime timestamp = reader.GetDateTime(3);
        //                        string senderName = GetUserNameById(senderId);
        //                        string formattedMessage = $"[{department}] {senderName}: {message}\n";
        //                        globalChatDisplay.AppendText(formattedMessage);
        //                        displayedMessageIds.Add(messageId);
        //                    }
        //                }
        //            }
        //        }
        //        globalChatDisplay.ScrollToCaret();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Failed to load department chat history: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}

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

        //private string GetUserDepartment(int userId)
        //{
        //    using (SqlConnection conn = Database.getConnection())
        //    {
        //        conn.Open();
        //        string query = "SELECT Department FROM Users WHERE UserID = @UserID";
        //        using (SqlCommand cmd = new SqlCommand(query, conn))
        //        {
        //            cmd.Parameters.AddWithValue("@UserID", userId);
        //            object result = cmd.ExecuteScalar();
        //            return result?.ToString() ?? "";
        //        }
        //    }
        //}

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // Get the form's scroll position
            Point scrollPos = AutoScrollPosition;

            // Adjust for scroll position when drawing the border
            Rectangle rect = new Rectangle(ClientRectangle.X - scrollPos.X, ClientRectangle.Y - scrollPos.Y, ClientRectangle.Width, ClientRectangle.Height);

            ControlPaint.DrawBorder(e.Graphics, rect, ThemeColor.darkColor, ButtonBorderStyle.Solid);
        }

        private void panel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 1)
            {
                mainForm.ReleaseCapture();
                mainForm.SendMessage(Handle, 0x112, 0xf012, 0);
            }
        }
        private void panel_Paint(object sender, PaintEventArgs e)
        {
            base.OnPaint(e);
            ControlPaint.DrawBorder(e.Graphics, ClientRectangle, ThemeColor.darkColor, ButtonBorderStyle.Solid);
        }

        private void globalChatSendButton_Click(object sender, EventArgs e)
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
    }
}