using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using TDF.Net.Classes;
using static TDF.Net.Program;

namespace TDF.Net
{
    public partial class loginForm : Form
    {
        public loginForm()
        {
            InitializeComponent();
            updateTheme();
            guiDropdown.Text = "Modern";

        }

        private bool signingup = false;
        private bool changingPassword = false;
        public static User loggedInUser; // Ensure this is a public field
        public static List<string> departments = new List<string>();
        public static List<string> titles = new List<string>();
        private static Form oldSessionForm = null;
        private bool isLoggingIn = false;

        #region Methods
        private async void startLoggingIn()
        {
            if (isLoggingIn) return;
            isLoggingIn = true;

            try
            {
                string username = txtUsername.Text;
                string password = txtPassword.Text;

                // Check for an outdated version
                string appVersion = Application.ProductVersion;
                using (SqlConnection conn = Database.getConnection())
                {
                    conn.Open();
                    string versionQuery = "SELECT LatestVersion FROM AppVersion";
                    using (SqlCommand versionCmd = new SqlCommand(versionQuery, conn))
                    {
                        string latestVersion = versionCmd.ExecuteScalar()?.ToString();
                        if (latestVersion != null && latestVersion != appVersion)
                        {
                            MessageBox.Show("You are using an old version. Please use the latest version from the Taxi Partition to continue.", "Update Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                }

                // Proceed with login validation
                bool isValidLogin = validateLogin(username, password);

                if (isValidLogin)
                {
                    // Log in the user and track the session
                    loggedInUser = getCurrentUserDetails(username);
                    makeUserConnected();
                    updateMachineName();

                    // Initialize SignalR and register this user
                    string serverUrl = $"http://{Database.serverIPAddress}:8080"; // Replace with your actual URL
                    //string serverUrl = $"http://localhost:8080"; // Replace with your actual URL
                    await SignalRManager.InitializeAsync(serverUrl, loggedInUser.userID);

                    // Open the appropriate UI
                    if (guiDropdown.Text == "Classic")
                    {
                        oldSessionForm = new mainForm(this);
                    }
                    else
                    {
                        oldSessionForm = new mainFormNewUI(this);
                    }

                    oldSessionForm.Show();

                    // Hide the login form and clear fields
                    clearFormFields();
                    Hide();
                }
                else
                {
                    MessageBox.Show("Invalid username or password.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            finally
            {
                isLoggingIn = false;
            }
        }
        private void makeUserConnected()
        {
            using (SqlConnection connection = Database.getConnection())
            {
                connection.Open();

                string query = "UPDATE Users SET IsConnected = 1 WHERE UserName = @userName";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@userName", loggedInUser.UserName);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public static List<string> getDepartments()
        {
            List<string> departments = new List<string>();

            string query = "SELECT DISTINCT Department FROM Departments";

            using (SqlConnection connection = Database.getConnection())
            {
                SqlCommand command = new SqlCommand(query, connection);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        string department = reader["Department"].ToString();
                        departments.Add(department);
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error retrieving departments: " + ex.Message);
                }
            }

            departments.Sort();
            return departments;
        }
        private void loadDepartments()
        {
            departments = getDepartments();
            departmentDropdown.DataSource = departments;
            departmentDropdown.SelectedIndex = -1;
        }
        public static async Task<List<string>> getTitlesAsync(string department)
        {
            List<string> titles = new List<string>();

            string query = $"SELECT DISTINCT Title FROM Departments Where Department = '{department}'";

            using (SqlConnection connection = Database.getConnection())
            {
                SqlCommand command = new SqlCommand(query, connection);

                try
                {
                    await connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();

                    while (await reader.ReadAsync())
                    {
                        string title = reader["Title"].ToString();
                        titles.Add(title);
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error retrieving titles: " + ex.Message);
                }
            }

            titles.Sort();
            return titles;
        }
        private async Task loadTitlesAsync()
        {
            titles = await getTitlesAsync(departmentDropdown.Text);
            titlesDropdown.DataSource = titles;
            titlesDropdown.SelectedIndex = -1;
        }
        private void updateTheme()
        {
            Color color = ThemeColor.selectThemeColor();
            ThemeColor.primaryColor = color;
            ThemeColor.darkColor = ThemeColor.changeColorBrightness(color, -0.3);
            ThemeColor.lightColor = ThemeColor.changeColorBrightness(color, +0.6);
            applyThemeLite(this);
        }
        private bool validateLogin(string username, string password)
        {
            using (SqlConnection conn = Database.getConnection())
            {
                conn.Open();

                // Proceed with login validation
                string query = "SELECT PasswordHash, Salt FROM Users WHERE UserName = @UserName";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserName", username);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string storedHash = reader["PasswordHash"].ToString();
                            string salt = reader["Salt"].ToString();
                            string hash = Security.hashPassword(password, salt);

                            return hash == storedHash; // Return true if password is correct
                        }
                    }
                }
            }

            return false; // Return false if username is not found or password is incorrect
        }
        private bool isUsernameTaken(string username)
        {
            using (SqlConnection conn = Database.getConnection())
            {
                conn.Open();
                string query = "SELECT COUNT(1) FROM Users WHERE UserName = @UserName";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserName", username);
                    int userCount = Convert.ToInt32(cmd.ExecuteScalar());

                    return userCount > 0;
                }
            }
        }
        private void clearFormFields()
        {
            txtUsername.Clear();
            txtPassword.Clear();
            nameTextBox.Clear();
        }
        private bool verifyCurrentPassword(string username, string currentPassword)
        {
            using (SqlConnection conn = Database.getConnection())
            {
                conn.Open();
                string query = "SELECT PasswordHash, Salt FROM Users WHERE UserName = @UserName";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserName", username);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string storedHash = reader["PasswordHash"].ToString();
                            string storedSalt = reader["Salt"].ToString();
                            string hashedInputPassword = Security.hashPassword(currentPassword, storedSalt);

                            // Compare the input hashed password with the stored hash
                            return storedHash == hashedInputPassword;
                        }
                        else
                        {
                            // User not found
                            return false;
                        }
                    }
                }
            }
        }
        private void updatePasswordInDatabase(string username, string newPasswordHash, string salt)
        {
            using (SqlConnection conn = Database.getConnection())
            {
                conn.Open();
                string query = "UPDATE Users SET PasswordHash = @PasswordHash, Salt = @Salt WHERE UserName = @UserName";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@PasswordHash", newPasswordHash);
                    cmd.Parameters.AddWithValue("@Salt", salt);
                    cmd.Parameters.AddWithValue("@UserName", username);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public static User getCurrentUserDetails(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                throw new ArgumentException("Username cannot be null or empty.");
            }

            User userDetails = null;

            using (SqlConnection conn = Database.getConnection())
            {
                conn.Open();

                // Query to retrieve UserID, Username, UserRole, FullName, and Picture from the database
                string query = "SELECT UserID, UserName, Role, FullName, Picture, Department, Title FROM Users WHERE UserName = @UserName";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserName", username);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            userDetails = new User
                            {
                                // Retrieve and set the UserID
                                userID = reader["UserID"] != DBNull.Value ? Convert.ToInt32(reader["UserID"]) : 0,

                                // Retrieve and set the Username
                                UserName = reader["UserName"] != DBNull.Value ? reader["UserName"].ToString() : null,

                                // Retrieve and set the UserRole
                                Role = reader["Role"] != DBNull.Value ? reader["Role"].ToString() : null,

                                // Retrieve and set the FullName
                                FullName = reader["FullName"] != DBNull.Value ? reader["FullName"].ToString() : null,

                                // Retrieve and set the Department
                                Department = reader["Department"] != DBNull.Value ? reader["Department"].ToString() : null,

                                // Retrieve and set the Title
                                Title = reader["Title"] != DBNull.Value ? reader["Title"].ToString() : null,

                                // Retrieve and set the Picture if it exists
                                Picture = reader["Picture"] != DBNull.Value
                                    ? Image.FromStream(new System.IO.MemoryStream((byte[])reader["Picture"]))
                                    : null
                            };
                        }
                    }
                }
            }

            return userDetails;
        }
        private async Task ensureAdminExistsAsync()
        {
            using (SqlConnection conn = Database.getConnection())
            {
                await conn.OpenAsync();  // Use asynchronous open connection

                string checkQuery = "SELECT COUNT(1) FROM Users WHERE Role = 'Admin'";
                using (SqlCommand cmd = new SqlCommand(checkQuery, conn))
                {
                    int adminExists = (int)await cmd.ExecuteScalarAsync();  // Use asynchronous ExecuteScalar

                    if (adminExists == 0)
                    {
                        DialogResult result = MessageBox.Show(
                            "No Admin found. Do you want to create a default Admin user?",
                            "Create Admin", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (result == DialogResult.Yes)
                        {
                            string salt = Security.generateSalt();
                            string hashedPassword = Security.hashPassword("123", salt);

                            User admin = new User
                            {
                                UserName = "admin",
                                Salt = salt,
                                PasswordHash = hashedPassword,
                                FullName = "Administrator",
                                Role = "Admin",
                                Department = "All",
                                Title = "Administrator"
                            };

                            admin.add();

                            MessageBox.Show("Default Admin created. User Name: 'admin', Password: '123'. Please change the password immediately.",
                                            "Admin Created", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
        }
        public static void updateMachineName()
        {
            string machineName = Environment.MachineName; // Get the PC name

            using (SqlConnection conn = Database.getConnection())
            {
                conn.Open();
                string query = "UPDATE Users SET MachineName = @MachineName WHERE FullName = @FullName";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MachineName", machineName);
                    cmd.Parameters.AddWithValue("@FullName", loggedInUser.FullName);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        //public static async Task StartListeningAsync(CancellationToken cancellationToken = default)
        //{
        //    while (!cancellationToken.IsCancellationRequested)
        //    {
        //        try
        //        {
        //            // Create a new NamedPipeServerStream for each connection.
        //            using (var serverPipe = new NamedPipeServerStream("KillPipe", PipeDirection.In, 1,
        //                       PipeTransmissionMode.Byte, PipeOptions.Asynchronous))
        //            {
        //                // Asynchronously wait for a connection.
        //                await serverPipe.WaitForConnectionAsync(cancellationToken);

        //                // Read the command asynchronously.
        //                using (var reader = new StreamReader(serverPipe))
        //                {
        //                    string command = await reader.ReadLineAsync();
        //                    if (command == "KILL")
        //                    {
        //                        // Kill signal received – exit the application.
        //                        Application.Exit();
        //                    }
        //                }
        //            }
        //        }
        //        catch (OperationCanceledException)
        //        {
        //            // The operation was canceled—exit gracefully.
        //            break;
        //        }
        //        catch (Exception ex)
        //        {
        //            // Optionally log the error. You might want to use a logging framework
        //            // instead of MessageBox in production.
        //            MessageBox.Show($"Pipe Error: {ex.Message}");
        //            // Continue listening even if an error occurs.
        //        }
        //    }
        //}
        #endregion

        #region Events
        private async void loginForm_Shown(object sender, EventArgs e)
        {
            //await ensureAdminExistsAsync();
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // Get the form's scroll position
            Point scrollPos = AutoScrollPosition;

            // Adjust for scroll position when drawing the border
            Rectangle rect = new Rectangle(ClientRectangle.X - scrollPos.X, ClientRectangle.Y - scrollPos.Y, ClientRectangle.Width, ClientRectangle.Height);

            ControlPaint.DrawBorder(e.Graphics, rect, ThemeColor.darkColor, ButtonBorderStyle.Solid);
        }
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Invalidate();  // Forces the form to repaint when resized
        }
        private void panel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 1)
            {
                mainForm.ReleaseCapture();
                mainForm.SendMessage(Handle, 0x112, 0xf012, 0);
            }
        }
        private void panel_Paint(object sender, PaintEventArgs e)
        {
            base.OnPaint(e);
            ControlPaint.DrawBorder(e.Graphics, ClientRectangle, ThemeColor.darkColor, ButtonBorderStyle.Solid);
        }
        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!signingup && !changingPassword)
                {
                     startLoggingIn();

                    // Optionally suppress the beep sound on Enter key press
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                }
            }
        }
        private async void departmentDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            await loadTitlesAsync();
        }
        #endregion

        #region Buttons
        private void loginButton_Click(object sender, EventArgs e)
        {
            if (changingPassword)
            {
                string username = txtUsername.Text;
                string currentPassword = txtPassword.Text;
                string newPassword = nameTextBox.Text;

                // Basic input validation
                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(currentPassword) || string.IsNullOrEmpty(newPassword))
                {
                    MessageBox.Show("All fields are required.");
                    return;
                }

                // Verify current password and update to the new one if correct
                if (verifyCurrentPassword(username, currentPassword))
                {
                    string salt = Security.generateSalt();
                    string hashedNewPassword = Security.hashPassword(newPassword, salt);
                    updatePasswordInDatabase(username, hashedNewPassword, salt);
                    clearFormFields();
                    MessageBox.Show("Password changed successfully!");
                }
                else
                {
                    MessageBox.Show("Username or current password is incorrect.");
                }
            }
            else
            {
                startLoggingIn();
            }
        }
        private void signupButton_Click(object sender, EventArgs e)
        {
            if (!signingup && !changingPassword)
            {
                loadDepartments();
                signingup = true;
                nameTextBox.Visible = true;
                nameLabel.Visible = true;
                loginButton.Visible = false;
                updateButton.Text = "Go Back";
                signupButton.Text = "Submit";
                departmentLabel.Visible = true;
                departmentDropdown.Visible = true;
                titleLabel.Visible = true;
                titlesDropdown.Visible = true;
                nameTextBox.UseSystemPasswordChar = false;
                //passPictureBox.Visible = true;
                clearFormFields();
                departmentDropdown.DataSource = departments;
                departmentDropdown.SelectedIndex = -1;
                //guiDropdown.Visible = false;
              //  guiLabel.Visible = false;
                return;
            }
            if (changingPassword)
            {
                changingPassword = false;
                nameLabel.Text = "User Name:";
                passwordLabel.Text = "Password:";
                loginButton.Text = "Log In";
                signupButton.Text = "Sign Up";
                loginButton.Visible = true;
                nameTextBox.Visible = false;
                nameLabel.Visible = false;
                updateButton.Visible = true;
                departmentLabel.Visible = false;
                departmentDropdown.Visible = false;
                titleLabel.Visible = false;
                titlesDropdown.Visible = false;
                nameTextBox.UseSystemPasswordChar = true;
                // passPictureBox.Visible = false;
              //  guiDropdown.Visible = true;
              //  guiLabel.Visible = true;
                clearFormFields();
                return;
            }

            string username = txtUsername.Text;
            string password = txtPassword.Text;
            string name = nameTextBox.Text;
            string department = departmentDropdown.Text;
            string title = titlesDropdown.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(name) || string.IsNullOrEmpty(department) || string.IsNullOrEmpty(title))
            {
                MessageBox.Show("Please fill the empty blank boxes.");
                return;
            }

            if (isUsernameTaken(username))
            {
                MessageBox.Show("Username is already taken. Please choose a different username.");
                return;
            }

            User newUser = new User();
            newUser.UserName = username;
            newUser.Salt = Security.generateSalt();
            newUser.PasswordHash = Security.hashPassword(password, newUser.Salt);
            newUser.FullName = name;
            newUser.Role = "User";
            newUser.Department = department;
            newUser.Title = title;

            newUser.add();

            MessageBox.Show("User registered successfully!");

            nameTextBox.Visible = false;
            nameLabel.Visible = false;
            signupButton.Visible = true;
            loginButton.Visible = true;
            updateButton.Visible = true;
            departmentDropdown.Visible = false;
            departmentLabel.Visible = false;
            titleLabel.Visible = false;
            titlesDropdown.Visible = false;
            signingup = false;
          //  guiLabel.Visible = true;
           // guiDropdown.Visible = true;
            signupButton.Text = "Sign Up";
            updateButton.Text = "Change password";

            clearFormFields();
        }
        private void updateButton_Click(object sender, EventArgs e)
        {
            if (!signingup)
            {
                nameTextBox.UseSystemPasswordChar = true;
                changingPassword = true;
                nameLabel.Text = "New Password:";
                passwordLabel.Text = "Old Password:";
                loginButton.Text = "Submit";
                signupButton.Text = "Go back";
                nameTextBox.Visible = true;
                nameLabel.Visible = true;
                updateButton.Visible = false;
                //passPictureBox.Visible = false;
               //guiDropdown.Visible = false;
                //guiLabel.Visible = false;
            }
            else
            {
                nameTextBox.UseSystemPasswordChar = false;
                signingup = false;
                nameTextBox.Visible = false;
                nameLabel.Visible = false;
                loginButton.Visible = true;
                updateButton.Text = "Change Password";
                signupButton.Text = "Sign Up";
                titlesDropdown.Visible = false;
                titleLabel.Visible = false;
              //  guiDropdown.Visible = true;
             //   guiLabel.Visible = true;
                //passPictureBox.Visible = false;
            }

            departmentDropdown.Visible = false;
            departmentLabel.Visible = false;

            clearFormFields();
        }
        #endregion
    }
}
