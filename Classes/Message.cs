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

        public Message()
        { 

        }
        public Message(int senderID, int receiverID, string messageText, DateTime timestamp, int isRead)
        {
            SenderID = senderID;
            ReceiverID = receiverID;
            MessageText = messageText;
            Timestamp = timestamp;
            IsRead = isRead;
        }

        public void add()
        {
            string query = "INSERT INTO Messages (SenderID, ReceiverID, MessageText) VALUES (@SenderID, @ReceiverID, @MessageText); SELECT SCOPE_IDENTITY();";
            int messageID;
            using (SqlConnection conn = Database.getConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@SenderID", SenderID);
                    cmd.Parameters.AddWithValue("@ReceiverID", ReceiverID);
                    cmd.Parameters.AddWithValue("@MessageText", MessageText);
                    messageID = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }
    }
}
