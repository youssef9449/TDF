﻿namespace TDF.Net.Forms
{
    partial class chatForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(chatForm));
            Bunifu.UI.WinForms.BunifuTextBox.StateProperties stateProperties1 = new Bunifu.UI.WinForms.BunifuTextBox.StateProperties();
            Bunifu.UI.WinForms.BunifuTextBox.StateProperties stateProperties2 = new Bunifu.UI.WinForms.BunifuTextBox.StateProperties();
            Bunifu.UI.WinForms.BunifuTextBox.StateProperties stateProperties3 = new Bunifu.UI.WinForms.BunifuTextBox.StateProperties();
            Bunifu.UI.WinForms.BunifuTextBox.StateProperties stateProperties4 = new Bunifu.UI.WinForms.BunifuTextBox.StateProperties();
            Bunifu.UI.WinForms.BunifuButton.BunifuButton2.BorderEdges borderEdges1 = new Bunifu.UI.WinForms.BunifuButton.BunifuButton2.BorderEdges();
            this.messageTextBox = new Bunifu.UI.WinForms.BunifuTextBox();
            this.sendButton = new Bunifu.UI.WinForms.BunifuButton.BunifuButton2();
            this.messagesListBox = new System.Windows.Forms.ListBox();
            this.panel = new System.Windows.Forms.Panel();
            this.usernameLabel = new Bunifu.UI.WinForms.BunifuLabel();
            this.controlBox = new Bunifu.UI.WinForms.BunifuFormControlBox();
            this.panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // messageTextBox
            // 
            this.messageTextBox.AcceptsReturn = false;
            this.messageTextBox.AcceptsTab = false;
            this.messageTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.messageTextBox.AnimationSpeed = 200;
            this.messageTextBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.messageTextBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.messageTextBox.AutoSizeHeight = true;
            this.messageTextBox.BackColor = System.Drawing.Color.Transparent;
            this.messageTextBox.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("messageTextBox.BackgroundImage")));
            this.messageTextBox.BorderColorActive = System.Drawing.Color.DodgerBlue;
            this.messageTextBox.BorderColorDisabled = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.messageTextBox.BorderColorHover = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(181)))), ((int)(((byte)(255)))));
            this.messageTextBox.BorderColorIdle = System.Drawing.Color.Silver;
            this.messageTextBox.BorderRadius = 1;
            this.messageTextBox.BorderThickness = 1;
            this.messageTextBox.CharacterCase = Bunifu.UI.WinForms.BunifuTextBox.CharacterCases.Normal;
            this.messageTextBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.messageTextBox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.messageTextBox.DefaultFont = new System.Drawing.Font("Segoe UI", 9.25F);
            this.messageTextBox.DefaultText = "";
            this.messageTextBox.FillColor = System.Drawing.Color.White;
            this.messageTextBox.HideSelection = true;
            this.messageTextBox.IconLeft = null;
            this.messageTextBox.IconLeftCursor = System.Windows.Forms.Cursors.IBeam;
            this.messageTextBox.IconPadding = 10;
            this.messageTextBox.IconRight = null;
            this.messageTextBox.IconRightCursor = System.Windows.Forms.Cursors.IBeam;
            this.messageTextBox.Lines = new string[0];
            this.messageTextBox.Location = new System.Drawing.Point(17, 281);
            this.messageTextBox.MaxLength = 32767;
            this.messageTextBox.MinimumSize = new System.Drawing.Size(1, 1);
            this.messageTextBox.Modified = false;
            this.messageTextBox.Multiline = false;
            this.messageTextBox.Name = "messageTextBox";
            stateProperties1.BorderColor = System.Drawing.Color.DodgerBlue;
            stateProperties1.FillColor = System.Drawing.Color.Empty;
            stateProperties1.ForeColor = System.Drawing.Color.Empty;
            stateProperties1.PlaceholderForeColor = System.Drawing.Color.Empty;
            this.messageTextBox.OnActiveState = stateProperties1;
            stateProperties2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            stateProperties2.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            stateProperties2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            stateProperties2.PlaceholderForeColor = System.Drawing.Color.DarkGray;
            this.messageTextBox.OnDisabledState = stateProperties2;
            stateProperties3.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(181)))), ((int)(((byte)(255)))));
            stateProperties3.FillColor = System.Drawing.Color.Empty;
            stateProperties3.ForeColor = System.Drawing.Color.Empty;
            stateProperties3.PlaceholderForeColor = System.Drawing.Color.Empty;
            this.messageTextBox.OnHoverState = stateProperties3;
            stateProperties4.BorderColor = System.Drawing.Color.Silver;
            stateProperties4.FillColor = System.Drawing.Color.White;
            stateProperties4.ForeColor = System.Drawing.Color.Empty;
            stateProperties4.PlaceholderForeColor = System.Drawing.Color.Empty;
            this.messageTextBox.OnIdleState = stateProperties4;
            this.messageTextBox.Padding = new System.Windows.Forms.Padding(3);
            this.messageTextBox.PasswordChar = '\0';
            this.messageTextBox.PlaceholderForeColor = System.Drawing.Color.Silver;
            this.messageTextBox.PlaceholderText = "";
            this.messageTextBox.ReadOnly = false;
            this.messageTextBox.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.messageTextBox.SelectedText = "";
            this.messageTextBox.SelectionLength = 0;
            this.messageTextBox.SelectionStart = 0;
            this.messageTextBox.ShortcutsEnabled = true;
            this.messageTextBox.Size = new System.Drawing.Size(400, 38);
            this.messageTextBox.Style = Bunifu.UI.WinForms.BunifuTextBox._Style.Bunifu;
            this.messageTextBox.TabIndex = 0;
            this.messageTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.messageTextBox.TextMarginBottom = 0;
            this.messageTextBox.TextMarginLeft = 3;
            this.messageTextBox.TextMarginTop = 1;
            this.messageTextBox.TextPlaceholder = "";
            this.messageTextBox.UseSystemPasswordChar = false;
            this.messageTextBox.WordWrap = true;
            this.messageTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.messageTextBox_KeyDown);
            // 
            // sendButton
            // 
            this.sendButton.AllowAnimations = true;
            this.sendButton.AllowMouseEffects = true;
            this.sendButton.AllowToggling = false;
            this.sendButton.AnimationSpeed = 200;
            this.sendButton.AutoGenerateColors = false;
            this.sendButton.AutoRoundBorders = false;
            this.sendButton.AutoSizeLeftIcon = true;
            this.sendButton.AutoSizeRightIcon = true;
            this.sendButton.BackColor = System.Drawing.Color.Transparent;
            this.sendButton.BackColor1 = System.Drawing.Color.DodgerBlue;
            this.sendButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("sendButton.BackgroundImage")));
            this.sendButton.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton2.BorderStyles.Solid;
            this.sendButton.ButtonText = "Send";
            this.sendButton.ButtonTextMarginLeft = 0;
            this.sendButton.ColorContrastOnClick = 45;
            this.sendButton.ColorContrastOnHover = 45;
            this.sendButton.Cursor = System.Windows.Forms.Cursors.Default;
            borderEdges1.BottomLeft = true;
            borderEdges1.BottomRight = true;
            borderEdges1.TopLeft = true;
            borderEdges1.TopRight = true;
            this.sendButton.CustomizableEdges = borderEdges1;
            this.sendButton.DialogResult = System.Windows.Forms.DialogResult.None;
            this.sendButton.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(191)))), ((int)(((byte)(191)))));
            this.sendButton.DisabledFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.sendButton.DisabledForecolor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(160)))), ((int)(((byte)(168)))));
            this.sendButton.FocusState = Bunifu.UI.WinForms.BunifuButton.BunifuButton2.ButtonStates.Pressed;
            this.sendButton.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.sendButton.ForeColor = System.Drawing.Color.White;
            this.sendButton.IconLeftAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.sendButton.IconLeftCursor = System.Windows.Forms.Cursors.Default;
            this.sendButton.IconLeftPadding = new System.Windows.Forms.Padding(11, 3, 3, 3);
            this.sendButton.IconMarginLeft = 11;
            this.sendButton.IconPadding = 10;
            this.sendButton.IconRightAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.sendButton.IconRightCursor = System.Windows.Forms.Cursors.Default;
            this.sendButton.IconRightPadding = new System.Windows.Forms.Padding(3, 3, 7, 3);
            this.sendButton.IconSize = 25;
            this.sendButton.IdleBorderColor = System.Drawing.Color.DodgerBlue;
            this.sendButton.IdleBorderRadius = 1;
            this.sendButton.IdleBorderThickness = 1;
            this.sendButton.IdleFillColor = System.Drawing.Color.DodgerBlue;
            this.sendButton.IdleIconLeftImage = null;
            this.sendButton.IdleIconRightImage = null;
            this.sendButton.IndicateFocus = false;
            this.sendButton.Location = new System.Drawing.Point(164, 325);
            this.sendButton.Name = "sendButton";
            this.sendButton.OnDisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(191)))), ((int)(((byte)(191)))));
            this.sendButton.OnDisabledState.BorderRadius = 1;
            this.sendButton.OnDisabledState.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton2.BorderStyles.Solid;
            this.sendButton.OnDisabledState.BorderThickness = 1;
            this.sendButton.OnDisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.sendButton.OnDisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(160)))), ((int)(((byte)(168)))));
            this.sendButton.OnDisabledState.IconLeftImage = null;
            this.sendButton.OnDisabledState.IconRightImage = null;
            this.sendButton.onHoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(181)))), ((int)(((byte)(255)))));
            this.sendButton.onHoverState.BorderRadius = 1;
            this.sendButton.onHoverState.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton2.BorderStyles.Solid;
            this.sendButton.onHoverState.BorderThickness = 1;
            this.sendButton.onHoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(181)))), ((int)(((byte)(255)))));
            this.sendButton.onHoverState.ForeColor = System.Drawing.Color.White;
            this.sendButton.onHoverState.IconLeftImage = null;
            this.sendButton.onHoverState.IconRightImage = null;
            this.sendButton.OnIdleState.BorderColor = System.Drawing.Color.DodgerBlue;
            this.sendButton.OnIdleState.BorderRadius = 1;
            this.sendButton.OnIdleState.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton2.BorderStyles.Solid;
            this.sendButton.OnIdleState.BorderThickness = 1;
            this.sendButton.OnIdleState.FillColor = System.Drawing.Color.DodgerBlue;
            this.sendButton.OnIdleState.ForeColor = System.Drawing.Color.White;
            this.sendButton.OnIdleState.IconLeftImage = null;
            this.sendButton.OnIdleState.IconRightImage = null;
            this.sendButton.OnPressedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(96)))), ((int)(((byte)(144)))));
            this.sendButton.OnPressedState.BorderRadius = 1;
            this.sendButton.OnPressedState.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton2.BorderStyles.Solid;
            this.sendButton.OnPressedState.BorderThickness = 1;
            this.sendButton.OnPressedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(96)))), ((int)(((byte)(144)))));
            this.sendButton.OnPressedState.ForeColor = System.Drawing.Color.White;
            this.sendButton.OnPressedState.IconLeftImage = null;
            this.sendButton.OnPressedState.IconRightImage = null;
            this.sendButton.Size = new System.Drawing.Size(102, 32);
            this.sendButton.TabIndex = 1;
            this.sendButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.sendButton.TextAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.sendButton.TextMarginLeft = 0;
            this.sendButton.TextPadding = new System.Windows.Forms.Padding(0);
            this.sendButton.UseDefaultRadiusAndThickness = true;
            this.sendButton.Click += new System.EventHandler(this.sendButton_Click);
            // 
            // messagesListBox
            // 
            this.messagesListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.messagesListBox.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.messagesListBox.FormattingEnabled = true;
            this.messagesListBox.ItemHeight = 15;
            this.messagesListBox.Location = new System.Drawing.Point(17, 50);
            this.messagesListBox.Name = "messagesListBox";
            this.messagesListBox.Size = new System.Drawing.Size(400, 214);
            this.messagesListBox.TabIndex = 2;
            // 
            // panel
            // 
            this.panel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(150)))), ((int)(((byte)(136)))));
            this.panel.Controls.Add(this.usernameLabel);
            this.panel.Controls.Add(this.controlBox);
            this.panel.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel.Location = new System.Drawing.Point(0, 0);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(429, 41);
            this.panel.TabIndex = 25;
            this.panel.Paint += new System.Windows.Forms.PaintEventHandler(this.panel_Paint);
            this.panel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel_MouseDown);
            // 
            // usernameLabel
            // 
            this.usernameLabel.AllowParentOverrides = false;
            this.usernameLabel.AutoEllipsis = false;
            this.usernameLabel.AutoSize = false;
            this.usernameLabel.Cursor = System.Windows.Forms.Cursors.Default;
            this.usernameLabel.CursorType = System.Windows.Forms.Cursors.Default;
            this.usernameLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.usernameLabel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.usernameLabel.Location = new System.Drawing.Point(12, 12);
            this.usernameLabel.Name = "usernameLabel";
            this.usernameLabel.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.usernameLabel.Size = new System.Drawing.Size(266, 20);
            this.usernameLabel.TabIndex = 26;
            this.usernameLabel.Text = "User";
            this.usernameLabel.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.usernameLabel.TextFormat = Bunifu.UI.WinForms.BunifuLabel.TextFormattingOptions.Default;
            this.usernameLabel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.usernameLabel_MouseDown);
            // 
            // controlBox
            // 
            this.controlBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.controlBox.BunifuFormDrag = null;
            this.controlBox.CloseBoxOptions.BackColor = System.Drawing.Color.Transparent;
            this.controlBox.CloseBoxOptions.BorderRadius = 0;
            this.controlBox.CloseBoxOptions.Enabled = true;
            this.controlBox.CloseBoxOptions.EnableDefaultAction = true;
            this.controlBox.CloseBoxOptions.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(17)))), ((int)(((byte)(35)))));
            this.controlBox.CloseBoxOptions.Icon = ((System.Drawing.Image)(resources.GetObject("controlBox.CloseBoxOptions.Icon")));
            this.controlBox.CloseBoxOptions.IconAlt = null;
            this.controlBox.CloseBoxOptions.IconColor = System.Drawing.Color.White;
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
            this.controlBox.Location = new System.Drawing.Point(267, 2);
            this.controlBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
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
            this.controlBox.MinimizeBox = true;
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
            this.controlBox.Size = new System.Drawing.Size(160, 37);
            this.controlBox.TabIndex = 19;
            // 
            // chatForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(429, 372);
            this.ControlBox = false;
            this.Controls.Add(this.panel);
            this.Controls.Add(this.messagesListBox);
            this.Controls.Add(this.sendButton);
            this.Controls.Add(this.messageTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximumSize = new System.Drawing.Size(429, 372);
            this.MinimumSize = new System.Drawing.Size(429, 372);
            this.Name = "chatForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "chatForm";
            this.Load += new System.EventHandler(this.chatForm_Load);
            this.Enter += new System.EventHandler(this.chatForm_Enter);
            this.panel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Bunifu.UI.WinForms.BunifuTextBox messageTextBox;
        private Bunifu.UI.WinForms.BunifuButton.BunifuButton2 sendButton;
        private System.Windows.Forms.ListBox messagesListBox;
        private System.Windows.Forms.Panel panel;
        private Bunifu.UI.WinForms.BunifuFormControlBox controlBox;
        private Bunifu.UI.WinForms.BunifuLabel usernameLabel;
    }
}