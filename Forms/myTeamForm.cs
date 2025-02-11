using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using TDF.Net;
using TDF.Net.Classes;
using static TDF.Forms.reportsForm;
using static TDF.Net.mainForm;

namespace TDF.Forms
{
    public partial class myTeamForm : Form
    {
        public myTeamForm(bool isModern)
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
        private void balanceDataGridView_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            // Ensure the cell is from the "Picture" column
            /*  if (e.RowIndex >= 0 && e.ColumnIndex == balanceDataGridView.Columns["Picture"].Index)
              {
                  // Get the cell bounds
                  var cellBounds = e.CellBounds;

                  // Ensure the bounds are square (use the smaller dimension for both width and height)
                  int size = Math.Min(cellBounds.Width, cellBounds.Height);
                  Rectangle squareBounds = new Rectangle(cellBounds.X, cellBounds.Y, size, size);

                  // Check if the cell contains an image
                  if (e.Value is Image img)
                  {
                      // Create a circular clipping region
                      using (GraphicsPath path = new GraphicsPath())
                      {
                          // Add a circle that fits within the square bounds
                          path.AddEllipse(squareBounds);
                          e.Graphics.SetClip(path); // Apply the circular clipping region

                          // Draw the image, scaling it to fit inside the circle
                          e.Graphics.DrawImage(img, squareBounds);

                          // Reset the clipping region
                          e.Graphics.ResetClip();
                      }

                      // Mark the event as handled to prevent the default cell painting
                      e.Handled = true;
                  }
              }*/
        }
        #endregion

        #region Methods
        private void getBalanceTable()
        {
            List<string> departments = getDepartmentsOfManager();

            // Base query for all users
            string query = @"SELECT AL.UserID, AL.FullName, AL.Annual, AL.CasualLeave, AL.AnnualUsed, AL.CasualUsed, 
                 AL.AnnualBalance, AL.CasualBalance, AL.Permissions, AL.PermissionsUsed, 
                 AL.PermissionsBalance, AL.UnpaidUsed, U.Picture
                 FROM AnnualLeave AL
                 INNER JOIN Users U ON AL.UserID = U.UserID
                 WHERE U.Role != 'Admin'";  // Exclude 'Admin' users


            // Append the department condition for managers (correctly placed BEFORE ORDER BY)
            if (hasManagerRole && departments.Count > 0)
            {
                string deptConditions = string.Join(", ", departments.Select((_, i) => $"@Dept{i}"));
                query += $" And U.Department IN ({deptConditions})";
            }

            // Add ORDER BY after filtering
            query += " ORDER BY AL.FullName ASC";

            try
            {
                using (SqlConnection connection = Database.getConnection())
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Add department parameters correctly
                        if (hasManagerRole && departments.Count > 0)
                        {
                            for (int i = 0; i < departments.Count; i++)
                            {
                                command.Parameters.AddWithValue($"@Dept{i}", departments[i]);
                            }
                        }

                        // Execute query and bind to DataGridView
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);

                            // Handle NULL images
                            foreach (DataRow row in dataTable.Rows)
                            {
                                if (row["Picture"] == DBNull.Value)
                                {
                                    row["Picture"] = ImageToByteArray(Properties.Resources.pngegg);
                                }
                            }

                            // Bind DataTable to DataGridView
                            balanceDataGridView.DataSource = dataTable;

                            // Set Picture column to display images properly
                            if (balanceDataGridView.Columns["Picture"] is DataGridViewImageColumn imgCol)
                            {
                                imgCol.ImageLayout = DataGridViewImageCellLayout.Zoom;
                            }
                        }
                    }

                    // Set column order explicitly
                    balanceDataGridView.Columns["FullName"].DisplayIndex = 1;
                    balanceDataGridView.Columns["Annual"].DisplayIndex = 2;
                    balanceDataGridView.Columns["AnnualUsed"].DisplayIndex = 3;
                    balanceDataGridView.Columns["AnnualBalance"].DisplayIndex = 4;
                    balanceDataGridView.Columns["UnpaidUsed"].DisplayIndex = balanceDataGridView.Columns.Count - 1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private byte[] ImageToByteArray(Image image)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                return ms.ToArray();
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