using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

public class MessageBalloon : Form
{
    private Point position;
    private string message;

    public MessageBalloon(Point position, string message)
    {
        this.position = position;
        this.message = message;
        InitializeForm();
    }

    private void InitializeForm()
    {
        // Set form properties for a lightweight, topmost balloon
        FormBorderStyle = FormBorderStyle.None;
        ShowInTaskbar = false;
        TopMost = true;
        StartPosition = FormStartPosition.Manual;
        Location = position;
        Size = CalculateSize(); // Dynamically size based on message length
        BackColor = Color.White; // Transparent background for custom drawing
        ControlBox = false; // No control box (minimize, maximize, close)

        // Ensure the form closes automatically after a duration
        var timer = new Timer { Interval = 500 }; // 5 seconds
        timer.Tick += (s, e) =>
        {
            timer.Stop();
            Close();
        };
        timer.Start();
    }

    private Size CalculateSize()
    {
        using (var g = CreateGraphics())
        {
            SizeF textSize = g.MeasureString(message, Font);
            // Add padding and space for the tail (e.g., 20px for tail, 10px padding)
            return new Size((int)textSize.Width + 40, (int)textSize.Height + 20);
        }
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        DrawSpeechBubble(e.Graphics);
    }

    private void DrawSpeechBubble(Graphics g)
    {
        // Create a graphics path for the speech bubble
        using (GraphicsPath path = new GraphicsPath())
        {
            // Define the bubble rectangle (padding for text and tail)
            Rectangle rect = new Rectangle(10, 5, Width - 30, Height - 15); // Adjust for tail and padding
            int tailWidth = 10;
            int tailHeight = 15;

            // Draw the main bubble (rounded rectangle)
            path.AddArc(rect.X, rect.Y, 20, 20, 180, 90); // Top-left corner
            path.AddArc(rect.Right - 20, rect.Y, 20, 20, 270, 90); // Top-right corner
            path.AddArc(rect.Right - 20, rect.Bottom - 20, 20, 20, 0, 90); // Bottom-right corner
            path.AddArc(rect.X, rect.Bottom - 20, 20, 20, 90, 90); // Bottom-left corner
            path.CloseFigure();

            // Add tail pointing left (triangle at the left edge)
            Point[] tailPoints = new Point[]
            {
                new Point(10, Height - 10), // Tail base left
                new Point(0, Height - tailHeight / 2), // Tail point (leftmost, middle)
                new Point(10, Height - 10) // Tail base right (mirroring left)
            };
            path.AddPolygon(tailPoints);

            // Draw the outline (black, 2px width)
            using (Pen pen = new Pen(Color.Black, 2))
            {
                g.DrawPath(pen, path);
            }

            // Draw the text inside the bubble
            using (Brush brush = new SolidBrush(Color.Black))
            {
                g.DrawString(message, Font, brush, rect.X + 5, rect.Y + 5);
            }
        }
    }

    protected override CreateParams CreateParams
    {
        get
        {
            CreateParams cp = base.CreateParams;
            cp.ExStyle |= 0x00080000; // WS_EX_LAYERED for transparency
            return cp;
        }
    }
}