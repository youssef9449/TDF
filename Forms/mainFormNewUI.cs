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
using System.Drawing.Drawing2D;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Infrastructure;
using static NotificationHub;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using Microsoft.AspNet.SignalR.Client.Hubs;

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

            SignalRManager.HubProxy.On<int, int>("UpdateMessageCount", UpdateMessageCounter);
            SignalRManager.HubProxy.On<int, string>("ReceivePendingMessage", HandlePendingMessage);

        }

        private ContextMenuStrip contextMenu;

        private loginForm loginForm;

        private bool isPanelExpanded, isFormClosing = false;
        private int expandedHeight; // Stores the full height of the panel when expanded
        private int contractedHeight = 50; // Height of the panel to show only the header
        private int previousUserCount = -1; 
        private FlowLayoutPanel flowLayout;
        private Dictionary<int, Panel> userPanels = new Dictionary<int, Panel>();


        #region Events
        private void mainFormNewUI_Load(object sender, EventArgs e)
        {
            MaximizedBounds = Screen.FromControl(this).WorkingArea;

            usersIconButton.BackgroundColor = primaryColor;
            usersIconButton.BorderColor = darkColor;
            expandedHeight = usersPanel.Height; // Store the original height when expanded
            //usersPanel.Height = contractedHeight; // Set the initial height to contracted


            // Subscribe to the SignalR "updateUserList" event.
            SignalRManager.HubProxy.On("updateUserList", () =>
            {
                // Check if the form's handle is created and not disposed before invoking.
                if (!IsDisposed && IsHandleCreated)
                {
                    try
                    {
                        this.Invoke(new Action(() =>
                        {
                            if (!IsDisposed)
                            {
                                displayConnectedUsers();
                            }
                        }));
                    }
                    catch (ObjectDisposedException)
                    {
                        // The form was disposed; no need to update.
                    }
                }
            });

            updateRoleStatus();
            setImageButtonVisibility();
            adjustShadowPanelAndImageButtons();
            updateUserDataControls();

            displayConnectedUsers();

            if (!IsDisposed && IsHandleCreated)
            {
                try
                {
                    LoadPendingMessages();

                }
                catch (ObjectDisposedException)
                {
                    // The form was disposed; no need to update.
                }
            }
            // Add new chat message handler
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
        protected override async void OnFormClosing(FormClosingEventArgs e)
        {
            if (!isFormClosing)
            {
                isFormClosing = true;

                // Update the database to mark the user as disconnected.
                makeUserDisconnected();

                // If connected via SignalR, gracefully disconnect.
                if (SignalRManager.IsConnected)
                {
                    try
                    {
                        // Notify the hub that this user is disconnecting.
                        await SignalRManager.HubProxy.Invoke("UserDisconnected", loggedInUser.UserName);
                        // Stop the SignalR connection.
                        SignalRManager.Connection.Stop();
                    }
                    catch (Exception ex)
                    {
                        // Log any error that occurs during disconnection.
                        Console.WriteLine("Error while stopping SignalR connection: " + ex.Message);
                    }
                }

                // Clear the logged-in user reference.
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
                //usersIconButton.Location = usersIconLocation;

            }
            else
            {
                // Expand the panel
                usersPanel.Height = expandedHeight; // Set the height to expanded
             //   usersPanel.AutoScroll = true; // Enable auto-scroll
                usersIconButton.Image = Resources.up; // Change the icon to "up"
               // displayConnectedUsers(); // Populate the panel with connected users
                isPanelExpanded = true; // Update the state
                //usersIconButton.Location = usersIconLocation;
            }
        }
        #endregion

        #region Methods
        /*private async System.Threading.Tasks.Task InitializeSignalR()
        {
            //serverIPAddress = "192.168.1.11";
            serverIPAddress = "localhost";

            string url = $"http://{serverIPAddress}:8080";
            await SignalRManager.InitializeAsync(url, loggedInUser.userID);
        }*/
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

        #region Message Handling Methods
        private async void HandlePendingMessage(int senderId, string message)
        {
            if (IsChatOpen(senderId)) return;

            BeginInvoke(new Action(async () =>
            {
                UpdateMessageCounter(senderId, 1);
                var pendingData = await SignalRManager.HubProxy.Invoke<Dictionary<int, PendingMessageData>>(
                    "GetPendingMessageCounts",
                    loggedInUser.userID
                );

                if (pendingData.TryGetValue(senderId, out var data) &&
                    userPanels.TryGetValue(senderId, out Panel panel))
                {
                    await ShowMessageBalloons(null, panel, data.Messages);
                }
            }));
        }
        private async Task LoadPendingMessages()
        {
            try
            {
                var pendingData = await SignalRManager.HubProxy.Invoke<Dictionary<int, PendingMessageData>>(
                    "GetPendingMessageCounts",
                    loggedInUser.userID
                );

                foreach (var entry in pendingData)
                {
                    var userPanel = GetUserPanel(entry.Key);
                    if (userPanel != null)
                    {
                        UpdateMessageCounter(entry.Key, entry.Value.Count);
                        if (entry.Value.Messages.Count > 0 && !IsChatOpen(entry.Key))
                        {
                            await ShowMessageBalloons(null, userPanel, entry.Value.Messages);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading pending messages: {ex.Message}");
            }
        }
        #endregion

        #region Updated UI Methods
        public void UpdateMessageCounter(int senderId, int count)
        {
            if (userPanels.TryGetValue(senderId, out Panel panel))
            {
                if (panel.InvokeRequired)
                {
                    panel.Invoke((MethodInvoker)delegate {
                        UpdateMessageCounterUI(panel, count);
                    });
                }
                else
                {
                    UpdateMessageCounterUI(panel, count);
                }
            }
        }
        private void UpdateMessageCounterUI(Panel panel, int count)
        {
            var pictureBox = panel.Controls.OfType<CircularPictureBox>().FirstOrDefault();
            if (pictureBox == null) return;

            Label counter = panel.Controls.Find("msgCounter", true).FirstOrDefault() as Label;
            if (counter == null)
            {
                counter = new Label
                {
                    Name = "msgCounter",
                    Size = new Size(20, 20),
                    BackColor = Color.FromArgb(33, 150, 243),
                    ForeColor = Color.White,
                    Font = new Font("Segoe UI", 8, FontStyle.Bold),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Location = new Point(pictureBox.Right - 15, pictureBox.Top - 5),
                    Visible = false
                };
                using (var gp = new GraphicsPath())
                {
                    gp.AddEllipse(0, 0, counter.Width, counter.Height);
                    counter.Region = new Region(gp);
                }
                panel.Controls.Add(counter);
            }

            counter.Text = count > 0 ? count.ToString() : "";
            counter.Visible = count > 0;

            if (count > 0 && flowLayout != null && !flowLayout.IsDisposed)
            {
                flowLayout.Controls.SetChildIndex(panel, 0);
                flowLayout.ScrollControlIntoView(panel);
            }
        }
        #endregion


        public bool IsChatOpen(int senderId) =>
                Application.OpenForms.OfType<chatForm>()
                    .Any(f => f.chatWithUserID == senderId);
        public async Task ShowMessageBalloons(int? senderId, Panel userPanel, List<string> messages)
        {
            var panel = userPanel ?? flowLayout.Controls
                .OfType<Panel>()
                .FirstOrDefault(p => (int)p.Tag == senderId);

            if (panel != null)
            {
                var pictureBox = panel.Controls.OfType<CircularPictureBox>().First();
                var screenPos = pictureBox.PointToScreen(new Point(0, 0));
                int offset = 0;

                foreach (var message in messages)
                {
                    BeginInvoke(new Action(() =>
                    {
                        new MessageBalloon(new Point(screenPos.X - 210, screenPos.Y + offset), message).Show();
                    }));
                    offset += 65;
                    await Task.Delay(500);
                }
            }
        }
        public async Task ShowSequentialBalloons(int senderId, List<string> messages)
        {
            var panel = flowLayout.Controls
                .OfType<Panel>()
                .FirstOrDefault(p => (int)p.Tag == senderId);

            if (panel != null)
            {
                var pictureBox = panel.Controls.OfType<CircularPictureBox>().First();
                var screenPos = pictureBox.PointToScreen(new Point(0, 0));
                int offset = 0;

                foreach (var message in messages)
                {
                    BeginInvoke(new Action(() =>
                    {
                        new MessageBalloon(new Point(screenPos.X - 210, screenPos.Y + offset), message).Show();
                    }));
                    offset += 65;
                    await Task.Delay(500);
                }
            }
        }
        #region Updated Chat Form Integration
        private async void OpenChatForm(int userId)
        {
            var user = getAllUsers().FirstOrDefault(u => u.userID == userId);
            if (user == null) return;

            var existingChat = Application.OpenForms.OfType<chatForm>().FirstOrDefault(f => f.chatWithUserID == userId);
            if (existingChat == null)
            {
                try
                {
                    if (!SignalRManager.IsConnected)
                    {
                        Console.WriteLine("SignalR not connected. Cannot mark messages as delivered.");
                        return;
                    }
                    await SignalRManager.HubProxy.Invoke("MarkMessagesAsDelivered", userId, loggedInUser.userID);
                    UpdateMessageCounter(userId, 0);
                    var messages = await SignalRManager.HubProxy.Invoke<List<string>>("GetPendingMessages", loggedInUser.userID, userId);
                    if (messages != null && messages.Count > 0)
                    {
                        await ShowMessageBalloons(null, userPanels[userId], messages);
                    }
                    new chatForm(loggedInUser.userID, userId, user.FullName, user.Picture).Show();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error in OpenChatForm: {ex.Message}");
                    MessageBox.Show("Failed to open chat. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                existingChat.BringToFront();
            }
        }
        #endregion

        private Panel GetUserPanel(int userId)
        {
            return flowLayout.Controls
                .OfType<Panel>()
                .FirstOrDefault(p => (int)p.Tag == userId);
        }
        public static List<User> getAllUsers()
        {
            List<User> connectedUsers = new List<User>();
            var pendingCounts = new Dictionary<int, int>();

            using (SqlConnection connection = getConnection())
            {
                connection.Open();

                // First get all pending counts for the logged-in user
                string countQuery = @"
            SELECT SenderID, COUNT(*) AS PendingCount
            FROM PendingChatMessages
            WHERE ReceiverID = @ReceiverID AND IsDelivered = 0
            GROUP BY SenderID";

                using (SqlCommand countCmd = new SqlCommand(countQuery, connection))
                {
                    countCmd.Parameters.AddWithValue("@ReceiverID", loggedInUser.userID);
                    using (SqlDataReader countReader = countCmd.ExecuteReader())
                    {
                        while (countReader.Read())
                        {
                            int senderId = Convert.ToInt32(countReader["SenderID"]);
                            int count = Convert.ToInt32(countReader["PendingCount"]);
                            pendingCounts[senderId] = count;
                        }
                    }
                }

                // Now get all users
                string userQuery = @"
            SELECT FullName, Department, Picture, UserID, isConnected
            FROM Users
            WHERE UserName <> @UserName
            ORDER BY isConnected DESC, FullName ASC";

                using (SqlCommand userCmd = new SqlCommand(userQuery, connection))
                {
                    userCmd.Parameters.AddWithValue("@UserName", loggedInUser.UserName);

                    using (SqlDataReader userReader = userCmd.ExecuteReader())
                    {
                        while (userReader.Read())
                        {
                            int userId = Convert.ToInt32(userReader["UserID"]);

                            var user = new User
                            {
                                FullName = userReader["FullName"].ToString(),
                                Department = userReader["Department"].ToString(),
                                userID = userId,
                                isConnected = Convert.ToInt32(userReader["isConnected"]),
                                Picture = userReader["Picture"] != DBNull.Value
                                          ? Image.FromStream(new MemoryStream((byte[])userReader["Picture"]))
                                          : Resources.pngegg,
                                PendingMessageCount = pendingCounts.TryGetValue(userId, out var count) ? count : 0
                            };

                            connectedUsers.Add(user);
                        }
                    }
                }
            }

            // Sort by pending messages, online status, then name
            return connectedUsers
                .OrderByDescending(u => u.PendingMessageCount)
                .ThenByDescending(u => u.isConnected)
                .ThenBy(u => u.FullName)
                .ToList();
        }
        private void displayConnectedUsers()
        {
            // Get all users with pending message counts
            List<User> connectedUsers = getAllUsers();
            int currentUserCount = connectedUsers.Count;
            int onlineCount = connectedUsers.Count(u => u.isConnected == 1);

            // --- HEADER: Find or create the header label
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
            headerLabel.Text = $"Online Users ({onlineCount})";

            // --- FLOWLAYOUTPANEL: Find or create the FlowLayoutPanel
            FlowLayoutPanel flowLayout = usersPanel.Controls
                .OfType<FlowLayoutPanel>()
                .FirstOrDefault(ctrl => ctrl.Name == "flowLayoutUsers");
            if (flowLayout == null)
            {
                flowLayout = new FlowLayoutPanel
                {
                    Name = "flowLayoutUsers",
                    Location = new Point(0, headerLabel.Bottom + 10),
                    Size = new Size(usersPanel.Width, 600),
                    AutoScroll = true,
                    FlowDirection = FlowDirection.TopDown,
                    WrapContents = false,
                    Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
                };
                usersPanel.Controls.Add(flowLayout);
            }
            else
            {
                flowLayout.Size = new Size(usersPanel.Width, 600);
            }

            // Save current scroll position
            int savedScrollPos = flowLayout.VerticalScroll.Value;

            // Order users by pending messages then online status
            connectedUsers = connectedUsers
                .OrderByDescending(u => u.PendingMessageCount)
                .ThenByDescending(u => u.isConnected)
                .ThenBy(u => u.FullName)
                .ToList();

            bool needRebuild = flowLayout.Controls.Count != currentUserCount;

            // Check if order needs updating
            if (!needRebuild)
            {
                for (int i = 0; i < flowLayout.Controls.Count; i++)
                {
                    if (flowLayout.Controls[i] is Panel panel &&
                        (int)panel.Tag != connectedUsers[i].userID)
                    {
                        needRebuild = true;
                        break;
                    }
                }
            }

            if (needRebuild)
            {
                usersPanel.SuspendLayout();
                flowLayout.SuspendLayout();
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

                    // Profile Picture
                    CircularPictureBox pictureBox = new CircularPictureBox
                    {
                        Image = user.Picture,
                        SizeMode = PictureBoxSizeMode.Zoom,
                        Size = new Size(60, 60),
                        Location = new Point((userPanel.Width - 60) / 2, 10),
                        Cursor = Cursors.Hand
                    };

                    // Message Counter
                    Label msgCounter = new Label
                    {
                        Text = user.PendingMessageCount > 0 ? user.PendingMessageCount.ToString() : "",
                        Size = new Size(20, 20),
                        BackColor = Color.FromArgb(33, 150, 243), // Material Blue
                        ForeColor = Color.White,
                        Font = new Font("Segoe UI", 8, FontStyle.Bold),
                        TextAlign = ContentAlignment.MiddleCenter,
                        Location = new Point(pictureBox.Right - 15, pictureBox.Top - 5),
                        Visible = user.PendingMessageCount > 0,
                        Name = "msgCounter",
                        Tag = user.userID
                    };

                    // Make counter circular
                    using (var gp = new GraphicsPath())
                    {
                        gp.AddEllipse(0, 0, msgCounter.Width, msgCounter.Height);
                        msgCounter.Region = new Region(gp);
                    }

                    // Click Handler
                    pictureBox.Click += (s, e) => OpenChatForm(user.userID);

                    // Online Indicator
                    Panel onlineIndicator = new Panel
                    {
                        Size = new Size(10, 10),
                        BackColor = Color.Green,
                        Visible = user.isConnected == 1,
                        Name = "onlineIndicator",
                        Location = new Point(
                            pictureBox.Right - 15,
                            pictureBox.Bottom - 15
                        )
                    };
                    using (var gp = new GraphicsPath())
                    {
                        gp.AddEllipse(0, 0, onlineIndicator.Width, onlineIndicator.Height);
                        onlineIndicator.Region = new Region(gp);
                    }

                    // Name Label
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

                    userPanel.Controls.AddRange(new Control[] {
                pictureBox,
                msgCounter,
                onlineIndicator,
                nameLabel
            });

                    flowLayout.Controls.Add(userPanel);
                }

                flowLayout.ResumeLayout();
                usersPanel.ResumeLayout();
            }
            else
            {
                // Update existing panels
                foreach (User user in connectedUsers)
                {
                    var userPanel = flowLayout.Controls
                        .OfType<Panel>()
                        .FirstOrDefault(p => (int)p.Tag == user.userID);

                    if (userPanel != null)
                    {
                        // Update online indicator
                        var onlineIndicator = userPanel.Controls.Find("onlineIndicator", true).FirstOrDefault();
                        if (onlineIndicator != null)
                            onlineIndicator.Visible = user.isConnected == 1;

                        // Update message counter
                        var msgCounter = userPanel.Controls.Find("msgCounter", true).FirstOrDefault() as Label;
                        if (msgCounter != null)
                        {
                            msgCounter.Text = user.PendingMessageCount > 0 ? user.PendingMessageCount.ToString() : "";
                            msgCounter.Visible = user.PendingMessageCount > 0;
                        }
                    }
                }
            }

            // Restore scroll position
            try
            {
                flowLayout.VerticalScroll.Value = Math.Min(savedScrollPos, flowLayout.VerticalScroll.Maximum);
            }
            catch { }
        }

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
        private async void logoutImageButton_Click(object sender, EventArgs e)
        {
            if (!isFormClosing)
            {
                isFormClosing = true;

                // Update the database to mark the user as disconnected.
                makeUserDisconnected();

                // If connected via SignalR, gracefully disconnect.
                if (SignalRManager.IsConnected)
                {
                    try
                    {
                        // Notify the hub that this user is disconnecting.
                        await SignalRManager.HubProxy.Invoke("UserDisconnected", loggedInUser.UserName);
                        // Stop the SignalR connection (Stop() is synchronous).
                        SignalRManager.Connection.Stop();
                    }
                    catch (Exception ex)
                    {
                        // Log any error that occurs during disconnection.
                        Console.WriteLine("Error while stopping SignalR connection: " + ex.Message);
                    }
                }

                // Clear the logged-in user reference.
                loggedInUser = null;
            }

            Close();
            loginForm.Show();
        }
        #endregion
    }
}
