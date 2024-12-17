using System;
using System.Windows.Forms;
using TDF.Classes;
using TDF.Net;

namespace TDF.Forms
{
    public partial class teamForm : Form
    {
        public teamForm()
        {
            InitializeComponent();
        }

        #region Events
        private void teamForm_Load(object sender, EventArgs e)
        {
            Program.loadForm(this);
        }
        private void panel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 1)
            {
                mainForm.ReleaseCapture();
                mainForm.SendMessage(Handle, 0x112, 0xf012, 0);
            }
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            ControlPaint.DrawBorder(e.Graphics, ClientRectangle, ThemeColor.SecondaryColor, ButtonBorderStyle.Solid);
        }
        private void panel_Paint(object sender, PaintEventArgs e)
        {
            base.OnPaint(e);
            ControlPaint.DrawBorder(e.Graphics, ClientRectangle, ThemeColor.SecondaryColor, ButtonBorderStyle.Solid);
        }
        #endregion

    }
}
