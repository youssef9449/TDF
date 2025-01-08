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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            Bunifu.UI.WinForms.BunifuButton.BunifuButton2.BorderEdges borderEdges1 = new Bunifu.UI.WinForms.BunifuButton.BunifuButton2.BorderEdges();
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
            this.generateButton = new Bunifu.UI.WinForms.BunifuButton.BunifuButton2();
            this.nameORdepDropdown = new Bunifu.UI.WinForms.BunifuDropdown();
            this.filterLabel = new Bunifu.UI.WinForms.BunifuLabel();
            this.bunifuLabel3 = new Bunifu.UI.WinForms.BunifuLabel();
            this.filterDropdown = new Bunifu.UI.WinForms.BunifuDropdown();
            this.bunifuLabel4 = new Bunifu.UI.WinForms.BunifuLabel();
            this.statusDropdown = new Bunifu.UI.WinForms.BunifuDropdown();
            this.bunifuLabel5 = new Bunifu.UI.WinForms.BunifuLabel();
            this.typeDropdown = new Bunifu.UI.WinForms.BunifuDropdown();
            this.bunifuLabel6 = new Bunifu.UI.WinForms.BunifuLabel();
            this.bunifuLabel7 = new Bunifu.UI.WinForms.BunifuLabel();
            this.bunifuLabel8 = new Bunifu.UI.WinForms.BunifuLabel();
            this.totalBalanceLabel = new Bunifu.UI.WinForms.BunifuLabel();
            this.usedBalanceLabel = new Bunifu.UI.WinForms.BunifuLabel();
            this.availableBalanceLabel = new Bunifu.UI.WinForms.BunifuLabel();
            this.balanceGroupBox = new Bunifu.UI.WinForms.BunifuGroupBox();
            this.filtersGroupBox = new Bunifu.UI.WinForms.BunifuGroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.reportsDataGridView)).BeginInit();
            this.balanceGroupBox.SuspendLayout();
            this.filtersGroupBox.SuspendLayout();
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
            this.controlBox.Location = new System.Drawing.Point(1109, 5);
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
            this.controlBox.Size = new System.Drawing.Size(73, 36);
            this.controlBox.TabIndex = 50;
            // 
            // reportsDataGridView
            // 
            this.reportsDataGridView.AllowCustomTheming = true;
            this.reportsDataGridView.AllowUserToAddRows = false;
            this.reportsDataGridView.AllowUserToDeleteRows = false;
            this.reportsDataGridView.AllowUserToResizeColumns = false;
            this.reportsDataGridView.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(251)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            this.reportsDataGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.reportsDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.reportsDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.reportsDataGridView.BackgroundColor = System.Drawing.Color.White;
            this.reportsDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.reportsDataGridView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.reportsDataGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.DodgerBlue;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI Semibold", 11.75F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(115)))), ((int)(((byte)(204)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.reportsDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
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
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(232)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.reportsDataGridView.DefaultCellStyle = dataGridViewCellStyle3;
            this.reportsDataGridView.EnableHeadersVisualStyles = false;
            this.reportsDataGridView.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(238)))), ((int)(((byte)(255)))));
            this.reportsDataGridView.HeaderBackColor = System.Drawing.Color.DodgerBlue;
            this.reportsDataGridView.HeaderBgColor = System.Drawing.Color.Empty;
            this.reportsDataGridView.HeaderForeColor = System.Drawing.Color.White;
            this.reportsDataGridView.Location = new System.Drawing.Point(1, 159);
            this.reportsDataGridView.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.reportsDataGridView.Name = "reportsDataGridView";
            this.reportsDataGridView.RowHeadersVisible = false;
            this.reportsDataGridView.RowHeadersWidth = 51;
            this.reportsDataGridView.RowTemplate.Height = 40;
            this.reportsDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.reportsDataGridView.Size = new System.Drawing.Size(1181, 507);
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
            this.fromDatePicker.Location = new System.Drawing.Point(449, 50);
            this.fromDatePicker.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.fromDatePicker.MinimumSize = new System.Drawing.Size(4, 32);
            this.fromDatePicker.Name = "fromDatePicker";
            this.fromDatePicker.Size = new System.Drawing.Size(136, 32);
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
            this.bunifuLabel1.Location = new System.Drawing.Point(492, 19);
            this.bunifuLabel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.bunifuLabel1.Name = "bunifuLabel1";
            this.bunifuLabel1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.bunifuLabel1.Size = new System.Drawing.Size(37, 20);
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
            this.bunifuLabel2.Location = new System.Drawing.Point(505, 86);
            this.bunifuLabel2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.bunifuLabel2.Name = "bunifuLabel2";
            this.bunifuLabel2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.bunifuLabel2.Size = new System.Drawing.Size(20, 20);
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
            this.toDatePicker.Location = new System.Drawing.Point(449, 118);
            this.toDatePicker.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.toDatePicker.MinimumSize = new System.Drawing.Size(4, 32);
            this.toDatePicker.Name = "toDatePicker";
            this.toDatePicker.Size = new System.Drawing.Size(136, 32);
            this.toDatePicker.TabIndex = 55;
            this.toDatePicker.Value = new System.DateTime(2025, 1, 8, 14, 50, 0, 0);
            // 
            // generateButton
            // 
            this.generateButton.AllowAnimations = true;
            this.generateButton.AllowMouseEffects = true;
            this.generateButton.AllowToggling = false;
            this.generateButton.AnimationSpeed = 200;
            this.generateButton.AutoGenerateColors = true;
            this.generateButton.AutoRoundBorders = false;
            this.generateButton.AutoSizeLeftIcon = true;
            this.generateButton.AutoSizeRightIcon = true;
            this.generateButton.BackColor = System.Drawing.Color.Transparent;
            this.generateButton.BackColor1 = System.Drawing.Color.DodgerBlue;
            this.generateButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("generateButton.BackgroundImage")));
            this.generateButton.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton2.BorderStyles.Solid;
            this.generateButton.ButtonText = "Generate";
            this.generateButton.ButtonTextMarginLeft = 0;
            this.generateButton.ColorContrastOnClick = 45;
            this.generateButton.ColorContrastOnHover = 45;
            this.generateButton.Cursor = System.Windows.Forms.Cursors.Default;
            borderEdges1.BottomLeft = true;
            borderEdges1.BottomRight = true;
            borderEdges1.TopLeft = true;
            borderEdges1.TopRight = true;
            this.generateButton.CustomizableEdges = borderEdges1;
            this.generateButton.DialogResult = System.Windows.Forms.DialogResult.None;
            this.generateButton.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(191)))), ((int)(((byte)(191)))));
            this.generateButton.DisabledFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.generateButton.DisabledForecolor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(160)))), ((int)(((byte)(168)))));
            this.generateButton.FocusState = Bunifu.UI.WinForms.BunifuButton.BunifuButton2.ButtonStates.Pressed;
            this.generateButton.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.generateButton.ForeColor = System.Drawing.Color.White;
            this.generateButton.IconLeftAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.generateButton.IconLeftCursor = System.Windows.Forms.Cursors.Default;
            this.generateButton.IconLeftPadding = new System.Windows.Forms.Padding(11, 3, 3, 3);
            this.generateButton.IconMarginLeft = 11;
            this.generateButton.IconPadding = 10;
            this.generateButton.IconRightAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.generateButton.IconRightCursor = System.Windows.Forms.Cursors.Default;
            this.generateButton.IconRightPadding = new System.Windows.Forms.Padding(3, 3, 7, 3);
            this.generateButton.IconSize = 25;
            this.generateButton.IdleBorderColor = System.Drawing.Color.DodgerBlue;
            this.generateButton.IdleBorderRadius = 1;
            this.generateButton.IdleBorderThickness = 1;
            this.generateButton.IdleFillColor = System.Drawing.Color.DodgerBlue;
            this.generateButton.IdleIconLeftImage = null;
            this.generateButton.IdleIconRightImage = null;
            this.generateButton.IndicateFocus = true;
            this.generateButton.Location = new System.Drawing.Point(621, 86);
            this.generateButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.generateButton.Name = "generateButton";
            this.generateButton.OnDisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(191)))), ((int)(((byte)(191)))));
            this.generateButton.OnDisabledState.BorderRadius = 1;
            this.generateButton.OnDisabledState.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton2.BorderStyles.Solid;
            this.generateButton.OnDisabledState.BorderThickness = 1;
            this.generateButton.OnDisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.generateButton.OnDisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(160)))), ((int)(((byte)(168)))));
            this.generateButton.OnDisabledState.IconLeftImage = null;
            this.generateButton.OnDisabledState.IconRightImage = null;
            this.generateButton.onHoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(131)))), ((int)(((byte)(193)))), ((int)(((byte)(255)))));
            this.generateButton.onHoverState.BorderRadius = 1;
            this.generateButton.onHoverState.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton2.BorderStyles.Solid;
            this.generateButton.onHoverState.BorderThickness = 1;
            this.generateButton.onHoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(131)))), ((int)(((byte)(193)))), ((int)(((byte)(255)))));
            this.generateButton.onHoverState.ForeColor = System.Drawing.Color.White;
            this.generateButton.onHoverState.IconLeftImage = null;
            this.generateButton.onHoverState.IconRightImage = null;
            this.generateButton.OnIdleState.BorderColor = System.Drawing.Color.DodgerBlue;
            this.generateButton.OnIdleState.BorderRadius = 1;
            this.generateButton.OnIdleState.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton2.BorderStyles.Solid;
            this.generateButton.OnIdleState.BorderThickness = 1;
            this.generateButton.OnIdleState.FillColor = System.Drawing.Color.DodgerBlue;
            this.generateButton.OnIdleState.ForeColor = System.Drawing.Color.White;
            this.generateButton.OnIdleState.IconLeftImage = null;
            this.generateButton.OnIdleState.IconRightImage = null;
            this.generateButton.OnPressedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(79)))), ((int)(((byte)(140)))));
            this.generateButton.OnPressedState.BorderRadius = 1;
            this.generateButton.OnPressedState.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton2.BorderStyles.Solid;
            this.generateButton.OnPressedState.BorderThickness = 1;
            this.generateButton.OnPressedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(79)))), ((int)(((byte)(140)))));
            this.generateButton.OnPressedState.ForeColor = System.Drawing.Color.White;
            this.generateButton.OnPressedState.IconLeftImage = null;
            this.generateButton.OnPressedState.IconRightImage = null;
            this.generateButton.Size = new System.Drawing.Size(129, 32);
            this.generateButton.TabIndex = 57;
            this.generateButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.generateButton.TextAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.generateButton.TextMarginLeft = 0;
            this.generateButton.TextPadding = new System.Windows.Forms.Padding(0);
            this.generateButton.UseDefaultRadiusAndThickness = true;
            this.generateButton.Click += new System.EventHandler(this.generateButton_Click);
            // 
            // nameORdepDropdown
            // 
            this.nameORdepDropdown.BackColor = System.Drawing.Color.Transparent;
            this.nameORdepDropdown.BackgroundColor = System.Drawing.Color.White;
            this.nameORdepDropdown.BorderColor = System.Drawing.Color.Silver;
            this.nameORdepDropdown.BorderRadius = 1;
            this.nameORdepDropdown.Color = System.Drawing.Color.Silver;
            this.nameORdepDropdown.Direction = Bunifu.UI.WinForms.BunifuDropdown.Directions.Down;
            this.nameORdepDropdown.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.nameORdepDropdown.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.nameORdepDropdown.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.nameORdepDropdown.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.nameORdepDropdown.DisabledIndicatorColor = System.Drawing.Color.DarkGray;
            this.nameORdepDropdown.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.nameORdepDropdown.DropdownBorderThickness = Bunifu.UI.WinForms.BunifuDropdown.BorderThickness.Thin;
            this.nameORdepDropdown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.nameORdepDropdown.DropDownTextAlign = Bunifu.UI.WinForms.BunifuDropdown.TextAlign.Center;
            this.nameORdepDropdown.FillDropDown = true;
            this.nameORdepDropdown.FillIndicator = false;
            this.nameORdepDropdown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.nameORdepDropdown.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.nameORdepDropdown.ForeColor = System.Drawing.Color.Black;
            this.nameORdepDropdown.FormattingEnabled = true;
            this.nameORdepDropdown.Icon = null;
            this.nameORdepDropdown.IndicatorAlignment = Bunifu.UI.WinForms.BunifuDropdown.Indicator.Right;
            this.nameORdepDropdown.IndicatorColor = System.Drawing.Color.DarkGray;
            this.nameORdepDropdown.IndicatorLocation = Bunifu.UI.WinForms.BunifuDropdown.Indicator.Right;
            this.nameORdepDropdown.IndicatorThickness = 2;
            this.nameORdepDropdown.IsDropdownOpened = false;
            this.nameORdepDropdown.ItemBackColor = System.Drawing.Color.White;
            this.nameORdepDropdown.ItemBorderColor = System.Drawing.Color.White;
            this.nameORdepDropdown.ItemForeColor = System.Drawing.Color.Black;
            this.nameORdepDropdown.ItemHeight = 26;
            this.nameORdepDropdown.ItemHighLightColor = System.Drawing.Color.DodgerBlue;
            this.nameORdepDropdown.ItemHighLightForeColor = System.Drawing.Color.White;
            this.nameORdepDropdown.ItemTopMargin = 3;
            this.nameORdepDropdown.Location = new System.Drawing.Point(9, 108);
            this.nameORdepDropdown.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.nameORdepDropdown.Name = "nameORdepDropdown";
            this.nameORdepDropdown.Size = new System.Drawing.Size(223, 32);
            this.nameORdepDropdown.TabIndex = 58;
            this.nameORdepDropdown.Text = null;
            this.nameORdepDropdown.TextAlignment = Bunifu.UI.WinForms.BunifuDropdown.TextAlign.Center;
            this.nameORdepDropdown.TextLeftMargin = 5;
            this.nameORdepDropdown.SelectedIndexChanged += new System.EventHandler(this.nameORdepDropdown_SelectedIndexChanged);
            // 
            // filterLabel
            // 
            this.filterLabel.AllowParentOverrides = false;
            this.filterLabel.AutoEllipsis = false;
            this.filterLabel.Cursor = System.Windows.Forms.Cursors.Default;
            this.filterLabel.CursorType = System.Windows.Forms.Cursors.Default;
            this.filterLabel.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.filterLabel.Location = new System.Drawing.Point(80, 76);
            this.filterLabel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.filterLabel.Name = "filterLabel";
            this.filterLabel.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.filterLabel.Size = new System.Drawing.Size(83, 20);
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
            this.bunifuLabel3.Location = new System.Drawing.Point(89, 9);
            this.bunifuLabel3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.bunifuLabel3.Name = "bunifuLabel3";
            this.bunifuLabel3.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.bunifuLabel3.Size = new System.Drawing.Size(56, 20);
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
            this.filterDropdown.Location = new System.Drawing.Point(9, 41);
            this.filterDropdown.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.filterDropdown.Name = "filterDropdown";
            this.filterDropdown.Size = new System.Drawing.Size(223, 32);
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
            this.bunifuLabel4.Location = new System.Drawing.Point(311, 19);
            this.bunifuLabel4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.bunifuLabel4.Name = "bunifuLabel4";
            this.bunifuLabel4.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.bunifuLabel4.Size = new System.Drawing.Size(43, 20);
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
            this.statusDropdown.Location = new System.Drawing.Point(260, 50);
            this.statusDropdown.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
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
            this.bunifuLabel5.Location = new System.Drawing.Point(321, 86);
            this.bunifuLabel5.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.bunifuLabel5.Name = "bunifuLabel5";
            this.bunifuLabel5.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.bunifuLabel5.Size = new System.Drawing.Size(35, 20);
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
            this.typeDropdown.Location = new System.Drawing.Point(260, 118);
            this.typeDropdown.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.typeDropdown.Name = "typeDropdown";
            this.typeDropdown.Size = new System.Drawing.Size(167, 32);
            this.typeDropdown.Sorted = true;
            this.typeDropdown.TabIndex = 64;
            this.typeDropdown.Text = null;
            this.typeDropdown.TextAlignment = Bunifu.UI.WinForms.BunifuDropdown.TextAlign.Center;
            this.typeDropdown.TextLeftMargin = 5;
            this.typeDropdown.SelectedIndexChanged += new System.EventHandler(this.typeDropdown_SelectedIndexChanged);
            // 
            // bunifuLabel6
            // 
            this.bunifuLabel6.AllowParentOverrides = false;
            this.bunifuLabel6.AutoEllipsis = false;
            this.bunifuLabel6.Cursor = System.Windows.Forms.Cursors.Default;
            this.bunifuLabel6.CursorType = System.Windows.Forms.Cursors.Default;
            this.bunifuLabel6.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.bunifuLabel6.Location = new System.Drawing.Point(56, 13);
            this.bunifuLabel6.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.bunifuLabel6.Name = "bunifuLabel6";
            this.bunifuLabel6.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.bunifuLabel6.Size = new System.Drawing.Size(93, 20);
            this.bunifuLabel6.TabIndex = 66;
            this.bunifuLabel6.Text = "Total Balance:";
            this.bunifuLabel6.TextAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.bunifuLabel6.TextFormat = Bunifu.UI.WinForms.BunifuLabel.TextFormattingOptions.Default;
            // 
            // bunifuLabel7
            // 
            this.bunifuLabel7.AllowParentOverrides = false;
            this.bunifuLabel7.AutoEllipsis = false;
            this.bunifuLabel7.Cursor = System.Windows.Forms.Cursors.Default;
            this.bunifuLabel7.CursorType = System.Windows.Forms.Cursors.Default;
            this.bunifuLabel7.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.bunifuLabel7.Location = new System.Drawing.Point(110, 38);
            this.bunifuLabel7.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.bunifuLabel7.Name = "bunifuLabel7";
            this.bunifuLabel7.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.bunifuLabel7.Size = new System.Drawing.Size(36, 20);
            this.bunifuLabel7.TabIndex = 67;
            this.bunifuLabel7.Text = "Used:";
            this.bunifuLabel7.TextAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.bunifuLabel7.TextFormat = Bunifu.UI.WinForms.BunifuLabel.TextFormattingOptions.Default;
            // 
            // bunifuLabel8
            // 
            this.bunifuLabel8.AllowParentOverrides = false;
            this.bunifuLabel8.AutoEllipsis = false;
            this.bunifuLabel8.Cursor = System.Windows.Forms.Cursors.Default;
            this.bunifuLabel8.CursorType = System.Windows.Forms.Cursors.Default;
            this.bunifuLabel8.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.bunifuLabel8.Location = new System.Drawing.Point(28, 63);
            this.bunifuLabel8.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.bunifuLabel8.Name = "bunifuLabel8";
            this.bunifuLabel8.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.bunifuLabel8.Size = new System.Drawing.Size(121, 20);
            this.bunifuLabel8.TabIndex = 68;
            this.bunifuLabel8.Text = "Available Balance:";
            this.bunifuLabel8.TextAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.bunifuLabel8.TextFormat = Bunifu.UI.WinForms.BunifuLabel.TextFormattingOptions.Default;
            // 
            // totalBalanceLabel
            // 
            this.totalBalanceLabel.AllowParentOverrides = false;
            this.totalBalanceLabel.AutoEllipsis = false;
            this.totalBalanceLabel.Cursor = System.Windows.Forms.Cursors.Default;
            this.totalBalanceLabel.CursorType = System.Windows.Forms.Cursors.Default;
            this.totalBalanceLabel.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.totalBalanceLabel.Location = new System.Drawing.Point(153, 13);
            this.totalBalanceLabel.Name = "totalBalanceLabel";
            this.totalBalanceLabel.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.totalBalanceLabel.Size = new System.Drawing.Size(36, 20);
            this.totalBalanceLabel.TabIndex = 69;
            this.totalBalanceLabel.Text = "------";
            this.totalBalanceLabel.TextAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.totalBalanceLabel.TextFormat = Bunifu.UI.WinForms.BunifuLabel.TextFormattingOptions.Default;
            // 
            // usedBalanceLabel
            // 
            this.usedBalanceLabel.AllowParentOverrides = false;
            this.usedBalanceLabel.AutoEllipsis = false;
            this.usedBalanceLabel.Cursor = System.Windows.Forms.Cursors.Default;
            this.usedBalanceLabel.CursorType = System.Windows.Forms.Cursors.Default;
            this.usedBalanceLabel.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.usedBalanceLabel.Location = new System.Drawing.Point(153, 40);
            this.usedBalanceLabel.Name = "usedBalanceLabel";
            this.usedBalanceLabel.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.usedBalanceLabel.Size = new System.Drawing.Size(36, 20);
            this.usedBalanceLabel.TabIndex = 70;
            this.usedBalanceLabel.Text = "------";
            this.usedBalanceLabel.TextAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.usedBalanceLabel.TextFormat = Bunifu.UI.WinForms.BunifuLabel.TextFormattingOptions.Default;
            // 
            // availableBalanceLabel
            // 
            this.availableBalanceLabel.AllowParentOverrides = false;
            this.availableBalanceLabel.AutoEllipsis = false;
            this.availableBalanceLabel.Cursor = System.Windows.Forms.Cursors.Default;
            this.availableBalanceLabel.CursorType = System.Windows.Forms.Cursors.Default;
            this.availableBalanceLabel.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.availableBalanceLabel.Location = new System.Drawing.Point(153, 63);
            this.availableBalanceLabel.Name = "availableBalanceLabel";
            this.availableBalanceLabel.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.availableBalanceLabel.Size = new System.Drawing.Size(36, 20);
            this.availableBalanceLabel.TabIndex = 71;
            this.availableBalanceLabel.Text = "------";
            this.availableBalanceLabel.TextAlignment = System.Drawing.ContentAlignment.TopCenter;
            this.availableBalanceLabel.TextFormat = Bunifu.UI.WinForms.BunifuLabel.TextFormattingOptions.Default;
            // 
            // balanceGroupBox
            // 
            this.balanceGroupBox.BorderColor = System.Drawing.Color.Transparent;
            this.balanceGroupBox.BorderRadius = 1;
            this.balanceGroupBox.BorderThickness = 1;
            this.balanceGroupBox.Controls.Add(this.availableBalanceLabel);
            this.balanceGroupBox.Controls.Add(this.bunifuLabel8);
            this.balanceGroupBox.Controls.Add(this.usedBalanceLabel);
            this.balanceGroupBox.Controls.Add(this.bunifuLabel6);
            this.balanceGroupBox.Controls.Add(this.totalBalanceLabel);
            this.balanceGroupBox.Controls.Add(this.bunifuLabel7);
            this.balanceGroupBox.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.balanceGroupBox.LabelAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.balanceGroupBox.LabelIndent = 10;
            this.balanceGroupBox.LineStyle = Bunifu.UI.WinForms.BunifuGroupBox.LineStyles.Solid;
            this.balanceGroupBox.Location = new System.Drawing.Point(832, 37);
            this.balanceGroupBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.balanceGroupBox.Name = "balanceGroupBox";
            this.balanceGroupBox.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.balanceGroupBox.Size = new System.Drawing.Size(212, 98);
            this.balanceGroupBox.TabIndex = 72;
            this.balanceGroupBox.TabStop = false;
            this.balanceGroupBox.Visible = false;
            // 
            // filtersGroupBox
            // 
            this.filtersGroupBox.BorderColor = System.Drawing.Color.Transparent;
            this.filtersGroupBox.BorderRadius = 1;
            this.filtersGroupBox.BorderThickness = 1;
            this.filtersGroupBox.Controls.Add(this.nameORdepDropdown);
            this.filtersGroupBox.Controls.Add(this.filterLabel);
            this.filtersGroupBox.Controls.Add(this.filterDropdown);
            this.filtersGroupBox.Controls.Add(this.bunifuLabel3);
            this.filtersGroupBox.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.filtersGroupBox.LabelAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.filtersGroupBox.LabelIndent = 10;
            this.filtersGroupBox.LineStyle = Bunifu.UI.WinForms.BunifuGroupBox.LineStyles.Solid;
            this.filtersGroupBox.Location = new System.Drawing.Point(10, 10);
            this.filtersGroupBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.filtersGroupBox.Name = "filtersGroupBox";
            this.filtersGroupBox.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.filtersGroupBox.Size = new System.Drawing.Size(244, 145);
            this.filtersGroupBox.TabIndex = 72;
            this.filtersGroupBox.TabStop = false;
            this.filtersGroupBox.Visible = false;
            // 
            // reportsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1187, 640);
            this.Controls.Add(this.filtersGroupBox);
            this.Controls.Add(this.bunifuLabel5);
            this.Controls.Add(this.typeDropdown);
            this.Controls.Add(this.bunifuLabel4);
            this.Controls.Add(this.statusDropdown);
            this.Controls.Add(this.generateButton);
            this.Controls.Add(this.bunifuLabel2);
            this.Controls.Add(this.toDatePicker);
            this.Controls.Add(this.bunifuLabel1);
            this.Controls.Add(this.fromDatePicker);
            this.Controls.Add(this.reportsDataGridView);
            this.Controls.Add(this.controlBox);
            this.Controls.Add(this.balanceGroupBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.Name = "reportsForm";
            this.Text = "Reports";
            this.Load += new System.EventHandler(this.reportForm_Load);
            this.Resize += new System.EventHandler(this.reportForm_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.reportsDataGridView)).EndInit();
            this.balanceGroupBox.ResumeLayout(false);
            this.balanceGroupBox.PerformLayout();
            this.filtersGroupBox.ResumeLayout(false);
            this.filtersGroupBox.PerformLayout();
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
        private Bunifu.UI.WinForms.BunifuButton.BunifuButton2 generateButton;
        private Bunifu.UI.WinForms.BunifuDropdown nameORdepDropdown;
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
        private Bunifu.UI.WinForms.BunifuLabel bunifuLabel6;
        private Bunifu.UI.WinForms.BunifuLabel bunifuLabel7;
        private Bunifu.UI.WinForms.BunifuLabel bunifuLabel8;
        private Bunifu.UI.WinForms.BunifuLabel totalBalanceLabel;
        private Bunifu.UI.WinForms.BunifuLabel usedBalanceLabel;
        private Bunifu.UI.WinForms.BunifuLabel availableBalanceLabel;
        private Bunifu.UI.WinForms.BunifuGroupBox balanceGroupBox;
        private Bunifu.UI.WinForms.BunifuGroupBox filtersGroupBox;
    }
}