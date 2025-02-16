namespace TDF.Forms
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
            Bunifu.UI.WinForms.BunifuTextBox.StateProperties stateProperties5 = new Bunifu.UI.WinForms.BunifuTextBox.StateProperties();
            Bunifu.UI.WinForms.BunifuTextBox.StateProperties stateProperties6 = new Bunifu.UI.WinForms.BunifuTextBox.StateProperties();
            Bunifu.UI.WinForms.BunifuTextBox.StateProperties stateProperties7 = new Bunifu.UI.WinForms.BunifuTextBox.StateProperties();
            Bunifu.UI.WinForms.BunifuTextBox.StateProperties stateProperties8 = new Bunifu.UI.WinForms.BunifuTextBox.StateProperties();
            Bunifu.UI.WinForms.BunifuButton.BunifuButton2.BorderEdges borderEdges2 = new Bunifu.UI.WinForms.BunifuButton.BunifuButton2.BorderEdges();
            this.messageTextBox = new Bunifu.UI.WinForms.BunifuTextBox();
            this.sendButton = new Bunifu.UI.WinForms.BunifuButton.BunifuButton2();
            this.messagesListBox = new System.Windows.Forms.ListBox();
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
            this.messageTextBox.Location = new System.Drawing.Point(8, 244);
            this.messageTextBox.MaxLength = 32767;
            this.messageTextBox.MinimumSize = new System.Drawing.Size(1, 1);
            this.messageTextBox.Modified = false;
            this.messageTextBox.Multiline = false;
            this.messageTextBox.Name = "messageTextBox";
            stateProperties5.BorderColor = System.Drawing.Color.DodgerBlue;
            stateProperties5.FillColor = System.Drawing.Color.Empty;
            stateProperties5.ForeColor = System.Drawing.Color.Empty;
            stateProperties5.PlaceholderForeColor = System.Drawing.Color.Empty;
            this.messageTextBox.OnActiveState = stateProperties5;
            stateProperties6.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            stateProperties6.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            stateProperties6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            stateProperties6.PlaceholderForeColor = System.Drawing.Color.DarkGray;
            this.messageTextBox.OnDisabledState = stateProperties6;
            stateProperties7.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(181)))), ((int)(((byte)(255)))));
            stateProperties7.FillColor = System.Drawing.Color.Empty;
            stateProperties7.ForeColor = System.Drawing.Color.Empty;
            stateProperties7.PlaceholderForeColor = System.Drawing.Color.Empty;
            this.messageTextBox.OnHoverState = stateProperties7;
            stateProperties8.BorderColor = System.Drawing.Color.Silver;
            stateProperties8.FillColor = System.Drawing.Color.White;
            stateProperties8.ForeColor = System.Drawing.Color.Empty;
            stateProperties8.PlaceholderForeColor = System.Drawing.Color.Empty;
            this.messageTextBox.OnIdleState = stateProperties8;
            this.messageTextBox.Padding = new System.Windows.Forms.Padding(3);
            this.messageTextBox.PasswordChar = '\0';
            this.messageTextBox.PlaceholderForeColor = System.Drawing.Color.Silver;
            this.messageTextBox.PlaceholderText = "Enter text";
            this.messageTextBox.ReadOnly = false;
            this.messageTextBox.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.messageTextBox.SelectedText = "";
            this.messageTextBox.SelectionLength = 0;
            this.messageTextBox.SelectionStart = 0;
            this.messageTextBox.ShortcutsEnabled = true;
            this.messageTextBox.Size = new System.Drawing.Size(396, 39);
            this.messageTextBox.Style = Bunifu.UI.WinForms.BunifuTextBox._Style.Bunifu;
            this.messageTextBox.TabIndex = 0;
            this.messageTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.messageTextBox.TextMarginBottom = 0;
            this.messageTextBox.TextMarginLeft = 3;
            this.messageTextBox.TextMarginTop = 1;
            this.messageTextBox.TextPlaceholder = "Enter text";
            this.messageTextBox.UseSystemPasswordChar = false;
            this.messageTextBox.WordWrap = true;
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
            borderEdges2.BottomLeft = true;
            borderEdges2.BottomRight = true;
            borderEdges2.TopLeft = true;
            borderEdges2.TopRight = true;
            this.sendButton.CustomizableEdges = borderEdges2;
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
            this.sendButton.Location = new System.Drawing.Point(155, 289);
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
            this.messagesListBox.FormattingEnabled = true;
            this.messagesListBox.Location = new System.Drawing.Point(8, 0);
            this.messagesListBox.Name = "messagesListBox";
            this.messagesListBox.Size = new System.Drawing.Size(396, 238);
            this.messagesListBox.TabIndex = 2;
            // 
            // chatForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(413, 333);
            this.Controls.Add(this.messagesListBox);
            this.Controls.Add(this.sendButton);
            this.Controls.Add(this.messageTextBox);
            this.Name = "chatForm";
            this.Text = "chatForm";
            this.Load += new System.EventHandler(this.chatForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Bunifu.UI.WinForms.BunifuTextBox messageTextBox;
        private Bunifu.UI.WinForms.BunifuButton.BunifuButton2 sendButton;
        private System.Windows.Forms.ListBox messagesListBox;
    }
}