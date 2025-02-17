using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using TDF.Net;
using static TDF.Net.loginForm;
using static TDF.Net.Program;

namespace TDF.Forms
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
            LoadMessages();
            Text = chatWithUserName;

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

            }
        }
        private void chatForm_Load(object sender, EventArgs e)
        {
            applyTheme(this);
            refreshTimer = new Timer();
            refreshTimer.Interval = 2000; // Refresh every 2 seconds
            refreshTimer.Tick += (s, args) => LoadMessages();
            refreshTimer.Start();
        }

        private void LoadMessages()
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
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string sender = (int)reader["SenderID"] == currentUserID ? "You" : "Them";
                            string message = reader["MessageText"].ToString();
                            DateTime timestamp = (DateTime)reader["Timestamp"];
                            messagesListBox.Items.Add($"{sender}: {message} ({timestamp:T})");
                        }
                    }
                }
            }
        }

        private void sendButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(messageTextBox.Text)) return;

            sendMessage(chatWithUserID, messageTextBox.Text);

            messageTextBox.Clear();
            LoadMessages(); // Refresh chat window
        }

        private void sendMessage(int receiverID, string messageText)
        {
            string query = "INSERT INTO Messages (SenderID, ReceiverID, MessageText) VALUES (@SenderID, @ReceiverID, @MessageText); SELECT SCOPE_IDENTITY();";
            int messageID;
            using (SqlConnection conn = Database.getConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@SenderID", loggedInUser.userID);
                    cmd.Parameters.AddWithValue("@ReceiverID", receiverID);
                    cmd.Parameters.AddWithValue("@MessageText", messageText);
                    messageID = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }

            // Insert notification
            string notificationQuery = "INSERT INTO Notifications (ReceiverID, SenderID, MessageID) VALUES (@ReceiverID, @SenderID, @MessageID)";
            using (SqlConnection conn = Database.getConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(notificationQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@ReceiverID", receiverID);
                    cmd.Parameters.AddWithValue("@SenderID", loggedInUser.userID);
                    cmd.Parameters.AddWithValue("@MessageID", messageID);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
