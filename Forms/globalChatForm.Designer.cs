namespace TDF.Forms
{
    partial class globalChatForm
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
            Bunifu.UI.WinForms.BunifuTextBox.StateProperties stateProperties1 = new Bunifu.UI.WinForms.BunifuTextBox.StateProperties();
            Bunifu.UI.WinForms.BunifuTextBox.StateProperties stateProperties2 = new Bunifu.UI.WinForms.BunifuTextBox.StateProperties();
            Bunifu.UI.WinForms.BunifuTextBox.StateProperties stateProperties3 = new Bunifu.UI.WinForms.BunifuTextBox.StateProperties();
            Bunifu.UI.WinForms.BunifuTextBox.StateProperties stateProperties4 = new Bunifu.UI.WinForms.BunifuTextBox.StateProperties();
            Bunifu.UI.WinForms.BunifuButton.BunifuButton2.BorderEdges borderEdges1 = new Bunifu.UI.WinForms.BunifuButton.BunifuButton2.BorderEdges();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(globalChatForm));
            this.panel = new System.Windows.Forms.Panel();
            this.globalChatInput = new Bunifu.UI.WinForms.BunifuTextBox();
            this.globalChatSendButton = new Bunifu.UI.WinForms.BunifuButton.BunifuButton2();
            this.globalChatPanel = new System.Windows.Forms.Panel();
            this.globalChatDisplay = new System.Windows.Forms.RichTextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.controlBox = new Bunifu.UI.WinForms.BunifuFormControlBox();
            this.panel.SuspendLayout();
            this.globalChatPanel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel
            // 
            this.panel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(150)))), ((int)(((byte)(136)))));
            this.panel.Controls.Add(this.panel1);
            this.panel.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel.Location = new System.Drawing.Point(0, 0);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(1097, 41);
            this.panel.TabIndex = 24;
            this.panel.Paint += new System.Windows.Forms.PaintEventHandler(this.panel_Paint);
            this.panel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel_MouseDown);
            // 
            // globalChatInput
            // 
            this.globalChatInput.AcceptsReturn = false;
            this.globalChatInput.AcceptsTab = false;
            this.globalChatInput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.globalChatInput.AnimationSpeed = 200;
            this.globalChatInput.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.globalChatInput.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.globalChatInput.AutoSizeHeight = true;
            this.globalChatInput.BackColor = System.Drawing.Color.Transparent;
            this.globalChatInput.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("globalChatInput.BackgroundImage")));
            this.globalChatInput.BorderColorActive = System.Drawing.Color.DodgerBlue;
            this.globalChatInput.BorderColorDisabled = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.globalChatInput.BorderColorHover = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(181)))), ((int)(((byte)(255)))));
            this.globalChatInput.BorderColorIdle = System.Drawing.Color.Silver;
            this.globalChatInput.BorderRadius = 1;
            this.globalChatInput.BorderThickness = 1;
            this.globalChatInput.CharacterCase = Bunifu.UI.WinForms.BunifuTextBox.CharacterCases.Normal;
            this.globalChatInput.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.globalChatInput.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.globalChatInput.DefaultFont = new System.Drawing.Font("Segoe UI", 15F);
            this.globalChatInput.DefaultText = "";
            this.globalChatInput.FillColor = System.Drawing.Color.White;
            this.globalChatInput.HideSelection = true;
            this.globalChatInput.IconLeft = null;
            this.globalChatInput.IconLeftCursor = System.Windows.Forms.Cursors.IBeam;
            this.globalChatInput.IconPadding = 10;
            this.globalChatInput.IconRight = null;
            this.globalChatInput.IconRightCursor = System.Windows.Forms.Cursors.IBeam;
            this.globalChatInput.Lines = new string[0];
            this.globalChatInput.Location = new System.Drawing.Point(3, 492);
            this.globalChatInput.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.globalChatInput.MaxLength = 32767;
            this.globalChatInput.MinimumSize = new System.Drawing.Size(1, 1);
            this.globalChatInput.Modified = false;
            this.globalChatInput.Multiline = false;
            this.globalChatInput.Name = "globalChatInput";
            stateProperties1.BorderColor = System.Drawing.Color.DodgerBlue;
            stateProperties1.FillColor = System.Drawing.Color.Empty;
            stateProperties1.ForeColor = System.Drawing.Color.Empty;
            stateProperties1.PlaceholderForeColor = System.Drawing.Color.Empty;
            this.globalChatInput.OnActiveState = stateProperties1;
            stateProperties2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            stateProperties2.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            stateProperties2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            stateProperties2.PlaceholderForeColor = System.Drawing.Color.DarkGray;
            this.globalChatInput.OnDisabledState = stateProperties2;
            stateProperties3.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(181)))), ((int)(((byte)(255)))));
            stateProperties3.FillColor = System.Drawing.Color.Empty;
            stateProperties3.ForeColor = System.Drawing.Color.Empty;
            stateProperties3.PlaceholderForeColor = System.Drawing.Color.Empty;
            this.globalChatInput.OnHoverState = stateProperties3;
            stateProperties4.BorderColor = System.Drawing.Color.Silver;
            stateProperties4.FillColor = System.Drawing.Color.White;
            stateProperties4.ForeColor = System.Drawing.Color.Empty;
            stateProperties4.PlaceholderForeColor = System.Drawing.Color.Empty;
            this.globalChatInput.OnIdleState = stateProperties4;
            this.globalChatInput.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.globalChatInput.PasswordChar = '\0';
            this.globalChatInput.PlaceholderForeColor = System.Drawing.Color.Silver;
            this.globalChatInput.PlaceholderText = "";
            this.globalChatInput.ReadOnly = false;
            this.globalChatInput.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.globalChatInput.SelectedText = "";
            this.globalChatInput.SelectionLength = 0;
            this.globalChatInput.SelectionStart = 0;
            this.globalChatInput.ShortcutsEnabled = true;
            this.globalChatInput.Size = new System.Drawing.Size(867, 83);
            this.globalChatInput.Style = Bunifu.UI.WinForms.BunifuTextBox._Style.Bunifu;
            this.globalChatInput.TabIndex = 25;
            this.globalChatInput.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.globalChatInput.TextMarginBottom = 0;
            this.globalChatInput.TextMarginLeft = 3;
            this.globalChatInput.TextMarginTop = 1;
            this.globalChatInput.TextPlaceholder = "";
            this.globalChatInput.UseSystemPasswordChar = false;
            this.globalChatInput.WordWrap = true;
            // 
            // globalChatSendButton
            // 
            this.globalChatSendButton.AllowAnimations = true;
            this.globalChatSendButton.AllowMouseEffects = true;
            this.globalChatSendButton.AllowToggling = false;
            this.globalChatSendButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.globalChatSendButton.AnimationSpeed = 200;
            this.globalChatSendButton.AutoGenerateColors = false;
            this.globalChatSendButton.AutoRoundBorders = false;
            this.globalChatSendButton.AutoSizeLeftIcon = true;
            this.globalChatSendButton.AutoSizeRightIcon = true;
            this.globalChatSendButton.BackColor = System.Drawing.Color.Transparent;
            this.globalChatSendButton.BackColor1 = System.Drawing.Color.DodgerBlue;
            this.globalChatSendButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("globalChatSendButton.BackgroundImage")));
            this.globalChatSendButton.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton2.BorderStyles.Solid;
            this.globalChatSendButton.ButtonText = "Send";
            this.globalChatSendButton.ButtonTextMarginLeft = 0;
            this.globalChatSendButton.ColorContrastOnClick = 45;
            this.globalChatSendButton.ColorContrastOnHover = 45;
            this.globalChatSendButton.Cursor = System.Windows.Forms.Cursors.Default;
            borderEdges1.BottomLeft = true;
            borderEdges1.BottomRight = true;
            borderEdges1.TopLeft = true;
            borderEdges1.TopRight = true;
            this.globalChatSendButton.CustomizableEdges = borderEdges1;
            this.globalChatSendButton.DialogResult = System.Windows.Forms.DialogResult.None;
            this.globalChatSendButton.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(191)))), ((int)(((byte)(191)))));
            this.globalChatSendButton.DisabledFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.globalChatSendButton.DisabledForecolor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(160)))), ((int)(((byte)(168)))));
            this.globalChatSendButton.FocusState = Bunifu.UI.WinForms.BunifuButton.BunifuButton2.ButtonStates.Pressed;
            this.globalChatSendButton.Font = new System.Drawing.Font("Segoe UI", 15F);
            this.globalChatSendButton.ForeColor = System.Drawing.Color.White;
            this.globalChatSendButton.IconLeftAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.globalChatSendButton.IconLeftCursor = System.Windows.Forms.Cursors.Default;
            this.globalChatSendButton.IconLeftPadding = new System.Windows.Forms.Padding(11, 3, 3, 3);
            this.globalChatSendButton.IconMarginLeft = 11;
            this.globalChatSendButton.IconPadding = 10;
            this.globalChatSendButton.IconRightAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.globalChatSendButton.IconRightCursor = System.Windows.Forms.Cursors.Default;
            this.globalChatSendButton.IconRightPadding = new System.Windows.Forms.Padding(3, 3, 7, 3);
            this.globalChatSendButton.IconSize = 25;
            this.globalChatSendButton.IdleBorderColor = System.Drawing.Color.DodgerBlue;
            this.globalChatSendButton.IdleBorderRadius = 1;
            this.globalChatSendButton.IdleBorderThickness = 1;
            this.globalChatSendButton.IdleFillColor = System.Drawing.Color.DodgerBlue;
            this.globalChatSendButton.IdleIconLeftImage = null;
            this.globalChatSendButton.IdleIconRightImage = null;
            this.globalChatSendButton.IndicateFocus = false;
            this.globalChatSendButton.Location = new System.Drawing.Point(875, 492);
            this.globalChatSendButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.globalChatSendButton.Name = "globalChatSendButton";
            this.globalChatSendButton.OnDisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(191)))), ((int)(((byte)(191)))));
            this.globalChatSendButton.OnDisabledState.BorderRadius = 1;
            this.globalChatSendButton.OnDisabledState.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton2.BorderStyles.Solid;
            this.globalChatSendButton.OnDisabledState.BorderThickness = 1;
            this.globalChatSendButton.OnDisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.globalChatSendButton.OnDisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(160)))), ((int)(((byte)(168)))));
            this.globalChatSendButton.OnDisabledState.IconLeftImage = null;
            this.globalChatSendButton.OnDisabledState.IconRightImage = null;
            this.globalChatSendButton.onHoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(181)))), ((int)(((byte)(255)))));
            this.globalChatSendButton.onHoverState.BorderRadius = 1;
            this.globalChatSendButton.onHoverState.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton2.BorderStyles.Solid;
            this.globalChatSendButton.onHoverState.BorderThickness = 1;
            this.globalChatSendButton.onHoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(181)))), ((int)(((byte)(255)))));
            this.globalChatSendButton.onHoverState.ForeColor = System.Drawing.Color.White;
            this.globalChatSendButton.onHoverState.IconLeftImage = null;
            this.globalChatSendButton.onHoverState.IconRightImage = null;
            this.globalChatSendButton.OnIdleState.BorderColor = System.Drawing.Color.DodgerBlue;
            this.globalChatSendButton.OnIdleState.BorderRadius = 1;
            this.globalChatSendButton.OnIdleState.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton2.BorderStyles.Solid;
            this.globalChatSendButton.OnIdleState.BorderThickness = 1;
            this.globalChatSendButton.OnIdleState.FillColor = System.Drawing.Color.DodgerBlue;
            this.globalChatSendButton.OnIdleState.ForeColor = System.Drawing.Color.White;
            this.globalChatSendButton.OnIdleState.IconLeftImage = null;
            this.globalChatSendButton.OnIdleState.IconRightImage = null;
            this.globalChatSendButton.OnPressedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(96)))), ((int)(((byte)(144)))));
            this.globalChatSendButton.OnPressedState.BorderRadius = 1;
            this.globalChatSendButton.OnPressedState.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton2.BorderStyles.Solid;
            this.globalChatSendButton.OnPressedState.BorderThickness = 1;
            this.globalChatSendButton.OnPressedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(96)))), ((int)(((byte)(144)))));
            this.globalChatSendButton.OnPressedState.ForeColor = System.Drawing.Color.White;
            this.globalChatSendButton.OnPressedState.IconLeftImage = null;
            this.globalChatSendButton.OnPressedState.IconRightImage = null;
            this.globalChatSendButton.Size = new System.Drawing.Size(217, 83);
            this.globalChatSendButton.TabIndex = 26;
            this.globalChatSendButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.globalChatSendButton.TextAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.globalChatSendButton.TextMarginLeft = 0;
            this.globalChatSendButton.TextPadding = new System.Windows.Forms.Padding(0);
            this.globalChatSendButton.UseDefaultRadiusAndThickness = true;
            this.globalChatSendButton.Click += new System.EventHandler(this.globalChatSendButton_Click);
            // 
            // globalChatPanel
            // 
            this.globalChatPanel.Controls.Add(this.globalChatDisplay);
            this.globalChatPanel.Location = new System.Drawing.Point(3, 43);
            this.globalChatPanel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.globalChatPanel.Name = "globalChatPanel";
            this.globalChatPanel.Size = new System.Drawing.Size(1089, 445);
            this.globalChatPanel.TabIndex = 27;
            // 
            // globalChatDisplay
            // 
            this.globalChatDisplay.BackColor = System.Drawing.Color.White;
            this.globalChatDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.globalChatDisplay.Font = new System.Drawing.Font("Tahoma", 15F);
            this.globalChatDisplay.Location = new System.Drawing.Point(0, 0);
            this.globalChatDisplay.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.globalChatDisplay.Name = "globalChatDisplay";
            this.globalChatDisplay.ReadOnly = true;
            this.globalChatDisplay.Size = new System.Drawing.Size(1089, 445);
            this.globalChatDisplay.TabIndex = 28;
            this.globalChatDisplay.Text = "";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(150)))), ((int)(((byte)(136)))));
            this.panel1.Controls.Add(this.controlBox);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1097, 41);
            this.panel1.TabIndex = 25;
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
            this.controlBox.Location = new System.Drawing.Point(935, 2);
            this.controlBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.controlBox.MaximizeBox = true;
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
            // globalChatForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1097, 585);
            this.Controls.Add(this.globalChatPanel);
            this.Controls.Add(this.globalChatSendButton);
            this.Controls.Add(this.globalChatInput);
            this.Controls.Add(this.panel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "globalChatForm";
            this.Text = "Global Chat";
            this.Load += new System.EventHandler(this.globalChatForm_Load);
            this.panel.ResumeLayout(false);
            this.globalChatPanel.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel;
        private Bunifu.UI.WinForms.BunifuTextBox globalChatInput;
        private Bunifu.UI.WinForms.BunifuButton.BunifuButton2 globalChatSendButton;
        private System.Windows.Forms.Panel globalChatPanel;
        private System.Windows.Forms.RichTextBox globalChatDisplay;
        private System.Windows.Forms.Panel panel1;
        private Bunifu.UI.WinForms.BunifuFormControlBox controlBox;
    }
}