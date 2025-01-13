using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TDF.Net;
using TDF.Net.Classes;
using static TDF.Forms.reportsForm;
using static TDF.Net.mainForm;

namespace TDF.Forms
{
    public partial class balanceForm : Form
    {
        public balanceForm(bool isModern)
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            if (isModern)
            {
                controlBox.Visible = !isModern;
                panel.MouseDown += new MouseEventHandler(panel_MouseDown);

            }
            else
            {
                panel.Visible = isModern;
            }
        }


        #region Events
        private void balanceForm_Load(object sender, EventArgs e)
        {
            Program.applyTheme(this);
            getBalanceTable();

        }
        private void balanceForm_Resize(object sender, EventArgs e)
        {
            Invalidate();
        }
        private void reportsDataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value != null)
            {
                if (e.Value is int && (int)e.Value == 0)
                {
                    e.Value = "-";
                    e.FormattingApplied = true;
                }
            }
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
        private void panel_Paint(object sender, PaintEventArgs e)
        {
            base.OnPaint(e);

            // Get the form's scroll position
            Point scrollPos = AutoScrollPosition;

            // Adjust for scroll position when drawing the border
            Rectangle rect = new Rectangle(ClientRectangle.X - scrollPos.X, ClientRectangle.Y - scrollPos.Y, ClientRectangle.Width, ClientRectangle.Height);

            ControlPaint.DrawBorder(e.Graphics, rect, ThemeColor.darkColor, ButtonBorderStyle.Solid);
        }
        private void panel_MouseDown(object sender, MouseEventArgs e)
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

        #endregion

        #region Methods
        private void getBalanceTable()
        {
            List<string> departments = getDepartmentsOfManager();

            // Base query for all users
            string query = @"SELECT AL.UserID, AL.FullName, AL.Annual, AL.CasualLeave, AL.AnnualUsed, AL.CasualUsed, 
                            AL.AnnualBalance, AL.CasualBalance, AL.Permissions, AL.PermissionsUsed, 
                            AL.PermissionsBalance, AL.UnpaidUsed
                     FROM AnnualLeave AL
                     INNER JOIN Users U ON AL.UserID = U.UserID";

            // Append the department condition for managers
            if (hasManagerRole && departments.Count > 0)
            {
                query += $" WHERE U.Department IN ({string.Join(", ", departments.Select((_, i) => $"@Dept{i}"))})";
            }
            try
            {
                using (SqlConnection connection = Database.getConnection())
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Add department parameters only if the user is a manager and has departments
                        if (hasManagerRole && departments.Count > 0)
                        {
                            for (int i = 0; i < departments.Count; i++)
                            {
                                command.Parameters.AddWithValue($"@Dept{i}", departments[i]);
                            }
                        }

                        // Execute the query and fill the DataTable
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);

                            // Bind the DataTable to the DataGridView
                            balanceDataGridView.DataSource = dataTable;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle any errors
                MessageBox.Show($"Error loading data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Buttons
        private void refreshButton_Click(object sender, EventArgs e)
        {
            getBalanceTable();
        }
        #endregion


    }
}