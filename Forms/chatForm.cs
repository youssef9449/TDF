using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Client;
using static TDF.Net.Program;
using static TDF.Net.mainForm;
using TDF.Net.Classes;
using Microsoft.AspNet.SignalR.Infrastructure;
using static NotificationHub;
using System.Collections.Generic;

namespace TDF.Net.Forms
{
    public partial class chatForm : Form
    {
        private int currentUserID;
        public int chatWithUserID;
        private string chatWithUserName;
        private HashSet<string> messageTexts = new HashSet<string>();

        // Helper for async BeginInvoke
        public Task InvokeAsync(Action action)
        {
            var tcs = new TaskCompletionSource<bool>();
            BeginInvoke(new Action(() =>
            {
                try
                {
                    action();
                    tcs.SetResult(true);
                }
                catch (Exception ex)
                {
                    tcs.SetException(ex);
                }
            }));
            return tcs.Task;
        }

        public chatForm(int currentUserID, int chatWithUserID, string chatWithUserName, Image chatWithUserImage)
        {
            InitializeComponent();
            this.currentUserID = currentUserID;
            this.chatWithUserID = chatWithUserID;
            this.chatWithUserName = chatWithUserName;
            Text = chatWithUserName;
            usernameLabel.Text = chatWithUserName;

            if (chatWithUserImage != null)
            {
                Bitmap bmp = new Bitmap(chatWithUserImage);
                Icon icon = Icon.FromHandle(bmp.GetHicon());
                Icon = icon;
            }
        }
        private async void chatForm_Load(object sender, EventArgs e)
        {
            applyTheme(this);
            try
            {
                if (SignalRManager.IsConnected)
                {
                    // Mark messages as delivered when chat opens
                    await SignalRManager.HubProxy.Invoke("MarkMessagesAsDelivered", chatWithUserID, currentUserID);
                }
                else
                {
                    Console.WriteLine("SignalR not connected in chatForm_Load.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error marking messages as delivered: {ex.Message}\nStackTrace: {ex.StackTrace}");
                MessageBox.Show("Failed to load chat history. Some messages may not be marked as delivered.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            await LoadMessagesAsync(true);
            BeginInvoke((Action)(() => scrollToBottom()));
        }

        public async Task LoadMessagesAsync(bool scrollToBottom = false)
        {
            messagesListBox.BeginUpdate();
            int currentTopIndex = messagesListBox.TopIndex;
            try
            {
                messagesListBox.Items.Clear();
                using (SqlConnection conn = Database.getConnection())
                {
                    string query = @"
                        SELECT SenderID, MessageText, Timestamp
                        FROM Messages
                        WHERE (SenderID = @CurrentUserID AND ReceiverID = @ChatWithUserID)
                           OR (SenderID = @ChatWithUserID AND ReceiverID = @CurrentUserID)
                        ORDER BY Timestamp ASC";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@CurrentUserID", currentUserID);
                        cmd.Parameters.AddWithValue("@ChatWithUserID", chatWithUserID);
                        await conn.OpenAsync();
                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                string sender = ((int)reader["SenderID"] == currentUserID) ? "You" : chatWithUserName;
                                string message = reader["MessageText"].ToString();
                                DateTime timestamp = (DateTime)reader["Timestamp"];
                                messagesListBox.Items.Add($"{sender}: {message} ({timestamp:T})");
                            }
                        }
                    }
                }

                if (scrollToBottom)
                    this.scrollToBottom();
                else if (messagesListBox.Items.Count > 0)
                    messagesListBox.TopIndex = Math.Min(currentTopIndex, messagesListBox.Items.Count - 1);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading messages: {ex.Message}\nStackTrace: {ex.StackTrace}");
            }
            finally
            {
                messagesListBox.EndUpdate();
            }
        }
        private async Task SendMessageAsync(int receiverID, string messageText)
        {
            TDF.Classes.Message message = new TDF.Classes.Message()
            {
                SenderID = currentUserID,
                ReceiverID = receiverID,
                MessageText = messageText,
            };
            message.add();

            // Append the message locally first to ensure the sender sees it immediately
            await AppendMessageAsync(currentUserID, messageText);

            try
            {
                if (SignalRManager.IsConnected)
                {
                    Console.WriteLine($"Sending message from {currentUserID} to {receiverID}: {messageText}");
                    await SignalRManager.HubProxy.Invoke("SendNotification", new List<int> { receiverID }, messageText, currentUserID, true, true);
                    Console.WriteLine($"Message sent to {receiverID}");
                }
                else
                {
                    Console.WriteLine("SignalR not connected. Message queued locally.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending message: {ex.Message}\nStackTrace: {ex.StackTrace}");
            }
        }

        public async Task AppendMessageAsync(int senderId, string messageText)
        {
            if (InvokeRequired)
            {
                await InvokeAsync(() => AppendMessageAsync(senderId, messageText));
                return;
            }

            messagesListBox.BeginUpdate();
            try
            {
                string senderName = (senderId == currentUserID) ? "You" : chatWithUserName;
                string fullMessage = $"{senderName}: {messageText} ({DateTime.Now:T})";
                if (messageTexts.Contains(fullMessage))
                {
                    Console.WriteLine("Duplicate message detected: " + fullMessage);
                    return;
                }
                messageTexts.Add(fullMessage);
                messagesListBox.Items.Add(fullMessage);
                scrollToBottom();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error appending message: {ex.Message}");
            }
            finally
            {
                messagesListBox.EndUpdate();
            }

            if (ActiveForm != this)
            {
                mainFormNewUI.PlaySound();
            }
        }

        private void scrollToBottom()
        {
            if (messagesListBox.Items.Count > 0)
                messagesListBox.TopIndex = messagesListBox.Items.Count - 1;
           // messageTextBox.Focus();
        }
        private async void sendButton_Click(object sender, EventArgs e)
        {
            await BeginSendingMessageAsync();
        }

        private async Task BeginSendingMessageAsync()
        {
            if (string.IsNullOrWhiteSpace(messageTextBox.Text)) return;

            string messageText = messageTextBox.Text;
            messageTextBox.Text = ""; // Clear immediately to prevent double send
            await SendMessageAsync(chatWithUserID, messageText);
        }

        #region Events
        private void usernameLabel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 1)
            {
                ReleaseCapture();
                SendMessage(Handle, 0x112, 0xf012, 0);
            }
        }
        private void panel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 1)
            {
                ReleaseCapture();
                SendMessage(Handle, 0x112, 0xf012, 0);
            }
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Point scrollPos = AutoScrollPosition;
            Rectangle rect = new Rectangle(ClientRectangle.X - scrollPos.X, ClientRectangle.Y - scrollPos.Y, ClientRectangle.Width, ClientRectangle.Height);
            ControlPaint.DrawBorder(e.Graphics, rect, ThemeColor.darkColor, ButtonBorderStyle.Solid);
        }
        private void panel_Paint(object sender, PaintEventArgs e)
        {
            base.OnPaint(e);
            Point scrollPos = AutoScrollPosition;
            Rectangle rect = new Rectangle(ClientRectangle.X - scrollPos.X, ClientRectangle.Y - scrollPos.Y, ClientRectangle.Width, ClientRectangle.Height);
            ControlPaint.DrawBorder(e.Graphics, rect, ThemeColor.darkColor, ButtonBorderStyle.Solid);
        }
        private void messageTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // Prevent double trigger
                e.Handled = true;
                BeginInvoke(new Action(async () => await BeginSendingMessageAsync()));
            }
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
        }
        #endregion
    }
}