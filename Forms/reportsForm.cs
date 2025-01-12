using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using TDF.Net.Classes;
using TDF.Net;
using static TDF.Net.Forms.addRequestForm;
using static TDF.Net.loginForm;
using static TDF.Net.mainForm;
using System.Drawing;


namespace TDF.Forms
{
    public partial class reportsForm : Form
    {
        public reportsForm(bool isModern)
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            if (isModern)
            {
                controlBox.Visible = !isModern;
                panelTitleBar.MouseDown += new MouseEventHandler(panelTitleBar_MouseDown);
                closeImg.MouseEnter += new EventHandler(closeImg_MouseEnter);
                closeImg.MouseLeave += new EventHandler(closeImg_MouseLeave);
                closeImg.MouseClick += new MouseEventHandler(closeImg_MouseClick);
                closeImg.MouseDown += new MouseEventHandler(closeImg_MouseDown);
                maxImage.MouseEnter += new EventHandler(maxImage_MouseEnter);
                maxImage.MouseLeave += new EventHandler(maxImage_MouseLeave);
                maxImage.MouseClick += new MouseEventHandler(maxImage_MouseClick);
                maxImage.MouseDown += new MouseEventHandler(maxImage_MouseDown);
                minImg.MouseEnter += new EventHandler(minImg_MouseEnter);
                minImg.MouseLeave += new EventHandler(minImg_MouseLeave);
                minImg.MouseClick += new MouseEventHandler(minImg_MouseClick);
                minImg.MouseDown += new MouseEventHandler(minImg_MouseDown);
            }
            else
            {
                panelTitleBar.Visible = isModern;
            }
        }


        #region Events
        private void reportForm_Load(object sender, EventArgs e)
        {
            Program.applyTheme(this);
            totalBalanceLabel.ForeColor = ThemeColor.darkColor;
            usedBalanceLabel.ForeColor = ThemeColor.darkColor;
            availableBalanceLabel.ForeColor = ThemeColor.darkColor;
            fromDatePicker.Value = new DateTime(DateTime.Now.Year, 1, 1);
            toDatePicker.Value = new DateTime(DateTime.Now.Year, 12, 31);
            filtersGroupBox.Visible = hasManagerRole || hasAdminRole;
            nameORdepDropdown.Visible = hasManagerRole || hasAdminRole;
            filterDropdown.SelectedIndex = hasManagerRole || hasAdminRole ? 0 : 1;
            nameORdepDropdown.SelectedItem = hasManagerRole || hasAdminRole ? "All" : loggedInUser.FullName;
            statusDropdown.SelectedIndex = 0;
            typeDropdown.SelectedIndex = 0;
            updateReport();
        }
        private void reportsDataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (reportsDataGridView.Columns[e.ColumnIndex].Name == "Status")
            {
                if (e.Value != null && e.Value.ToString() == "Approved")
                {
                    e.CellStyle.ForeColor = Color.Green;
                }
                else if (e.Value != null && e.Value.ToString() == "Rejected")
                {
                    e.CellStyle.ForeColor = Color.Red;
                }
                else
                {
                    e.CellStyle.ForeColor = Color.Blue;
                }
            }
            // Ensure you're handling the correct columns
            if (reportsDataGridView.Columns[e.ColumnIndex].Name == "Days" || reportsDataGridView.Columns[e.ColumnIndex].Name == "Hours")
            {
                // Check if e.Value is not null
                if (e.Value != null)
                {
                    // Handle "Days" column (if it's integer-based)
                    if (e.Value is int && (int)e.Value == 0)
                    {
                        e.Value = "-";
                        e.FormattingApplied = true;
                    }
                    // Handle "Hours" column (if it's a double or float-based value)
                    else if (e.Value is double && (double)e.Value == 0.00)
                    {
                        e.Value = "-";
                        e.FormattingApplied = true;
                    }
                }
            }
        }
        private void reportForm_Resize(object sender, EventArgs e)
        {
            Invalidate();
        }
        private void filterDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            filterLabel.Text = filterDropdown.Text;

            Func<List<string>> updateMethod;

            if (filterDropdown.Text == "Department")
            {
                updateMethod = hasAdminRole
                    ? new Func<List<string>>(getDepartments)
                    : new Func<List<string>>(getDepartmentsOfManager);
            }
            else
            {
                updateMethod = hasAdminRole
                    ? new Func<List<string>>(getNames)
                    : new Func<List<string>>(() => getNamesForManager(getDepartmentsOfManager()));
            }

            updateDropDown(updateMethod);
        }
        private void typeDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            updateBalanceLabels();
        }
        private void nameORdepDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            updateBalanceLabels();
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
        private void panelTitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 1)
            {
                ReleaseCapture();
                SendMessage(Handle, 0x112, 0xf012, 0);
            }
        }
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Invalidate();  // Forces the form to repaint when resized
        }
        private void closeImg_MouseEnter(object sender, EventArgs e)
        {
            closeImg.Image = Properties.Resources.close_hover;
        }
        private void closeImg_MouseLeave(object sender, EventArgs e)
        {
            closeImg.Image = Properties.Resources.close_nofocus;
        }
        private void closeImg_MouseClick(object sender, MouseEventArgs e)
        {
            Close();
        }
        private void closeImg_MouseDown(object sender, MouseEventArgs e)
        {
            closeImg.Image = Properties.Resources.close_press;
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
        private void maxImage_MouseClick(object sender, MouseEventArgs e)
        {
            WindowState = WindowState == FormWindowState.Normal ? FormWindowState.Maximized : FormWindowState.Normal;
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
        private void minImg_MouseClick(object sender, MouseEventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }
        #endregion

        #region Methods
        private void updateReport()
        {
            DateTime startDate = fromDatePicker.Value.Date;
            DateTime endDate = toDatePicker.Value.Date;

            if (startDate > endDate)
            {
                MessageBox.Show("Ending date can't be earlier than the beginning date.");
                return;
            }

            string baseQuery = @"SELECT RequestUserFullName, RequestType, RequestNumberOfDays, RequestStatus, RequestFromDay, RequestDepartment, RequestBeginningTime, RequestEndingTime
                         FROM Requests
                         WHERE CONVERT(date, RequestFromDay, 120) >= @startDate 
                         AND CONVERT(date, RequestFromDay, 120) <= @endDate";

            // Determine filter condition based on dropdown selections
            string condition = "";

            if (filterDropdown.Text == "Department" && nameORdepDropdown.Text != "All")
            {
                condition = " AND RequestDepartment = @filterValue";
            }
            else if (filterDropdown.Text == "Name" && nameORdepDropdown.Text != "All")
            {
                condition = " AND RequestUserFullName = @filterValue";
            }

            if (statusDropdown.Text != "All")
            {
                condition += " AND RequestStatus = @status";
            }

            if (typeDropdown.Text != "All")
            {
                condition += " AND RequestType = @type";
            }

            // If the user has a manager role, add condition to filter by manager's departments
            string departmentsCondition = "";
            if (hasManagerRole)
            {
                List<string> managerDepartments = getDepartmentsOfManager();
                if (managerDepartments.Count > 0)
                {
                    departmentsCondition = " AND RequestDepartment IN (" +
                        string.Join(", ", managerDepartments.Select((_, i) => $"@department{i}")) + ")";
                }
            }

            // Combine all conditions and add ORDER BY
            string orderByClause = " ORDER BY RequestFromDay ASC";

            using (SqlConnection connection = Database.getConnection())
            {
                SqlCommand command = new SqlCommand(baseQuery + condition + departmentsCondition + orderByClause, connection);
                command.Parameters.AddWithValue("@startDate", startDate);
                command.Parameters.AddWithValue("@endDate", endDate);

                // Add parameter for filter value if applicable
                if (!string.IsNullOrEmpty(condition))
                {
                    command.Parameters.AddWithValue("@filterValue", nameORdepDropdown.Text);
                }

                if (statusDropdown.Text != "All")
                {
                    command.Parameters.AddWithValue("@status", statusDropdown.Text);
                }

                if (typeDropdown.Text != "All")
                {
                    command.Parameters.AddWithValue("@type", typeDropdown.Text);
                }

                // Add parameters for manager's departments if the user has a manager role
                if (hasManagerRole)
                {
                    List<string> managerDepartments = getDepartmentsOfManager();
                    for (int i = 0; i < managerDepartments.Count; i++)
                    {
                        command.Parameters.AddWithValue($"@department{i}", managerDepartments[i]);
                    }
                }

                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);


                    //  DataGridViewTextBoxColumn hoursColumn = new DataGridViewTextBoxColumn();
                    //  hoursColumn.Name = "Hours";
                    //  hoursColumn.HeaderText = "Hours";
                    //  hoursColumn.ValueType = typeof(double);  // Set type to double
                    //    reportsDataGridView.Columns.Add(hoursColumn);

                    // Add a new "Hours" column to the DataTable to store the calculated hours
                     dataTable.Columns.Add("Hours", typeof(double));
                    // Loop through each row and calculate the difference between EndingTime and BeginningTime

                    foreach (DataRow row in dataTable.Rows)
                    {
                        // Check if both the BeginningTime and EndingTime are not DBNull
                        if (row["RequestBeginningTime"] != DBNull.Value && row["RequestEndingTime"] != DBNull.Value)
                        {
                            // Safely cast to TimeSpan
                            TimeSpan beginTime = (TimeSpan)row["RequestBeginningTime"];
                            TimeSpan endTime = (TimeSpan)row["RequestEndingTime"];

                            // Calculate the difference in hours (TotalHours gives the result as a double)
                            double hoursDifference = endTime.TotalHours - beginTime.TotalHours;

                            // Assign the calculated hours as double to the Hours column
                            row["Hours"] = hoursDifference;  // Explicitly store as double
                        }
                        else
                        {
                            // If either time is DBNull, set the Hours column to 0
                            row["Hours"] = 0;  // Default to 0 hours if either time is null
                        }
                    }

                    // Remove the RequestBeginningTime and RequestEndingTime columns from the DataTable
                    dataTable.Columns.Remove("RequestBeginningTime");
                    dataTable.Columns.Remove("RequestEndingTime");

                    // Bind the result to the DataGridView
                    reportsDataGridView.DataSource = dataTable;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading the report:\n{ex.Message}\n\nQuery: {command.CommandText}");
                }
            }
        }
        public static List<string> getDepartmentsOfManager()
        {
            List<string> departments = new List<string>();

            try
            {
                using (var connection = Database.getConnection())
                {
                    connection.Open();

                    string query = "SELECT Department FROM Users Where UserID = @UserID";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@UserID", loggedInUser.userID);

                        using (var reader = command.ExecuteReader())
                        {

                            while (reader.Read())
                            {
                                if (!reader.IsDBNull(0))
                                {
                                    string departmentData = reader.GetString(0);
                                    string[] splitDepartments = departmentData.Split(new[] { " - " }, StringSplitOptions.RemoveEmptyEntries);
                                    foreach (var department in splitDepartments)
                                    {
                                        if (!departments.Contains(department.Trim()))
                                        {
                                            departments.Add(department.Trim());
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error fetching departments: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            departments.Sort();
            return departments;
        }
        private List<string> getNamesForManager(List<string> departments)
        {
            var users = new List<string>();

            try
            {
                using (var connection = Database.getConnection())
                {
                    connection.Open();

                    // Build a query with parameters for departments
                    string query = "SELECT DISTINCT FullName FROM Users WHERE Department IN (@Departments)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Add parameters for the departments
                        string departmentParameter = string.Join(",", departments.Select((d, i) => $"@Dept{i}"));
                        command.CommandText = query.Replace("@Departments", departmentParameter);

                        // Add each department as a parameter
                        for (int i = 0; i < departments.Count; i++)
                        {
                            command.Parameters.AddWithValue($"@Dept{i}", departments[i]);
                        }

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                if (!reader.IsDBNull(0))
                                {
                                    users.Add(reader.GetString(0));
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error fetching users: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            users.Sort();
            return users;
        }
        private void updateDropDown(Func<List<string>> method)
        {
            nameORdepDropdown.Items.Clear();

            List<string> list = method();

            foreach (string item in list)
            {
                nameORdepDropdown.Items.Add(item);
            }

            nameORdepDropdown.Items.Insert(0, "All");

            nameORdepDropdown.SelectedIndex = 0;
        }
        private List<string> getNames()
        {
            List<string> names = new List<string>();

            string query = "SELECT DISTINCT RequestUserFullName " +
                           "FROM Requests";

            using (SqlConnection connection = Database.getConnection())
            {
                SqlCommand command = new SqlCommand(query, connection);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        string name = reader["RequestUserFullName"].ToString();
                        names.Add(name);
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error retrieving departments: " + ex.Message);
                }
            }

            names.Sort();
            return names;
        }
        private int getUsedLeaveDays(string leaveType, int? userID = null, string userName = null)
        {
            int days = 0;

            try
            {
                using (SqlConnection conn = Database.getConnection())
                {
                    conn.Open();

                    // Determine the query based on the provided parameters
                    string query;
                    if (userID.HasValue)
                    {
                        query = $"SELECT {leaveType} FROM AnnualLeave WHERE UserID = @UserID";
                    }
                    else if (!string.IsNullOrEmpty(userName))
                    {
                        query = $"SELECT {leaveType} FROM AnnualLeave WHERE FullName = @FullName";
                    }
                    else
                    {
                        throw new ArgumentException("Either userID or FullName must be provided.");
                    }

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Add the appropriate parameter
                        if (userID.HasValue)
                        {
                            cmd.Parameters.AddWithValue("@UserID", userID.Value);
                        }
                        else if (!string.IsNullOrEmpty(userName))
                        {
                            cmd.Parameters.AddWithValue("@FullName", userName);
                        }

                        object result = cmd.ExecuteScalar();

                        if (result != null && int.TryParse(result.ToString(), out int sum))
                        {
                            days = sum;
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("A database error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An unexpected error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return days;
        }
        private void updateBalanceLabels()
        {
            if (filterDropdown.Text == "Name" && nameORdepDropdown.Text != "All")
            {
                bool isAnnualOrEmergencyOrEmergency = typeDropdown.Text == "Annual" || typeDropdown.Text == "Emergency" || typeDropdown.Text == "Permission";

                balanceGroupBox.Visible = isAnnualOrEmergencyOrEmergency;

                if (isAnnualOrEmergencyOrEmergency)
                {
                    string leaveType = typeDropdown.Text == "Annual" ? "Annual" : typeDropdown.Text == "Emergency" ? "CasualLeave" : "Permissions";
                    string usedLeaveType = typeDropdown.Text == "Annual" ? "AnnualUsed" : typeDropdown.Text == "Emergency" ? "CasualUsed" : "PermissionsUsed";

                    totalBalanceLabel.Text = getLeaveDays(leaveType, userName: nameORdepDropdown.Text).ToString();
                    usedBalanceLabel.Text = getUsedLeaveDays(usedLeaveType, userName: nameORdepDropdown.Text).ToString();

                    int totalBalance = int.Parse(totalBalanceLabel.Text);
                    int usedBalance = int.Parse(usedBalanceLabel.Text);

                    availableBalanceLabel.Text = (totalBalance - usedBalance).ToString();
                }
            }
        }
        #endregion

        #region Buttons
        private void generateButton_Click(object sender, EventArgs e)
        {
            updateReport();

        }
        #endregion

    }
}