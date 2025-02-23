using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

public class SpeechBubbleControl : UserControl
{
    private string message;
    private int tailWidth = 10;
    private int tailHeight = 15;
    private Timer closeTimer; // Field for the timer

    public SpeechBubbleControl(Point position, string message)
    {
        this.message = message;
        InitializeControl(position);
    }

    private void InitializeControl(Point position)
    {
        // Set basic properties for a lightweight control
        BackColor = Color.Transparent;
        Size = CalculateSize(); // Dynamically size based on message
        Location = position;
        BorderStyle = BorderStyle.None; // Use BorderStyle for UserControl
        TabStop = false; // Prevent focus
        AutoScaleMode = AutoScaleMode.Font; // Set AutoScaleMode explicitly for UserControl
        AutoSize = false;

        // Set region for non-rectangular transparency
        UpdateRegion();

        // Auto-close after 5 seconds
        closeTimer = new Timer { Interval = 5000 }; // 5 seconds
        closeTimer.Tick += (s, e) =>
        {
            closeTimer.Stop();
            Dispose(); // Clean up the control
        };
        closeTimer.Start();
    }

    private Size CalculateSize()
    {
        using (var g = CreateGraphics())
        {
            SizeF textSize = g.MeasureString(message, Font);
            // Add padding and space for the tail (20px for tail, 10px padding)
            return new Size((int)textSize.Width + 40, (int)textSize.Height + 20);
        }
    }

    private void UpdateRegion()
    {
        using (GraphicsPath path = new GraphicsPath())
        {
            // Define the bubble rectangle (padding for text and tail)
            Rectangle rect = new Rectangle(10, 5, Width - 30, Height - 15); // Adjust for tail and padding

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

            Region = new Region(path); // Set the region for transparency
        }
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        DrawSpeechBubble(e.Graphics);
    }

    private void DrawSpeechBubble(Graphics g)
    {
        using (GraphicsPath path = new GraphicsPath())
        {
            // Define the bubble rectangle (padding for text and tail)
            Rectangle rect = new Rectangle(10, 5, Width - 30, Height - 15); // Adjust for tail and padding

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

    protected override void OnSizeChanged(EventArgs e)
    {
        base.OnSizeChanged(e);
        UpdateRegion(); // Update region when size changes
    }

    // Override Dispose to handle custom resources (Timer and Region)
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            // Dispose of managed resources
            closeTimer?.Dispose();
            Region?.Dispose(); // Dispose of the Region if it exists
            // No components field needed since we don't use the designer
        }
        base.Dispose(disposing);
    }
}