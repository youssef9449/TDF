﻿using Bunifu.UI.WinForms;
using TDF.Net;
using TDF.Net.Classes;
using System;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using TDF.Classes;

namespace TDF.Forms
{
    public partial class settingsForm : Form
    {
        public settingsForm()
        {
            InitializeComponent();
            Program.loadForm(this);
        }


        #region Events
        private void settingsForm_Load(object sender, EventArgs e)
        {
            updateForm(loginForm.loggedInUser);
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            ControlPaint.DrawBorder(e.Graphics, ClientRectangle, ThemeColor.SecondaryColor, ButtonBorderStyle.Solid);
        }
        private void panel_Paint(object sender, PaintEventArgs e)
        {
            base.OnPaint(e);
            ControlPaint.DrawBorder(e.Graphics, ClientRectangle, ThemeColor.SecondaryColor, ButtonBorderStyle.Solid);
        }
        private void panel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 1)
            {
                mainForm.ReleaseCapture();
                mainForm.SendMessage(Handle, 0x112, 0xf012, 0);
            }
        }
        #endregion

        #region Methods
        private void updateForm(User user)
        {
            userPictureBox.Image = user.Picture != null ? user.Picture : userPictureBox.Image;
            nameLabel.Text = user.FullName;
            roleLabel.Text = user.Role;
            depLabel.Text = user.Department;
        }

        public void UpdatePassword(string username, BunifuTextBox oldPasswordTextBox, BunifuTextBox newPasswordTextBox)
        {
            string oldPassword = oldPasswordTextBox.Text;
            string newPassword = newPasswordTextBox.Text;

            using (SqlConnection conn = Database.GetConnection())
            {
                conn.Open();

                // Retrieve the current salt and password hash for the user
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

                            // Hash the old password with the stored salt to verify it
                            string oldPasswordHash = Security.hashPassword(oldPassword, storedSalt);

                            if (oldPasswordHash == storedHash)
                            {
                                reader.Close(); // Close the reader before executing another command

                                // Generate a new salt and hash for the new password
                                string newSalt = Security.generateSalt();
                                string newPasswordHash = Security.hashPassword(newPassword, newSalt);

                                // Update the password and salt in the database
                                string updateQuery = "UPDATE Users SET PasswordHash = @NewPasswordHash, Salt = @NewSalt WHERE UserName = @UserName";
                                using (SqlCommand updateCmd = new SqlCommand(updateQuery, conn))
                                {
                                    updateCmd.Parameters.AddWithValue("@NewPasswordHash", newPasswordHash);
                                    updateCmd.Parameters.AddWithValue("@NewSalt", newSalt);
                                    updateCmd.Parameters.AddWithValue("@UserName", username);

                                    int rowsAffected = updateCmd.ExecuteNonQuery();

                                    if (rowsAffected > 0)
                                    {
                                        MessageBox.Show("Password updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        bunifuTextBox2.Clear();
                                        bunifuTextBox3.Clear();
                                    }
                                    else
                                    {
                                        MessageBox.Show("Password update failed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show("Old password is incorrect.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            MessageBox.Show("User not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }
        public void UpdateFullName(string username, BunifuTextBox fullNameTextBox)
        {
            string newFullName = fullNameTextBox.Text;

            using (SqlConnection conn = Database.GetConnection())
            {
                conn.Open();

                // SQL query to update FullName in the database
                string query = "UPDATE Users SET FullName = @FullName WHERE UserName = @UserName";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@FullName", newFullName);
                    cmd.Parameters.AddWithValue("@UserName", username);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {

                        MessageBox.Show("Full Name updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Failed to update Full Name.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                string secondQuery;
                
                secondQuery = mainForm.hasManagerRole ? "UPDATE Requests SET RequestCloser = @newFullName " +
                    "WHERE RequestCloser = @oldFullUserName" : "UPDATE Requests SET RequestUserFullName = @newFullName " +
                    "WHERE RequestUserFullName = @oldFullUserName";

                using (SqlCommand cmd = new SqlCommand(secondQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@newFullName", newFullName);
                    cmd.Parameters.AddWithValue("@oldFullUserName", nameLabel.Text);

                    cmd.ExecuteNonQuery();
                }

                loginForm.loggedInUser.FullName = newFullName;
                nameLabel.Text = newFullName;
                mainForm.updatedUserData = true;
                bunifuTextBox1.Clear();

            }
        }
        #endregion

        private void pictureButton_Click(object sender, EventArgs e)
        {
           
        }
        private void updateButton_Click(object sender, EventArgs e)
        {
            string currentUserName = loginForm.loggedInUser.UserName; // assuming you have stored the current user's UserName

            // Check if there’s text in the FullName textbox and update if needed
            if (!string.IsNullOrWhiteSpace(bunifuTextBox1.Text))
            {
                UpdateFullName(currentUserName, bunifuTextBox1);
            }

            // Check if there’s text in the password boxes and update if needed
            if (!string.IsNullOrWhiteSpace(bunifuTextBox2.Text) && !string.IsNullOrWhiteSpace(bunifuTextBox3.Text))
            {
                UpdatePassword(currentUserName, bunifuTextBox2, bunifuTextBox3);
            }
        }

    }
}
