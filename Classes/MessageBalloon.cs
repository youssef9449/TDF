using System;
using System.Drawing;
using System.Windows.Forms;

namespace TDF.Classes
{
    public class MessageBalloon : Form
    {
        public MessageBalloon(Point anchorPoint, string message)
        {
            FormBorderStyle = FormBorderStyle.None;
            StartPosition = FormStartPosition.Manual;
            BackColor = Color.LightBlue;
            TopMost = true;
            ShowInTaskbar = false;

            // Label with auto-sizing and wrapping
            var label = new Label
            {
                Text = message,
                AutoSize = true,
                MaximumSize = new Size(250, 0), // Max width, unlimited height
                Padding = new Padding(10),
                Font = new Font("Segoe UI", 10)
            };
            Controls.Add(label);

            // Size the form based on the label
            Size = new Size(label.Width + 20, label.Height + 20);

            // Position with screen boundary check
            int x = anchorPoint.X - Width - 10;
            if (x < 0) x = anchorPoint.X + 10; // Flip to right if off-screen
            int y = anchorPoint.Y;
            if (y + Height > Screen.PrimaryScreen.WorkingArea.Height)
                y = Screen.PrimaryScreen.WorkingArea.Height - Height;
            Location = new Point(x, y);

            // Timer for auto-close
            var timer = new Timer { Interval = 5000 };
            timer.Tick += (s, e) => { timer.Stop(); Close(); };
            timer.Start();

            // Click to dismiss
            Click += (s, e) => Close();
            label.Click += (s, e) => Close();
        }
    }
}