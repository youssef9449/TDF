using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using TDF.Forms;
using TDF.Net.Forms;
using static TDF.Classes.ThemeColor;
using static TDF.Net.loginForm;
using static TDF.Net.Program;

namespace TDF.Net
{
    public partial class mainForm : Form
    {
        public mainForm()
        {
            InitializeComponent();
            MaximizedBounds = Screen.FromHandle(Handle).WorkingArea;
        }

        public mainForm(loginForm loginForm)
        {
            InitializeComponent();
            MaximizedBounds = Screen.FromHandle(Handle).WorkingArea;
            //initializeCustomColorDropdown();

            applyTheme(this);
            formPanel.BackColor = Color.White;

            List<string> managerRoles = new List<string> { "Manager", "Team Leader" };

            hasManagerRole = loggedInUser.Role != null && managerRoles.Any(role =>
                 string.Equals(loggedInUser.Role, role, StringComparison.OrdinalIgnoreCase));

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
        private void initializeCustomColorDropdown()
        {
            // Set ComboBox properties
            colorDropdown.DrawMode = DrawMode.OwnerDrawFixed;
            colorDropdown.DropDownStyle = ComboBoxStyle.DropDownList;
            colorDropdown.Items.Clear();

            // Add colors from ThemeColor.ColorList
            foreach (var colorHex in colorList)
            {
                colorDropdown.Items.Add(colorHex);
            }

            // Attach DrawItem event for custom rendering
            colorDropdown.DrawItem += colorDropdown_DrawItem;
        }
        public void updateUserDataControls()
        {
            circularPictureBox.Image = loggedInUser.Picture != null ? loggedInUser.Picture : circularPictureBox.Image;

            usernameLabel.Text = $"Welcome, {loggedInUser.FullName}!";
        }
        private void uploadPictureForLoggedInUser()
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
                    saveUserPicture(loggedInUser.UserName, imageBytes);

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
        private void saveUserPicture(string username, byte[] imageBytes)
        {
            if (string.IsNullOrEmpty(username))
            {
                MessageBox.Show("No user is currently logged in.");
                return;
            }

            try
            {
                using (SqlConnection conn = Database.getConnection())
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
        private void showFormInPanel(Form form)
        {
            form.TopLevel = false; // Make it a child control rather than a top-level form
            form.Dock = DockStyle.Fill;

            formPanel.Controls.Clear();
            formPanel.Controls.Add(form);
            form.Show();

            formPanel.Controls.Add(TDFpictureBox);
            TDFpictureBox.Show();
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
            ControlPaint.DrawBorder(e.Graphics, ClientRectangle, darkColor, ButtonBorderStyle.Solid);
        }
        private void gradientPanel_Paint(object sender, PaintEventArgs e)
        {
            base.OnPaint(e);
            ControlPaint.DrawBorder(e.Graphics, panel.ClientRectangle, darkColor, ButtonBorderStyle.Solid);
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
        private void updateMenuItem_Click(object sender, EventArgs e)
        {
            uploadPictureForLoggedInUser();
        }
        private void removeMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult confirmation = MessageBox.Show(
      "Are you sure you want to remove the picture?",
      "Confirm Picture removal",
      MessageBoxButtons.YesNo,
      MessageBoxIcon.Warning);

            if (confirmation == DialogResult.Yes)
            {
                using (SqlConnection conn = Database.getConnection())
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
            updateMenuItem.Click += updateMenuItem_Click;

            // Add items to the ContextMenuStrip
            contextMenu.Items.Add(updateMenuItem);

            if (loggedInUser.Picture != null)
            {
                // Add "Remove" menu item
                ToolStripMenuItem removeMenuItem = new ToolStripMenuItem("Remove");
                removeMenuItem.Click += removeMenuItem_Click;
                contextMenu.Items.Add(removeMenuItem);
            }

            contextMenu.Show(Cursor.Position);
        }
        private void colorDropdown_DrawItem(object sender, DrawItemEventArgs e)
        {
            try
            {
                // Ensure a valid index
                if (e.Index < 0 || e.Index >= colorDropdown.Items.Count)
                    return;

                // Get the current color hex code from the dropdown item
                string colorHex = colorDropdown.Items[e.Index].ToString();

                // Get the actual color from the hex code
                Color color = ColorTranslator.FromHtml(colorHex);

                // Get the color name from the dictionary
                string colorName = colorNames.ContainsKey(colorHex) ? colorNames[colorHex] : "Unknown Color";

                // Draw the background
                e.DrawBackground();

                // Draw the color swatch
                using (Brush brush = new SolidBrush(color))
                {
                    e.Graphics.FillRectangle(brush, new Rectangle(e.Bounds.Left + 2, e.Bounds.Top + 2, 20, e.Bounds.Height - 4));
                }

                // Draw the color name (not hex code)
                using (Brush textBrush = new SolidBrush(e.ForeColor))
                {
                    e.Graphics.DrawString(colorName, e.Font, textBrush, e.Bounds.Left + 25, e.Bounds.Top + 2);
                }

                // Draw the focus rectangle if the item is selected
                e.DrawFocusRectangle();
            }
            catch (ArgumentException ex)
            {
                Debug.WriteLine($"ArgumentException: {ex.Message}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unexpected error: {ex.Message}");
            }
        }
        private void colorDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (colorDropdown.SelectedIndex >= 0)
            {
                string selectedColorHex = colorDropdown.SelectedItem.ToString();
                Color selectedColor = ColorTranslator.FromHtml(selectedColorHex);
                primaryColor = selectedColor;
                darkColor = changeColorBrightness(selectedColor, -0.3);
                lightColor = changeColorBrightness(selectedColor, +0.6);
                applyTheme(this);

                Invalidate();
                Refresh();
                formPanel.BackColor = Color.White;

            }
        }
        #endregion

        #region Buttons
        private void requestsButton_Click(object sender, EventArgs e)
        {
            showFormInPanel(new requestsForm());
        }
        private void controlPanelButton_Click(object sender, EventArgs e)
        {
            showFormInPanel(new controlPanelForm());
        }
        private void logoutButton_Click(object sender, EventArgs e)
        {
            loggedInUser = null;
            Close();
            loginForm.Show();
            /*settingsForm settingsForm = new settingsForm();
            settingsForm.ShowDialog();

            if (updatedUserData)
            {
                updateUserDataControls();
            }*/
        }
        #endregion
    }
}
