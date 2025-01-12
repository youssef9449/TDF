using System.Windows.Forms;

namespace TDF.Net.Forms
{
    partial class requestsForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(requestsForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderEdges borderEdges1 = new Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderEdges();
            Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderEdges borderEdges2 = new Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderEdges();
            Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderEdges borderEdges3 = new Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderEdges();
            this.requestsDataGridView = new Bunifu.UI.WinForms.BunifuDataGridView();
            this.RequestID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RequestUserFullName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RequestType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RequestFromDay = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RequestToDay = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NumberOfDays = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.remainingBalance = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RequestReason = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RequestBeginningTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RequestEndingTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RequestRejectReason = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RequestStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Edit = new System.Windows.Forms.DataGridViewImageColumn();
            this.Remove = new System.Windows.Forms.DataGridViewImageColumn();
            this.Report = new System.Windows.Forms.DataGridViewImageColumn();
            this.Approve = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Reject = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.pendingRadioButton = new Bunifu.UI.WinForms.BunifuRadioButton();
            this.closedRadioButton = new Bunifu.UI.WinForms.BunifuRadioButton();
            this.pendingLabel = new Bunifu.UI.WinForms.BunifuLabel();
            this.closedLabel = new Bunifu.UI.WinForms.BunifuLabel();
            this.controlBox = new Bunifu.UI.WinForms.BunifuFormControlBox();
            this.dataGridViewImageColumn1 = new System.Windows.Forms.DataGridViewImageColumn();
            this.dataGridViewImageColumn2 = new System.Windows.Forms.DataGridViewImageColumn();
            this.dataGridViewImageColumn3 = new System.Windows.Forms.DataGridViewImageColumn();
            this.refreshButton = new Bunifu.UI.WinForms.BunifuButton.BunifuButton();
            this.addRequestButton = new Bunifu.UI.WinForms.BunifuButton.BunifuButton();
            this.applyButton = new Bunifu.UI.WinForms.BunifuButton.BunifuButton();
            ((System.ComponentModel.ISupportInitialize)(this.requestsDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // requestsDataGridView
            // 
            this.requestsDataGridView.AllowCustomTheming = true;
            this.requestsDataGridView.AllowUserToAddRows = false;
            this.requestsDataGridView.AllowUserToDeleteRows = false;
            this.requestsDataGridView.AllowUserToResizeColumns = false;
            this.requestsDataGridView.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(251)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            this.requestsDataGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            resources.ApplyResources(this.requestsDataGridView, "requestsDataGridView");
            this.requestsDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;
            this.requestsDataGridView.BackgroundColor = System.Drawing.Color.White;
            this.requestsDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.requestsDataGridView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.requestsDataGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.DodgerBlue;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI Semibold", 11.75F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(115)))), ((int)(((byte)(204)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.requestsDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.requestsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.requestsDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.RequestID,
            this.RequestUserFullName,
            this.RequestType,
            this.RequestFromDay,
            this.RequestToDay,
            this.NumberOfDays,
            this.remainingBalance,
            this.RequestReason,
            this.RequestBeginningTime,
            this.RequestEndingTime,
            this.RequestRejectReason,
            this.RequestStatus,
            this.Edit,
            this.Remove,
            this.Report,
            this.Approve,
            this.Reject});
            this.requestsDataGridView.CurrentTheme.AlternatingRowsStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(251)))), ((int)(((byte)(255)))));
            this.requestsDataGridView.CurrentTheme.AlternatingRowsStyle.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.requestsDataGridView.CurrentTheme.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Black;
            this.requestsDataGridView.CurrentTheme.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(232)))), ((int)(((byte)(255)))));
            this.requestsDataGridView.CurrentTheme.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Black;
            this.requestsDataGridView.CurrentTheme.BackColor = System.Drawing.Color.White;
            this.requestsDataGridView.CurrentTheme.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(238)))), ((int)(((byte)(255)))));
            this.requestsDataGridView.CurrentTheme.HeaderStyle.BackColor = System.Drawing.Color.DodgerBlue;
            this.requestsDataGridView.CurrentTheme.HeaderStyle.Font = new System.Drawing.Font("Segoe UI Semibold", 11.75F, System.Drawing.FontStyle.Bold);
            this.requestsDataGridView.CurrentTheme.HeaderStyle.ForeColor = System.Drawing.Color.White;
            this.requestsDataGridView.CurrentTheme.HeaderStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(115)))), ((int)(((byte)(204)))));
            this.requestsDataGridView.CurrentTheme.HeaderStyle.SelectionForeColor = System.Drawing.Color.White;
            this.requestsDataGridView.CurrentTheme.Name = null;
            this.requestsDataGridView.CurrentTheme.RowsStyle.BackColor = System.Drawing.Color.White;
            this.requestsDataGridView.CurrentTheme.RowsStyle.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.requestsDataGridView.CurrentTheme.RowsStyle.ForeColor = System.Drawing.Color.Black;
            this.requestsDataGridView.CurrentTheme.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(232)))), ((int)(((byte)(255)))));
            this.requestsDataGridView.CurrentTheme.RowsStyle.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle12.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle12.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(232)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.requestsDataGridView.DefaultCellStyle = dataGridViewCellStyle12;
            this.requestsDataGridView.EnableHeadersVisualStyles = false;
            this.requestsDataGridView.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(238)))), ((int)(((byte)(255)))));
            this.requestsDataGridView.HeaderBackColor = System.Drawing.Color.DodgerBlue;
            this.requestsDataGridView.HeaderBgColor = System.Drawing.Color.Empty;
            this.requestsDataGridView.HeaderForeColor = System.Drawing.Color.White;
            this.requestsDataGridView.Name = "requestsDataGridView";
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle13.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle13.Font = new System.Drawing.Font("Tahoma", 5F);
            dataGridViewCellStyle13.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle13.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle13.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle13.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.requestsDataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle13;
            this.requestsDataGridView.RowHeadersVisible = false;
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle14.BackColor = System.Drawing.Color.White;
            this.requestsDataGridView.RowsDefaultCellStyle = dataGridViewCellStyle14;
            this.requestsDataGridView.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.requestsDataGridView.RowTemplate.Height = 40;
            this.requestsDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.requestsDataGridView.Theme = Bunifu.UI.WinForms.BunifuDataGridView.PresetThemes.Light;
            this.requestsDataGridView.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.requestsDataGridView_CellBeginEdit);
            this.requestsDataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.requestsDataGridView_CellContentClick);
            this.requestsDataGridView.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.requestsDataGridView_CellFormatting);
            this.requestsDataGridView.CellMouseEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.requestsDataGridView_CellMouseEnter);
            this.requestsDataGridView.CellMouseLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.requestsDataGridView_CellMouseLeave);
            // 
            // RequestID
            // 
            this.RequestID.DataPropertyName = "RequestID";
            this.RequestID.Frozen = true;
            resources.ApplyResources(this.RequestID, "RequestID");
            this.RequestID.Name = "RequestID";
            this.RequestID.ReadOnly = true;
            // 
            // RequestUserFullName
            // 
            this.RequestUserFullName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.RequestUserFullName.DataPropertyName = "RequestUserFullName";
            this.RequestUserFullName.Frozen = true;
            resources.ApplyResources(this.RequestUserFullName, "RequestUserFullName");
            this.RequestUserFullName.Name = "RequestUserFullName";
            this.RequestUserFullName.ReadOnly = true;
            // 
            // RequestType
            // 
            this.RequestType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.RequestType.DataPropertyName = "RequestType";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.RequestType.DefaultCellStyle = dataGridViewCellStyle3;
            this.RequestType.Frozen = true;
            resources.ApplyResources(this.RequestType, "RequestType");
            this.RequestType.Name = "RequestType";
            this.RequestType.ReadOnly = true;
            // 
            // RequestFromDay
            // 
            this.RequestFromDay.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.RequestFromDay.DataPropertyName = "RequestFromDay";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.Format = "dd/MM/yyyy";
            dataGridViewCellStyle4.NullValue = null;
            this.RequestFromDay.DefaultCellStyle = dataGridViewCellStyle4;
            this.RequestFromDay.Frozen = true;
            resources.ApplyResources(this.RequestFromDay, "RequestFromDay");
            this.RequestFromDay.Name = "RequestFromDay";
            this.RequestFromDay.ReadOnly = true;
            // 
            // RequestToDay
            // 
            this.RequestToDay.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.RequestToDay.DataPropertyName = "RequestToDay";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.Format = "dd/MM/yyyy";
            dataGridViewCellStyle5.NullValue = "-";
            this.RequestToDay.DefaultCellStyle = dataGridViewCellStyle5;
            this.RequestToDay.Frozen = true;
            resources.ApplyResources(this.RequestToDay, "RequestToDay");
            this.RequestToDay.Name = "RequestToDay";
            this.RequestToDay.ReadOnly = true;
            // 
            // NumberOfDays
            // 
            this.NumberOfDays.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.NumberOfDays.DataPropertyName = "RequestNumberOfDays";
            this.NumberOfDays.Frozen = true;
            resources.ApplyResources(this.NumberOfDays, "NumberOfDays");
            this.NumberOfDays.Name = "NumberOfDays";
            this.NumberOfDays.ReadOnly = true;
            // 
            // remainingBalance
            // 
            this.remainingBalance.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.remainingBalance.DataPropertyName = "remainingBalance";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.Format = "N0";
            dataGridViewCellStyle6.NullValue = "-";
            this.remainingBalance.DefaultCellStyle = dataGridViewCellStyle6;
            this.remainingBalance.Frozen = true;
            resources.ApplyResources(this.remainingBalance, "remainingBalance");
            this.remainingBalance.Name = "remainingBalance";
            this.remainingBalance.ReadOnly = true;
            this.remainingBalance.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // RequestReason
            // 
            this.RequestReason.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.RequestReason.DataPropertyName = "RequestReason";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.NullValue = "-";
            this.RequestReason.DefaultCellStyle = dataGridViewCellStyle7;
            resources.ApplyResources(this.RequestReason, "RequestReason");
            this.RequestReason.Name = "RequestReason";
            this.RequestReason.ReadOnly = true;
            // 
            // RequestBeginningTime
            // 
            this.RequestBeginningTime.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.RequestBeginningTime.DataPropertyName = "RequestBeginningTime";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle8.Format = "t";
            dataGridViewCellStyle8.NullValue = "-";
            this.RequestBeginningTime.DefaultCellStyle = dataGridViewCellStyle8;
            resources.ApplyResources(this.RequestBeginningTime, "RequestBeginningTime");
            this.RequestBeginningTime.Name = "RequestBeginningTime";
            this.RequestBeginningTime.ReadOnly = true;
            // 
            // RequestEndingTime
            // 
            this.RequestEndingTime.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.RequestEndingTime.DataPropertyName = "RequestEndingTime";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle9.Format = "t";
            dataGridViewCellStyle9.NullValue = "-";
            this.RequestEndingTime.DefaultCellStyle = dataGridViewCellStyle9;
            resources.ApplyResources(this.RequestEndingTime, "RequestEndingTime");
            this.RequestEndingTime.Name = "RequestEndingTime";
            this.RequestEndingTime.ReadOnly = true;
            // 
            // RequestRejectReason
            // 
            this.RequestRejectReason.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.RequestRejectReason.DataPropertyName = "RequestRejectReason";
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle10.NullValue = "-";
            this.RequestRejectReason.DefaultCellStyle = dataGridViewCellStyle10;
            resources.ApplyResources(this.RequestRejectReason, "RequestRejectReason");
            this.RequestRejectReason.Name = "RequestRejectReason";
            // 
            // RequestStatus
            // 
            this.RequestStatus.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.RequestStatus.DataPropertyName = "RequestStatus";
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.RequestStatus.DefaultCellStyle = dataGridViewCellStyle11;
            resources.ApplyResources(this.RequestStatus, "RequestStatus");
            this.RequestStatus.Name = "RequestStatus";
            this.RequestStatus.ReadOnly = true;
            // 
            // Edit
            // 
            this.Edit.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            resources.ApplyResources(this.Edit, "Edit");
            this.Edit.Image = global::TDF.Properties.Resources.edit;
            this.Edit.Name = "Edit";
            this.Edit.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Edit.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // Remove
            // 
            this.Remove.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            resources.ApplyResources(this.Remove, "Remove");
            this.Remove.Image = global::TDF.Properties.Resources.delete;
            this.Remove.Name = "Remove";
            this.Remove.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Remove.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // Report
            // 
            this.Report.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            resources.ApplyResources(this.Report, "Report");
            this.Report.Image = global::TDF.Properties.Resources.pdf;
            this.Report.Name = "Report";
            // 
            // Approve
            // 
            this.Approve.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            resources.ApplyResources(this.Approve, "Approve");
            this.Approve.Name = "Approve";
            this.Approve.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Approve.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // Reject
            // 
            this.Reject.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            resources.ApplyResources(this.Reject, "Reject");
            this.Reject.Name = "Reject";
            this.Reject.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Reject.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // pendingRadioButton
            // 
            this.pendingRadioButton.AllowBindingControlLocation = false;
            this.pendingRadioButton.BackColor = System.Drawing.Color.Transparent;
            this.pendingRadioButton.BindingControlPosition = Bunifu.UI.WinForms.BunifuRadioButton.BindingControlPositions.Right;
            this.pendingRadioButton.BorderThickness = 1;
            this.pendingRadioButton.Checked = true;
            resources.ApplyResources(this.pendingRadioButton, "pendingRadioButton");
            this.pendingRadioButton.Name = "pendingRadioButton";
            this.pendingRadioButton.OutlineColor = System.Drawing.Color.DodgerBlue;
            this.pendingRadioButton.OutlineColorTabFocused = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(96)))), ((int)(((byte)(144)))));
            this.pendingRadioButton.OutlineColorUnchecked = System.Drawing.Color.DarkGray;
            this.pendingRadioButton.RadioColor = System.Drawing.Color.DodgerBlue;
            this.pendingRadioButton.RadioColorTabFocused = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(96)))), ((int)(((byte)(144)))));
            this.pendingRadioButton.CheckedChanged2 += new System.EventHandler<Bunifu.UI.WinForms.BunifuRadioButton.CheckedChangedEventArgs>(this.pendingRadioButton_CheckedChanged);
            // 
            // closedRadioButton
            // 
            this.closedRadioButton.AllowBindingControlLocation = false;
            this.closedRadioButton.BackColor = System.Drawing.Color.Transparent;
            this.closedRadioButton.BindingControlPosition = Bunifu.UI.WinForms.BunifuRadioButton.BindingControlPositions.Right;
            this.closedRadioButton.BorderThickness = 1;
            this.closedRadioButton.Checked = false;
            resources.ApplyResources(this.closedRadioButton, "closedRadioButton");
            this.closedRadioButton.Name = "closedRadioButton";
            this.closedRadioButton.OutlineColor = System.Drawing.Color.DodgerBlue;
            this.closedRadioButton.OutlineColorTabFocused = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(96)))), ((int)(((byte)(144)))));
            this.closedRadioButton.OutlineColorUnchecked = System.Drawing.Color.DarkGray;
            this.closedRadioButton.RadioColor = System.Drawing.Color.DodgerBlue;
            this.closedRadioButton.RadioColorTabFocused = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(96)))), ((int)(((byte)(144)))));
            // 
            // pendingLabel
            // 
            this.pendingLabel.AllowParentOverrides = false;
            this.pendingLabel.AutoEllipsis = false;
            this.pendingLabel.CursorType = null;
            resources.ApplyResources(this.pendingLabel, "pendingLabel");
            this.pendingLabel.Name = "pendingLabel";
            this.pendingLabel.TextAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.pendingLabel.TextFormat = Bunifu.UI.WinForms.BunifuLabel.TextFormattingOptions.Default;
            // 
            // closedLabel
            // 
            this.closedLabel.AllowParentOverrides = false;
            this.closedLabel.AutoEllipsis = false;
            this.closedLabel.CursorType = null;
            resources.ApplyResources(this.closedLabel, "closedLabel");
            this.closedLabel.Name = "closedLabel";
            this.closedLabel.TextAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.closedLabel.TextFormat = Bunifu.UI.WinForms.BunifuLabel.TextFormattingOptions.Default;
            // 
            // controlBox
            // 
            resources.ApplyResources(this.controlBox, "controlBox");
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
            this.controlBox.ShowDesignBorders = false;
            // 
            // dataGridViewImageColumn1
            // 
            this.dataGridViewImageColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            resources.ApplyResources(this.dataGridViewImageColumn1, "dataGridViewImageColumn1");
            this.dataGridViewImageColumn1.Image = global::TDF.Properties.Resources.edit;
            this.dataGridViewImageColumn1.Name = "dataGridViewImageColumn1";
            this.dataGridViewImageColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewImageColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // dataGridViewImageColumn2
            // 
            this.dataGridViewImageColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            resources.ApplyResources(this.dataGridViewImageColumn2, "dataGridViewImageColumn2");
            this.dataGridViewImageColumn2.Image = global::TDF.Properties.Resources.delete;
            this.dataGridViewImageColumn2.Name = "dataGridViewImageColumn2";
            this.dataGridViewImageColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewImageColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // dataGridViewImageColumn3
            // 
            this.dataGridViewImageColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            resources.ApplyResources(this.dataGridViewImageColumn3, "dataGridViewImageColumn3");
            this.dataGridViewImageColumn3.Image = global::TDF.Properties.Resources.pdf;
            this.dataGridViewImageColumn3.Name = "dataGridViewImageColumn3";
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
            resources.ApplyResources(this.refreshButton, "refreshButton");
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
            this.refreshButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.refreshButton.TextAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.refreshButton.TextMarginLeft = 0;
            this.refreshButton.TextPadding = new System.Windows.Forms.Padding(0);
            this.refreshButton.UseDefaultRadiusAndThickness = true;
            this.refreshButton.Click += new System.EventHandler(this.refreshButton_Click);
            // 
            // addRequestButton
            // 
            this.addRequestButton.AllowAnimations = true;
            this.addRequestButton.AllowMouseEffects = true;
            this.addRequestButton.AllowToggling = false;
            this.addRequestButton.AnimationSpeed = 200;
            this.addRequestButton.AutoGenerateColors = false;
            this.addRequestButton.AutoRoundBorders = false;
            this.addRequestButton.AutoSizeLeftIcon = true;
            this.addRequestButton.AutoSizeRightIcon = true;
            this.addRequestButton.BackColor = System.Drawing.Color.Transparent;
            this.addRequestButton.BackColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(122)))), ((int)(((byte)(183)))));
            resources.ApplyResources(this.addRequestButton, "addRequestButton");
            this.addRequestButton.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            this.addRequestButton.ButtonText = "Add Request";
            this.addRequestButton.ButtonTextMarginLeft = 0;
            this.addRequestButton.ColorContrastOnClick = 45;
            this.addRequestButton.ColorContrastOnHover = 45;
            this.addRequestButton.Cursor = System.Windows.Forms.Cursors.Default;
            borderEdges2.BottomLeft = true;
            borderEdges2.BottomRight = true;
            borderEdges2.TopLeft = true;
            borderEdges2.TopRight = true;
            this.addRequestButton.CustomizableEdges = borderEdges2;
            this.addRequestButton.DialogResult = System.Windows.Forms.DialogResult.None;
            this.addRequestButton.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(191)))), ((int)(((byte)(191)))));
            this.addRequestButton.DisabledFillColor = System.Drawing.Color.Empty;
            this.addRequestButton.DisabledForecolor = System.Drawing.Color.Empty;
            this.addRequestButton.FocusState = Bunifu.UI.WinForms.BunifuButton.BunifuButton.ButtonStates.Pressed;
            this.addRequestButton.ForeColor = System.Drawing.Color.White;
            this.addRequestButton.IconLeft = null;
            this.addRequestButton.IconLeftAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.addRequestButton.IconLeftCursor = System.Windows.Forms.Cursors.Default;
            this.addRequestButton.IconLeftPadding = new System.Windows.Forms.Padding(11, 3, 3, 3);
            this.addRequestButton.IconMarginLeft = 11;
            this.addRequestButton.IconPadding = 10;
            this.addRequestButton.IconRight = null;
            this.addRequestButton.IconRightAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.addRequestButton.IconRightCursor = System.Windows.Forms.Cursors.Default;
            this.addRequestButton.IconRightPadding = new System.Windows.Forms.Padding(3, 3, 7, 3);
            this.addRequestButton.IconSize = 25;
            this.addRequestButton.IdleBorderColor = System.Drawing.Color.Empty;
            this.addRequestButton.IdleBorderRadius = 0;
            this.addRequestButton.IdleBorderThickness = 0;
            this.addRequestButton.IdleFillColor = System.Drawing.Color.Empty;
            this.addRequestButton.IdleIconLeftImage = null;
            this.addRequestButton.IdleIconRightImage = null;
            this.addRequestButton.IndicateFocus = false;
            this.addRequestButton.Name = "addRequestButton";
            this.addRequestButton.OnDisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(191)))), ((int)(((byte)(191)))));
            this.addRequestButton.OnDisabledState.BorderRadius = 1;
            this.addRequestButton.OnDisabledState.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            this.addRequestButton.OnDisabledState.BorderThickness = 1;
            this.addRequestButton.OnDisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.addRequestButton.OnDisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(160)))), ((int)(((byte)(168)))));
            this.addRequestButton.OnDisabledState.IconLeftImage = null;
            this.addRequestButton.OnDisabledState.IconRightImage = null;
            this.addRequestButton.onHoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(150)))), ((int)(((byte)(255)))));
            this.addRequestButton.onHoverState.BorderRadius = 1;
            this.addRequestButton.onHoverState.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            this.addRequestButton.onHoverState.BorderThickness = 1;
            this.addRequestButton.onHoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(150)))), ((int)(((byte)(255)))));
            this.addRequestButton.onHoverState.ForeColor = System.Drawing.Color.White;
            this.addRequestButton.onHoverState.IconLeftImage = null;
            this.addRequestButton.onHoverState.IconRightImage = null;
            this.addRequestButton.OnIdleState.BorderColor = System.Drawing.Color.DodgerBlue;
            this.addRequestButton.OnIdleState.BorderRadius = 1;
            this.addRequestButton.OnIdleState.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            this.addRequestButton.OnIdleState.BorderThickness = 1;
            this.addRequestButton.OnIdleState.FillColor = System.Drawing.Color.DodgerBlue;
            this.addRequestButton.OnIdleState.ForeColor = System.Drawing.Color.White;
            this.addRequestButton.OnIdleState.IconLeftImage = null;
            this.addRequestButton.OnIdleState.IconRightImage = null;
            this.addRequestButton.OnPressedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(96)))), ((int)(((byte)(144)))));
            this.addRequestButton.OnPressedState.BorderRadius = 1;
            this.addRequestButton.OnPressedState.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            this.addRequestButton.OnPressedState.BorderThickness = 1;
            this.addRequestButton.OnPressedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(96)))), ((int)(((byte)(144)))));
            this.addRequestButton.OnPressedState.ForeColor = System.Drawing.Color.White;
            this.addRequestButton.OnPressedState.IconLeftImage = null;
            this.addRequestButton.OnPressedState.IconRightImage = null;
            this.addRequestButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.addRequestButton.TextAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.addRequestButton.TextMarginLeft = 0;
            this.addRequestButton.TextPadding = new System.Windows.Forms.Padding(0);
            this.addRequestButton.UseDefaultRadiusAndThickness = true;
            this.addRequestButton.Click += new System.EventHandler(this.addRequestButton_Click);
            // 
            // applyButton
            // 
            this.applyButton.AllowAnimations = true;
            this.applyButton.AllowMouseEffects = true;
            this.applyButton.AllowToggling = false;
            this.applyButton.AnimationSpeed = 200;
            this.applyButton.AutoGenerateColors = false;
            this.applyButton.AutoRoundBorders = false;
            this.applyButton.AutoSizeLeftIcon = true;
            this.applyButton.AutoSizeRightIcon = true;
            this.applyButton.BackColor = System.Drawing.Color.Transparent;
            this.applyButton.BackColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(122)))), ((int)(((byte)(183)))));
            resources.ApplyResources(this.applyButton, "applyButton");
            this.applyButton.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            this.applyButton.ButtonText = "Apply";
            this.applyButton.ButtonTextMarginLeft = 0;
            this.applyButton.ColorContrastOnClick = 45;
            this.applyButton.ColorContrastOnHover = 45;
            this.applyButton.Cursor = System.Windows.Forms.Cursors.Default;
            borderEdges3.BottomLeft = true;
            borderEdges3.BottomRight = true;
            borderEdges3.TopLeft = true;
            borderEdges3.TopRight = true;
            this.applyButton.CustomizableEdges = borderEdges3;
            this.applyButton.DialogResult = System.Windows.Forms.DialogResult.None;
            this.applyButton.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(191)))), ((int)(((byte)(191)))));
            this.applyButton.DisabledFillColor = System.Drawing.Color.Empty;
            this.applyButton.DisabledForecolor = System.Drawing.Color.Empty;
            this.applyButton.FocusState = Bunifu.UI.WinForms.BunifuButton.BunifuButton.ButtonStates.Pressed;
            this.applyButton.ForeColor = System.Drawing.Color.White;
            this.applyButton.IconLeft = null;
            this.applyButton.IconLeftAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.applyButton.IconLeftCursor = System.Windows.Forms.Cursors.Default;
            this.applyButton.IconLeftPadding = new System.Windows.Forms.Padding(11, 3, 3, 3);
            this.applyButton.IconMarginLeft = 11;
            this.applyButton.IconPadding = 10;
            this.applyButton.IconRight = null;
            this.applyButton.IconRightAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.applyButton.IconRightCursor = System.Windows.Forms.Cursors.Default;
            this.applyButton.IconRightPadding = new System.Windows.Forms.Padding(3, 3, 7, 3);
            this.applyButton.IconSize = 25;
            this.applyButton.IdleBorderColor = System.Drawing.Color.Empty;
            this.applyButton.IdleBorderRadius = 0;
            this.applyButton.IdleBorderThickness = 0;
            this.applyButton.IdleFillColor = System.Drawing.Color.Empty;
            this.applyButton.IdleIconLeftImage = null;
            this.applyButton.IdleIconRightImage = null;
            this.applyButton.IndicateFocus = false;
            this.applyButton.Name = "applyButton";
            this.applyButton.OnDisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(191)))), ((int)(((byte)(191)))));
            this.applyButton.OnDisabledState.BorderRadius = 1;
            this.applyButton.OnDisabledState.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            this.applyButton.OnDisabledState.BorderThickness = 1;
            this.applyButton.OnDisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.applyButton.OnDisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(160)))), ((int)(((byte)(168)))));
            this.applyButton.OnDisabledState.IconLeftImage = null;
            this.applyButton.OnDisabledState.IconRightImage = null;
            this.applyButton.onHoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(150)))), ((int)(((byte)(255)))));
            this.applyButton.onHoverState.BorderRadius = 1;
            this.applyButton.onHoverState.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            this.applyButton.onHoverState.BorderThickness = 1;
            this.applyButton.onHoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(150)))), ((int)(((byte)(255)))));
            this.applyButton.onHoverState.ForeColor = System.Drawing.Color.White;
            this.applyButton.onHoverState.IconLeftImage = null;
            this.applyButton.onHoverState.IconRightImage = null;
            this.applyButton.OnIdleState.BorderColor = System.Drawing.Color.DodgerBlue;
            this.applyButton.OnIdleState.BorderRadius = 1;
            this.applyButton.OnIdleState.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            this.applyButton.OnIdleState.BorderThickness = 1;
            this.applyButton.OnIdleState.FillColor = System.Drawing.Color.DodgerBlue;
            this.applyButton.OnIdleState.ForeColor = System.Drawing.Color.White;
            this.applyButton.OnIdleState.IconLeftImage = null;
            this.applyButton.OnIdleState.IconRightImage = null;
            this.applyButton.OnPressedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(96)))), ((int)(((byte)(144)))));
            this.applyButton.OnPressedState.BorderRadius = 1;
            this.applyButton.OnPressedState.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            this.applyButton.OnPressedState.BorderThickness = 1;
            this.applyButton.OnPressedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(96)))), ((int)(((byte)(144)))));
            this.applyButton.OnPressedState.ForeColor = System.Drawing.Color.White;
            this.applyButton.OnPressedState.IconLeftImage = null;
            this.applyButton.OnPressedState.IconRightImage = null;
            this.applyButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.applyButton.TextAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.applyButton.TextMarginLeft = 0;
            this.applyButton.TextPadding = new System.Windows.Forms.Padding(0);
            this.applyButton.UseDefaultRadiusAndThickness = true;
            this.applyButton.Click += new System.EventHandler(this.applyButton_Click);
            // 
            // requestsForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.controlBox);
            this.Controls.Add(this.closedLabel);
            this.Controls.Add(this.pendingLabel);
            this.Controls.Add(this.closedRadioButton);
            this.Controls.Add(this.pendingRadioButton);
            this.Controls.Add(this.applyButton);
            this.Controls.Add(this.refreshButton);
            this.Controls.Add(this.addRequestButton);
            this.Controls.Add(this.requestsDataGridView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "requestsForm";
            this.Load += new System.EventHandler(this.Requests_Load);
            this.Resize += new System.EventHandler(this.requestsForm_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.requestsDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Bunifu.UI.WinForms.BunifuDataGridView requestsDataGridView;
        private Bunifu.UI.WinForms.BunifuButton.BunifuButton addRequestButton;
        private Bunifu.UI.WinForms.BunifuButton.BunifuButton refreshButton;
        private Bunifu.UI.WinForms.BunifuButton.BunifuButton applyButton;
        private Bunifu.UI.WinForms.BunifuRadioButton pendingRadioButton;
        private Bunifu.UI.WinForms.BunifuRadioButton closedRadioButton;
        private Bunifu.UI.WinForms.BunifuLabel pendingLabel;
        private Bunifu.UI.WinForms.BunifuLabel closedLabel;
        private Bunifu.UI.WinForms.BunifuFormControlBox controlBox;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn1;
        private DataGridViewImageColumn dataGridViewImageColumn2;
        private DataGridViewImageColumn dataGridViewImageColumn3;
        private DataGridViewTextBoxColumn RequestID;
        private DataGridViewTextBoxColumn RequestUserFullName;
        private DataGridViewTextBoxColumn RequestType;
        private DataGridViewTextBoxColumn RequestFromDay;
        private DataGridViewTextBoxColumn RequestToDay;
        private DataGridViewTextBoxColumn NumberOfDays;
        private DataGridViewTextBoxColumn remainingBalance;
        private DataGridViewTextBoxColumn RequestReason;
        private DataGridViewTextBoxColumn RequestBeginningTime;
        private DataGridViewTextBoxColumn RequestEndingTime;
        private DataGridViewTextBoxColumn RequestRejectReason;
        private DataGridViewTextBoxColumn RequestStatus;
        private DataGridViewImageColumn Edit;
        private DataGridViewImageColumn Remove;
        private DataGridViewImageColumn Report;
        private DataGridViewCheckBoxColumn Approve;
        private DataGridViewCheckBoxColumn Reject;
    }
}