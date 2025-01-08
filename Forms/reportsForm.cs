using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using TDF.Classes;
using TDF.Net;
using static TDF.Net.loginForm;

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
            filterDropdown.SelectedIndex = 0;
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
        #endregion

        #region Methods
        private void updateDropDown(Func<List<string>> method)
        {
            depnameDropdown.Items.Clear();

            List<string> list = method();

            foreach (string item in list)
            {
                depnameDropdown.Items.Add(item);
            }

            depnameDropdown.Items.Insert(0, "All");

            depnameDropdown.SelectedIndex = 0;
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
            if (filterDropdown.Text == "Department" && depnameDropdown.Text != "All")
            {
                condition = " AND RequestDepartment = @filterValue";
            }
            else if (filterDropdown.Text == "Name" && depnameDropdown.Text != "All")
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
                    command.Parameters.AddWithValue("@filterValue", depnameDropdown.Text);
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
        #endregion

        #region Buttons
        private void generateButton_Click(object sender, EventArgs e)
        {
            updateReport();
        }
        #endregion

    }
}
