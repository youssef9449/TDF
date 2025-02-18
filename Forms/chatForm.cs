using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using static TDF.Net.loginForm;
using static TDF.Net.Program;
using static TDF.Net.mainForm;
using TDF.Net.Classes;
using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;

namespace TDF.Net.Forms
{
    public partial class chatForm : Form
    {
        private int currentUserID;
        private int chatWithUserID;
        private Timer refreshTimer;

        public chatForm(int currentUserID, int chatWithUserID, string chatWithUserName, Image chatWithUserImage)
        {
            InitializeComponent();
            this.currentUserID = currentUserID;
            this.chatWithUserID = chatWithUserID;

            LoadMessagesAsync();

            Text = chatWithUserName;
            usernameLabel.Text = chatWithUserName;

            // Convert the Image to an Icon if it is not null
            if (chatWithUserImage != null)
            {
                // Ensure the image is a Bitmap
                Bitmap bmp = new Bitmap(chatWithUserImage);
                // Create an icon from the bitmap handle
                Icon icon = Icon.FromHandle(bmp.GetHicon());
                Icon = icon;
                // Optionally, you may want to release the handle later if creating many icons.
            }
            else
            {
                return;
            }
        }

        #region Events
        private void chatForm_Load(object sender, EventArgs e)
        {
            applyTheme(this);
            refreshTimer = new Timer();
            refreshTimer.Interval = 2000; // Refresh every 2 seconds
            refreshTimer.Tick += async (s, args) => await LoadMessagesAsync();
            refreshTimer.Start();
            BeginInvoke((Action)(() => scrollToBottomFunc()));

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

            // Get the form's scroll position
            Point scrollPos = AutoScrollPosition;

            // Adjust for scroll position when drawing the border
            Rectangle rect = new Rectangle(ClientRectangle.X - scrollPos.X, ClientRectangle.Y - scrollPos.Y, ClientRectangle.Width, ClientRectangle.Height);

            ControlPaint.DrawBorder(e.Graphics, rect, ThemeColor.darkColor, ButtonBorderStyle.Solid);
        }
        private void panel_Paint(object sender, PaintEventArgs e)
        {
            base.OnPaint(e);

            // Get the form's scroll position
            Point scrollPos = AutoScrollPosition;

            // Adjust for scroll position when drawing the border
            Rectangle rect = new Rectangle(ClientRectangle.X - scrollPos.X, ClientRectangle.Y - scrollPos.Y, ClientRectangle.Width, ClientRectangle.Height);

            ControlPaint.DrawBorder(e.Graphics, rect, ThemeColor.darkColor, ButtonBorderStyle.Solid);
        }
        private void messageTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                beginSendingMessageAsync();
                e.Handled = true;  // This prevents the "ding" sound that occurs when pressing Enter
            }
        }
        #endregion

        #region Mehods
        private async Task beginSendingMessageAsync()
        {
            if (string.IsNullOrWhiteSpace(messageTextBox.Text)) return;

            await sendMessageAsync(chatWithUserID, messageTextBox.Text); // Make sendMessage async
            messageTextBox.Clear();
        }
        private async Task LoadMessagesAsync(bool scrollToBottom = false)
        {
            messagesListBox.BeginUpdate();
            int currentTopIndex = messagesListBox.TopIndex;
            int currentTopItemHeight = 0;
            if (messagesListBox.Items.Count > 0 && currentTopIndex >= 0 && currentTopIndex < messagesListBox.Items.Count)
            {
                currentTopItemHeight = messagesListBox.GetItemHeight(currentTopIndex);
            }
            try
            {
                messagesListBox.Items.Clear();

                using (SqlConnection conn = Database.getConnection())  // *** Database query code restored ***
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
                                string sender = (int)reader["SenderID"] == currentUserID ? "You" : Text; 
                                string message = reader["MessageText"].ToString();
                                DateTime timestamp = (DateTime)reader["Timestamp"];
                                messagesListBox.Items.Add($"{sender}: {message} ({timestamp:T})");
                            }
                        }
                    }
                } // *** End of restored database query code ***

                if (scrollToBottom)
                {
                    scrollToBottomFunc();
                }
                else if (messagesListBox.Items.Count > 0 && currentTopIndex >= 0 && currentTopIndex < messagesListBox.Items.Count)
                {
                    messagesListBox.TopIndex = Math.Min(currentTopIndex, messagesListBox.Items.Count - 1); //Prevent index out of range exception.
                }
                else if (messagesListBox.Items.Count > 0)
                {
                    messagesListBox.TopIndex = 0; // if no messages were present before and now there are, scroll to the top.
                }

            }
            finally
            {
                messagesListBox.EndUpdate();
            }
        }
        private async Task sendMessageAsync(int receiverID, string messageText)
        {

            TDF.Classes.Message message = new TDF.Classes.Message()
            {
                SenderID = loggedInUser.userID,
                ReceiverID = receiverID,
                MessageText = messageText,
            };

            message.add();

            await LoadMessagesAsync(true); // Pass a flag to indicate scrolling
        }
        private void scrollToBottomFunc()
        {
            if (messagesListBox.Items.Count > 0)
            {
                messagesListBox.TopIndex = messagesListBox.Items.Count - 1;
            }
            messageTextBox.Focus();
        }
        #endregion

        private void sendButton_Click(object sender, EventArgs e)
        {
            beginSendingMessageAsync();
        }

    }
}
