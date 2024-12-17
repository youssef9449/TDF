using TDF.Forms;
using TDF.Net.Forms;
using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using TDF.Classes;
using System.Drawing;
using static TDF.Net.loginForm;

namespace TDF.Net
{
    public partial class mainForm : Form
    {
        public mainForm()
        {
            InitializeComponent();
            MaximizedBounds = Screen.FromHandle(Handle).WorkingArea;
            Program.loadForm(this);
            formPanel.BackColor = Color.White;
        }

        public static bool hasManagerRole = loggedInUser.Role != null && loggedInUser.Role != "User";

        //Drag Form
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        public extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        public static extern void SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);

        #region Methods
        public void updateUserDataControls()
        {
            circularPictureBox.Image = loggedInUser.Picture != null ? loggedInUser.Picture : circularPictureBox.Image;

            usernameLabel.Text = $"Welcome, {loggedInUser.FullName}!";
        }
        #endregion

        #region Events
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            ControlPaint.DrawBorder(e.Graphics, ClientRectangle, ThemeColor.SecondaryColor, ButtonBorderStyle.Solid);
        }
        private void gradientPanel_Paint(object sender, PaintEventArgs e)
        {
            base.OnPaint(e);
            ControlPaint.DrawBorder(e.Graphics, panel.ClientRectangle, ThemeColor.SecondaryColor, ButtonBorderStyle.Solid);
        }
        private void mainForm_Load(object sender, EventArgs e)
        {
            updateUserDataControls();
            myTeamButton.Visible = !string.Equals(loggedInUser.Role, "User", StringComparison.OrdinalIgnoreCase);
            usersControlButton.Visible = string.Equals(loggedInUser.Role, "Admin", StringComparison.OrdinalIgnoreCase);
        }
        private void panelTitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 1)
            {
                ReleaseCapture();
                SendMessage(Handle, 0x112, 0xf012, 0);
            }
        }
        private void closeImg_MouseEnter(object sender, EventArgs e)
        {
            closeImg.Image = Properties.Resources.close_hover;
        }
        private void closeImg_MouseLeave(object sender, EventArgs e)
        {
            closeImg.Image = Properties.Resources.close_nofocus;
        }
        private void closeImg_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void maxImage_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
                WindowState = FormWindowState.Maximized;
            else
                WindowState = FormWindowState.Normal;
        }
        private void maxImage_MouseEnter(object sender, EventArgs e)
        {
            maxImage.Image = Properties.Resources.max_hover;
        }
        private void maxImage_MouseLeave(object sender, EventArgs e)
        {
            maxImage.Image = Properties.Resources.close_nofocus;
        }
        private void maxImage_MouseDown(object sender, MouseEventArgs e)
        {
            maxImage.Image = Properties.Resources.max_press;
        }
        private void minImg_MouseEnter(object sender, EventArgs e)
        {
            minImg.Image = Properties.Resources.min_hover;
        }
        private void minImg_MouseLeave(object sender, EventArgs e)
        {
            minImg.Image = Properties.Resources.close_nofocus;
        }
        private void minImg_MouseDown(object sender, MouseEventArgs e)
        {
            minImg.Image = Properties.Resources.min_press;
        }
        private void closeImg_MouseClick(object sender, MouseEventArgs e)
        {
            Application.Exit();
        }
        private void maxImage_MouseClick(object sender, MouseEventArgs e)
        {
            WindowState = WindowState == FormWindowState.Normal ? FormWindowState.Maximized : FormWindowState.Normal;
        }
        private void minImg_MouseClick(object sender, MouseEventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }
        private void mainForm_Resize(object sender, EventArgs e)
        {
            Invalidate();
        }

        #endregion

        #region Buttons
        private void requestsButton_Click(object sender, EventArgs e)
        {
            requestsForm requestsForm = new requestsForm();
            // requestsForm.ShowDialog();
            requestsForm.TopLevel = false; // Make it a child control rather than a top-level form
            requestsForm.FormBorderStyle = FormBorderStyle.None; // Remove the border if desired
            requestsForm.Dock = DockStyle.Fill; // Fill the panel

            //formPanel.Controls.Clear(); // Optional: Clear any existing controls in the panel
            formPanel.Controls.Add(requestsForm); // Add form to panel
            TDFpictureBox.SendToBack();
            requestsForm.Show(); // Display the form

        }
        private void usersControlButton_Click(object sender, EventArgs e)
        {
            //UploadPictureForLoggedInUser();
            controlPanelForm controlPanelForm = new controlPanelForm();
            controlPanelForm.ShowDialog();
        }
        private void settingsButton_Click(object sender, EventArgs e)
        {
            settingsForm settingsForm = new settingsForm();
            settingsForm.ShowDialog();

            if (settingsForm.updatedUserData)
            {
                updateUserDataControls();
            }
        }
        private void myTeamButton_Click(object sender, EventArgs e)
        {
            teamForm teamForm = new teamForm();
            teamForm.ShowDialog();
        }
        #endregion
    }
}
