using Bunifu.UI.WinForms;
using Bunifu.UI.WinForms.BunifuButton;
using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Media;
using System.Threading.Tasks;
using System.Windows.Forms;
using TDF.Classes;
using TDF.Forms;
using TDF.Net.Classes;
using TDF.Net.Forms;
using TDF.Properties;
using static NotificationHub;
using static TDF.Net.Classes.ThemeColor;
using static TDF.Net.Database;
using static TDF.Net.loginForm;
using static TDF.Net.mainForm;
using static TDF.Net.Program;

namespace TDF.Net
{
    public partial class mainFormNewUI : Form
    {

        public mainFormNewUI(loginForm loginForm)
        {
            InitializeComponent();

            applyTheme(this);

            formPanel.BackColor = Color.White;
            this.loginForm = loginForm; // Store a reference to the login form  

            SignalRManager.HubProxy.On<int, int>("UpdateMessageCount", UpdateMessageCounter);
            SignalRManager.HubProxy.On<int, string>("receivePendingMessage", HandlePendingMessage);

            flowLayout = new FlowLayoutPanel
            {
                Name = "flowLayoutUsers",
                Location = new Point(0, 30),
                Size = new Size(usersPanel.Width, 600),
                AutoScroll = true,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };
            usersPanel.Controls.Add(flowLayout);
        }   

        private ContextMenuStrip contextMenu;

        private loginForm loginForm;

        private bool isPanelExpanded = true;
        private bool isFormClosing = false;
        private int expandedHeight; // Stores the full height of the panel when expanded
        private int contractedHeight = 40; // Height of the panel to show only the header
        public int previousUserCount = -1; 
        private FlowLayoutPanel flowLayout;
        private Dictionary<int, Panel> userPanels = new Dictionary<int, Panel>();
        private List<User> cachedUsers = new List<User>();
        private IDisposable updateUserListSubscription;
        private Dictionary<int, int> pendingMessageCounts = new Dictionary<int, int>();
        private int lastNotificationCount = 0;
        private readonly object userCacheLock = new object();
        private Label notificationHeader; // Fixed header
        // Helper for async Invoke
        private Task InvokeAsync(Action action)
        {
            return Task.Run(() => this.Invoke(action));
        }


        #region Events
        private async void mainFormNewUI_Load(object sender, EventArgs e)
        {
            MaximizedBounds = Screen.FromControl(this).WorkingArea;
            usersIconButton.BackgroundColor = primaryColor;
            usersIconButton.BorderColor = darkColor;
            expandedHeight = usersPanel.Height;


            updateUserListSubscription = SignalRManager.HubProxy.On("updateUserList", () =>
            {
                if (!IsDisposed && IsHandleCreated)
                {
                    BeginInvoke(new Action(() => DisplayConnectedUsersAsync(true)));
                }
            });


            updateRoleStatus();
            setImageButtonVisibility();
            adjustShadowPanelAndImageButtons();
            updateUserDataControls();

            if (hasHRRole || hasManagerRole)
            {
                notificationHeader = new Label
                {
                    Text = "Number of Notifications: (0)",
                    Dock = DockStyle.Top,
                    Height = 30,
                    Font = new Font("Segoe UI", 10, FontStyle.Bold),
                    TextAlign = ContentAlignment.MiddleCenter
                };

                notificationsShadowPanel.Visible = hasHRRole || hasManagerRole;

                notificationsShadowPanel.Controls.Add(notificationsPanel);
                notificationsShadowPanel.Controls.Add(notificationHeader);
                notificationHeader.BringToFront();

                if (!IsDisposed && IsHandleCreated && !isFormClosing)
                {
                    try
                    {
                        await LoadUnreadNotifications(); // Fetch and display notifications
                    }
                    catch (Exception ex)
                    {
                        // Handle any errors (e.g., log them)
                        Console.WriteLine($"Error loading notifications: {ex.Message}");
                    }
                }
            }

            // Force initial refresh
            if (!IsDisposed && IsHandleCreated && !isFormClosing)
            {
                try
                {
                    await GetAllUsersAsync(true);
                    DisplayConnectedUsersAsync(true);
                    await LoadPendingMessages();
                       //   LoadUnreadNotifications();
                }
                catch (ObjectDisposedException) { }
            }

        }
        protected override void OnMove(EventArgs e)
        {
            base.OnMove(e);
            MaximizedBounds = Screen.FromControl(this).WorkingArea;
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
        private async void closeImg_MouseClick(object sender, MouseEventArgs e)
        {
            if (!isFormClosing)
            {
                isFormClosing = true;
                UnsubscribeFromEvents();  // Unsubscribe from SignalR events
                await triggerServerDisconnect();

                loggedInUser = null;
                Application.Exit();
            }
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
                using (SqlConnection conn = getConnection())
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
        private async void usersIconButton_Click(object sender, EventArgs e)
        {
            if (isPanelExpanded)
            {
                usersPanel.Height = contractedHeight;
                usersIconButton.Image = Resources.down;
                isPanelExpanded = false;
            }
            else
            {
                usersPanel.Height = expandedHeight;
                usersIconButton.Image = Resources.up;
                isPanelExpanded = true;
            }
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            isFormClosing = true; // Ensure flag is set during close
            base.OnFormClosing(e);
        }
        #endregion

        #region Methods
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
                using (SqlConnection conn = getConnection())
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
            Console.WriteLine($"HandlePendingMessage for user {loggedInUser.userID} from sender {senderId}: {message}");
            if (senderId == loggedInUser.userID)
            {
                Console.WriteLine("Ignoring message from self.");
                return;
            }

            if (IsChatOpen(senderId))
            {
                var chatForm = Application.OpenForms.OfType<chatForm>().FirstOrDefault(f => f.chatWithUserID == senderId);
                if (chatForm != null && !chatForm.IsDisposed && chatForm.IsHandleCreated)
                {
                    chatForm.BeginInvoke(new Action(async () =>
                    {
                        await chatForm.AppendMessageAsync(senderId, message);
                    }));
                }
            }
            else
            {
                if (!pendingMessageCounts.ContainsKey(senderId))
                {
                    pendingMessageCounts[senderId] = 0;
                }
                pendingMessageCounts[senderId]++;
                int newCount = pendingMessageCounts[senderId];
                UpdateMessageCounter(senderId, newCount);

                var userPanel = GetUserPanel(senderId);
                if (userPanel != null)
                {
                    var pictureBox = userPanel.Controls.OfType<CircularPictureBox>().FirstOrDefault();
                    if (pictureBox != null)
                    {
                        BeginInvoke(new Action(() =>
                        {
                            var balloonBasePoint = pictureBox.PointToScreen(new Point(pictureBox.Left - 180, pictureBox.Top));
                            new SpeechBubbleControl(balloonBasePoint, message).Show();
                        }));
                    }
                }
            }
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
            counter.Invalidate(); // Force redraw
            counter.BringToFront();
        }
        #endregion


        public bool IsChatOpen(int senderId) =>
                Application.OpenForms.OfType<chatForm>()
                    .Any(f => f.chatWithUserID == senderId);
        public async Task ShowMessageBalloons(int? senderId, Panel userPanel, List<string> messages)
        {
            if (!senderId.HasValue || senderId.Value == loggedInUser.userID)
            {
                Console.WriteLine("Skipping balloons for self or invalid sender.");
                return;
            }

            if (userPanels.TryGetValue(senderId.Value, out userPanel))
            {
                var pictureBox = userPanel.Controls.OfType<CircularPictureBox>().FirstOrDefault();
                if (pictureBox != null)
                {
                    // Position balloon to the left of the sender's picture
                    var balloonBasePoint = pictureBox.PointToScreen(new Point(pictureBox.Left - 180, pictureBox.Top));
                    foreach (var message in messages)
                    {
                        SpeechBubbleControl balloon = new SpeechBubbleControl(balloonBasePoint, message);
                        balloon.Show();
                        PlaySound(); // Play sound when showing balloon (chat not open)
                    }
                }
            }
            else
            {
                Console.WriteLine($"User panel not found for sender {senderId.Value}, skipping balloon.");
            }
        }


        #region Updated Chat Form Integration
        private async void OpenChatForm(int userId)
        {
            var user = (await GetAllUsersAsync()).FirstOrDefault(u => u.userID == userId);
            if (user == null) return;

            var existingChat = Application.OpenForms.OfType<chatForm>().FirstOrDefault(f => f.chatWithUserID == userId);
            if (existingChat != null)
            {
                existingChat.BringToFront();
                return;
            }
                
            try
            {
                if (!SignalRManager.IsConnected)
                {
                    Console.WriteLine("SignalR not connected. Cannot open chat.");
                    MessageBox.Show("Cannot open chat: connection lost.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Get pending messages before updating UI
                var messages = await SignalRManager.HubProxy.Invoke<List<string>>("GetPendingMessages", loggedInUser.userID, userId);

                // Clear the pending count for this sender when the user opens the chat.
                if (pendingMessageCounts.ContainsKey(userId))
                {
                    pendingMessageCounts.Remove(userId);
                    UpdateMessageCounter(userId, 0);
                }


                // Safely update UI components
                if (userPanels.ContainsKey(userId))
                {
                    UpdateMessageCounter(userId, 0);

                }
                else
                {
                    Console.WriteLine($"Warning: User panel not found for userId: {userId}");
                }

                // Possibly mark messages as delivered in the DB
                //UpdateMessageCounter(userId, 0);

                // Open new chat form
                new chatForm(loggedInUser.userID, userId, user.FullName, user.Picture).Show();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in OpenChatForm: {ex.Message}\nStackTrace: {ex.StackTrace}");
                if (ex.InnerException != null)
                    Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                MessageBox.Show("Failed to open chat. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        private Panel GetUserPanel(int userId)
        {
            return flowLayout.Controls
                .OfType<Panel>()
                .FirstOrDefault(p => (int)p.Tag == userId);
        }
        public async Task<List<User>> GetAllUsersAsync(bool forceRefresh = false)
        {
            if (isFormClosing || IsDisposed)
            {
                return new List<User>();
            }
            // Return an empty list if there's no logged-in user.
            if (loggedInUser == null)
                return new List<User>();

            lock (userCacheLock)
            {
                if (!forceRefresh && cachedUsers.Any())
                {
                    return new List<User>(cachedUsers); // Return a copy
                }
            }

            List<User> users = new List<User>();
            try
            {
                var connectedUserIDs = await SignalRManager.HubProxy.Invoke<List<int>>("GetConnectedUserIDs");
                using (SqlConnection connection = getConnection())
                {
                    await connection.OpenAsync();
                    string query = @"
                SELECT 
                    u.UserID, 
                    u.FullName, 
                    u.Department, 
                    u.Picture, 
                    ISNULL(pc.PendingCount, 0) AS PendingMessageCount
                FROM Users u
                LEFT JOIN (
                    SELECT SenderID, COUNT(*) AS PendingCount
                    FROM PendingChatMessages
                    WHERE ReceiverID = @ReceiverID AND IsDelivered = 0
                    GROUP BY SenderID
                ) pc ON u.UserID = pc.SenderID
                WHERE u.UserName <> @UserName
                ORDER BY ISNULL(pc.PendingCount, 0) DESC, 
                         CASE WHEN u.UserID IN (" + (connectedUserIDs.Any() ? string.Join(",", connectedUserIDs) : "0") + @") THEN 1 ELSE 0 END DESC, 
                         u.FullName ASC";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@ReceiverID", loggedInUser.userID);
                        cmd.Parameters.AddWithValue("@UserName", loggedInUser.UserName);

                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                int userId = reader.GetInt32(reader.GetOrdinal("UserID"));
                                var user = new User
                                {
                                    userID = userId,
                                    FullName = reader.GetString(reader.GetOrdinal("FullName")),
                                    Department = reader.GetString(reader.GetOrdinal("Department")),
                                    Picture = reader["Picture"] != DBNull.Value
                                        ? Image.FromStream(new MemoryStream((byte[])reader["Picture"]))
                                        : Resources.pngegg,
                                    isConnected = connectedUserIDs.Contains(userId) ? 1 : 0,
                                    PendingMessageCount = reader.GetInt32(reader.GetOrdinal("PendingMessageCount"))
                                };
                                users.Add(user);
                            }
                        }
                    }
                }

                lock (userCacheLock)
                {
                    cachedUsers = users; // Update cache
                }
                return new List<User>(users);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching users: {ex.Message}");
                return new List<User>();
            }
        }
        public async Task DisplayConnectedUsersAsync(bool forceFullRefresh = false)
        {
            List<User> connectedUsers = await GetAllUsersAsync(forceFullRefresh);
            int currentUserCount = connectedUsers.Count;
            int onlineCount = connectedUsers.Count(u => u.isConnected == 1);

            if (InvokeRequired)
            {
                await InvokeAsync(async () => await DisplayConnectedUsersAsync(forceFullRefresh));
                return;
            }

            // Update header label
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

            flowLayout.Size = new Size(usersPanel.Width, 600); // Ensure size updates with form resize

            int savedScrollPos = flowLayout.VerticalScroll.Value;

            // Determine if a full rebuild is needed
            bool needRebuild = forceFullRefresh || flowLayout.Controls.Count != currentUserCount || !UserPanelsMatch(connectedUsers);

            if (needRebuild)
            {
                usersPanel.SuspendLayout();
                flowLayout.SuspendLayout();
                flowLayout.Controls.Clear();
                userPanels.Clear();

                foreach (User user in connectedUsers)
                {
                    Panel userPanel = new Panel
                    {
                        Size = new Size(flowLayout.Width - 25, 120),
                        Margin = new Padding(5),
                        BorderStyle = BorderStyle.None,
                        Tag = user.userID
                    };

                    CircularPictureBox pictureBox = new CircularPictureBox
                    {
                        Image = user.Picture,
                        SizeMode = PictureBoxSizeMode.Zoom,
                        Size = new Size(60, 60),
                        Location = new Point((userPanel.Width - 60) / 2, 10),
                        Cursor = Cursors.Hand
                    };
                    pictureBox.SendToBack();
                    pictureBox.Click += (s, e) => OpenChatForm(user.userID);

                    Label msgCounter = new Label
                    {
                        Name = "msgCounter",
                        Size = new Size(20, 20),
                        BackColor = Color.FromArgb(33, 150, 243),
                        ForeColor = Color.White,
                        Font = new Font("Segoe UI", 8, FontStyle.Bold),
                        TextAlign = ContentAlignment.MiddleCenter,
                        Location = new Point(pictureBox.Left - 25, pictureBox.Top - 5),
                        Visible = false
                    };
                    using (var gp = new GraphicsPath())
                    {
                        gp.AddEllipse(0, 0, msgCounter.Width, msgCounter.Height);
                        msgCounter.Region = new Region(gp);
                    }

                    Label onlineIndicator = new Label
                    {
                        Name = "onlineIndicator",
                        Size = new Size(10, 10),
                        BackColor = user.isConnected == 1 ? Color.LimeGreen : Color.Red,
                        Location = new Point(pictureBox.Right - 10, pictureBox.Top + 5),
                        Visible = true
                    };
                    using (var gp = new GraphicsPath())
                    {
                        gp.AddEllipse(0, 0, onlineIndicator.Width, onlineIndicator.Height);
                        onlineIndicator.Region = new Region(gp);
                    }

                    Label nameLabel = new Label
                    {
                        Text = $"{user.FullName.Split(' ')[0]} - {user.Department}",
                        Font = new Font("Segoe UI", 10, FontStyle.Bold),
                        ForeColor = Color.Black,
                        TextAlign = ContentAlignment.MiddleCenter,
                        AutoSize = true,
                        MaximumSize = new Size(userPanel.Width - 20, 0),
                        Location = new Point(10, pictureBox.Bottom + 5)
                    };

                    userPanel.Controls.AddRange(new Control[] { pictureBox, msgCounter, onlineIndicator, nameLabel });
                    flowLayout.Controls.Add(userPanel);
                    userPanels[user.userID] = userPanel;

                    int initialCount = pendingMessageCounts.ContainsKey(user.userID) ? pendingMessageCounts[user.userID] : user.PendingMessageCount;
                    UpdateMessageCounterUI(userPanel, initialCount);
                    pictureBox.SendToBack();
                }

                flowLayout.ResumeLayout();
                usersPanel.ResumeLayout();
            }
            else
            {
                // Update only online indicators
                foreach (User user in connectedUsers)
                {
                    if (userPanels.TryGetValue(user.userID, out Panel userPanel))
                    {
                        var onlineIndicator = userPanel.Controls.Find("onlineIndicator", true).FirstOrDefault() as Label;
                        if (onlineIndicator != null)
                        {
                            onlineIndicator.BackColor = user.isConnected == 1 ? Color.LimeGreen : Color.Red;
                            onlineIndicator.Visible = true;
                            onlineIndicator.Invalidate(); // Force repaint
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
        private bool UserPanelsMatch(List<User> connectedUsers)
        {
            if (flowLayout == null || flowLayout.Controls.Count != connectedUsers.Count) return false;
            for (int i = 0; i < flowLayout.Controls.Count; i++)
            {
                if (flowLayout.Controls[i] is Panel panel && (int)panel.Tag != connectedUsers[i].userID)
                {
                    return false;
                }
            }
            return true;
        }
        public static async Task triggerServerDisconnect()
        {
            if (SignalRManager.IsConnected && loggedInUser != null)
            {
                try
                {
                    await SignalRManager.HubProxy.Invoke("DisconnectUser", loggedInUser.userID);
                    SignalRManager.Connection.Stop();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error stopping SignalR connection: {ex.Message}");
                }
            }
        }
        private void UnsubscribeFromEvents()
        {
            if (updateUserListSubscription != null)
            {
                updateUserListSubscription.Dispose();
                updateUserListSubscription = null;
            }
            // Dispose of other subscriptions similarly if you have them.
        }
        public async Task LoadUnreadNotifications()
        {
            if (!hasHRRole && !hasManagerRole)
            {
                return; // Skip loading for non-HR/Manager users
            }

            // Clear existing notifications
            notificationsPanel.Controls.Clear();

            using (SqlConnection conn = getConnection())
            {
                conn.Open();
                string query = @"
                SELECT rn.NotificationId, r.RequestID, r.RequestType, r.RequestUserFullName, r.RequestDepartment, r.RequestUserID
                FROM RequestNotifications rn
                JOIN Requests r ON rn.RequestId = r.RequestID
                WHERE rn.UserId = @UserId AND rn.IsRead = 0";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserId", loggedInUser.userID);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        int notificationCount = 0;
                        while (reader.Read())
                        {
                            int notificationId = reader.GetInt32(0);
                            string requestType = reader.GetString(2);
                            string userName = reader.GetString(3);
                            string department = reader.GetString(4);
                            int requestUserId = reader.GetInt32(5);

                            // Skip notification if the logged-in user is the request owner and has a manager or HR role.
                            if (loggedInUser.userID == requestUserId && (hasManagerRole || hasHRRole))
                            {
                                continue;
                            }

                            notificationCount++;

                            // Create a container for the individual notification using a FlowLayoutPanel.
                            FlowLayoutPanel notifContainer = new FlowLayoutPanel
                            {
                                FlowDirection = FlowDirection.TopDown,
                                WrapContents = false,
                                AutoSize = true,
                                Width = notificationsPanel.Width - 20,
                                BackColor = Color.White,
                                Margin = new Padding(0, 5, 0, 5)
                            };

                            // Create the label displaying the notification details.
                            Label lbl = new Label
                            {
                                Text = $"New {requestType} request from {userName} in {department}",
                                Font = new Font("Segoe UI", 9, FontStyle.Regular),
                                ForeColor = Color.Black,
                                AutoSize = true,
                               // TextAlign = ContentAlignment.MiddleCenter,
                                MaximumSize = new Size(notifContainer.Width - 20, 0)
                            };

                           // int leftMargin = (notifContainer.Width - lbl.Width) / 2;
                         //   lbl.Margin = new Padding(leftMargin, 5, 0, 0);

                            notifContainer.Controls.Add(lbl);

                            // Create the "Mark as Read" button.
                            BunifuButton2 btnMarkRead = new BunifuButton2
                            {
                                Text = "Mark as Read",
                                Size = new Size(90, 25),
                                Tag = notificationId,
                                Font = new Font("Segoe UI", 8, FontStyle.Bold),
                                Cursor = Cursors.Hand
                            };

                            // Add custom styling for mouse events.
                            btnMarkRead.OnDisabledState.BorderColor = darkColor;
                            btnMarkRead.OnDisabledState.FillColor = primaryColor;
                            btnMarkRead.OnDisabledState.ForeColor = Color.White;

                            btnMarkRead.onHoverState.BorderColor = darkColor;
                            btnMarkRead.onHoverState.FillColor = darkColor;
                            btnMarkRead.onHoverState.ForeColor = Color.White;

                            btnMarkRead.OnIdleState.BorderColor = darkColor;
                            btnMarkRead.OnIdleState.FillColor = primaryColor;
                            btnMarkRead.OnIdleState.ForeColor = Color.White;

                            btnMarkRead.OnPressedState.BorderColor = darkColor;
                            btnMarkRead.OnPressedState.FillColor = primaryColor;
                            btnMarkRead.OnPressedState.ForeColor = Color.White;
                            btnMarkRead.AutoRoundBorders = true;
                            btnMarkRead.IdleBorderRadius = 15;

                          //  int leftMargin = (notifContainer.Width - btnMarkRead.Width) / 2;
                            btnMarkRead.Margin = new Padding((notifContainer.Width - btnMarkRead.Width) / 2, 5, 0, 0);

                            // Use a correct event handler that casts sender as a Button.
                            btnMarkRead.Click += BtnMarkRead_Click;

                            notifContainer.Controls.Add(btnMarkRead);

                            // Add the individual notification container to the scrollable notifications panel.
                            notificationsPanel.Controls.Add(notifContainer);
                        }

                        // Update the header with the current notification count.
                        notificationHeader.Text = $"Number of Notifications: ({notificationCount})";
                    }
                }
            }

            // Check if there is an increase in notifications to show a snackbar.
            int currentCount = notificationsPanel.Controls.Count;
            if (currentCount > lastNotificationCount)
            {
                bool showNotification = false;
                using (SqlConnection conn = getConnection())
                {
                    conn.Open();
                    string query = @"
                    SELECT r.RequestUserID
                    FROM RequestNotifications rn
                    JOIN Requests r ON rn.RequestId = r.RequestID
                    WHERE rn.UserId = @UserId AND rn.IsRead = 0";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserId", loggedInUser.userID);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int requestUserId = reader.GetInt32(0);
                                if (loggedInUser.userID != requestUserId || (!hasManagerRole && !hasHRRole))
                                {
                                    showNotification = true;
                                    break;
                                }
                            }
                        }
                    }
                }

                if (showNotification)
                {
                    ShowNotificationSnackbar("You have new request(s).");
                }
            }
            lastNotificationCount = currentCount;
        }

        private void BtnMarkRead_Click(object sender, EventArgs e)
        {
            int notificationId = (int)((BunifuButton2)sender).Tag;
            MarkNotificationAsRead(notificationId);
        }
        private void MarkNotificationAsRead(int notificationId)
        {
            using (SqlConnection conn = getConnection())
            {
                conn.Open();
                string checkQuery = "SELECT COUNT(*) FROM RequestNotifications WHERE NotificationId = @NotificationId AND UserId = @UserId";
                using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                {
                    checkCmd.Parameters.AddWithValue("@NotificationId", notificationId);
                    checkCmd.Parameters.AddWithValue("@UserId", loggedInUser.userID);
                    int count = (int)checkCmd.ExecuteScalar();
                    if (count == 0)
                    {
                        MessageBox.Show("You are not authorized to mark this notification as read.");
                        return;
                    }
                }

                string query = "UPDATE RequestNotifications SET IsRead = 1 WHERE NotificationId = @NotificationId";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@NotificationId", notificationId);
                    cmd.ExecuteNonQuery();
                }
            }
            LoadUnreadNotifications();
        }
        private void ShowNotificationSnackbar(string message)
        {
            // Show Bunifu Snackbar
            notificationsSnackbar.Show(this, message, BunifuSnackbar.MessageTypes.Information);
            // Play notification sound

            // Use a system sound (e.g., Windows chime)
            // SystemSounds.Exclamation.Play();

            // Play notification sound from Audio folder
            try
            {
                string audioPath = Path.Combine(Application.StartupPath, "Audio", "Request Notification.wav");
                if (File.Exists(audioPath))
                {
                    using (var soundPlayer = new SoundPlayer(audioPath))
                    {
                        soundPlayer.Play();
                    }
                }
                else
                {
                    Console.WriteLine($"Audio file not found at: {audioPath}");
                    SystemSounds.Exclamation.Play(); // Fallback to system sound
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error playing notification sound: {ex.Message}");
                SystemSounds.Exclamation.Play(); // Fallback on error
            }
        }
        private void AddUserPanel(int userId, bool isConnected)
        {
            User newUser = GetUserDetails(userId);
            if (newUser == null) return;

            Panel userPanel = new Panel
            {
                Size = new Size(flowLayout.Width - 25, 120),
                Margin = new Padding(5),
                BorderStyle = BorderStyle.None,
                Tag = userId
            };

            CircularPictureBox pictureBox = new CircularPictureBox
            {
                Image = newUser.Picture,
                SizeMode = PictureBoxSizeMode.Zoom,
                Size = new Size(60, 60),
                Location = new Point((userPanel.Width - 60) / 2, 10),
                Cursor = Cursors.Hand
            };
            pictureBox.Click += (s, e) => OpenChatForm(userId);
            pictureBox.SendToBack();

            Label msgCounter = new Label
            {
                Name = "msgCounter",
                Size = new Size(20, 20),
                BackColor = Color.FromArgb(33, 150, 243),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 8, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(pictureBox.Left - 25, pictureBox.Top - 5),
                Visible = false
            };
            using (var gp = new GraphicsPath())
            {
                gp.AddEllipse(0, 0, msgCounter.Width, msgCounter.Height);
                msgCounter.Region = new Region(gp);
            }

            Label onlineIndicator = new Label
            {
                Name = "onlineIndicator",
                Size = new Size(10, 10),
                BackColor = isConnected ? Color.LimeGreen : Color.Red,
                Location = new Point(pictureBox.Right - 10, pictureBox.Top + 5),
                Visible = true
            };
            using (var gp = new GraphicsPath())
            {
                gp.AddEllipse(0, 0, onlineIndicator.Width, onlineIndicator.Height);
                onlineIndicator.Region = new Region(gp);
            }

            Label nameLabel = new Label
            {
                Text = $"{newUser.FullName.Split(' ')[0]} - {newUser.Department}",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.Black,
                TextAlign = ContentAlignment.MiddleCenter,
                AutoSize = true,
                MaximumSize = new Size(userPanel.Width - 20, 0),
                Location = new Point(10, pictureBox.Bottom + 5)
            };

            userPanel.Controls.AddRange(new Control[] { pictureBox, msgCounter, onlineIndicator, nameLabel });
            flowLayout.Controls.Add(userPanel);
            flowLayout.Controls.SetChildIndex(userPanel, 0); // Insert at top
            flowLayout.ScrollControlIntoView(userPanel); // Ensure visible
            userPanels[userId] = userPanel;

            int initialCount = pendingMessageCounts.ContainsKey(userId) ? pendingMessageCounts[userId] : newUser.PendingMessageCount;
            UpdateMessageCounterUI(userPanel, initialCount);
        }
        private User GetUserDetails(int userId)
        {
            try
            {
                using (SqlConnection connection = getConnection())
                {
                    connection.Open();
                    string query = @"
                    SELECT UserID, FullName, Department, Picture, 0 AS PendingMessageCount
                    FROM Users 
                    WHERE UserID = @UserID";
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@UserID", userId);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new User
                                {
                                    userID = reader.GetInt32(0),
                                    FullName = reader.GetString(1),
                                    Department = reader.GetString(2),
                                    Picture = reader["Picture"] != DBNull.Value
                                        ? Image.FromStream(new MemoryStream((byte[])reader["Picture"]))
                                        : Resources.pngegg,
                                    isConnected = 1, // Assume connected since this is a new user
                                    PendingMessageCount = reader.GetInt32(4)
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching user details: {ex.Message}");
            }
            return null;
        }
        public void UpdateUserStatus(int userId, bool isConnected)
        {
            if (userPanels.TryGetValue(userId, out Panel userPanel))
            {
                var onlineIndicator = userPanel.Controls.Find("onlineIndicator", true).FirstOrDefault() as Label;
                if (onlineIndicator != null)
                {
                    onlineIndicator.BackColor = isConnected ? Color.LimeGreen : Color.Red;
                    onlineIndicator.Visible = true;
                    onlineIndicator.Invalidate();
                    // Move reconnected user to top if connecting
                    if (isConnected)
                    {
                        flowLayout.Controls.Remove(userPanel);
                        flowLayout.Controls.Add(userPanel); // Add to bottom for now
                        flowLayout.Controls.SetChildIndex(userPanel, 0); // Move to top
                        flowLayout.ScrollControlIntoView(userPanel);
                    }
                }
            }
            else if (isConnected) // Only add new users when connecting, not disconnecting
            {
                AddUserPanel(userId, isConnected);
            }

            // Update online count
            int onlineCount = userPanels.Count(p => p.Value.Controls.Find("onlineIndicator", true).FirstOrDefault()?.BackColor == Color.LimeGreen);
            var headerLabel = usersPanel.Controls.OfType<Label>().FirstOrDefault(ctrl => ctrl.Tag?.ToString() == "header");
            if (headerLabel != null) headerLabel.Text = $"Online Users ({onlineCount})";
        }
        public static void PlaySound()
        {
            try
            {
                string audioPath = Path.Combine(Application.StartupPath, "Audio", "Message.wav");
                if (File.Exists(audioPath))
                {
                    using (var soundPlayer = new SoundPlayer(audioPath))
                    {
                        soundPlayer.Play();
                    }
                }
                else
                {
                    Console.WriteLine($"Audio file not found at: {audioPath}");
                    SystemSounds.Exclamation.Play();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error playing notification sound: {ex.Message}");
                SystemSounds.Exclamation.Play();
            }
        }

        #endregion

        #region Buttons
        private void requestsImageButton_Click(object sender, EventArgs e)
        {
            //  showFormInPanel(new requestsForm(false));
            requestsForm requestsForm = new requestsForm(true, loggedInUser);
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

                UnsubscribeFromEvents();
                await triggerServerDisconnect();
                SignalRManager.ResetConnection(); // Reset SignalR state
                loggedInUser = null;
                Close();
                loginForm.Show();
            }
        }


        #endregion
    }
}
