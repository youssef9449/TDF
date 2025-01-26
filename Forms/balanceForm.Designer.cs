namespace TDF.Forms
{
    partial class balanceForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(balanceForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderEdges borderEdges1 = new Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderEdges();
            this.controlBox = new Bunifu.UI.WinForms.BunifuFormControlBox();
            this.balanceDataGridView = new Bunifu.UI.WinForms.BunifuDataGridView();
            this.UserID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FullName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Annual = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AnnualUsed = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AnnualBalance = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CasualLeave = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CasualUsed = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CasualBalance = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UnpaidUsed = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Permissions = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PermissionsUsed = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PermissionsBalance = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.refreshButton = new Bunifu.UI.WinForms.BunifuButton.BunifuButton();
            this.panel = new System.Windows.Forms.Panel();
            this.bunifuFormControlBox1 = new Bunifu.UI.WinForms.BunifuFormControlBox();
            ((System.ComponentModel.ISupportInitialize)(this.balanceDataGridView)).BeginInit();
            this.panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // controlBox
            // 
            this.controlBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.controlBox.BackColor = System.Drawing.Color.Transparent;
            this.controlBox.BunifuFormDrag = null;
            this.controlBox.CloseBoxOptions.BackColor = System.Drawing.Color.Transparent;
            this.controlBox.CloseBoxOptions.BorderRadius = 0;
            this.controlBox.CloseBoxOptions.Enabled = true;
            this.controlBox.CloseBoxOptions.EnableDefaultAction = true;
            this.controlBox.CloseBoxOptions.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(17)))), ((int)(((byte)(35)))));
            this.controlBox.CloseBoxOptions.Icon = ((System.Drawing.Image)(resources.GetObject("controlBox.CloseBoxOptions.Icon")));
            this.controlBox.CloseBoxOptions.IconAlt = null;
            this.controlBox.CloseBoxOptions.IconColor = System.Drawing.Color.Black;
            this.controlBox.CloseBoxOptions.IconHoverColor = System.Drawing.Color.White;
            this.controlBox.CloseBoxOptions.IconPressedColor = System.Drawing.Color.White;
            this.controlBox.CloseBoxOptions.IconSize = new System.Drawing.Size(18, 18);
            this.controlBox.CloseBoxOptions.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(17)))), ((int)(((byte)(35)))));
            this.controlBox.ForeColor = System.Drawing.Color.White;
            this.controlBox.HelpBox = false;
            this.controlBox.HelpBoxOptions.BackColor = System.Drawing.Color.Transparent;
            this.controlBox.HelpBoxOptions.BorderRadius = 0;
            this.controlBox.HelpBoxOptions.Enabled = true;
            this.controlBox.HelpBoxOptions.EnableDefaultAction = true;
            this.controlBox.HelpBoxOptions.HoverColor = System.Drawing.Color.LightGray;
            this.controlBox.HelpBoxOptions.Icon = ((System.Drawing.Image)(resources.GetObject("controlBox.HelpBoxOptions.Icon")));
            this.controlBox.HelpBoxOptions.IconAlt = null;
            this.controlBox.HelpBoxOptions.IconColor = System.Drawing.Color.Black;
            this.controlBox.HelpBoxOptions.IconHoverColor = System.Drawing.Color.Black;
            this.controlBox.HelpBoxOptions.IconPressedColor = System.Drawing.Color.Black;
            this.controlBox.HelpBoxOptions.IconSize = new System.Drawing.Size(22, 22);
            this.controlBox.HelpBoxOptions.PressedColor = System.Drawing.Color.Silver;
            this.controlBox.Location = new System.Drawing.Point(1439, 11);
            this.controlBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.controlBox.MaximizeBox = false;
            this.controlBox.MaximizeBoxOptions.BackColor = System.Drawing.Color.Transparent;
            this.controlBox.MaximizeBoxOptions.BorderRadius = 0;
            this.controlBox.MaximizeBoxOptions.Enabled = true;
            this.controlBox.MaximizeBoxOptions.EnableDefaultAction = true;
            this.controlBox.MaximizeBoxOptions.HoverColor = System.Drawing.Color.LightGray;
            this.controlBox.MaximizeBoxOptions.Icon = ((System.Drawing.Image)(resources.GetObject("controlBox.MaximizeBoxOptions.Icon")));
            this.controlBox.MaximizeBoxOptions.IconAlt = ((System.Drawing.Image)(resources.GetObject("controlBox.MaximizeBoxOptions.IconAlt")));
            this.controlBox.MaximizeBoxOptions.IconColor = System.Drawing.Color.White;
            this.controlBox.MaximizeBoxOptions.IconHoverColor = System.Drawing.Color.Black;
            this.controlBox.MaximizeBoxOptions.IconPressedColor = System.Drawing.Color.Black;
            this.controlBox.MaximizeBoxOptions.IconSize = new System.Drawing.Size(16, 16);
            this.controlBox.MaximizeBoxOptions.PressedColor = System.Drawing.Color.Silver;
            this.controlBox.MinimizeBox = false;
            this.controlBox.MinimizeBoxOptions.BackColor = System.Drawing.Color.Transparent;
            this.controlBox.MinimizeBoxOptions.BorderRadius = 0;
            this.controlBox.MinimizeBoxOptions.Enabled = true;
            this.controlBox.MinimizeBoxOptions.EnableDefaultAction = true;
            this.controlBox.MinimizeBoxOptions.HoverColor = System.Drawing.Color.LightGray;
            this.controlBox.MinimizeBoxOptions.Icon = ((System.Drawing.Image)(resources.GetObject("controlBox.MinimizeBoxOptions.Icon")));
            this.controlBox.MinimizeBoxOptions.IconAlt = null;
            this.controlBox.MinimizeBoxOptions.IconColor = System.Drawing.Color.White;
            this.controlBox.MinimizeBoxOptions.IconHoverColor = System.Drawing.Color.Black;
            this.controlBox.MinimizeBoxOptions.IconPressedColor = System.Drawing.Color.Black;
            this.controlBox.MinimizeBoxOptions.IconSize = new System.Drawing.Size(14, 14);
            this.controlBox.MinimizeBoxOptions.PressedColor = System.Drawing.Color.Silver;
            this.controlBox.Name = "controlBox";
            this.controlBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.controlBox.ShowDesignBorders = false;
            this.controlBox.Size = new System.Drawing.Size(67, 37);
            this.controlBox.TabIndex = 50;
            // 
            // balanceDataGridView
            // 
            this.balanceDataGridView.AllowCustomTheming = true;
            this.balanceDataGridView.AllowUserToAddRows = false;
            this.balanceDataGridView.AllowUserToDeleteRows = false;
            this.balanceDataGridView.AllowUserToResizeColumns = false;
            this.balanceDataGridView.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(251)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            this.balanceDataGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.balanceDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.balanceDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.balanceDataGridView.BackgroundColor = System.Drawing.Color.White;
            this.balanceDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.balanceDataGridView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.balanceDataGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.DodgerBlue;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI Semibold", 11.75F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.NullValue = null;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(115)))), ((int)(((byte)(204)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.balanceDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.balanceDataGridView.ColumnHeadersHeight = 40;
            this.balanceDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.balanceDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.UserID,
            this.FullName,
            this.Annual,
            this.AnnualUsed,
            this.AnnualBalance,
            this.CasualLeave,
            this.CasualUsed,
            this.CasualBalance,
            this.UnpaidUsed,
            this.Permissions,
            this.PermissionsUsed,
            this.PermissionsBalance});
            this.balanceDataGridView.CurrentTheme.AlternatingRowsStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(251)))), ((int)(((byte)(255)))));
            this.balanceDataGridView.CurrentTheme.AlternatingRowsStyle.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.balanceDataGridView.CurrentTheme.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Black;
            this.balanceDataGridView.CurrentTheme.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(232)))), ((int)(((byte)(255)))));
            this.balanceDataGridView.CurrentTheme.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Black;
            this.balanceDataGridView.CurrentTheme.BackColor = System.Drawing.Color.White;
            this.balanceDataGridView.CurrentTheme.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(238)))), ((int)(((byte)(255)))));
            this.balanceDataGridView.CurrentTheme.HeaderStyle.BackColor = System.Drawing.Color.DodgerBlue;
            this.balanceDataGridView.CurrentTheme.HeaderStyle.Font = new System.Drawing.Font("Segoe UI Semibold", 11.75F, System.Drawing.FontStyle.Bold);
            this.balanceDataGridView.CurrentTheme.HeaderStyle.ForeColor = System.Drawing.Color.White;
            this.balanceDataGridView.CurrentTheme.HeaderStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(115)))), ((int)(((byte)(204)))));
            this.balanceDataGridView.CurrentTheme.HeaderStyle.SelectionForeColor = System.Drawing.Color.White;
            this.balanceDataGridView.CurrentTheme.Name = null;
            this.balanceDataGridView.CurrentTheme.RowsStyle.BackColor = System.Drawing.Color.White;
            this.balanceDataGridView.CurrentTheme.RowsStyle.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.balanceDataGridView.CurrentTheme.RowsStyle.ForeColor = System.Drawing.Color.Black;
            this.balanceDataGridView.CurrentTheme.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(232)))), ((int)(((byte)(255)))));
            this.balanceDataGridView.CurrentTheme.RowsStyle.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(232)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.balanceDataGridView.DefaultCellStyle = dataGridViewCellStyle7;
            this.balanceDataGridView.EnableHeadersVisualStyles = false;
            this.balanceDataGridView.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(238)))), ((int)(((byte)(255)))));
            this.balanceDataGridView.HeaderBackColor = System.Drawing.Color.DodgerBlue;
            this.balanceDataGridView.HeaderBgColor = System.Drawing.Color.Empty;
            this.balanceDataGridView.HeaderForeColor = System.Drawing.Color.White;
            this.balanceDataGridView.Location = new System.Drawing.Point(5, 135);
            this.balanceDataGridView.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.balanceDataGridView.Name = "balanceDataGridView";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Tahoma", 8F);
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.balanceDataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.balanceDataGridView.RowHeadersVisible = false;
            this.balanceDataGridView.RowHeadersWidth = 51;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.balanceDataGridView.RowsDefaultCellStyle = dataGridViewCellStyle9;
            this.balanceDataGridView.RowTemplate.Height = 40;
            this.balanceDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.balanceDataGridView.Size = new System.Drawing.Size(1505, 720);
            this.balanceDataGridView.TabIndex = 51;
            this.balanceDataGridView.Theme = Bunifu.UI.WinForms.BunifuDataGridView.PresetThemes.Light;
            this.balanceDataGridView.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.reportsDataGridView_CellFormatting);
            // 
            // UserID
            // 
            this.UserID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.UserID.DataPropertyName = "UserID";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.UserID.DefaultCellStyle = dataGridViewCellStyle3;
            this.UserID.HeaderText = "UserID";
            this.UserID.MinimumWidth = 6;
            this.UserID.Name = "UserID";
            this.UserID.ReadOnly = true;
            this.UserID.Visible = false;
            this.UserID.Width = 82;
            // 
            // FullName
            // 
            this.FullName.DataPropertyName = "FullName";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.FullName.DefaultCellStyle = dataGridViewCellStyle4;
            this.FullName.HeaderText = "Name";
            this.FullName.MinimumWidth = 140;
            this.FullName.Name = "FullName";
            this.FullName.ReadOnly = true;
            // 
            // Annual
            // 
            this.Annual.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.Annual.DataPropertyName = "Annual";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Annual.DefaultCellStyle = dataGridViewCellStyle5;
            this.Annual.HeaderText = "Annual";
            this.Annual.MinimumWidth = 6;
            this.Annual.Name = "Annual";
            this.Annual.ReadOnly = true;
            this.Annual.Width = 83;
            // 
            // AnnualUsed
            // 
            this.AnnualUsed.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.AnnualUsed.DataPropertyName = "AnnualUsed";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.NullValue = null;
            this.AnnualUsed.DefaultCellStyle = dataGridViewCellStyle6;
            this.AnnualUsed.HeaderText = "Annual Used";
            this.AnnualUsed.MinimumWidth = 6;
            this.AnnualUsed.Name = "AnnualUsed";
            this.AnnualUsed.ReadOnly = true;
            this.AnnualUsed.Width = 124;
            // 
            // AnnualBalance
            // 
            this.AnnualBalance.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.AnnualBalance.DataPropertyName = "AnnualBalance";
            this.AnnualBalance.HeaderText = "Annual Bal.";
            this.AnnualBalance.MinimumWidth = 6;
            this.AnnualBalance.Name = "AnnualBalance";
            this.AnnualBalance.ReadOnly = true;
            this.AnnualBalance.Width = 113;
            // 
            // CasualLeave
            // 
            this.CasualLeave.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.CasualLeave.DataPropertyName = "CasualLeave";
            this.CasualLeave.HeaderText = "Emergency";
            this.CasualLeave.MinimumWidth = 6;
            this.CasualLeave.Name = "CasualLeave";
            this.CasualLeave.ReadOnly = true;
            this.CasualLeave.Width = 114;
            // 
            // CasualUsed
            // 
            this.CasualUsed.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.CasualUsed.DataPropertyName = "CasualUsed";
            this.CasualUsed.HeaderText = "Emergency Used";
            this.CasualUsed.MinimumWidth = 6;
            this.CasualUsed.Name = "CasualUsed";
            this.CasualUsed.ReadOnly = true;
            this.CasualUsed.Width = 155;
            // 
            // CasualBalance
            // 
            this.CasualBalance.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.CasualBalance.DataPropertyName = "CasualBalance";
            this.CasualBalance.HeaderText = "Emergency Bal.";
            this.CasualBalance.MinimumWidth = 6;
            this.CasualBalance.Name = "CasualBalance";
            this.CasualBalance.ReadOnly = true;
            this.CasualBalance.Width = 144;
            // 
            // UnpaidUsed
            // 
            this.UnpaidUsed.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.UnpaidUsed.DataPropertyName = "UnpaidUsed";
            this.UnpaidUsed.HeaderText = "Unpaid Used";
            this.UnpaidUsed.MinimumWidth = 6;
            this.UnpaidUsed.Name = "UnpaidUsed";
            this.UnpaidUsed.ReadOnly = true;
            this.UnpaidUsed.Width = 126;
            // 
            // Permissions
            // 
            this.Permissions.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.Permissions.DataPropertyName = "Permissions";
            this.Permissions.HeaderText = "Permissions";
            this.Permissions.MinimumWidth = 6;
            this.Permissions.Name = "Permissions";
            this.Permissions.ReadOnly = true;
            this.Permissions.Width = 118;
            // 
            // PermissionsUsed
            // 
            this.PermissionsUsed.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.PermissionsUsed.DataPropertyName = "PermissionsUsed";
            this.PermissionsUsed.HeaderText = "Perm. Used";
            this.PermissionsUsed.MinimumWidth = 6;
            this.PermissionsUsed.Name = "PermissionsUsed";
            this.PermissionsUsed.ReadOnly = true;
            this.PermissionsUsed.Width = 115;
            // 
            // PermissionsBalance
            // 
            this.PermissionsBalance.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.PermissionsBalance.DataPropertyName = "PermissionsBalance";
            this.PermissionsBalance.HeaderText = "Perm. Bal.";
            this.PermissionsBalance.MinimumWidth = 6;
            this.PermissionsBalance.Name = "PermissionsBalance";
            this.PermissionsBalance.ReadOnly = true;
            this.PermissionsBalance.Width = 104;
            // 
            // refreshButton
            // 
            this.refreshButton.AllowAnimations = true;
            this.refreshButton.AllowMouseEffects = true;
            this.refreshButton.AllowToggling = false;
            this.refreshButton.AnimationSpeed = 200;
            this.refreshButton.AutoGenerateColors = false;
            this.refreshButton.AutoRoundBorders = false;
            this.refreshButton.AutoSizeLeftIcon = true;
            this.refreshButton.AutoSizeRightIcon = true;
            this.refreshButton.BackColor = System.Drawing.Color.Transparent;
            this.refreshButton.BackColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(122)))), ((int)(((byte)(183)))));
            this.refreshButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("refreshButton.BackgroundImage")));
            this.refreshButton.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            this.refreshButton.ButtonText = "Refresh";
            this.refreshButton.ButtonTextMarginLeft = 0;
            this.refreshButton.ColorContrastOnClick = 45;
            this.refreshButton.ColorContrastOnHover = 45;
            this.refreshButton.Cursor = System.Windows.Forms.Cursors.Default;
            borderEdges1.BottomLeft = true;
            borderEdges1.BottomRight = true;
            borderEdges1.TopLeft = true;
            borderEdges1.TopRight = true;
            this.refreshButton.CustomizableEdges = borderEdges1;
            this.refreshButton.DialogResult = System.Windows.Forms.DialogResult.None;
            this.refreshButton.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(191)))), ((int)(((byte)(191)))));
            this.refreshButton.DisabledFillColor = System.Drawing.Color.Empty;
            this.refreshButton.DisabledForecolor = System.Drawing.Color.Empty;
            this.refreshButton.FocusState = Bunifu.UI.WinForms.BunifuButton.BunifuButton.ButtonStates.Pressed;
            this.refreshButton.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.refreshButton.ForeColor = System.Drawing.Color.White;
            this.refreshButton.IconLeft = null;
            this.refreshButton.IconLeftAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.refreshButton.IconLeftCursor = System.Windows.Forms.Cursors.Default;
            this.refreshButton.IconLeftPadding = new System.Windows.Forms.Padding(11, 3, 3, 3);
            this.refreshButton.IconMarginLeft = 11;
            this.refreshButton.IconPadding = 10;
            this.refreshButton.IconRight = null;
            this.refreshButton.IconRightAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.refreshButton.IconRightCursor = System.Windows.Forms.Cursors.Default;
            this.refreshButton.IconRightPadding = new System.Windows.Forms.Padding(3, 3, 7, 3);
            this.refreshButton.IconSize = 25;
            this.refreshButton.IdleBorderColor = System.Drawing.Color.Empty;
            this.refreshButton.IdleBorderRadius = 0;
            this.refreshButton.IdleBorderThickness = 0;
            this.refreshButton.IdleFillColor = System.Drawing.Color.Empty;
            this.refreshButton.IdleIconLeftImage = null;
            this.refreshButton.IdleIconRightImage = null;
            this.refreshButton.IndicateFocus = false;
            this.refreshButton.Location = new System.Drawing.Point(12, 85);
            this.refreshButton.Name = "refreshButton";
            this.refreshButton.OnDisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(191)))), ((int)(((byte)(191)))));
            this.refreshButton.OnDisabledState.BorderRadius = 1;
            this.refreshButton.OnDisabledState.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            this.refreshButton.OnDisabledState.BorderThickness = 1;
            this.refreshButton.OnDisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.refreshButton.OnDisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(160)))), ((int)(((byte)(168)))));
            this.refreshButton.OnDisabledState.IconLeftImage = null;
            this.refreshButton.OnDisabledState.IconRightImage = null;
            this.refreshButton.onHoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(150)))), ((int)(((byte)(255)))));
            this.refreshButton.onHoverState.BorderRadius = 1;
            this.refreshButton.onHoverState.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            this.refreshButton.onHoverState.BorderThickness = 1;
            this.refreshButton.onHoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(150)))), ((int)(((byte)(255)))));
            this.refreshButton.onHoverState.ForeColor = System.Drawing.Color.White;
            this.refreshButton.onHoverState.IconLeftImage = null;
            this.refreshButton.onHoverState.IconRightImage = null;
            this.refreshButton.OnIdleState.BorderColor = System.Drawing.Color.DodgerBlue;
            this.refreshButton.OnIdleState.BorderRadius = 1;
            this.refreshButton.OnIdleState.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            this.refreshButton.OnIdleState.BorderThickness = 1;
            this.refreshButton.OnIdleState.FillColor = System.Drawing.Color.DodgerBlue;
            this.refreshButton.OnIdleState.ForeColor = System.Drawing.Color.White;
            this.refreshButton.OnIdleState.IconLeftImage = null;
            this.refreshButton.OnIdleState.IconRightImage = null;
            this.refreshButton.OnPressedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(96)))), ((int)(((byte)(144)))));
            this.refreshButton.OnPressedState.BorderRadius = 1;
            this.refreshButton.OnPressedState.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            this.refreshButton.OnPressedState.BorderThickness = 1;
            this.refreshButton.OnPressedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(96)))), ((int)(((byte)(144)))));
            this.refreshButton.OnPressedState.ForeColor = System.Drawing.Color.White;
            this.refreshButton.OnPressedState.IconLeftImage = null;
            this.refreshButton.OnPressedState.IconRightImage = null;
            this.refreshButton.Size = new System.Drawing.Size(121, 30);
            this.refreshButton.TabIndex = 52;
            this.refreshButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.refreshButton.TextAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.refreshButton.TextMarginLeft = 0;
            this.refreshButton.TextPadding = new System.Windows.Forms.Padding(0);
            this.refreshButton.UseDefaultRadiusAndThickness = true;
            this.refreshButton.Click += new System.EventHandler(this.refreshButton_Click);
            // 
            // panel
            // 
            this.panel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(150)))), ((int)(((byte)(136)))));
            this.panel.Controls.Add(this.bunifuFormControlBox1);
            this.panel.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel.Location = new System.Drawing.Point(0, 0);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(1518, 41);
            this.panel.TabIndex = 53;
            this.panel.Paint += new System.Windows.Forms.PaintEventHandler(this.panel_Paint);
            // 
            // bunifuFormControlBox1
            // 
            this.bunifuFormControlBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bunifuFormControlBox1.BunifuFormDrag = null;
            this.bunifuFormControlBox1.CloseBoxOptions.BackColor = System.Drawing.Color.Transparent;
            this.bunifuFormControlBox1.CloseBoxOptions.BorderRadius = 0;
            this.bunifuFormControlBox1.CloseBoxOptions.Enabled = true;
            this.bunifuFormControlBox1.CloseBoxOptions.EnableDefaultAction = true;
            this.bunifuFormControlBox1.CloseBoxOptions.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(17)))), ((int)(((byte)(35)))));
            this.bunifuFormControlBox1.CloseBoxOptions.Icon = ((System.Drawing.Image)(resources.GetObject("bunifuFormControlBox1.CloseBoxOptions.Icon")));
            this.bunifuFormControlBox1.CloseBoxOptions.IconAlt = null;
            this.bunifuFormControlBox1.CloseBoxOptions.IconColor = System.Drawing.Color.White;
            this.bunifuFormControlBox1.CloseBoxOptions.IconHoverColor = System.Drawing.Color.White;
            this.bunifuFormControlBox1.CloseBoxOptions.IconPressedColor = System.Drawing.Color.White;
            this.bunifuFormControlBox1.CloseBoxOptions.IconSize = new System.Drawing.Size(18, 18);
            this.bunifuFormControlBox1.CloseBoxOptions.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(17)))), ((int)(((byte)(35)))));
            this.bunifuFormControlBox1.ForeColor = System.Drawing.Color.White;
            this.bunifuFormControlBox1.HelpBox = false;
            this.bunifuFormControlBox1.HelpBoxOptions.BackColor = System.Drawing.Color.Transparent;
            this.bunifuFormControlBox1.HelpBoxOptions.BorderRadius = 0;
            this.bunifuFormControlBox1.HelpBoxOptions.Enabled = true;
            this.bunifuFormControlBox1.HelpBoxOptions.EnableDefaultAction = true;
            this.bunifuFormControlBox1.HelpBoxOptions.HoverColor = System.Drawing.Color.LightGray;
            this.bunifuFormControlBox1.HelpBoxOptions.Icon = ((System.Drawing.Image)(resources.GetObject("bunifuFormControlBox1.HelpBoxOptions.Icon")));
            this.bunifuFormControlBox1.HelpBoxOptions.IconAlt = null;
            this.bunifuFormControlBox1.HelpBoxOptions.IconColor = System.Drawing.Color.Black;
            this.bunifuFormControlBox1.HelpBoxOptions.IconHoverColor = System.Drawing.Color.Black;
            this.bunifuFormControlBox1.HelpBoxOptions.IconPressedColor = System.Drawing.Color.Black;
            this.bunifuFormControlBox1.HelpBoxOptions.IconSize = new System.Drawing.Size(22, 22);
            this.bunifuFormControlBox1.HelpBoxOptions.PressedColor = System.Drawing.Color.Silver;
            this.bunifuFormControlBox1.Location = new System.Drawing.Point(1356, 2);
            this.bunifuFormControlBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.bunifuFormControlBox1.MaximizeBox = true;
            this.bunifuFormControlBox1.MaximizeBoxOptions.BackColor = System.Drawing.Color.Transparent;
            this.bunifuFormControlBox1.MaximizeBoxOptions.BorderRadius = 0;
            this.bunifuFormControlBox1.MaximizeBoxOptions.Enabled = true;
            this.bunifuFormControlBox1.MaximizeBoxOptions.EnableDefaultAction = true;
            this.bunifuFormControlBox1.MaximizeBoxOptions.HoverColor = System.Drawing.Color.LightGray;
            this.bunifuFormControlBox1.MaximizeBoxOptions.Icon = ((System.Drawing.Image)(resources.GetObject("bunifuFormControlBox1.MaximizeBoxOptions.Icon")));
            this.bunifuFormControlBox1.MaximizeBoxOptions.IconAlt = ((System.Drawing.Image)(resources.GetObject("bunifuFormControlBox1.MaximizeBoxOptions.IconAlt")));
            this.bunifuFormControlBox1.MaximizeBoxOptions.IconColor = System.Drawing.Color.White;
            this.bunifuFormControlBox1.MaximizeBoxOptions.IconHoverColor = System.Drawing.Color.Black;
            this.bunifuFormControlBox1.MaximizeBoxOptions.IconPressedColor = System.Drawing.Color.Black;
            this.bunifuFormControlBox1.MaximizeBoxOptions.IconSize = new System.Drawing.Size(16, 16);
            this.bunifuFormControlBox1.MaximizeBoxOptions.PressedColor = System.Drawing.Color.Silver;
            this.bunifuFormControlBox1.MinimizeBox = true;
            this.bunifuFormControlBox1.MinimizeBoxOptions.BackColor = System.Drawing.Color.Transparent;
            this.bunifuFormControlBox1.MinimizeBoxOptions.BorderRadius = 0;
            this.bunifuFormControlBox1.MinimizeBoxOptions.Enabled = true;
            this.bunifuFormControlBox1.MinimizeBoxOptions.EnableDefaultAction = true;
            this.bunifuFormControlBox1.MinimizeBoxOptions.HoverColor = System.Drawing.Color.LightGray;
            this.bunifuFormControlBox1.MinimizeBoxOptions.Icon = ((System.Drawing.Image)(resources.GetObject("bunifuFormControlBox1.MinimizeBoxOptions.Icon")));
            this.bunifuFormControlBox1.MinimizeBoxOptions.IconAlt = null;
            this.bunifuFormControlBox1.MinimizeBoxOptions.IconColor = System.Drawing.Color.White;
            this.bunifuFormControlBox1.MinimizeBoxOptions.IconHoverColor = System.Drawing.Color.Black;
            this.bunifuFormControlBox1.MinimizeBoxOptions.IconPressedColor = System.Drawing.Color.Black;
            this.bunifuFormControlBox1.MinimizeBoxOptions.IconSize = new System.Drawing.Size(14, 14);
            this.bunifuFormControlBox1.MinimizeBoxOptions.PressedColor = System.Drawing.Color.Silver;
            this.bunifuFormControlBox1.Name = "bunifuFormControlBox1";
            this.bunifuFormControlBox1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.bunifuFormControlBox1.ShowDesignBorders = false;
            this.bunifuFormControlBox1.Size = new System.Drawing.Size(160, 37);
            this.bunifuFormControlBox1.TabIndex = 19;
            // 
            // balanceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1518, 859);
            this.Controls.Add(this.panel);
            this.Controls.Add(this.refreshButton);
            this.Controls.Add(this.balanceDataGridView);
            this.Controls.Add(this.controlBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.Name = "balanceForm";
            this.Text = "Reports";
            this.Load += new System.EventHandler(this.balanceForm_Load);
            this.Resize += new System.EventHandler(this.balanceForm_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.balanceDataGridView)).EndInit();
            this.panel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private Bunifu.UI.WinForms.BunifuFormControlBox controlBox;
        private Bunifu.UI.WinForms.BunifuDataGridView balanceDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn UserID;
        private System.Windows.Forms.DataGridViewTextBoxColumn FullName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Annual;
        private System.Windows.Forms.DataGridViewTextBoxColumn AnnualUsed;
        private System.Windows.Forms.DataGridViewTextBoxColumn AnnualBalance;
        private System.Windows.Forms.DataGridViewTextBoxColumn CasualLeave;
        private System.Windows.Forms.DataGridViewTextBoxColumn CasualUsed;
        private System.Windows.Forms.DataGridViewTextBoxColumn CasualBalance;
        private System.Windows.Forms.DataGridViewTextBoxColumn UnpaidUsed;
        private System.Windows.Forms.DataGridViewTextBoxColumn Permissions;
        private System.Windows.Forms.DataGridViewTextBoxColumn PermissionsUsed;
        private System.Windows.Forms.DataGridViewTextBoxColumn PermissionsBalance;
        private Bunifu.UI.WinForms.BunifuButton.BunifuButton refreshButton;
        private System.Windows.Forms.Panel panel;
        private Bunifu.UI.WinForms.BunifuFormControlBox bunifuFormControlBox1;
    }
}