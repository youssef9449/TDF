using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using TDF.Classes;
using TDF.Forms;
using TDF.Net.Classes;
using TDF.Net.Forms;
using TDF.Properties;
using Bunifu.UI.WinForms;
using static TDF.Net.Classes.ThemeColor;
using static TDF.Net.loginForm;
using static TDF.Net.mainForm;
using static TDF.Net.Program;
using static TDF.Net.Database;
using Timer = System.Windows.Forms.Timer;
using Microsoft.AspNet.SignalR.Client;

namespace TDF.Net
{
    public partial class mainFormNewUI : Form
    {
        public mainFormNewUI()
        {
            InitializeComponent();
        }

        public mainFormNewUI(loginForm loginForm)
        {
            InitializeComponent();

            applyTheme(this);
            //notificationsSnackbar.InformationOptions.BackColor = Color.Black;
            //notificationsSnackbar.InformationOptions.ForeColor = Color.White;
            //notificationsSnackbar.InformationOptions.BorderColor = darkColor;
            //notificationsSnackbar.InformationOptions.ActionBackColor = lightColor;
            formPanel.BackColor = Color.White;
            this.loginForm = loginForm; // Store a reference to the login form
            startNotificationChecker();
        }

        private ContextMenuStrip contextMenu;

        private loginForm loginForm;

        private Timer connectedUsersTimer;
        private bool isPanelExpanded, isFormClosing = false;
        private int expandedHeight; // Stores the full height of the panel when expanded
        private Point usersIconLocation;
        private int contractedHeight = 50; // Height of the panel to show only the header
        Timer notificationTimer = new Timer();

        private FlowLayoutPanel flowLayout;
        private Label headerLabel;
        private int previousUserCount = -1; 
        private Point previousScrollPosition = Point.Empty;

        private HubConnection connection;
        private IHubProxy hubProxy;

        #region Events
        private void mainFormNewUI_Load(object sender, EventArgs e)
        {
            MaximizedBounds = Screen.FromControl(this).WorkingArea;

            usersIconButton.BackgroundColor = primaryColor;
            usersIconButton.BorderColor = darkColor;
            expandedHeight = usersPanel.Height; // Store the original height when expanded
            usersPanel.Height = contractedHeight; // Set the initial height to contracted
            usersIconLocation = usersIconButton.Location;
            InitializeSignalR(); 

            updateRoleStatus();
            setImageButtonVisibility();
            adjustShadowPanelAndImageButtons();
            updateUserDataControls();

            displayConnectedUsers();

            connectedUsersTimer = new Timer();
            connectedUsersTimer.Interval = 10000; // 10 seconds
            connectedUsersTimer.Tick += ConnectedUsersTimer_Tick;
            connectedUsersTimer.Start();
        }
        protected override void OnMove(EventArgs e)
        {
            base.OnMove(e);
            MaximizedBounds = Screen.FromControl(this).WorkingArea;
        }
        private void ConnectedUsersTimer_Tick(object sender, EventArgs e)
        {
            //if (isPanelExpanded) return;
            displayConnectedUsers();
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // Get the form's scroll position
            var scrollPos = AutoScrollPosition;

            // Adjust for scroll position when drawing the border
            var rect = new Rectangle(ClientRectangle.X - scrollPos.X, ClientRectangle.Y - scrollPos.Y, ClientRectangle.Width, ClientRectangle.Height);

            ControlPaint.DrawBorder(e.Graphics, rect, darkColor, ButtonBorderStyle.Solid);
        }
        private void panelTitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 1)
            {
                ReleaseCapture();
                SendMessage(Handle, 0x112, 0xf012, 0);
            }
        }
        private void panelTitleBar_Paint(object sender, PaintEventArgs e)
        {
            base.OnPaint(e);

            // Get the form's scroll position
            var scrollPos = AutoScrollPosition;

            // Adjust for scroll position when drawing the border
            var rect = new Rectangle(ClientRectangle.X - scrollPos.X, ClientRectangle.Y - scrollPos.Y, ClientRectangle.Width, ClientRectangle.Height);

            ControlPaint.DrawBorder(e.Graphics, rect, darkColor, ButtonBorderStyle.Solid);
        }
        private void closeImg_MouseEnter(object sender, EventArgs e)
        {
            closeImg.Image = Resources.close_hover;
        }
        private void closeImg_MouseLeave(object sender, EventArgs e)
        {
            closeImg.Image = Resources.close_nofocus;
        }
        private void closeImg_MouseDown(object sender, MouseEventArgs e)
        {
            closeImg.Image = Resources.close_press;
        }
        private void maxImage_MouseEnter(object sender, EventArgs e)
        {
            maxImage.Image = Resources.max_hover;
        }
        private void maxImage_MouseLeave(object sender, EventArgs e)
        {
            maxImage.Image = Resources.close_nofocus;
        }
        private void maxImage_MouseDown(object sender, MouseEventArgs e)
        {
            maxImage.Image = Resources.max_press;
        }
        private void minImg_MouseEnter(object sender, EventArgs e)
        {
            minImg.Image = Resources.min_hover;
        }
        private void minImg_MouseLeave(object sender, EventArgs e)
        {
            minImg.Image = Resources.close_nofocus;
        }
        private void minImg_MouseDown(object sender, MouseEventArgs e)
        {
            minImg.Image = Resources.min_press;
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
        private void mainFormNewUI_Resize(object sender, EventArgs e)
        {
            shadowPanel.Left = (ClientSize.Width - shadowPanel.Width) / 2;

            if (WindowState == FormWindowState.Normal)
            {
                usersPanel.MinimumSize = new Size(usersPanel.Width, contractedHeight);
            }
        }
        private void formPanel_Paint(object sender, PaintEventArgs e)
        {
            /*base.OnPaint(e);

            // Get the form's scroll position
            var scrollPos = AutoScrollPosition;

            // Adjust for scroll position when drawing the border
            var rect = new Rectangle(ClientRectangle.X - scrollPos.X, ClientRectangle.Y - scrollPos.Y, ClientRectangle.Width, ClientRectangle.Height);

            ControlPaint.DrawBorder(e.Graphics, rect, darkColor, ButtonBorderStyle.Solid);*/
        }
        private void bunifuLabel_Paint(object sender, PaintEventArgs e)
        {
            // Define the parts of the text
            string beforeHeart = "Made with ";
            string afterHeart = " for TDF+ by Youssef";
            string heart = "❤";

            // Set up fonts and brushes
            Font font = bunifuLabel.Font;
            Brush textBrush = new SolidBrush(bunifuLabel.ForeColor);
            Brush heartBrush = new SolidBrush(Color.Red);

            // Measure the size of each text segment
            SizeF beforeHeartSize = e.Graphics.MeasureString(beforeHeart, font);
            SizeF heartSize = e.Graphics.MeasureString(heart, font);
            SizeF afterHeartSize = e.Graphics.MeasureString(afterHeart, font);

            // Calculate total width and height
            float totalWidth = beforeHeartSize.Width + heartSize.Width + afterHeartSize.Width;
            float totalHeight = Math.Max(beforeHeartSize.Height, Math.Max(heartSize.Height, afterHeartSize.Height));

            // Resize the label to fit the content
            bunifuLabel.Width = (int)Math.Ceiling(totalWidth);
            bunifuLabel.Height = (int)Math.Ceiling(totalHeight);

            // Draw the first part of the text
            e.Graphics.DrawString(beforeHeart, font, textBrush, new PointF(0, 0));

            // Draw the red heart
            e.Graphics.DrawString(heart, font, heartBrush, new PointF(beforeHeartSize.Width, 0));

            // Draw the rest of the text
            e.Graphics.DrawString(afterHeart, font, textBrush, new PointF(beforeHeartSize.Width + heartSize.Width, 0));
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
                            circularPictureBox.Image = Resources.pngegg;
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
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (!isFormClosing)
            {
                isFormClosing = true;

                makeUserDisconnected();

                if (connectedUsersTimer != null)
                {
                    connectedUsersTimer.Stop();
                    connectedUsersTimer.Dispose();
                }

                if (notificationTimer != null)
                {
                    notificationTimer.Stop();
                    notificationTimer.Dispose();
                }

                hubProxy = null;
                loggedInUser = null;
            }

            base.OnFormClosing(e);
        }
        private void circularPictureBox_Click(object sender, EventArgs e)
        {
            // Initialize the ContextMenuStrip
            contextMenu = new ContextMenuStrip();

            // Add "Update" menu item
            ToolStripMenuItem updateMenuItem = new ToolStripMenuItem("Update Profile Picture");
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
        private void usersIconButton_Click(object sender, EventArgs e)
        {
            if (isPanelExpanded)
            {
                // Contract the panel
                usersPanel.Height = contractedHeight; // Set the height to contracted
              //  usersPanel.AutoScroll = false; // Disable auto-scroll
                usersIconButton.Image = Resources.down; // Change the icon to "down"
                isPanelExpanded = false; // Update the state
                usersIconButton.Location = usersIconLocation;

            }
            else
            {
                // Expand the panel
                usersPanel.Height = expandedHeight; // Set the height to expanded
             //   usersPanel.AutoScroll = true; // Enable auto-scroll
                usersIconButton.Image = Resources.up; // Change the icon to "up"
                displayConnectedUsers(); // Populate the panel with connected users
                isPanelExpanded = true; // Update the state
                usersIconButton.Location = usersIconLocation;
            }
        }
        #endregion

        #region Methods
        private async System.Threading.Tasks.Task InitializeSignalR()
        {
            string url = $"http://{serverIPAddress}:8080";
            connection = new HubConnection(url);
            hubProxy = connection.CreateHubProxy("NotificationHub");

            // Subscribe to receive notifications.
            hubProxy.On<string>("receiveNotification", (message) =>
            {
                // Ensure UI updates happen on the UI thread.
                this.Invoke(new Action(() =>
                {
                    MessageBox.Show(message, "New Notification");
                }));
            });

            try
            {
                await connection.Start();
                MessageBox.Show("Connected to SignalR Server.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error connecting to SignalR Server: " + ex.Message);
                Console.WriteLine("Error: " + ex.Message);
            }
        }

    
private void startNotificationChecker()
        {

            notificationTimer.Interval = 3000; // Check every 3 seconds
            if (loggedInUser != null)
            {
                notificationTimer.Tick += (s, e) => checkForNewMessages();
                notificationTimer.Start();
            }
        }
        private void checkForNewMessages()
        {
            if (loggedInUser != null)
            {
                string query = "SELECT SenderID, COUNT(*) AS UnreadCount FROM Notifications WHERE ReceiverID = @ReceiverID AND IsSeen = 0 GROUP BY SenderID";
                using (SqlConnection conn = Database.getConnection())
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ReceiverID", loggedInUser.userID);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int senderID = reader.GetInt32(0);
                                int unreadCount = reader.GetInt32(1);

                                // Trigger the notification
                                showNewMessageNotification(senderID, unreadCount);
                            }
                        }
                    }
                }
            }
        }
        private string getUserName(int userID)
        {
            string userName = string.Empty;
            using (SqlConnection conn = Database.getConnection())
            {
                string query = "SELECT FullName FROM Users WHERE UserID = @UserID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", userID);
                    conn.Open();
                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        userName = result.ToString();
                    }
                }
            }
            return userName;
        }
        private void showNewMessageNotification(int senderID, int unreadCount)
        {
            if (this.IsDisposed || this.Disposing)
                return;

            string senderName = getUserName(senderID);
            string message = $"{senderName} sent you {unreadCount} new message(s)";

            notificationsSnackbar.Show(
                this,
                message,
                BunifuSnackbar.MessageTypes.Information,
                5000,
                "",
                BunifuSnackbar.Positions.TopCenter
            );

            markNotificationsAsSeen(senderID);
        }
        private void markNotificationsAsSeen(int senderID)
        {
            string query = "UPDATE Notifications SET IsSeen = 1 WHERE ReceiverID = @ReceiverID AND SenderID = @SenderID AND IsSeen = 0";
            using (SqlConnection conn = Database.getConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ReceiverID", loggedInUser.userID);
                    cmd.Parameters.AddWithValue("@SenderID", senderID);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public static void updateRoleStatus()
        {
            hasManagerRole = loggedInUser.Role != null && managerRoles.Any(role =>
              string.Equals(loggedInUser.Role, role, StringComparison.OrdinalIgnoreCase));

            hasAdminRole = loggedInUser.Role != null && string.Equals(loggedInUser.Role, "Admin", StringComparison.OrdinalIgnoreCase);
            hasHRRole = loggedInUser.Role != null && hrRoles.Any(role => string.Equals(loggedInUser.Role, role, StringComparison.OrdinalIgnoreCase));
        }
        public void updateUserDataControls()
        {
            circularPictureBox.Image = loggedInUser.Picture ?? Resources.pngegg;

            usernameLabel.Text = $"Welcome, {loggedInUser.FullName.Split(' ')[0]}!";
        }
        private void setImageButtonVisibility()
        {
            // Remove all controls for non-admin, non-manager users, no HR users
            if (!(hasAdminRole || hasManagerRole || hasHRRole))
            {
                // Remove controls from formPanel and shadowPanel
                formPanel.Controls.Remove(controlPanelImageButton);
                formPanel.Controls.Remove(teamImageButton);

                shadowPanel.Controls.Remove(controlPanelLabel);
                shadowPanel.Controls.Remove(teamImageLabel);
            }
            else
            {
                // If the user is an Admin or HR, add all controls to the respective panels
                if (hasAdminRole || hasHRRole)
                {
                    // Add image buttons to formPanel if not already there
                    if (!formPanel.Controls.Contains(controlPanelImageButton))
                    {
                        formPanel.Controls.Add(controlPanelImageButton);
                    }
                    if (!formPanel.Controls.Contains(teamImageButton))
                    {
                        formPanel.Controls.Add(teamImageButton);
                    }

                    // Add labels to shadowPanel if not already there
                    if (!shadowPanel.Controls.Contains(controlPanelLabel))
                    {
                        shadowPanel.Controls.Add(controlPanelLabel);
                    }
                    if (!shadowPanel.Controls.Contains(teamImageLabel))
                    {
                        shadowPanel.Controls.Add(teamImageLabel);
                    }
                }

                // If the user is a Manager, add only team-related controls and ensure the admin controls are removed
                if (hasManagerRole)
                {
                    // Add team-related image button and label
                    if (!formPanel.Controls.Contains(teamImageButton))
                    {
                        formPanel.Controls.Add(teamImageButton);
                    }
                    if (!shadowPanel.Controls.Contains(teamImageLabel))
                    {
                        shadowPanel.Controls.Add(teamImageLabel);
                    }

                    // Remove admin controls (controlPanelImageButton and controlPanelLabel) for managers
                    formPanel.Controls.Remove(controlPanelImageButton);
                    shadowPanel.Controls.Remove(controlPanelLabel);
                }
            }
        }
        /*private void showFormInPanel(Form form)
        {
            form.TopLevel = false; // Make it a child control rather than a top-level form
            form.Dock = DockStyle.Fill;

            foreach (Control control in formPanel.Controls)
            {
                control.Hide();
            }

            formPanel.Controls.Add(form);


            // Restore hidden controls when the form is closed
            form.FormClosed += (s, e) =>
            {
                foreach (Control control in formPanel.Controls)
                {
                    control.Show();
                }
                setImageButtonVisibility();
            };

            form.Show();
        }*/
        private void adjustShadowPanelAndImageButtons()
        {
            // Set anchor style for all controls in shadowPanel
            foreach (Control control in shadowPanel.Controls)
            {
                control.Anchor = AnchorStyles.Left;
            }

            // Set the shadowPanel size based on the user's role
            if (!hasAdminRole && !hasManagerRole && !hasHRRole)
            {
                shadowPanel.Size = new Size(540, 75);
            }
            else if (hasManagerRole)
            {
                shadowPanel.Size = new Size(720, 75);
            }

            // Center the shadowPanel horizontally
            int initialShadowPanelLeft = shadowPanel.Left;
            shadowPanel.Left = (ClientSize.Width - shadowPanel.Width) / 2;

            // Calculate the horizontal movement (how much the shadowPanel moved)
            int movementDelta = shadowPanel.Left - initialShadowPanelLeft;

            // Loop through all controls in formPanel
            foreach (Control control in formPanel.Controls)
            {
                if (control is Bunifu.UI.WinForms.BunifuImageButton bunifuImageButton)
                {
                    int additionalOffset = 0;

                    // Adjust the button position based on the user role
                    if (!hasAdminRole && !hasManagerRole & !hasHRRole)
                    {
                        // additionalOffset = 165; // Adjust this value for non-admin/non-manager
                        additionalOffset = -5; // Adjust this value for non-admin/non-manager
                    }
                    else if (hasManagerRole)
                    {
                       // additionalOffset = 78; // Adjust this value for manager role
                    }

                    // Move the button the same distance as the shadowPanel and apply offset
                    control.Left += movementDelta + additionalOffset;
                }
            }
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
        public static List<User> getAllUsers()
        {
            List<User> connectedUsers = new List<User>();

            using (SqlConnection connection = Database.getConnection())
            {
                connection.Open();

                // Use parameterized query for safety and clarity.
                string query = "SELECT FullName, Department, Picture, UserID, isConnected " +
                               "FROM Users " +
                               "WHERE UserName <> @UserName " +
                               "ORDER BY isConnected DESC";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    // Add parameter value.
                    cmd.Parameters.AddWithValue("@UserName", loggedInUser.UserName);

                    // Use a using block for the reader.
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var user = new User
                            {
                                FullName = reader["FullName"].ToString(),
                                Department = reader["Department"].ToString(),
                                userID = reader["UserID"] != DBNull.Value ? Convert.ToInt32(reader["UserID"]) : 0,
                                isConnected = reader["isConnected"] != DBNull.Value ? Convert.ToInt32(reader["isConnected"]) : 0,
                                Picture = reader["Picture"] != DBNull.Value
                                          ? Image.FromStream(new MemoryStream((byte[])reader["Picture"]))
                                          : Resources.pngegg // Default to null if no image.
                            };

                            connectedUsers.Add(user);
                        }
                    }
                }
            }
            return connectedUsers;
        }
        private void displayConnectedUsers()
        {
            // Get all users (the SQL query orders them by isConnected DESC).
            List<User> connectedUsers = getAllUsers();
            int currentUserCount = connectedUsers.Count;
            int onlineCount = connectedUsers.Count(u => u.isConnected == 1);

            // --- HEADER: Find or create the header label.
            Label headerLabel = usersPanel.Controls
                .OfType<Label>()
                .FirstOrDefault(ctrl => ctrl.Tag != null && ctrl.Tag.ToString() == "header");
            if (headerLabel == null)
            {
                headerLabel = new Label
                {
                    Tag = "header",
                    Location = new Point(10, 10),
                    AutoSize = true,
                    Font = new Font("Segoe UI", 12, FontStyle.Bold)
                };
                usersPanel.Controls.Add(headerLabel);
            }
            headerLabel.Text = $"Online Users ({onlineCount})";  // Always update header

            // --- FLOWLAYOUTPANEL: Find or create the FlowLayoutPanel.
            FlowLayoutPanel flowLayout = usersPanel.Controls
                .OfType<FlowLayoutPanel>()
                .FirstOrDefault(ctrl => ctrl.Name == "flowLayoutUsers");
            if (flowLayout == null)
            {
                flowLayout = new FlowLayoutPanel
                {
                    Name = "flowLayoutUsers",
                    // Position below the header
                    Location = new Point(0, headerLabel.Bottom + 10),
                    Size = new Size(usersPanel.Width, 600), // Fixed height; adjust as desired
                    AutoScroll = true,
                    FlowDirection = FlowDirection.TopDown,
                    WrapContents = false,
                    Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
                };
                usersPanel.Controls.Add(flowLayout);
            }
            else
            {
                // Update the flowLayout size (in case the parent changed)
                flowLayout.Size = new Size(usersPanel.Width, 600);
            }

            // Save the current scroll position.
            int savedScrollPos = flowLayout.VerticalScroll.Value;

            // Determine whether we need to rebuild the entire list.
            bool needRebuild = false;
            if (flowLayout.Controls.Count != currentUserCount)
            {
                needRebuild = true;
            }
            else
            {
                // Check the order of user panels (each panel's Tag stores the userID).
                for (int i = 0; i < flowLayout.Controls.Count; i++)
                {
                    Panel panel = flowLayout.Controls[i] as Panel;
                    if (panel != null)
                    {
                        int panelUserId = (int)panel.Tag;
                        // Since connectedUsers is sorted by isConnected DESC via SQL,
                        // the i-th item should match the panel's Tag.
                        if (panelUserId != connectedUsers[i].userID)
                        {
                            needRebuild = true;
                            break;
                        }
                    }
                }
            }

            if (needRebuild)
            {
                // Save the scroll position.
                previousScrollPosition = flowLayout.AutoScrollPosition;

                // Suspend layout to reduce flicker.
                usersPanel.SuspendLayout();
                flowLayout.SuspendLayout();

                // Clear and rebuild the FlowLayoutPanel.
                flowLayout.Controls.Clear();
                foreach (User user in connectedUsers)
                {
                    Panel userPanel = new Panel
                    {
                        Size = new Size(flowLayout.Width - 25, 120),
                        Margin = new Padding(5),
                        BorderStyle = BorderStyle.None,
                        Tag = user.userID
                    };

                    // Increase picture size a bit.
                    CircularPictureBox pictureBox = new CircularPictureBox
                    {
                        Image = user.Picture,
                        SizeMode = PictureBoxSizeMode.Zoom,
                        Size = new Size(60, 60),
                        Location = new Point((userPanel.Width - 60) / 2, 10),
                        Cursor = Cursors.Hand
                    };
                    pictureBox.Click += (s, e) =>
                    {
                        chatForm cf = new chatForm(loggedInUser.userID, user.userID, user.FullName, user.Picture);
                        cf.Show();
                    };
                    userPanel.Controls.Add(pictureBox);

                    // Show full name and department.
                    Label nameLabel = new Label
                    {
                        Text = $"{user.FullName.Split(' ')[0]} - {user.Department}",
                        Font = new Font("Segoe UI", 10, FontStyle.Bold),
                        ForeColor = darkColor,
                        TextAlign = ContentAlignment.MiddleCenter,
                        AutoSize = true,
                        MaximumSize = new Size(userPanel.Width - 20, 0),
                        Location = new Point(10, pictureBox.Bottom + 5)
                    };
                    userPanel.Controls.Add(nameLabel);

                    // Online indicator (10x10 circle).
                    Panel onlineIndicator = new Panel
                    {
                        Size = new Size(10, 10),
                        BackColor = Color.Green,
                        Visible = user.isConnected == 1,
                        Name = "onlineIndicator"
                    };
                    onlineIndicator.Location = new Point(
                        pictureBox.Right - onlineIndicator.Width / 2,
                        pictureBox.Bottom - onlineIndicator.Height / 2
                    );
                    System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
                    path.AddEllipse(0, 0, onlineIndicator.Width, onlineIndicator.Height);
                    onlineIndicator.Region = new Region(path);
                    userPanel.Controls.Add(onlineIndicator);

                    flowLayout.Controls.Add(userPanel);
                }
                previousUserCount = currentUserCount;

                // Restore scroll position.
                if (previousScrollPosition != Point.Empty)
                {
                    flowLayout.AutoScrollPosition = new Point(previousScrollPosition.X, Math.Abs(previousScrollPosition.Y));
                }

                flowLayout.ResumeLayout();
                usersPanel.ResumeLayout();
            }
            else
            {
                // If no rebuild is needed, simply update online indicators.
                foreach (User user in connectedUsers)
                {
                    foreach (Control ctrl in flowLayout.Controls)
                    {
                        if (ctrl is Panel userPanel && (int)userPanel.Tag == user.userID)
                        {
                            var onlineIndicator = userPanel.Controls.Find("onlineIndicator", true).FirstOrDefault();
                            if (onlineIndicator != null)
                            {
                                onlineIndicator.Visible = user.isConnected == 1;
                            }
                            break;
                        }
                    }
                }
            }

            // Always update header label in case online count changed.
            headerLabel.Text = $"Online Users ({onlineCount})";

            // Restore scroll position (again, to be sure).
            try
            {
                flowLayout.VerticalScroll.Value = savedScrollPos;
                flowLayout.AutoScrollPosition = new Point(0, savedScrollPos);
            }
            catch { }
        }
        /* private void startPipeListener()
         {
             Thread pipeListenerThread = new Thread(new ThreadStart(ListenForMessages));
             pipeListenerThread.IsBackground = true;
             pipeListenerThread.Start();
         }
         private void ListenForMessages()
         {
             using (NamedPipeServerStream pipeServer = new NamedPipeServerStream("UserLogoutPipe", PipeDirection.In))
             {
                 // Wait for a connection from a client (new session)
                 pipeServer.WaitForConnection();

                 using (StreamReader reader = new StreamReader(pipeServer))
                 {
                     // Read the message from the new session
                     string message = reader.ReadLine();
                     if (message == "UserLoggedOut")
                     {
                         // Use a valid control on the UI thread for invoking the action
                         Invoke((Action)(() =>
                         {
                             MessageBox.Show("You have been logged out because the user is opened on another PC.");
                             Close(); // Close the old session (form)

                             // Show the login form on the main thread
                             if (loginForm != null)
                             {
                                 loginForm.Show();
                             }
                         }));
                     }
                 }
             }
         }*/
        #endregion

        #region Buttons
        private void requestsImageButton_Click(object sender, EventArgs e)
        {
            //  showFormInPanel(new requestsForm(false));
            requestsForm requestsForm = new requestsForm(true);
            requestsForm.ShowDialog();
        }
        private void reportsImageButton_Click(object sender, EventArgs e)
        {
           // showFormInPanel(new reportsForm(false));
            reportsForm reportsForm = new reportsForm(true);
            reportsForm.ShowDialog();
        }
        private void teamImageButton_Click(object sender, EventArgs e)
        {
            myTeamForm balanceForm = new myTeamForm(true);
            balanceForm.ShowDialog();
            //showFormInPanel(new balanceForm(false));
        }
        private void controlPanelImageButton_Click(object sender, EventArgs e)
        {
            controlPanelForm controlPanelForm = new controlPanelForm(true);

            controlPanelForm.userUpdated += updateRoleStatus;
            controlPanelForm.userUpdated += setImageButtonVisibility;
            controlPanelForm.userUpdated += adjustShadowPanelAndImageButtons;
            controlPanelForm.userUpdated += updateUserDataControls;

            //showFormInPanel(controlPanelForm);
            controlPanelForm.ShowDialog();
        }
        private void logoutImageButton_Click(object sender, EventArgs e)
        {
            makeUserDisconnected();

            loggedInUser = null;
            Close();
            loginForm.Show();
        }
        #endregion
    }
}
