using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Client;
using static TDF.Net.Program;
using static TDF.Net.mainForm;
using TDF.Net.Classes;

namespace TDF.Net.Forms
{
    public partial class chatForm : Form
    {
        private int currentUserID;
        public int chatWithUserID;
        private string chatWithUserName;
        ///private Image chatWithUserImage;

        #region Events
        public chatForm(int currentUserID, int chatWithUserID, string chatWithUserName, Image chatWithUserImage)
        {
            InitializeComponent();
            this.currentUserID = currentUserID;
            this.chatWithUserID = chatWithUserID;
            this.chatWithUserName = chatWithUserName;
            //this.chatWithUserImage = chatWithUserImage;

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
            // Optionally subscribe to chat-specific notifications
            SignalRManager.HubProxy.On<string>("receiveNotification", message =>
            {
                if (message.Contains($"New message from {chatWithUserID}:"))
                {
                    BeginInvoke(new Action(async () =>
                    {
                        await LoadMessagesAsync(true);
                    }));
                }
            });

            await LoadMessagesAsync();
            BeginInvoke((Action)(() => scrollToBottom()));
        }
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
                BeginInvoke(new Action(async () => await BeginSendingMessageAsync()));
                e.Handled = true;
            }
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
        }
        #endregion

        #region Methods
        private async Task BeginSendingMessageAsync()
        {
            if (string.IsNullOrWhiteSpace(messageTextBox.Text))
                return;

            await SendMessageAsync(chatWithUserID, messageTextBox.Text);
            messageTextBox.Clear();
        }
        private async Task LoadMessagesAsync(bool scrollToBottom = false)
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

            // Save the message to the database
            message.add();

            try
            {
                if (SignalRManager.IsConnected)
                {
                    await SignalRManager.HubProxy.Invoke("SendMessageNotification", receiverID,
                        $"New message from {currentUserID}: {messageText}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending the message: {ex.Message}");
            }

            await LoadMessagesAsync(true);
        }
        private void scrollToBottom()
        {
            if (messagesListBox.Items.Count > 0)
                messagesListBox.TopIndex = messagesListBox.Items.Count - 1;
            messageTextBox.Focus();
        }
        #endregion

        private void sendButton_Click(object sender, EventArgs e)
        {
            BeginInvoke(new Action(async () => await BeginSendingMessageAsync()));
        }
    }
}