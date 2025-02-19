using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDF.Classes
{
    public class PendingNotification
    {
        public int NotificationID { get; set; }
        public int SenderID { get; set; }   // New property
        public string MessageText { get; set; }
    }
}
