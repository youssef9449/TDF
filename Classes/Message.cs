using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDF.Net;

namespace TDF.Classes
{
    public class Message
    {

        public int MessageID { get; set; }
        public int SenderID { get; set; }
        public int ReceiverID { get; set; }
        public string MessageText { get; set; }
        public DateTime Timestamp { get; set; }
        public int IsRead { get; set; }
        public int IsDelivered { get; set; }

        public Message()
        { 

        }

        public Message(int senderID, int receiverID, string messageText, DateTime timestamp, int isRead, int isDelivered)
        {
            SenderID = senderID;
            ReceiverID = receiverID;
            MessageText = messageText;
            Timestamp = timestamp;
            IsRead = isRead;
            IsDelivered = isDelivered;
        }

        public void add()
        {
            using (SqlConnection conn = Database.getConnection())
            {
                conn.Open();
                string query = "INSERT INTO Messages (SenderID, ReceiverID, MessageText, Timestamp, IsDelivered, IsRead) " +
                              "VALUES (@SenderID, @ReceiverID, @MessageText, @Timestamp, @IsDelivered, @IsRead); " +
                              "SELECT SCOPE_IDENTITY();";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@SenderID", SenderID);
                    cmd.Parameters.AddWithValue("@ReceiverID", ReceiverID);
                    cmd.Parameters.AddWithValue("@MessageText", MessageText);
                    cmd.Parameters.AddWithValue("@Timestamp", Timestamp);
                    cmd.Parameters.AddWithValue("@IsDelivered", IsDelivered);
                    cmd.Parameters.AddWithValue("@IsRead", IsRead);
                    MessageID = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }
    }
}
