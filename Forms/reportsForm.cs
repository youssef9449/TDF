using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
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
            Program.applyTheme(this);
            updateDepartments();

        }


        #region Events
        private void reportForm_Load(object sender, EventArgs e)
        {
            //filterDropdown.Text = "";
            //depDropdown.SelectedIndex = -1;

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

        #endregion

        #region Methods
        private void updateDepartments()
        {
            departments = getDepartments();

            foreach (string department in departments)
            {
                depDropdown.Items.Add(department);
            }

            depDropdown.Items.Insert(0, "All");

            depDropdown.SelectedIndex = 0;
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

            string baseQuery = "Select RequestUserFullName, RequestType, RequestNumberOfDays, RequestStatus, RequestFromDay, RequestDepartment from Requests " +
                "Where CONVERT(date, RequestFromDay, 120) >=  @startDate AND CONVERT(date, RequestFromDay, 120) <= @endDate";

            string condition = depDropdown.Text != "All" ? " AND RequestDepartment = @department" : ""; // Filter by department if selected
            using (SqlConnection connection = Database.getConnection())
            {
                SqlCommand command = new SqlCommand(baseQuery + condition, connection);
                command.Parameters.AddWithValue("@startDate", startDate);
                command.Parameters.AddWithValue("@endDate", endDate);

                if (depDropdown.Text != "All")
                {
                    command.Parameters.AddWithValue("@department", depDropdown.Text);
                }
                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    reportsDataGridView.DataSource = dataTable;

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading the report: " + ex.Message);
                }
            }

        }
        #endregion

        private void generateButton_Click(object sender, EventArgs e)
        {
            updateReport();
        }
    }
}
