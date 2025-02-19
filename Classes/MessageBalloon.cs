using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TDF.Classes
{
    public class MessageBalloon : Form
    {
        public MessageBalloon(Point anchorPoint, string message)
        {
            Size = new Size(200, 60);
            FormBorderStyle = FormBorderStyle.None;
            StartPosition = FormStartPosition.Manual;
            Location = new Point(anchorPoint.X - Width - 10, anchorPoint.Y);
            BackColor = Color.LightBlue;
            TopMost = true;
            ShowInTaskbar = false;

            var label = new Label
            {
                Text = message,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter
            };
            Controls.Add(label);

            var timer = new Timer { Interval = 5000 };
            timer.Tick += (s, e) => { timer.Stop(); Close(); };
            timer.Start();
        }

    }
}
