using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using TDF.Classes;
using TDF.Net;
using static TDF.Net.Forms.addRequestForm;
using static TDF.Net.loginForm;
using static TDF.Net.mainForm;


namespace TDF.Forms
{
    public partial class reportsForm : Form
    {
        public reportsForm()
        {
            InitializeComponent();
        }


        #region Events
        private void reportForm_Load(object sender, EventArgs e)
        {
            Program.applyTheme(this);
            totalBalanceLabel.ForeColor = ThemeColor.darkColor;
            usedBalanceLabel.ForeColor = ThemeColor.darkColor;
            availableBalanceLabel.ForeColor = ThemeColor.darkColor;
            filtersGroupBox.Visible = hasManagerRole || hasAdminRole;
            nameORdepDropdown.Visible = hasManagerRole || hasAdminRole;
            filterDropdown.SelectedIndex = hasManagerRole || hasAdminRole ? 0 : 1;
            nameORdepDropdown.SelectedIndex = nameORdepDropdown.Items.Count == 1 || hasManagerRole || hasAdminRole ? 0 :  1;
            statusDropdown.SelectedIndex = 0;
            typeDropdown.SelectedIndex = 0;
        }
        private void reportForm_Resize(object sender, EventArgs e)
        {
            Invalidate();
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            ControlPaint.DrawBorder(e.Graphics, ClientRectangle, ThemeColor.darkColor, ButtonBorderStyle.Solid);
        }
        private void filterDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            filterLabel.Text = filterDropdown.Text;

            if (filterDropdown.Text == "Department")
            {
                updateDropDown(getDepartments);
            }
            else
            {
                updateDropDown(getNames);
            }
        }
        private void typeDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (filterDropdown.Text == "Name" && nameORdepDropdown.Text != "All")
            {
                bool isAnnualOrEmergency = typeDropdown.Text == "Annual" || typeDropdown.Text == "Emergency";

                balanceGroupBox.Visible = isAnnualOrEmergency;

                if (isAnnualOrEmergency)
                {
                    string leaveType = typeDropdown.Text == "Annual" ? "Annual" : "CasualLeave";
                    string usedLeaveType = typeDropdown.Text == "Annual" ? "AnnualUsed" : "CasualUsed";

                    totalBalanceLabel.Text = getLeaveDays(leaveType, userName: nameORdepDropdown.Text).ToString();
                    usedBalanceLabel.Text = getUsedLeaveDays(usedLeaveType, userName: nameORdepDropdown.Text).ToString();

                    int totalBalance = int.Parse(totalBalanceLabel.Text);
                    int usedBalance = int.Parse(usedBalanceLabel.Text);

                    availableBalanceLabel.Text = (totalBalance - usedBalance).ToString();
                }
            }
        }
        private void nameORdepDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (filterDropdown.Text == "Name" && nameORdepDropdown.Text != "All")
            {
                bool isAnnualOrEmergency = typeDropdown.Text == "Annual" || typeDropdown.Text == "Emergency";

                balanceGroupBox.Visible = isAnnualOrEmergency;

                if (isAnnualOrEmergency)
                {
                    string leaveType = typeDropdown.Text == "Annual" ? "Annual" : "CasualLeave";
                    string usedLeaveType = typeDropdown.Text == "Annual" ? "AnnualUsed" : "CasualUsed";

                    totalBalanceLabel.Text = getLeaveDays(leaveType, userName: nameORdepDropdown.Text).ToString();
                    usedBalanceLabel.Text = getUsedLeaveDays(usedLeaveType, userName: nameORdepDropdown.Text).ToString();

                    int totalBalance = int.Parse(totalBalanceLabel.Text);
                    int usedBalance = int.Parse(usedBalanceLabel.Text);

                    availableBalanceLabel.Text = (totalBalance - usedBalance).ToString();
                }
            }
        }
        #endregion

        #region Methods
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
        private void updateReport()
        {
            DateTime startDate = fromDatePicker.Value.Date;
            DateTime endDate = toDatePicker.Value.Date;

            if (startDate > endDate)
            {
                MessageBox.Show("Ending date can't be earlier than the beginning date.");
                return;
            }

            string baseQuery = @"SELECT RequestUserFullName, RequestType, RequestNumberOfDays, RequestStatus, RequestFromDay, RequestDepartment
                                 FROM Requests
                                 WHERE CONVERT(date, RequestFromDay, 120) >= @startDate 
                                 AND CONVERT(date, RequestFromDay, 120) <= @endDate";

            // Determine filter condition based on the dropdown selection
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

            using (SqlConnection connection = Database.getConnection())
            {
                SqlCommand command = new SqlCommand(baseQuery + condition, connection);
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

                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    // Bind the result to the DataGridView
                    reportsDataGridView.DataSource = dataTable;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading the report:\n{ex.Message}\n\nQuery: {command.CommandText}");
                }
            }
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

        #endregion

        #region Buttons
        private void generateButton_Click(object sender, EventArgs e)
        {
            updateReport();

        }

        #endregion

    }
}
