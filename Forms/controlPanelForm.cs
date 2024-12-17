using Bunifu.UI.WinForms;
using TDF.Classes;
using TDF.Net;
using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using static TDF.Net.loginForm;
using Excel = Microsoft.Office.Interop.Excel;

namespace TDF.Forms
{
    public partial class controlPanelForm : Form
    {
        public controlPanelForm()
        {
            InitializeComponent();
            Program.loadForm(this);
        }

        #region Methods
        void updateDepartments()
        {
            departments = GetDepartments();
            depDropdown.DataSource = departments;
            depListBox.DataSource = departments;
            depDropdown.Text = "";
            depTextBox.Text = "";

            depDropdown.BindingContext = new BindingContext();
            depListBox.BindingContext = new BindingContext();
        }
        void loadUserNames()
        {

            string query,filter;

            filter = filterDropdown.Text;

            switch (filter)
            {
                case "Name":
                    query = $"SELECT FullName, Department, Role FROM Users where FullName LIKE '%{searchTextBox.Text}%' AND NOT Role ='Admin'";
                    break;
                case "Department":
                    query = $"SELECT FullName, Department, Role FROM Users where Department LIKE '%{searchTextBox.Text}%' AND NOT Role ='Admin'";
                    break;
                case "Role":
                    query = $"SELECT FullName, Department, Role FROM Users where Role LIKE '%{searchTextBox.Text}%' AND NOT Role ='Admin'";
                    break;
                default:
                    query = "SELECT FullName, Department, Role FROM Users where NOT Role ='Admin'";
                    break;
            }

            using (SqlConnection connection = Database.GetConnection())
            {
                SqlCommand command = new SqlCommand(query, connection);

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
                        string displayName = $"{fullName} - {departmentName} - {role}";

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
        public static void ImportUsersFromExcel(string filePath)
        {
            try
            {
                // Initialize Excel application
                Excel.Application excelApp = new Excel.Application();
                Excel.Workbook workbook = excelApp.Workbooks.Open(filePath);
                Excel.Worksheet worksheet = workbook.Sheets[1];
                Excel.Range range = worksheet.UsedRange;

                bool headerSkipped = false;

                using (SqlConnection conn = Database.GetConnection())
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
        #endregion

        #region Events
        private void controlPanelForm_Load(object sender, EventArgs e)
        {
            updateDepartments();

            filterDropdown.Text = "";
            depDropdown.Text = "";

            loadUserNames();
        }
        private void searchTextBox_TextChange(object sender, EventArgs e)
        {
            loadUserNames();
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
            ControlPaint.DrawBorder(e.Graphics, ClientRectangle, ThemeColor.SecondaryColor, ButtonBorderStyle.Solid);
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            ControlPaint.DrawBorder(e.Graphics, ClientRectangle, ThemeColor.SecondaryColor, ButtonBorderStyle.Solid);
        }
        #endregion

        #region Buttons
        private void roleButton_Click(object sender, EventArgs e)
        {
            if (usersCheckedListBox.CheckedItems.Count == 0)
            {
                MessageBox.Show("Please select user(s) to update.");
                return;
            }

            string role = roleDropdown.Text;

            if (string.IsNullOrEmpty(role))
            {
                MessageBox.Show("Please select a role from the dropdown menu.");
                return;
            }

            using (SqlConnection conn = Database.GetConnection())
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
            DialogResult result = MessageBox.Show(
                "Are you sure you want to delete the selected users ? " +
                "this will also delete the requests associated with them.",
                "Confirm Deletion",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                using (SqlConnection conn = Database.GetConnection())
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
            if (usersCheckedListBox.CheckedItems.Count == 0)
            {
                MessageBox.Show($"Please select user(s) to update.");
                return;
            }

            if (string.IsNullOrEmpty(depDropdown.Text))
            {
                MessageBox.Show("Please select a department from the dropdown menu.");
                return;
            }

            using (SqlConnection conn = Database.GetConnection())
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
            string newDepartmentName = depTextBox.Text;

            if (string.IsNullOrEmpty(newDepartmentName))
            {
                MessageBox.Show("Please type a new value in the box.");
                depTextBox.Focus();
                return;
            }

            string oldDepartmentName = depListBox.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(oldDepartmentName))
            {
                MessageBox.Show("Please choose a department to update.");
                depListBox.Focus();
                return;
            }

            int usersAffected, requestsAffected;

            using (SqlConnection conn = Database.GetConnection())
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

            using (SqlConnection conn = Database.GetConnection())
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
            string departmentName = depListBox.SelectedItem?.ToString(); // Use null-safe access

            if (string.IsNullOrEmpty(departmentName))
            {
                MessageBox.Show("Please choose a department to delete.");
                depListBox.Focus();
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
            using (SqlConnection conn = Database.GetConnection())
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
            if (usersCheckedListBox.CheckedItems.Count == 0)
            {
                MessageBox.Show("Please select a user to rename.");
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

            using (SqlConnection conn = Database.GetConnection())
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

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;
                    ImportUsersFromExcel(filePath);
                }
            }
        }
        #endregion

    }
}
