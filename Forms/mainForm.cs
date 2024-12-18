using TDF.Forms;
using TDF.Net.Forms;
using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using TDF.Classes;
using System.Drawing;
using static TDF.Net.loginForm;
using System.Data.SqlClient;
using System.IO;

namespace TDF.Net
{
    public partial class mainForm : Form
    {
        public mainForm(loginForm loginForm)
        {
            InitializeComponent();
            MaximizedBounds = Screen.FromHandle(Handle).WorkingArea;
            Program.loadForm(this);
            formPanel.BackColor = Color.White;
            hasManagerRole = loggedInUser.Role != null && (string.Equals(loggedInUser.Role, "Manager", StringComparison.OrdinalIgnoreCase) || string.Equals(loggedInUser.Role, "Team Leader", StringComparison.OrdinalIgnoreCase));
            hasAdminRole = loggedInUser.Role != null && string.Equals(loggedInUser.Role, "Admin", StringComparison.OrdinalIgnoreCase);
            this.loginForm = loginForm; // Store a reference to the login form
        }

        public static bool hasManagerRole, hasAdminRole, updatedUserData;
        private ContextMenuStrip contextMenu;
        private loginForm loginForm;

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
        private void UploadPictureForLoggedInUser()
        {
            // Use OpenFileDialog to let the user choose an image
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Select a Profile Picture";
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png";  // Supported image types

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Get the selected file path
                string selectedFilePath = openFileDialog.FileName;

                try
                {
                    // Validate file type based on extension
                    string fileExtension = Path.GetExtension(selectedFilePath).ToLower();
                    if (fileExtension != ".jpg" && fileExtension != ".jpeg" && fileExtension != ".png")
                    {
                        MessageBox.Show("Invalid file type. Please select a .jpg, .jpeg, or .png file.");
                        return;
                    }

                    // Validate file size (e.g., max 5MB)
                    FileInfo fileInfo = new FileInfo(selectedFilePath);
                    long maxFileSize = 5 * 1024 * 1024; // 5 MB
                    if (fileInfo.Length > maxFileSize)
                    {
                        MessageBox.Show("File size too large. Please select a file smaller than 5 MB.");
                        return;
                    }

                    // Convert the selected image file to a byte array
                    byte[] imageBytes = File.ReadAllBytes(selectedFilePath);

                    // Check if the image byte array is null or empty
                    if (imageBytes == null || imageBytes.Length == 0)
                    {
                        MessageBox.Show("The selected file is invalid or empty.");
                        return;
                    }

                    // Save the image to the database
                    SaveUserPicture(loggedInUser.UserName, imageBytes);

                    circularPictureBox.Image = loggedInUser.Picture;
                }
                catch (UnauthorizedAccessException ex)
                {
                    // Display more detailed information about the error
                    MessageBox.Show($"Access to the file is denied. Please choose another file or try again later.\n\nDetails: {ex.Message}", "Unauthorized Access", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                catch (IOException ex)
                {
                    // Display more detailed information about the I/O error
                    MessageBox.Show($"Error reading the file. Please ensure the file is accessible and try again.\n\nDetails: {ex.Message}", "File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                catch (Exception ex)
                {
                    // Display more detailed information about any other unexpected errors
                    MessageBox.Show($"An unexpected error occurred: {ex.Message}\n\nStack Trace:\n{ex.StackTrace}", "Unexpected Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            else
            {
                MessageBox.Show("No image selected.");
            }
        }
        private void SaveUserPicture(string username, byte[] imageBytes)
        {
            if (string.IsNullOrEmpty(username))
            {
                MessageBox.Show("No user is currently logged in.");
                return;
            }

            try
            {
                using (SqlConnection conn = Database.GetConnection())
                {
                    conn.Open();
                    string query = "UPDATE Users SET Picture = @Picture WHERE UserName = @UserName";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserName", username);
                        cmd.Parameters.AddWithValue("@Picture", imageBytes);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            if (loggedInUser != null && loggedInUser.UserName == username)
                            {
                                using (MemoryStream ms = new MemoryStream(imageBytes))
                                {
                                    loggedInUser.Picture = Image.FromStream(ms);
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("No user found with the given username.");
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("A database error occurred: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while saving the image: " + ex.Message);
            }
        }
        #endregion

        #region Events
        private void mainForm_Load(object sender, EventArgs e)
        {
            updateUserDataControls();
            //myTeamButton.Visible = !string.Equals(loggedInUser.Role, "User", StringComparison.OrdinalIgnoreCase);
            controlPanelButton.Visible = hasAdminRole;
        }
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
        private void UpdateMenuItem_Click(object sender, EventArgs e)
        {
            UploadPictureForLoggedInUser();
        }
        private void RemoveMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult confirmation = MessageBox.Show(
      "Are you sure you want to remove the picture?",
      "Confirm Picture removal",
      MessageBoxButtons.YesNo,
      MessageBoxIcon.Warning);

            if (confirmation == DialogResult.Yes)
            {
                using (SqlConnection conn = Database.GetConnection())
                {
                    conn.Open();

                    // SQL query to set the Picture column to NULL
                    string query = "UPDATE Users SET Picture = NULL WHERE UserName = @UserName";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Pass the logged-in user's UserName as a parameter
                        cmd.Parameters.AddWithValue("@UserName", loggedInUser.UserName);

                        // Execute the query
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            loggedInUser.Picture = null;
                            circularPictureBox.Image = Properties.Resources.pngegg;
                        }
                        else
                        {
                            MessageBox.Show("Failed to remove picture or user not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            else
            {
                return;
            }
        }
        private void circularPictureBox_Click(object sender, EventArgs e)
        {
            // Initialize the ContextMenuStrip
            contextMenu = new ContextMenuStrip();

            // Add "Update" menu item
            ToolStripMenuItem updateMenuItem = new ToolStripMenuItem("Update");
            updateMenuItem.Click += UpdateMenuItem_Click;

            // Add items to the ContextMenuStrip
            contextMenu.Items.Add(updateMenuItem);

            if (loggedInUser.Picture != null)
            {
                // Add "Remove" menu item
                ToolStripMenuItem removeMenuItem = new ToolStripMenuItem("Remove");
                removeMenuItem.Click += RemoveMenuItem_Click;
                contextMenu.Items.Add(removeMenuItem);
            }

            contextMenu.Show(Cursor.Position);
        }
        private void circularPictureBox_MouseEnter(object sender, EventArgs e)
        {
            circularPictureBox.Cursor = Cursors.Hand;
        }
        private void circularPictureBox_MouseLeave(object sender, EventArgs e)
        {
            circularPictureBox.Cursor = Cursors.Default; // Reset cursor to default
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
        private void controlPanelButton_Click(object sender, EventArgs e)
        {
            controlPanelForm controlPanelForm = new controlPanelForm();
            controlPanelForm.ShowDialog();
        }
        private void logoutButton_Click(object sender, EventArgs e)
        {
            Close();
            loggedInUser = null;
            loginForm.Show();

            /*settingsForm settingsForm = new settingsForm();
            settingsForm.ShowDialog();

            if (updatedUserData)
            {
                updateUserDataControls();
            }*/
        }
        private void myTeamButton_Click(object sender, EventArgs e)
        {
            teamForm teamForm = new teamForm();
            teamForm.ShowDialog();
        }
        #endregion
    }
}
