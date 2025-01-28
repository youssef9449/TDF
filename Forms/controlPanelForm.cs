using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using TDF.Net;
using TDF.Net.Classes;
using Bunifu.UI.WinForms;
using static TDF.Net.loginForm;
using static TDF.Net.mainForm;
using Excel = Microsoft.Office.Interop.Excel;

namespace TDF.Forms
{
    public partial class controlPanelForm : Form
    {
        public controlPanelForm(bool isModern)
        {
            InitializeComponent();
            Program.applyTheme(this);
            StartPosition = FormStartPosition.CenterScreen;

            if (isModern)
            {
                controlBox.Visible = !isModern;
                panel.MouseDown += new MouseEventHandler(panel_MouseDown);
                panel.Paint += panel_Paint;
            }
            else
            {
                panel.Visible = isModern;
            }

            spoofButton.Visible = hasAdminRole;
            passwordLabel.Visible = hasAdminRole;
            passwordTextBox.Visible = hasAdminRole;
            resetPasswordButton.Visible = hasAdminRole;
            importButton.Visible = hasAdminRole;
            deleteButton.Visible = hasAdminRole;

            if(hasHRRole)
            {
                roleDropdown.Items.Remove("HR Director");
            }
        }

        List<string> title = new List<string>();
        public event Action userUpdated;

        #region Methods
        private void updateDepartments()
        {
            departments = getDepartments();

            // Update the UI components once the departments are loaded
            depDropdown.DataSource = departments;

            depCheckedListBox.Items.Clear();
            foreach (string department in departments)
            {
                depCheckedListBox.Items.Add(department);
            }

            depDropdown.SelectedIndex = -1;
            depTextBox.Text = "";

            // Reset the bindings to ensure proper data binding
            depDropdown.BindingContext = new BindingContext();
            depCheckedListBox.BindingContext = new BindingContext();
        }
        private string buildUsersQuery(string filter, string searchValue)
        {
            string baseQuery = "SELECT FullName, Department, Role, Title FROM Users WHERE NOT Role = 'Admin'";

            switch (filter)
            {
                case "Name":
                    return $"{baseQuery} AND FullName LIKE @searchValue";
                case "Department":
                    return $"{baseQuery} AND Department LIKE @searchValue";
                case "Role":
                    return $"{baseQuery} AND Role LIKE @searchValue";
                case "Title":
                    return $"{baseQuery} AND Title LIKE @searchValue";
                default:
                    return $"{baseQuery} AND (FullName LIKE @searchValue OR Department LIKE @searchValue OR Role LIKE @searchValue OR Title LIKE @searchValue)";
            }
        }
        private void loadUserNames()
        {
            string filter = filterDropdown.Text;
            string searchValue = searchTextBox.Text;
            string query = buildUsersQuery(filter, searchValue);

            using (SqlConnection connection = Database.getConnection())
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@searchValue", $"%{searchValue}%");

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    usersCheckedListBox.Items.Clear();

                    while (reader.Read())
                    {
                        string fullName = reader["FullName"].ToString();
                        string departmentName = reader["Department"].ToString();
                        string role = reader["Role"].ToString();
                        string title = reader["Title"].ToString();
                        string displayName = $"{fullName} - {departmentName} - {title} - {role}";

                        usersCheckedListBox.Items.Add(displayName);
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading user names: " + ex.Message);
                }
            }
        }
        private void importUsersFromExcel(string filePath)
        {
            try
            {
                // Initialize Excel application
                Excel.Application excelApp = new Excel.Application();
                Excel.Workbook workbook = excelApp.Workbooks.Open(filePath);
                Excel.Worksheet worksheet = workbook.Sheets[1];
                Excel.Range range = worksheet.UsedRange;

                bool headerSkipped = false;

                using (SqlConnection conn = Database.getConnection())
                {
                    conn.Open();

                    for (int row = 1; row <= range.Rows.Count; row++)
                    {
                        // Read the first row and check if it matches expected column headers
                        string userName = (range.Cells[row, 1] as Excel.Range)?.Value?.ToString();
                        string fullName = (range.Cells[row, 2] as Excel.Range)?.Value?.ToString();
                        string role = (range.Cells[row, 3] as Excel.Range)?.Value?.ToString();
                        string department = (range.Cells[row, 4] as Excel.Range)?.Value?.ToString();
                        string password = (range.Cells[row, 5] as Excel.Range)?.Value?.ToString();

                        if (!headerSkipped)
                        {
                            if (userName?.ToLower() == "username" &&
                                fullName?.ToLower() == "fullname" &&
                                role?.ToLower() == "role" &&
                                department?.ToLower() == "department" &&
                                password?.ToLower() == "password")
                            {
                                headerSkipped = true; // Mark the header row as skipped
                                continue;
                            }
                        }

                        // Skip rows with empty required data
                        if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
                        {
                            continue;
                        }

                        // Generate salt and hash the password
                        string salt = Security.generateSalt();
                        string hashedPassword = Security.hashPassword(password, salt);

                        // Insert into the database
                        string query = @"
                        INSERT INTO Users (UserName, FullName, Role, Department, PasswordHash, Salt)
                        VALUES (@UserName, @FullName, @Role, @Department, @PasswordHash, @Salt)";
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@UserName", userName);
                            cmd.Parameters.AddWithValue("@FullName", fullName);
                            cmd.Parameters.AddWithValue("@Role", role);
                            cmd.Parameters.AddWithValue("@Department", department);
                            cmd.Parameters.AddWithValue("@PasswordHash", hashedPassword);
                            cmd.Parameters.AddWithValue("@Salt", salt);

                            cmd.ExecuteNonQuery();
                        }
                    }
                }

                // Clean up Excel resources
                workbook.Close(false);
                excelApp.Quit();

                MessageBox.Show("Users imported successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error importing users: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private bool userSelected()
        {
            if (usersCheckedListBox.CheckedItems.Count == 0)
            {
                MessageBox.Show("Please select at least one user.", "No user Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }
        private bool departmentSelected()
        {
            if (depCheckedListBox.CheckedItems.Count == 0)
            {
                MessageBox.Show("Please select at least one department.", "No department Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }
        #endregion

        #region Events
        private void controlPanelForm_Load(object sender, EventArgs e)
        {
            filterDropdown.Text = "Name";

            updateDepartments();

            depDropdown.SelectedIndex = -1;

            loadUserNames();

            removeBalanceDropdown.Text = "Annual";
        }
        private void searchTextBox_TextChange(object sender, EventArgs e)
        {
            loadUserNames();
        }
        private void panel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 1)
            {
                ReleaseCapture();
                SendMessage(Handle, 0x112, 0xf012, 0);
            }
        }
        private void controlPanelForm_Resize(object sender, EventArgs e)
        {
            Invalidate();
        }
        private void filterDropdown_SelectedValueChanged(object sender, EventArgs e)
        {
            loadUserNames();
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
        private async void depDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            title = await getTitlesAsync(depDropdown.Text);
            titleDropdown.DataSource = title;
            titleDropdown.SelectedIndex = -1;
        }
        private void panel_Paint(object sender, PaintEventArgs e)
        {
            base.OnPaint(e);

            // Get the form's scroll position
            Point scrollPos = AutoScrollPosition;

            // Adjust for scroll position when drawing the border
            Rectangle rect = new Rectangle(ClientRectangle.X - scrollPos.X, ClientRectangle.Y - scrollPos.Y, ClientRectangle.Width, ClientRectangle.Height);

            ControlPaint.DrawBorder(e.Graphics, rect, ThemeColor.darkColor, ButtonBorderStyle.Solid);
        }
        private void usersCheckBox_CheckedChanged(object sender, BunifuCheckBox.CheckedChangedEventArgs e)
        {
            // Get the check state of the "Select All" checkbox
            bool isChecked = usersCheckBox.Checked;

            // Loop through all items in the CheckedListBox
            for (int i = 0; i < usersCheckedListBox.Items.Count; i++)
            {
                usersCheckedListBox.SetItemChecked(i, isChecked);
            }
        }
        #endregion

        #region Buttons
        private void roleButton_Click(object sender, EventArgs e)
        {
            if (!userSelected())
            {
                return;
            }

            string role = roleDropdown.Text;

            if (string.IsNullOrEmpty(role))
            {
                MessageBox.Show("Please select a role from the dropdown menu.");
                return;
            }

            using (SqlConnection conn = Database.getConnection())
            {
                conn.Open();

                foreach (object selectedItem in usersCheckedListBox.CheckedItems)
                {
                    // Split the selected item to get the FullName (format: "FullName - Department")
                    string userFullName = selectedItem.ToString().Split('-')[0].Trim();

                    string query = "UPDATE Users SET Role = @Role WHERE FullName = @FullName";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Role", role);
                        cmd.Parameters.AddWithValue("@FullName", userFullName);

                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show($"Selected users have been granted {role} status.");
            }

            loadUserNames();
            roleDropdown.Text = "";
        }
        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (!userSelected())
            {
                return;
            }
            DialogResult result = MessageBox.Show(
                "Are you sure you want to delete the selected users ? " +
                "this will also delete the requests associated with them.",
                "Confirm Deletion",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                using (SqlConnection conn = Database.getConnection())
                {
                    conn.Open();

                    foreach (object selectedItem in usersCheckedListBox.CheckedItems)
                    {
                        string userFullName = selectedItem.ToString().Split('-')[0].Trim();

                        // Get the UserID based on FullName
                        string getUserIdQuery = "SELECT UserID FROM Users WHERE FullName = @FullName";
                        int userId;

                        using (SqlCommand getUserIdCmd = new SqlCommand(getUserIdQuery, conn))
                        {
                            getUserIdCmd.Parameters.AddWithValue("@FullName", userFullName);
                            userId = (int)getUserIdCmd.ExecuteScalar();
                        }

                        // Delete related requests for the user
                        string deleteRequestsQuery = "DELETE FROM Requests WHERE RequestUserID = @UserID";
                        using (SqlCommand deleteRequestsCmd = new SqlCommand(deleteRequestsQuery, conn))
                        {
                            deleteRequestsCmd.Parameters.AddWithValue("@UserID", userId);
                            deleteRequestsCmd.ExecuteNonQuery();
                        }

                        // Delete the user
                        string deleteUserQuery = "DELETE FROM Users WHERE UserID = @UserID";
                        using (SqlCommand deleteUserCmd = new SqlCommand(deleteUserQuery, conn))
                        {
                            deleteUserCmd.Parameters.AddWithValue("@UserID", userId);
                            deleteUserCmd.ExecuteNonQuery();
                        }
                    }

                    MessageBox.Show("Selected users have been deleted.");
                }

                loadUserNames();
            }
        }
        private void depButton_Click(object sender, EventArgs e)
        {
            if (!userSelected())
            {
                return;
            }
            if (string.IsNullOrEmpty(depDropdown.Text))
            {
                MessageBox.Show("Please select a department from the dropdown menu.");
                return;
            }

            using (SqlConnection conn = Database.getConnection())
            {
                conn.Open();

                foreach (object selectedItem in usersCheckedListBox.CheckedItems)
                {
                    string userFullName = selectedItem.ToString().Split('-')[0].Trim();

                    string query = "UPDATE Users SET Department = @Department WHERE FullName = @FullName";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Department", depDropdown.Text);
                        cmd.Parameters.AddWithValue("@FullName", userFullName);

                        cmd.ExecuteNonQuery();
                    }

                    string secondQuery = "UPDATE Requests SET RequestDepartment = @Department WHERE RequestUserFullName = @FullName";
                    using (SqlCommand cmd = new SqlCommand(secondQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@Department", depDropdown.Text);
                        cmd.Parameters.AddWithValue("@FullName", userFullName);

                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show($"Selected users have been moved to the {depDropdown.Text} department.");
            }

            loadUserNames();
            depDropdown.Text = "";
        }
        private void updateDepButton_Click(object sender, EventArgs e)
        {
            if (!departmentSelected())
            {
                return;
            }
            string newDepartmentName = depTextBox.Text;

            if (string.IsNullOrEmpty(newDepartmentName))
            {
                MessageBox.Show("Please type a new value in the box.");
                depTextBox.Focus();
                return;
            }

            string oldDepartmentName = depCheckedListBox.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(oldDepartmentName))
            {
                MessageBox.Show("Please choose a department to update.");
                depCheckedListBox.Focus();
                return;
            }

            int usersAffected, requestsAffected;

            using (SqlConnection conn = Database.getConnection())
            {
                conn.Open();

                // Check if the new department name already exists
                string checkQuery = "SELECT COUNT(*) FROM Departments WHERE Department = @newDepartmentName";
                using (SqlCommand cmd = new SqlCommand(checkQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@newDepartmentName", newDepartmentName);

                    int count = (int)cmd.ExecuteScalar();
                    if (count > 0)
                    {
                        MessageBox.Show($"The department '{newDepartmentName}' already exists. Please choose another name.");
                        return;
                    }
                }

                // Update the department name in the Departments table
                string query = "UPDATE Departments SET Department = @newDepartmentName WHERE Department = @oldDepartmentName";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@newDepartmentName", newDepartmentName);
                    cmd.Parameters.AddWithValue("@oldDepartmentName", oldDepartmentName);

                    cmd.ExecuteNonQuery();
                }

                // Update the department name in the Users table
                string updateUsers = "UPDATE Users SET Department = @newDepartmentName WHERE Department = @oldDepartmentName";
                using (SqlCommand cmd = new SqlCommand(updateUsers, conn))
                {
                    cmd.Parameters.AddWithValue("@oldDepartmentName", oldDepartmentName);
                    cmd.Parameters.AddWithValue("@newDepartmentName", newDepartmentName);

                    usersAffected = cmd.ExecuteNonQuery();
                }

                // Update the department name in the Requests table
                string updateRequests = "UPDATE Requests SET RequestDepartment = @newDepartmentName WHERE RequestDepartment = @oldDepartmentName";
                using (SqlCommand cmd = new SqlCommand(updateRequests, conn))
                {
                    cmd.Parameters.AddWithValue("@oldDepartmentName", oldDepartmentName);
                    cmd.Parameters.AddWithValue("@newDepartmentName", newDepartmentName);

                    requestsAffected = cmd.ExecuteNonQuery();
                }

                MessageBox.Show($"Selected Department has been updated, {usersAffected} user(s) and {requestsAffected} request(s) were affected");
            }
            updateDepartments();
            loadUserNames();
        }
        private void addDepButton_Click(object sender, EventArgs e)
        {
            string newDepartmentName = depTextBox.Text.Trim();

            if (string.IsNullOrEmpty(newDepartmentName))
            {
                MessageBox.Show("Please type the new department name in the box.");
                depTextBox.Focus();
                return;
            }

            using (SqlConnection conn = Database.getConnection())
            {
                conn.Open();

                // Check if the department already exists
                string checkQuery = "SELECT COUNT(1) FROM Departments WHERE Department = @DepartmentName";
                using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                {
                    checkCmd.Parameters.AddWithValue("@DepartmentName", newDepartmentName);

                    int exists = (int)checkCmd.ExecuteScalar(); // Returns 1 if exists, 0 otherwise

                    if (exists > 0)
                    {
                        MessageBox.Show($"The department '{newDepartmentName}' already exists.");
                        return;
                    }
                }

                // Add the new department
                string insertQuery = "INSERT INTO Departments (Department) VALUES (@DepartmentName)";
                using (SqlCommand insertCmd = new SqlCommand(insertQuery, conn))
                {
                    insertCmd.Parameters.AddWithValue("@DepartmentName", newDepartmentName);
                    insertCmd.ExecuteNonQuery();
                }

                MessageBox.Show($"The department '{newDepartmentName}' has been added successfully.");
            }

            updateDepartments();
        }
        private void deleteDepButton_Click(object sender, EventArgs e)
        {
            string departmentName = depCheckedListBox.SelectedItem?.ToString(); // Use null-safe access

            if (!departmentSelected())
            {
                return;
            }

            // Display confirmation dialog
            DialogResult result = MessageBox.Show(
                $"Are you sure you want to delete the {departmentName} department? This action will affect associated users and requests.",
                "Confirm Deletion",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result != DialogResult.Yes)
            {
                // If user selects 'No', return without deleting
                return;
            }

            int usersAffected, requestsAffected;
            using (SqlConnection conn = Database.getConnection())
            {
                conn.Open();

                // Delete department
                string query = "DELETE FROM Departments WHERE Department = @departmentName";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@departmentName", departmentName);
                    cmd.ExecuteNonQuery();
                }

                // Update users associated with the department
                string updateUsers = "UPDATE Users SET Department = '' WHERE Department = @departmentName";

                using (SqlCommand cmd = new SqlCommand(updateUsers, conn))
                {
                    cmd.Parameters.AddWithValue("@departmentName", departmentName);
                    usersAffected = cmd.ExecuteNonQuery();
                }

                // Update requests associated with the department
                string updateRequests = "UPDATE Requests SET RequestDepartment = '' WHERE RequestDepartment = @departmentName";

                using (SqlCommand cmd = new SqlCommand(updateRequests, conn))
                {
                    cmd.Parameters.AddWithValue("@departmentName", departmentName);
                    requestsAffected = cmd.ExecuteNonQuery();
                }

                MessageBox.Show($"The {departmentName} department has been deleted. {usersAffected} user(s) and {requestsAffected} request(s) were affected.");
            }

            updateDepartments();
            loadUserNames();
        }
        private void renameButton_Click(object sender, EventArgs e)
        {
            if (!userSelected())
            {
                return;
            }

            if (usersCheckedListBox.CheckedItems.Count > 1)
            {
                MessageBox.Show("you can't rename more than one user at the same time, please select only one.");
                return;
            }

            string newName = nameTextBox.Text;

            if (string.IsNullOrEmpty(newName))
            {
                MessageBox.Show("Please type a new name for the selected user.");
                nameTextBox.Focus();
                return;
            }

            string oldName = usersCheckedListBox.SelectedItem.ToString().Split('-')[0].Trim();

            using (SqlConnection conn = Database.getConnection())
            {
                conn.Open();

                string query = "UPDATE Users SET FullName = @newName WHERE FullName = @oldName";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@newName", newName);
                    cmd.Parameters.AddWithValue("@oldName", oldName);

                    cmd.ExecuteNonQuery();
                }

                string updateRequests = "UPDATE Requests SET RequestUserFullName = @newName WHERE RequestUserFullName = @oldName";

                using (SqlCommand cmd = new SqlCommand(updateRequests, conn))
                {
                    cmd.Parameters.AddWithValue("@newName", newName);
                    cmd.Parameters.AddWithValue("@oldName", oldName);

                    cmd.ExecuteNonQuery();
                }
                string updateLeaves = "UPDATE AnnualLeave SET FullName = @newName WHERE FullName = @oldName";

                using (SqlCommand cmd = new SqlCommand(updateLeaves, conn))
                {
                    cmd.Parameters.AddWithValue("@newName", newName);
                    cmd.Parameters.AddWithValue("@oldName", oldName);

                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show($"Selected user has been renamed.");
            }

            loadUserNames();
        }
        private void importButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Excel Files (*.xls;*.xlsx)|*.xls;*.xlsx;*.xlsb";
                openFileDialog.Title = "Select Excel File";

                try
                {
                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        string filePath = openFileDialog.FileName;

                        if (!File.Exists(filePath))
                        {
                            MessageBox.Show("The selected file does not exist.", "File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        try
                        {
                            importUsersFromExcel(filePath);
                            loadUserNames();
                            MessageBox.Show("Import successful.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"An error occurred during the import process:\n{ex.Message}", "Import Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An unexpected error occurred:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void resetPasswordButton_Click(object sender, EventArgs e)
        {
            if (!userSelected())
            {
                return;
            }
            if (string.IsNullOrEmpty(passwordTextBox.Text))
            {
                MessageBox.Show("Please enter a password first.");
                passwordTextBox.Focus();
                return;
            }

            DialogResult confirmation = MessageBox.Show("Are you sure you want to update the password for the selected users?",
                                       "Confirm Password Reset",
                                        MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirmation == DialogResult.Yes)
            {
                using (SqlConnection conn = Database.getConnection())
                {
                    conn.Open();

                    foreach (object selectedItem in usersCheckedListBox.CheckedItems)
                    {
                        string userFullName = selectedItem.ToString().Split('-')[0].Trim();

                        // Generate a new salt
                        string salt = Security.generateSalt();

                        string hashedPassword = Security.hashPassword(passwordTextBox.Text, salt);

                        string query = "UPDATE Users SET PasswordHash = @PasswordHash, Salt = @Salt WHERE FullName = @FullName";
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@PasswordHash", hashedPassword);
                            cmd.Parameters.AddWithValue("@Salt", salt);
                            cmd.Parameters.AddWithValue("@FullName", userFullName);

                            // Execute the query
                            cmd.ExecuteNonQuery();
                        }
                    }

                    MessageBox.Show($"Password changed for the selected users.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Password change canceled.", "Canceled", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void leaveButton_Click(object sender, EventArgs e)
        {
            if (!userSelected())
            {
                return;
            }
            // Initialize variables for leave values
            int? annual = null, casualLeave = null;

            // Validate and parse Annual leave value if it's valid
            if (!string.IsNullOrWhiteSpace(annualTextBox.Text) && int.TryParse(annualTextBox.Text, out int parsedAnnual) && parsedAnnual >= 0)
            {
                annual = parsedAnnual;
            }
            else if (!string.IsNullOrWhiteSpace(annualTextBox.Text))
            {
                MessageBox.Show("Please enter a valid non-negative number for Annual leave.");
                return;
            }

            // Validate and parse Casual leave value if it's valid
            if (!string.IsNullOrWhiteSpace(casualTextBox.Text) && int.TryParse(casualTextBox.Text, out int parsedCasual) && parsedCasual >= 0)
            {
                casualLeave = parsedCasual;
            }
            else if (!string.IsNullOrWhiteSpace(casualTextBox.Text))
            {
                MessageBox.Show("Please enter a valid non-negative number for Casual leave.");
                return;
            }

            // If no valid data, show a message
            if (annual == null && casualLeave == null)
            {
                MessageBox.Show("Please enter at least one valid leave value.");
                return;
            }

            // Proceed with updating the database
            using (SqlConnection conn = Database.getConnection())
            {
                conn.Open();

                foreach (object selectedItem in usersCheckedListBox.CheckedItems)
                {
                    string userFullName = selectedItem.ToString().Split('-')[0].Trim();

                    string query = "UPDATE AnnualLeave SET ";

                    // Add the annual update part if valid
                    if (annual.HasValue)
                    {
                        query += "Annual = @Annual ";
                    }

                    // Add the casual leave update part if valid
                    if (casualLeave.HasValue)
                    {
                        if (annual.HasValue) query += ", "; // Add a comma if both fields are being updated
                        query += "CasualLeave = @CasualLeave ";
                    }

                    query += "WHERE FullName = @FullName";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Add the parameters only if the respective values are valid
                        if (annual.HasValue)
                        {
                            cmd.Parameters.AddWithValue("@Annual", annual.Value);
                        }
                        if (casualLeave.HasValue)
                        {
                            cmd.Parameters.AddWithValue("@CasualLeave", casualLeave.Value);
                        }

                        cmd.Parameters.AddWithValue("@FullName", userFullName);
                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Updated leave balance successfully.");
            }
        }
        private void userDepButton_Click(object sender, EventArgs e)
        {
            if (!userSelected())
            {
                return;
            }
            if (!departmentSelected())
            {
                return;
            }
            var selectedItems = depCheckedListBox.CheckedItems.Cast<string>();

            // Join the selected items with a "-" separator
            string selectedDepartments = string.Join(" - ", selectedItems);

            using (SqlConnection conn = Database.getConnection())
            {
                conn.Open();

                foreach (object selectedItem in usersCheckedListBox.CheckedItems)
                {
                    string userFullName = selectedItem.ToString().Split('-')[0].Trim();

                    string query = "UPDATE Users SET Department = @Department WHERE FullName = @FullName";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Department", selectedDepartments);
                        cmd.Parameters.AddWithValue("@FullName", userFullName);

                        cmd.ExecuteNonQuery();
                    }

                    string secondQuery = "UPDATE Requests SET RequestDepartment = @Department WHERE RequestUserFullName = @FullName";
                    using (SqlCommand cmd = new SqlCommand(secondQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@Department", selectedDepartments);
                        cmd.Parameters.AddWithValue("@FullName", userFullName);

                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show($"Selected users have been moved to the {selectedDepartments} department.");
            }

            loadUserNames();
        }
        private void titleButton_Click(object sender, EventArgs e)
        {
            if (!userSelected())
            {
                return;
            }

            string title = titleDropdown.Text;

            if (string.IsNullOrEmpty(title))
            {
                MessageBox.Show("Please select a title from the dropdown menu.");
                titleDropdown.Focus();
                return;
            }

            using (SqlConnection conn = Database.getConnection())
            {
                conn.Open();

                foreach (object selectedItem in usersCheckedListBox.CheckedItems)
                {
                    // Split the selected item to get the FullName (format: "FullName - Department")
                    string userFullName = selectedItem.ToString().Split('-')[0].Trim();

                    string query = "UPDATE Users SET Title = @Title WHERE FullName = @FullName";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Title", title);
                        cmd.Parameters.AddWithValue("@FullName", userFullName);

                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show($"Selected users have been granted {title} title.");
            }

            loadUserNames();
            titleDropdown.Text = "";
            depDropdown.Text = "";
        }
        private void spoofButton_Click(object sender, EventArgs e)
        {
            if (usersCheckedListBox.CheckedItems.Count != 1)
            {
                MessageBox.Show("Please select one user.", "Invalid Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show("Are you sure you want to spoof the selected user?", "Confirm Spoofing",
                         MessageBoxButtons.YesNo,
                         MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                makeUserDisconnected();
                string userFullName = usersCheckedListBox.CheckedItems[0].ToString().Split('-')[0].Trim();

                using (SqlConnection conn = Database.getConnection())
                {
                    conn.Open();
                    string query = "SELECT UserID, Picture, UserName, FullName, Title, Department, Role FROM Users WHERE FullName = @FullName";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@FullName", userFullName);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read()) // If a record exists
                            {
                                loggedInUser.userID = Convert.ToInt32(reader["UserID"]);
                                loggedInUser.UserName = reader["UserName"].ToString();
                                loggedInUser.FullName = reader["FullName"].ToString();
                                loggedInUser.Title = reader["Title"].ToString();
                                loggedInUser.Department = reader["Department"].ToString();
                                loggedInUser.Role = reader["Role"].ToString();

                                // Load the picture from the database
                                if (reader["Picture"] != DBNull.Value)
                                {
                                    byte[] imageBytes = (byte[])reader["Picture"];
                                    using (MemoryStream ms = new MemoryStream(imageBytes))
                                    {
                                        loggedInUser.Picture = Image.FromStream(ms);
                                    }
                                }
                                else
                                {
                                    loggedInUser.Picture = null; // Handle cases where no image exists
                                }
                                userUpdated?.Invoke();
                            }
                            else
                            {
                                MessageBox.Show("No user found with the selected name.", "User Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
        }
        private void removeDaysButton_Click(object sender, EventArgs e)
        {
            if (!userSelected())
            {
                return;
            }

            // Validate removedAmountTextBox
           /* if (!decimal.TryParse(removedAmountTextBox.Text, out decimal removedAmount) || removedAmount <= 0)
            {
                MessageBox.Show("Please enter a valid positive number in the removed amount field.");
                removedAmountTextBox.Focus();
                return;
            }*/

            // Validate reasonTextBox
            if (string.IsNullOrWhiteSpace(reasonTextBox.Text))
            {
                MessageBox.Show("Please provide a reason.");
                reasonTextBox.Focus();
                return;
            }

            string requestReason = reasonTextBox.Text.Trim();
            string requestType = removeBalanceDropdown.Text;
            string loggedInUserFullName = loggedInUser.FullName;

            DateTime fromdate = Convert.ToDateTime(fromDayDatePicker.Text);

            // Determine the column to update
            string columnToUpdate;
            if (removeBalanceDropdown.Text == "Annual")
            {
                columnToUpdate = "AnnualUsed";
            }
            else if (removeBalanceDropdown.Text == "Emergency")
            {
                columnToUpdate = "CasualUsed";
            }
            else
            {
                MessageBox.Show("Please select a valid leave type (Annual or Emergency).");
                return;
            }

            using (SqlConnection connection = Database.getConnection())
            {
                connection.Open();

                foreach (var selectedUser in usersCheckedListBox.CheckedItems)
                {
                    // Extract user details from the selected item
                    string[] userParts = selectedUser.ToString().Split('-');
                    string fullName = userParts[0].Trim();
                    string departmentName = userParts[1].Trim();

                    // Fetch UserID from AnnualLeave table
                    string getUserIdQuery = "SELECT UserID FROM AnnualLeave WHERE FullName = @FullName";
                    int userId;
                    using (SqlCommand getUserIdCommand = new SqlCommand(getUserIdQuery, connection))
                    {
                        getUserIdCommand.Parameters.AddWithValue("@FullName", fullName);
                        object result = getUserIdCommand.ExecuteScalar();
                        if (result == null)
                        {
                            MessageBox.Show($"UserID not found for user: {fullName}");
                            continue;
                        }
                        userId = Convert.ToInt32(result);
                    }

                    // Update the leave balance in AnnualLeave
                    string updateQuery = $@"
                UPDATE AnnualLeave
                SET {columnToUpdate} = {columnToUpdate} + @RemovedAmount
                WHERE FullName = @FullName";

                    using (SqlCommand updateCommand = new SqlCommand(updateQuery, connection))
                    {
                        updateCommand.Parameters.AddWithValue("@RemovedAmount", 1);
                        updateCommand.Parameters.AddWithValue("@FullName", fullName);
                        updateCommand.ExecuteNonQuery();
                    }

                    // Insert a row into the Requests table
                    string insertQuery = @"
                INSERT INTO Requests 
                (
                    RequestUserID, RequestUserFullName, RequestFromDay, RequestToDay, 
                    RequestBeginningTime, RequestEndingTime, RequestReason, RequestStatus, 
                    RequestType, RequestRejectReason, RequestCloser, RequestDepartment, 
                    RequestNumberOfDays, RequestHRStatus, RequestHRCloser
                )
                VALUES
                (
                    @RequestUserID, @RequestUserFullName, @RequestFromDay, @RequestToDay, 
                    NULL, NULL, @RequestReason, @RequestStatus, 
                    @RequestType, NULL, @RequestCloser, @RequestDepartment, 
                    @RequestNumberOfDays, @RequestHRStatus, @RequestHRCloser
                )";

                    using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
                    {
                        insertCommand.Parameters.AddWithValue("@RequestUserID", userId);
                        insertCommand.Parameters.AddWithValue("@RequestUserFullName", fullName);
                        insertCommand.Parameters.AddWithValue("@RequestFromDay", fromdate);
                        insertCommand.Parameters.AddWithValue("@RequestToDay", fromdate);
                        insertCommand.Parameters.AddWithValue("@RequestReason", requestReason);
                        insertCommand.Parameters.AddWithValue("@RequestStatus", "Approved");
                        insertCommand.Parameters.AddWithValue("@RequestType", requestType);
                        insertCommand.Parameters.AddWithValue("@RequestCloser", loggedInUserFullName);
                        insertCommand.Parameters.AddWithValue("@RequestDepartment", departmentName);
                        insertCommand.Parameters.AddWithValue("@RequestNumberOfDays", 1);
                        insertCommand.Parameters.AddWithValue("@RequestHRStatus", "Approved");
                        insertCommand.Parameters.AddWithValue("@RequestHRCloser", loggedInUserFullName);
                        insertCommand.ExecuteNonQuery();
                    }
                }

                connection.Close();
            }

            MessageBox.Show("Leave balances updated successfully.");
        }
        #endregion

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            if (!userSelected())
            {
                return;
            }

            // Validate reasonTextBox
            if (string.IsNullOrWhiteSpace(reasonTextBox.Text))
            {
                MessageBox.Show("Please provide a reason.");
                reasonTextBox.Focus();
                return;
            }

            string requestReason = reasonTextBox.Text.Trim();
            string requestType = removeBalanceDropdown.Text;
            string loggedInUserFullName = loggedInUser.FullName;

            DateTime fromdate = Convert.ToDateTime(fromDayDatePicker.Text);
            DateTime todate = Convert.ToDateTime(toDayDatePicker.Text);

            int numberOfDays = (todate - fromdate).Days +1;

            using (SqlConnection connection = Database.getConnection())
            {
                connection.Open();

                foreach (var selectedUser in usersCheckedListBox.CheckedItems)
                {
                    // Extract user details from the selected item
                    string[] userParts = selectedUser.ToString().Split('-');
                    string fullName = userParts[0].Trim();
                    string departmentName = userParts[1].Trim();

                    // Fetch UserID from AnnualLeave table
                    string getUserIdQuery = "SELECT UserID FROM AnnualLeave WHERE FullName = @FullName";
                    int userId;
                    using (SqlCommand getUserIdCommand = new SqlCommand(getUserIdQuery, connection))
                    {
                        getUserIdCommand.Parameters.AddWithValue("@FullName", fullName);
                        object result = getUserIdCommand.ExecuteScalar();
                        if (result == null)
                        {
                            MessageBox.Show($"UserID not found for user: {fullName}");
                            continue;
                        }
                        userId = Convert.ToInt32(result);
                    }

                    // Insert a row into the Requests table
                    string insertQuery = @"
                INSERT INTO Requests 
                (
                    RequestUserID, RequestUserFullName, RequestFromDay, RequestToDay, 
                    RequestBeginningTime, RequestEndingTime, RequestReason, RequestStatus, 
                    RequestType, RequestRejectReason, RequestCloser, RequestDepartment, 
                    RequestNumberOfDays, RequestHRStatus, RequestHRCloser
                )
                VALUES
                (
                    @RequestUserID, @RequestUserFullName, @RequestFromDay, @RequestToDay, 
                    NULL, NULL, @RequestReason, @RequestStatus, 
                    @RequestType, NULL, @RequestCloser, @RequestDepartment, 
                    @RequestNumberOfDays, @RequestHRStatus, @RequestHRCloser
                )";

                    using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
                    {
                        insertCommand.Parameters.AddWithValue("@RequestUserID", userId);
                        insertCommand.Parameters.AddWithValue("@RequestUserFullName", fullName);
                        insertCommand.Parameters.AddWithValue("@RequestFromDay", fromdate);
                        insertCommand.Parameters.AddWithValue("@RequestToDay", todate);
                        insertCommand.Parameters.AddWithValue("@RequestReason", requestReason);
                        insertCommand.Parameters.AddWithValue("@RequestStatus", "Approved");
                        insertCommand.Parameters.AddWithValue("@RequestType", requestType);
                        insertCommand.Parameters.AddWithValue("@RequestCloser", "Nourhan Niazy");
                        insertCommand.Parameters.AddWithValue("@RequestDepartment", departmentName);
                        insertCommand.Parameters.AddWithValue("@RequestNumberOfDays", numberOfDays);
                        insertCommand.Parameters.AddWithValue("@RequestHRStatus", "Approved");
                        insertCommand.Parameters.AddWithValue("@RequestHRCloser", "Nourhan Niazy");
                        insertCommand.ExecuteNonQuery();
                    }
                }

                connection.Close();
            }

            MessageBox.Show("Requests added successfully.");
        }
    }
}
