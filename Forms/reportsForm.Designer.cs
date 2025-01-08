namespace TDF.Forms
{
    partial class reportsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(reportsForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            Bunifu.UI.WinForms.BunifuButton.BunifuButton2.BorderEdges borderEdges2 = new Bunifu.UI.WinForms.BunifuButton.BunifuButton2.BorderEdges();
            this.controlBox = new Bunifu.UI.WinForms.BunifuFormControlBox();
            this.reportsDataGridView = new Bunifu.UI.WinForms.BunifuDataGridView();
            this.Date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.userName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.requestType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Days = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Department = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fromDatePicker = new Bunifu.UI.WinForms.BunifuDatePicker();
            this.bunifuLabel1 = new Bunifu.UI.WinForms.BunifuLabel();
            this.bunifuLabel2 = new Bunifu.UI.WinForms.BunifuLabel();
            this.toDatePicker = new Bunifu.UI.WinForms.BunifuDatePicker();
            this.bunifuButton21 = new Bunifu.UI.WinForms.BunifuButton.BunifuButton2();
            this.depnameDropdown = new Bunifu.UI.WinForms.BunifuDropdown();
            this.filterLabel = new Bunifu.UI.WinForms.BunifuLabel();
            this.bunifuLabel3 = new Bunifu.UI.WinForms.BunifuLabel();
            this.filterDropdown = new Bunifu.UI.WinForms.BunifuDropdown();
            this.bunifuLabel4 = new Bunifu.UI.WinForms.BunifuLabel();
            this.statusDropdown = new Bunifu.UI.WinForms.BunifuDropdown();
            this.bunifuLabel5 = new Bunifu.UI.WinForms.BunifuLabel();
            this.typeDropdown = new Bunifu.UI.WinForms.BunifuDropdown();
            ((System.ComponentModel.ISupportInitialize)(this.reportsDataGridView)).BeginInit();
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
            this.controlBox.Location = new System.Drawing.Point(1467, 11);
            this.controlBox.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
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
            this.controlBox.Size = new System.Drawing.Size(78, 46);
            this.controlBox.TabIndex = 50;
            // 
            // reportsDataGridView
            // 
            this.reportsDataGridView.AllowCustomTheming = true;
            this.reportsDataGridView.AllowUserToAddRows = false;
            this.reportsDataGridView.AllowUserToDeleteRows = false;
            this.reportsDataGridView.AllowUserToResizeColumns = false;
            this.reportsDataGridView.AllowUserToResizeRows = false;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(251)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black;
            this.reportsDataGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.reportsDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.reportsDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.reportsDataGridView.BackgroundColor = System.Drawing.Color.White;
            this.reportsDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.reportsDataGridView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.reportsDataGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.DodgerBlue;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Segoe UI Semibold", 11.75F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(115)))), ((int)(((byte)(204)))));
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.reportsDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.reportsDataGridView.ColumnHeadersHeight = 40;
            this.reportsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.reportsDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Date,
            this.userName,
            this.requestType,
            this.Days,
            this.Status,
            this.Department});
            this.reportsDataGridView.CurrentTheme.AlternatingRowsStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(251)))), ((int)(((byte)(255)))));
            this.reportsDataGridView.CurrentTheme.AlternatingRowsStyle.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.reportsDataGridView.CurrentTheme.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Black;
            this.reportsDataGridView.CurrentTheme.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(232)))), ((int)(((byte)(255)))));
            this.reportsDataGridView.CurrentTheme.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Black;
            this.reportsDataGridView.CurrentTheme.BackColor = System.Drawing.Color.White;
            this.reportsDataGridView.CurrentTheme.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(238)))), ((int)(((byte)(255)))));
            this.reportsDataGridView.CurrentTheme.HeaderStyle.BackColor = System.Drawing.Color.DodgerBlue;
            this.reportsDataGridView.CurrentTheme.HeaderStyle.Font = new System.Drawing.Font("Segoe UI Semibold", 11.75F, System.Drawing.FontStyle.Bold);
            this.reportsDataGridView.CurrentTheme.HeaderStyle.ForeColor = System.Drawing.Color.White;
            this.reportsDataGridView.CurrentTheme.HeaderStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(115)))), ((int)(((byte)(204)))));
            this.reportsDataGridView.CurrentTheme.HeaderStyle.SelectionForeColor = System.Drawing.Color.White;
            this.reportsDataGridView.CurrentTheme.Name = null;
            this.reportsDataGridView.CurrentTheme.RowsStyle.BackColor = System.Drawing.Color.White;
            this.reportsDataGridView.CurrentTheme.RowsStyle.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.reportsDataGridView.CurrentTheme.RowsStyle.ForeColor = System.Drawing.Color.Black;
            this.reportsDataGridView.CurrentTheme.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(232)))), ((int)(((byte)(255)))));
            this.reportsDataGridView.CurrentTheme.RowsStyle.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(232)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.reportsDataGridView.DefaultCellStyle = dataGridViewCellStyle6;
            this.reportsDataGridView.EnableHeadersVisualStyles = false;
            this.reportsDataGridView.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(238)))), ((int)(((byte)(255)))));
            this.reportsDataGridView.HeaderBackColor = System.Drawing.Color.DodgerBlue;
            this.reportsDataGridView.HeaderBgColor = System.Drawing.Color.Empty;
            this.reportsDataGridView.HeaderForeColor = System.Drawing.Color.White;
            this.reportsDataGridView.Location = new System.Drawing.Point(2, 207);
            this.reportsDataGridView.Name = "reportsDataGridView";
            this.reportsDataGridView.RowHeadersVisible = false;
            this.reportsDataGridView.RowHeadersWidth = 51;
            this.reportsDataGridView.RowTemplate.Height = 40;
            this.reportsDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.reportsDataGridView.Size = new System.Drawing.Size(1566, 624);
            this.reportsDataGridView.TabIndex = 51;
            this.reportsDataGridView.Theme = Bunifu.UI.WinForms.BunifuDataGridView.PresetThemes.Light;
            // 
            // Date
            // 
            this.Date.DataPropertyName = "RequestFromDay";
            this.Date.HeaderText = "Date";
            this.Date.MinimumWidth = 6;
            this.Date.Name = "Date";
            this.Date.ReadOnly = true;
            // 
            // userName
            // 
            this.userName.DataPropertyName = "RequestUserFullName";
            this.userName.HeaderText = "Name";
            this.userName.MinimumWidth = 6;
            this.userName.Name = "userName";
            this.userName.ReadOnly = true;
            // 
            // requestType
            // 
            this.requestType.DataPropertyName = "RequestType";
            this.requestType.HeaderText = "Request";
            this.requestType.MinimumWidth = 6;
            this.requestType.Name = "requestType";
            this.requestType.ReadOnly = true;
            // 
            // Days
            // 
            this.Days.DataPropertyName = "RequestNumberOfDays";
            this.Days.HeaderText = "No. of Days";
            this.Days.MinimumWidth = 6;
            this.Days.Name = "Days";
            this.Days.ReadOnly = true;
            // 
            // Status
            // 
            this.Status.DataPropertyName = "RequestStatus";
            this.Status.HeaderText = "Status";
            this.Status.MinimumWidth = 6;
            this.Status.Name = "Status";
            this.Status.ReadOnly = true;
            // 
            // Department
            // 
            this.Department.DataPropertyName = "RequestDepartment";
            this.Department.HeaderText = "Department";
            this.Department.MinimumWidth = 6;
            this.Department.Name = "Department";
            this.Department.ReadOnly = true;
            // 
            // fromDatePicker
            // 
            this.fromDatePicker.BackColor = System.Drawing.Color.Transparent;
            this.fromDatePicker.BorderColor = System.Drawing.Color.Silver;
            this.fromDatePicker.BorderRadius = 1;
            this.fromDatePicker.Color = System.Drawing.Color.Silver;
            this.fromDatePicker.DateBorderThickness = Bunifu.UI.WinForms.BunifuDatePicker.BorderThickness.Thin;
            this.fromDatePicker.DateTextAlign = Bunifu.UI.WinForms.BunifuDatePicker.TextAlign.Center;
            this.fromDatePicker.DisabledColor = System.Drawing.Color.Gray;
            this.fromDatePicker.DisplayWeekNumbers = false;
            this.fromDatePicker.DPHeight = 0;
            this.fromDatePicker.DropDownAlign = System.Windows.Forms.LeftRightAlignment.Right;
            this.fromDatePicker.FillDatePicker = false;
            this.fromDatePicker.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.fromDatePicker.ForeColor = System.Drawing.Color.Black;
            this.fromDatePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.fromDatePicker.Icon = ((System.Drawing.Image)(resources.GetObject("fromDatePicker.Icon")));
            this.fromDatePicker.IconColor = System.Drawing.Color.Transparent;
            this.fromDatePicker.IconLocation = Bunifu.UI.WinForms.BunifuDatePicker.Indicator.None;
            this.fromDatePicker.LeftTextMargin = 5;
            this.fromDatePicker.Location = new System.Drawing.Point(1000, 129);
            this.fromDatePicker.MinimumSize = new System.Drawing.Size(4, 32);
            this.fromDatePicker.Name = "fromDatePicker";
            this.fromDatePicker.Size = new System.Drawing.Size(158, 32);
            this.fromDatePicker.TabIndex = 52;
            this.fromDatePicker.Value = new System.DateTime(2025, 1, 8, 13, 37, 0, 0);
            // 
            // bunifuLabel1
            // 
            this.bunifuLabel1.AllowParentOverrides = false;
            this.bunifuLabel1.AutoEllipsis = false;
            this.bunifuLabel1.Cursor = System.Windows.Forms.Cursors.Default;
            this.bunifuLabel1.CursorType = System.Windows.Forms.Cursors.Default;
            this.bunifuLabel1.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.bunifuLabel1.Location = new System.Drawing.Point(1050, 90);
            this.bunifuLabel1.Name = "bunifuLabel1";
            this.bunifuLabel1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.bunifuLabel1.Size = new System.Drawing.Size(46, 25);
            this.bunifuLabel1.TabIndex = 54;
            this.bunifuLabel1.Text = "From:";
            this.bunifuLabel1.TextAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.bunifuLabel1.TextFormat = Bunifu.UI.WinForms.BunifuLabel.TextFormattingOptions.Default;
            // 
            // bunifuLabel2
            // 
            this.bunifuLabel2.AllowParentOverrides = false;
            this.bunifuLabel2.AutoEllipsis = false;
            this.bunifuLabel2.CursorType = null;
            this.bunifuLabel2.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.bunifuLabel2.Location = new System.Drawing.Point(1254, 90);
            this.bunifuLabel2.Name = "bunifuLabel2";
            this.bunifuLabel2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.bunifuLabel2.Size = new System.Drawing.Size(24, 25);
            this.bunifuLabel2.TabIndex = 56;
            this.bunifuLabel2.Text = "To:";
            this.bunifuLabel2.TextAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.bunifuLabel2.TextFormat = Bunifu.UI.WinForms.BunifuLabel.TextFormattingOptions.Default;
            // 
            // toDatePicker
            // 
            this.toDatePicker.BackColor = System.Drawing.Color.Transparent;
            this.toDatePicker.BorderColor = System.Drawing.Color.Silver;
            this.toDatePicker.BorderRadius = 1;
            this.toDatePicker.Color = System.Drawing.Color.Silver;
            this.toDatePicker.DateBorderThickness = Bunifu.UI.WinForms.BunifuDatePicker.BorderThickness.Thin;
            this.toDatePicker.DateTextAlign = Bunifu.UI.WinForms.BunifuDatePicker.TextAlign.Center;
            this.toDatePicker.DisabledColor = System.Drawing.Color.Gray;
            this.toDatePicker.DisplayWeekNumbers = false;
            this.toDatePicker.DPHeight = 0;
            this.toDatePicker.DropDownAlign = System.Windows.Forms.LeftRightAlignment.Right;
            this.toDatePicker.FillDatePicker = false;
            this.toDatePicker.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toDatePicker.ForeColor = System.Drawing.Color.Black;
            this.toDatePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.toDatePicker.Icon = ((System.Drawing.Image)(resources.GetObject("toDatePicker.Icon")));
            this.toDatePicker.IconColor = System.Drawing.Color.Transparent;
            this.toDatePicker.IconLocation = Bunifu.UI.WinForms.BunifuDatePicker.Indicator.None;
            this.toDatePicker.LeftTextMargin = 5;
            this.toDatePicker.Location = new System.Drawing.Point(1189, 129);
            this.toDatePicker.MinimumSize = new System.Drawing.Size(4, 32);
            this.toDatePicker.Name = "toDatePicker";
            this.toDatePicker.Size = new System.Drawing.Size(158, 32);
            this.toDatePicker.TabIndex = 55;
            this.toDatePicker.Value = new System.DateTime(2025, 1, 8, 14, 50, 0, 0);
            // 
            // bunifuButton21
            // 
            this.bunifuButton21.AllowAnimations = true;
            this.bunifuButton21.AllowMouseEffects = true;
            this.bunifuButton21.AllowToggling = false;
            this.bunifuButton21.AnimationSpeed = 200;
            this.bunifuButton21.AutoGenerateColors = true;
            this.bunifuButton21.AutoRoundBorders = false;
            this.bunifuButton21.AutoSizeLeftIcon = true;
            this.bunifuButton21.AutoSizeRightIcon = true;
            this.bunifuButton21.BackColor = System.Drawing.Color.Transparent;
            this.bunifuButton21.BackColor1 = System.Drawing.Color.DodgerBlue;
            this.bunifuButton21.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("bunifuButton21.BackgroundImage")));
            this.bunifuButton21.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton2.BorderStyles.Solid;
            this.bunifuButton21.ButtonText = "Generate";
            this.bunifuButton21.ButtonTextMarginLeft = 0;
            this.bunifuButton21.ColorContrastOnClick = 45;
            this.bunifuButton21.ColorContrastOnHover = 45;
            this.bunifuButton21.Cursor = System.Windows.Forms.Cursors.Default;
            borderEdges2.BottomLeft = true;
            borderEdges2.BottomRight = true;
            borderEdges2.TopLeft = true;
            borderEdges2.TopRight = true;
            this.bunifuButton21.CustomizableEdges = borderEdges2;
            this.bunifuButton21.DialogResult = System.Windows.Forms.DialogResult.None;
            this.bunifuButton21.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(191)))), ((int)(((byte)(191)))));
            this.bunifuButton21.DisabledFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.bunifuButton21.DisabledForecolor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(160)))), ((int)(((byte)(168)))));
            this.bunifuButton21.FocusState = Bunifu.UI.WinForms.BunifuButton.BunifuButton2.ButtonStates.Pressed;
            this.bunifuButton21.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.bunifuButton21.ForeColor = System.Drawing.Color.White;
            this.bunifuButton21.IconLeftAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bunifuButton21.IconLeftCursor = System.Windows.Forms.Cursors.Default;
            this.bunifuButton21.IconLeftPadding = new System.Windows.Forms.Padding(11, 3, 3, 3);
            this.bunifuButton21.IconMarginLeft = 11;
            this.bunifuButton21.IconPadding = 10;
            this.bunifuButton21.IconRightAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.bunifuButton21.IconRightCursor = System.Windows.Forms.Cursors.Default;
            this.bunifuButton21.IconRightPadding = new System.Windows.Forms.Padding(3, 3, 7, 3);
            this.bunifuButton21.IconSize = 25;
            this.bunifuButton21.IdleBorderColor = System.Drawing.Color.DodgerBlue;
            this.bunifuButton21.IdleBorderRadius = 1;
            this.bunifuButton21.IdleBorderThickness = 1;
            this.bunifuButton21.IdleFillColor = System.Drawing.Color.DodgerBlue;
            this.bunifuButton21.IdleIconLeftImage = null;
            this.bunifuButton21.IdleIconRightImage = null;
            this.bunifuButton21.IndicateFocus = true;
            this.bunifuButton21.Location = new System.Drawing.Point(1395, 129);
            this.bunifuButton21.Name = "bunifuButton21";
            this.bunifuButton21.OnDisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(191)))), ((int)(((byte)(191)))));
            this.bunifuButton21.OnDisabledState.BorderRadius = 1;
            this.bunifuButton21.OnDisabledState.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton2.BorderStyles.Solid;
            this.bunifuButton21.OnDisabledState.BorderThickness = 1;
            this.bunifuButton21.OnDisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.bunifuButton21.OnDisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(160)))), ((int)(((byte)(168)))));
            this.bunifuButton21.OnDisabledState.IconLeftImage = null;
            this.bunifuButton21.OnDisabledState.IconRightImage = null;
            this.bunifuButton21.onHoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(131)))), ((int)(((byte)(193)))), ((int)(((byte)(255)))));
            this.bunifuButton21.onHoverState.BorderRadius = 1;
            this.bunifuButton21.onHoverState.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton2.BorderStyles.Solid;
            this.bunifuButton21.onHoverState.BorderThickness = 1;
            this.bunifuButton21.onHoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(131)))), ((int)(((byte)(193)))), ((int)(((byte)(255)))));
            this.bunifuButton21.onHoverState.ForeColor = System.Drawing.Color.White;
            this.bunifuButton21.onHoverState.IconLeftImage = null;
            this.bunifuButton21.onHoverState.IconRightImage = null;
            this.bunifuButton21.OnIdleState.BorderColor = System.Drawing.Color.DodgerBlue;
            this.bunifuButton21.OnIdleState.BorderRadius = 1;
            this.bunifuButton21.OnIdleState.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton2.BorderStyles.Solid;
            this.bunifuButton21.OnIdleState.BorderThickness = 1;
            this.bunifuButton21.OnIdleState.FillColor = System.Drawing.Color.DodgerBlue;
            this.bunifuButton21.OnIdleState.ForeColor = System.Drawing.Color.White;
            this.bunifuButton21.OnIdleState.IconLeftImage = null;
            this.bunifuButton21.OnIdleState.IconRightImage = null;
            this.bunifuButton21.OnPressedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(79)))), ((int)(((byte)(140)))));
            this.bunifuButton21.OnPressedState.BorderRadius = 1;
            this.bunifuButton21.OnPressedState.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton2.BorderStyles.Solid;
            this.bunifuButton21.OnPressedState.BorderThickness = 1;
            this.bunifuButton21.OnPressedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(79)))), ((int)(((byte)(140)))));
            this.bunifuButton21.OnPressedState.ForeColor = System.Drawing.Color.White;
            this.bunifuButton21.OnPressedState.IconLeftImage = null;
            this.bunifuButton21.OnPressedState.IconRightImage = null;
            this.bunifuButton21.Size = new System.Drawing.Size(150, 39);
            this.bunifuButton21.TabIndex = 57;
            this.bunifuButton21.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.bunifuButton21.TextAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.bunifuButton21.TextMarginLeft = 0;
            this.bunifuButton21.TextPadding = new System.Windows.Forms.Padding(0);
            this.bunifuButton21.UseDefaultRadiusAndThickness = true;
            this.bunifuButton21.Click += new System.EventHandler(this.generateButton_Click);
            // 
            // depnameDropdown
            // 
            this.depnameDropdown.BackColor = System.Drawing.Color.Transparent;
            this.depnameDropdown.BackgroundColor = System.Drawing.Color.White;
            this.depnameDropdown.BorderColor = System.Drawing.Color.Silver;
            this.depnameDropdown.BorderRadius = 1;
            this.depnameDropdown.Color = System.Drawing.Color.Silver;
            this.depnameDropdown.Direction = Bunifu.UI.WinForms.BunifuDropdown.Directions.Down;
            this.depnameDropdown.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.depnameDropdown.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.depnameDropdown.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.depnameDropdown.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.depnameDropdown.DisabledIndicatorColor = System.Drawing.Color.DarkGray;
            this.depnameDropdown.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.depnameDropdown.DropdownBorderThickness = Bunifu.UI.WinForms.BunifuDropdown.BorderThickness.Thin;
            this.depnameDropdown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.depnameDropdown.DropDownTextAlign = Bunifu.UI.WinForms.BunifuDropdown.TextAlign.Center;
            this.depnameDropdown.FillDropDown = true;
            this.depnameDropdown.FillIndicator = false;
            this.depnameDropdown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.depnameDropdown.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.depnameDropdown.ForeColor = System.Drawing.Color.Black;
            this.depnameDropdown.FormattingEnabled = true;
            this.depnameDropdown.Icon = null;
            this.depnameDropdown.IndicatorAlignment = Bunifu.UI.WinForms.BunifuDropdown.Indicator.Right;
            this.depnameDropdown.IndicatorColor = System.Drawing.Color.DarkGray;
            this.depnameDropdown.IndicatorLocation = Bunifu.UI.WinForms.BunifuDropdown.Indicator.Right;
            this.depnameDropdown.IndicatorThickness = 2;
            this.depnameDropdown.IsDropdownOpened = false;
            this.depnameDropdown.ItemBackColor = System.Drawing.Color.White;
            this.depnameDropdown.ItemBorderColor = System.Drawing.Color.White;
            this.depnameDropdown.ItemForeColor = System.Drawing.Color.Black;
            this.depnameDropdown.ItemHeight = 26;
            this.depnameDropdown.ItemHighLightColor = System.Drawing.Color.DodgerBlue;
            this.depnameDropdown.ItemHighLightForeColor = System.Drawing.Color.White;
            this.depnameDropdown.ItemTopMargin = 3;
            this.depnameDropdown.Location = new System.Drawing.Point(303, 129);
            this.depnameDropdown.Name = "depnameDropdown";
            this.depnameDropdown.Size = new System.Drawing.Size(260, 32);
            this.depnameDropdown.TabIndex = 58;
            this.depnameDropdown.Text = null;
            this.depnameDropdown.TextAlignment = Bunifu.UI.WinForms.BunifuDropdown.TextAlign.Center;
            this.depnameDropdown.TextLeftMargin = 5;
            // 
            // filterLabel
            // 
            this.filterLabel.AllowParentOverrides = false;
            this.filterLabel.AutoEllipsis = false;
            this.filterLabel.Cursor = System.Windows.Forms.Cursors.Default;
            this.filterLabel.CursorType = System.Windows.Forms.Cursors.Default;
            this.filterLabel.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.filterLabel.Location = new System.Drawing.Point(386, 90);
            this.filterLabel.Name = "filterLabel";
            this.filterLabel.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.filterLabel.Size = new System.Drawing.Size(99, 25);
            this.filterLabel.TabIndex = 59;
            this.filterLabel.Text = "Department:";
            this.filterLabel.TextAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.filterLabel.TextFormat = Bunifu.UI.WinForms.BunifuLabel.TextFormattingOptions.Default;
            // 
            // bunifuLabel3
            // 
            this.bunifuLabel3.AllowParentOverrides = false;
            this.bunifuLabel3.AutoEllipsis = false;
            this.bunifuLabel3.Cursor = System.Windows.Forms.Cursors.Default;
            this.bunifuLabel3.CursorType = System.Windows.Forms.Cursors.Default;
            this.bunifuLabel3.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.bunifuLabel3.Location = new System.Drawing.Point(106, 90);
            this.bunifuLabel3.Name = "bunifuLabel3";
            this.bunifuLabel3.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.bunifuLabel3.Size = new System.Drawing.Size(67, 25);
            this.bunifuLabel3.TabIndex = 61;
            this.bunifuLabel3.Text = "Filter by:";
            this.bunifuLabel3.TextAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.bunifuLabel3.TextFormat = Bunifu.UI.WinForms.BunifuLabel.TextFormattingOptions.Default;
            // 
            // filterDropdown
            // 
            this.filterDropdown.BackColor = System.Drawing.Color.Transparent;
            this.filterDropdown.BackgroundColor = System.Drawing.Color.White;
            this.filterDropdown.BorderColor = System.Drawing.Color.Silver;
            this.filterDropdown.BorderRadius = 1;
            this.filterDropdown.Color = System.Drawing.Color.Silver;
            this.filterDropdown.Direction = Bunifu.UI.WinForms.BunifuDropdown.Directions.Down;
            this.filterDropdown.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.filterDropdown.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.filterDropdown.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.filterDropdown.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.filterDropdown.DisabledIndicatorColor = System.Drawing.Color.DarkGray;
            this.filterDropdown.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.filterDropdown.DropdownBorderThickness = Bunifu.UI.WinForms.BunifuDropdown.BorderThickness.Thin;
            this.filterDropdown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.filterDropdown.DropDownTextAlign = Bunifu.UI.WinForms.BunifuDropdown.TextAlign.Center;
            this.filterDropdown.FillDropDown = true;
            this.filterDropdown.FillIndicator = false;
            this.filterDropdown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.filterDropdown.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.filterDropdown.ForeColor = System.Drawing.Color.Black;
            this.filterDropdown.FormattingEnabled = true;
            this.filterDropdown.Icon = null;
            this.filterDropdown.IndicatorAlignment = Bunifu.UI.WinForms.BunifuDropdown.Indicator.Right;
            this.filterDropdown.IndicatorColor = System.Drawing.Color.DarkGray;
            this.filterDropdown.IndicatorLocation = Bunifu.UI.WinForms.BunifuDropdown.Indicator.Right;
            this.filterDropdown.IndicatorThickness = 2;
            this.filterDropdown.IsDropdownOpened = false;
            this.filterDropdown.ItemBackColor = System.Drawing.Color.White;
            this.filterDropdown.ItemBorderColor = System.Drawing.Color.White;
            this.filterDropdown.ItemForeColor = System.Drawing.Color.Black;
            this.filterDropdown.ItemHeight = 26;
            this.filterDropdown.ItemHighLightColor = System.Drawing.Color.DodgerBlue;
            this.filterDropdown.ItemHighLightForeColor = System.Drawing.Color.White;
            this.filterDropdown.Items.AddRange(new object[] {
            "Department",
            "Name"});
            this.filterDropdown.ItemTopMargin = 3;
            this.filterDropdown.Location = new System.Drawing.Point(12, 129);
            this.filterDropdown.Name = "filterDropdown";
            this.filterDropdown.Size = new System.Drawing.Size(260, 32);
            this.filterDropdown.TabIndex = 60;
            this.filterDropdown.Text = null;
            this.filterDropdown.TextAlignment = Bunifu.UI.WinForms.BunifuDropdown.TextAlign.Center;
            this.filterDropdown.TextLeftMargin = 5;
            this.filterDropdown.SelectedIndexChanged += new System.EventHandler(this.filterDropdown_SelectedIndexChanged);
            // 
            // bunifuLabel4
            // 
            this.bunifuLabel4.AllowParentOverrides = false;
            this.bunifuLabel4.AutoEllipsis = false;
            this.bunifuLabel4.Cursor = System.Windows.Forms.Cursors.Default;
            this.bunifuLabel4.CursorType = System.Windows.Forms.Cursors.Default;
            this.bunifuLabel4.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.bunifuLabel4.Location = new System.Drawing.Point(654, 90);
            this.bunifuLabel4.Name = "bunifuLabel4";
            this.bunifuLabel4.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.bunifuLabel4.Size = new System.Drawing.Size(53, 25);
            this.bunifuLabel4.TabIndex = 63;
            this.bunifuLabel4.Text = "Status:";
            this.bunifuLabel4.TextAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.bunifuLabel4.TextFormat = Bunifu.UI.WinForms.BunifuLabel.TextFormattingOptions.Default;
            // 
            // statusDropdown
            // 
            this.statusDropdown.BackColor = System.Drawing.Color.Transparent;
            this.statusDropdown.BackgroundColor = System.Drawing.Color.White;
            this.statusDropdown.BorderColor = System.Drawing.Color.Silver;
            this.statusDropdown.BorderRadius = 1;
            this.statusDropdown.Color = System.Drawing.Color.Silver;
            this.statusDropdown.Direction = Bunifu.UI.WinForms.BunifuDropdown.Directions.Down;
            this.statusDropdown.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.statusDropdown.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.statusDropdown.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.statusDropdown.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.statusDropdown.DisabledIndicatorColor = System.Drawing.Color.DarkGray;
            this.statusDropdown.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.statusDropdown.DropdownBorderThickness = Bunifu.UI.WinForms.BunifuDropdown.BorderThickness.Thin;
            this.statusDropdown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.statusDropdown.DropDownTextAlign = Bunifu.UI.WinForms.BunifuDropdown.TextAlign.Center;
            this.statusDropdown.FillDropDown = true;
            this.statusDropdown.FillIndicator = false;
            this.statusDropdown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.statusDropdown.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.statusDropdown.ForeColor = System.Drawing.Color.Black;
            this.statusDropdown.FormattingEnabled = true;
            this.statusDropdown.Icon = null;
            this.statusDropdown.IndicatorAlignment = Bunifu.UI.WinForms.BunifuDropdown.Indicator.Right;
            this.statusDropdown.IndicatorColor = System.Drawing.Color.DarkGray;
            this.statusDropdown.IndicatorLocation = Bunifu.UI.WinForms.BunifuDropdown.Indicator.Right;
            this.statusDropdown.IndicatorThickness = 2;
            this.statusDropdown.IsDropdownOpened = false;
            this.statusDropdown.ItemBackColor = System.Drawing.Color.White;
            this.statusDropdown.ItemBorderColor = System.Drawing.Color.White;
            this.statusDropdown.ItemForeColor = System.Drawing.Color.Black;
            this.statusDropdown.ItemHeight = 26;
            this.statusDropdown.ItemHighLightColor = System.Drawing.Color.DodgerBlue;
            this.statusDropdown.ItemHighLightForeColor = System.Drawing.Color.White;
            this.statusDropdown.Items.AddRange(new object[] {
            "All",
            "Approved",
            "Pending",
            "Rejected"});
            this.statusDropdown.ItemTopMargin = 3;
            this.statusDropdown.Location = new System.Drawing.Point(594, 129);
            this.statusDropdown.Name = "statusDropdown";
            this.statusDropdown.Size = new System.Drawing.Size(167, 32);
            this.statusDropdown.Sorted = true;
            this.statusDropdown.TabIndex = 62;
            this.statusDropdown.Text = null;
            this.statusDropdown.TextAlignment = Bunifu.UI.WinForms.BunifuDropdown.TextAlign.Center;
            this.statusDropdown.TextLeftMargin = 5;
            // 
            // bunifuLabel5
            // 
            this.bunifuLabel5.AllowParentOverrides = false;
            this.bunifuLabel5.AutoEllipsis = false;
            this.bunifuLabel5.Cursor = System.Windows.Forms.Cursors.Default;
            this.bunifuLabel5.CursorType = System.Windows.Forms.Cursors.Default;
            this.bunifuLabel5.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.bunifuLabel5.Location = new System.Drawing.Point(850, 90);
            this.bunifuLabel5.Name = "bunifuLabel5";
            this.bunifuLabel5.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.bunifuLabel5.Size = new System.Drawing.Size(42, 25);
            this.bunifuLabel5.TabIndex = 65;
            this.bunifuLabel5.Text = "Type:";
            this.bunifuLabel5.TextAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.bunifuLabel5.TextFormat = Bunifu.UI.WinForms.BunifuLabel.TextFormattingOptions.Default;
            // 
            // typeDropdown
            // 
            this.typeDropdown.BackColor = System.Drawing.Color.Transparent;
            this.typeDropdown.BackgroundColor = System.Drawing.Color.White;
            this.typeDropdown.BorderColor = System.Drawing.Color.Silver;
            this.typeDropdown.BorderRadius = 1;
            this.typeDropdown.Color = System.Drawing.Color.Silver;
            this.typeDropdown.Direction = Bunifu.UI.WinForms.BunifuDropdown.Directions.Down;
            this.typeDropdown.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.typeDropdown.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.typeDropdown.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.typeDropdown.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.typeDropdown.DisabledIndicatorColor = System.Drawing.Color.DarkGray;
            this.typeDropdown.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.typeDropdown.DropdownBorderThickness = Bunifu.UI.WinForms.BunifuDropdown.BorderThickness.Thin;
            this.typeDropdown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.typeDropdown.DropDownTextAlign = Bunifu.UI.WinForms.BunifuDropdown.TextAlign.Center;
            this.typeDropdown.FillDropDown = true;
            this.typeDropdown.FillIndicator = false;
            this.typeDropdown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.typeDropdown.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.typeDropdown.ForeColor = System.Drawing.Color.Black;
            this.typeDropdown.FormattingEnabled = true;
            this.typeDropdown.Icon = null;
            this.typeDropdown.IndicatorAlignment = Bunifu.UI.WinForms.BunifuDropdown.Indicator.Right;
            this.typeDropdown.IndicatorColor = System.Drawing.Color.DarkGray;
            this.typeDropdown.IndicatorLocation = Bunifu.UI.WinForms.BunifuDropdown.Indicator.Right;
            this.typeDropdown.IndicatorThickness = 2;
            this.typeDropdown.IsDropdownOpened = false;
            this.typeDropdown.ItemBackColor = System.Drawing.Color.White;
            this.typeDropdown.ItemBorderColor = System.Drawing.Color.White;
            this.typeDropdown.ItemForeColor = System.Drawing.Color.Black;
            this.typeDropdown.ItemHeight = 26;
            this.typeDropdown.ItemHighLightColor = System.Drawing.Color.DodgerBlue;
            this.typeDropdown.ItemHighLightForeColor = System.Drawing.Color.White;
            this.typeDropdown.Items.AddRange(new object[] {
            "All",
            "Annual",
            "Emergency",
            "External Assignment",
            "Permission",
            "Work From Home"});
            this.typeDropdown.ItemTopMargin = 3;
            this.typeDropdown.Location = new System.Drawing.Point(790, 129);
            this.typeDropdown.Name = "typeDropdown";
            this.typeDropdown.Size = new System.Drawing.Size(167, 32);
            this.typeDropdown.Sorted = true;
            this.typeDropdown.TabIndex = 64;
            this.typeDropdown.Text = null;
            this.typeDropdown.TextAlignment = Bunifu.UI.WinForms.BunifuDropdown.TextAlign.Center;
            this.typeDropdown.TextLeftMargin = 5;
            // 
            // reportsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1573, 871);
            this.Controls.Add(this.bunifuLabel5);
            this.Controls.Add(this.typeDropdown);
            this.Controls.Add(this.bunifuLabel4);
            this.Controls.Add(this.statusDropdown);
            this.Controls.Add(this.bunifuLabel3);
            this.Controls.Add(this.filterDropdown);
            this.Controls.Add(this.filterLabel);
            this.Controls.Add(this.depnameDropdown);
            this.Controls.Add(this.bunifuButton21);
            this.Controls.Add(this.bunifuLabel2);
            this.Controls.Add(this.toDatePicker);
            this.Controls.Add(this.bunifuLabel1);
            this.Controls.Add(this.fromDatePicker);
            this.Controls.Add(this.reportsDataGridView);
            this.Controls.Add(this.controlBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "reportsForm";
            this.Text = "Reports";
            this.Load += new System.EventHandler(this.reportForm_Load);
            this.Resize += new System.EventHandler(this.reportForm_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.reportsDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Bunifu.UI.WinForms.BunifuFormControlBox controlBox;
        private Bunifu.UI.WinForms.BunifuDataGridView reportsDataGridView;
        private Bunifu.UI.WinForms.BunifuDatePicker fromDatePicker;
        private Bunifu.UI.WinForms.BunifuLabel bunifuLabel1;
        private Bunifu.UI.WinForms.BunifuLabel bunifuLabel2;
        private Bunifu.UI.WinForms.BunifuDatePicker toDatePicker;
        private Bunifu.UI.WinForms.BunifuButton.BunifuButton2 bunifuButton21;
        private Bunifu.UI.WinForms.BunifuDropdown depnameDropdown;
        private Bunifu.UI.WinForms.BunifuLabel filterLabel;
        private System.Windows.Forms.DataGridViewTextBoxColumn Date;
        private System.Windows.Forms.DataGridViewTextBoxColumn userName;
        private System.Windows.Forms.DataGridViewTextBoxColumn requestType;
        private System.Windows.Forms.DataGridViewTextBoxColumn Days;
        private System.Windows.Forms.DataGridViewTextBoxColumn Status;
        private System.Windows.Forms.DataGridViewTextBoxColumn Department;
        private Bunifu.UI.WinForms.BunifuLabel bunifuLabel3;
        private Bunifu.UI.WinForms.BunifuDropdown filterDropdown;
        private Bunifu.UI.WinForms.BunifuLabel bunifuLabel4;
        private Bunifu.UI.WinForms.BunifuDropdown statusDropdown;
        private Bunifu.UI.WinForms.BunifuLabel bunifuLabel5;
        private Bunifu.UI.WinForms.BunifuDropdown typeDropdown;
    }
}