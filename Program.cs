using Bunifu.UI.WinForms;
using Bunifu.UI.WinForms.BunifuButton;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using TDF.Classes;
using TDF.Net.Classes;
using static TDF.Net.loginForm;

namespace TDF.Net
{
    internal static class Program
    {
        // Assume you set CrashLogger.LoggedInUserFullName when the user logs in.
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Check if the application is already running
            string appName = Process.GetCurrentProcess().ProcessName;
         /*   if (Process.GetProcessesByName(appName).Length > 1)
            {
                MessageBox.Show("Another instance of the TDF app is already running.","Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }*/

            //setProcessDpiAwareness(); // Enable DPI scaling

            Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

            // Set culture settings
            CultureInfo English = new CultureInfo("en-GB");
            CultureInfo.DefaultThreadCurrentUICulture = English;
            CultureInfo.DefaultThreadCurrentCulture = English;
            CultureInfo.CurrentCulture = English;
            CultureInfo.CurrentUICulture = English;

            // Start the application
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new loginForm());

            /* try
             {
                 using (var connection = GetConnection())
                 {
                     connection.Open();
                 }

             }
             catch (Exception ex)
             {
                 MessageBox.Show($"Connection failed: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
             }*/
        }

        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            try
            {
                // Log the exception
                CrashLogger.LogException(e.Exception);

                // Call your method to update the user's status to "disconnected"
                mainFormNewUI.triggerServerDisconnect();
                // Ensure this method is accessible in this context
            }
            catch (Exception ex2)
            {
                // If disconnecting also fails, you might log or ignore this secondary error.
                CrashLogger.LogException(ex2);
            }

            //  MessageBox.Show("An unexpected error occurred. Please check the log file for details.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Application.Exit();

        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            try
            {
                Exception ex = e.ExceptionObject as Exception;
                if (ex != null)
                {
                    CrashLogger.LogException(ex);
                }
                else
                {
                    CrashLogger.LogException(new Exception("An unhandled non-exception error occurred."));
                }

                // Call your disconnect method here as well
                mainFormNewUI.triggerServerDisconnect();
            }
            catch (Exception ex2)
            {
                CrashLogger.LogException(ex2);
            }

            Environment.Exit(1);
        }

        static bool roundCorners = true;
        static int borderRadius = 25;

        public static void applyTheme(Form form)
        {
            form.AutoScaleMode = AutoScaleMode.Dpi;

            foreach (Control ctrl in form.Controls)
            {
                if (ctrl is Panel pbl)
                {
                    pbl.BackColor = ThemeColor.primaryColor;

                    foreach (Control innerCtrl in pbl.Controls)
                    {
                        if (innerCtrl is BunifuFormControlBox controlBox)
                        {
                            controlBox.BackColor = ThemeColor.primaryColor;
                            controlBox.CloseBoxOptions.HoverColor = ThemeColor.darkColor;
                            controlBox.CloseBoxOptions.IconHoverColor = Color.Black;
                            controlBox.CloseBoxOptions.IconPressedColor = Color.Black;
                            controlBox.CloseBoxOptions.PressedColor = ThemeColor.darkColor;
                            controlBox.MaximizeBoxOptions.HoverColor = ThemeColor.darkColor;
                            controlBox.MaximizeBoxOptions.PressedColor = ThemeColor.darkColor;
                            controlBox.MinimizeBoxOptions.HoverColor = ThemeColor.darkColor;
                            controlBox.MinimizeBoxOptions.PressedColor = ThemeColor.darkColor;
                        }
                    }
                }
                if (ctrl is BunifuGradientPanel bpbl)
                {
                    //bpbl.BackColor = ThemeColor.PrimaryColor;

                    bpbl.GradientBottomRight = ThemeColor.primaryColor;
                    bpbl.GradientBottomLeft = ThemeColor.lightColor;
                    bpbl.GradientTopRight = ThemeColor.lightColor;
                    bpbl.GradientTopLeft = ThemeColor.primaryColor;

                    foreach (Control innerCtrl in bpbl.Controls)
                    {
                        if (innerCtrl is BunifuButton2 BunifuButton)
                        {
                            BunifuButton.OnDisabledState.BorderColor = ThemeColor.darkColor;
                            BunifuButton.OnDisabledState.FillColor = ThemeColor.primaryColor;
                            BunifuButton.OnDisabledState.ForeColor = Color.White;

                            BunifuButton.onHoverState.BorderColor = ThemeColor.darkColor;
                            BunifuButton.onHoverState.FillColor = ThemeColor.darkColor;
                            BunifuButton.onHoverState.ForeColor = Color.White;

                            BunifuButton.OnIdleState.BorderColor = ThemeColor.darkColor;
                            BunifuButton.OnIdleState.FillColor = ThemeColor.primaryColor;
                            BunifuButton.OnIdleState.ForeColor = Color.White;

                            BunifuButton.OnPressedState.BorderColor = ThemeColor.darkColor;
                            BunifuButton.OnPressedState.FillColor = ThemeColor.primaryColor;
                            BunifuButton.OnPressedState.ForeColor = Color.White;
                            BunifuButton.Font = new Font(BunifuButton.Font, BunifuButton.Font.Style | FontStyle.Bold);
                            BunifuButton.AutoRoundBorders = roundCorners;
                            //BunifuButton.AutoGenerateColors = true;
                            BunifuButton.IdleBorderRadius = borderRadius;

                            BunifuButton.Cursor = Cursors.Hand;
                            BunifuButton.Invalidate();
                            BunifuButton.Refresh();
                        }
                    }
                }
                if (ctrl.GetType() == typeof(TabControl))
                {
                    TabControl tc = (TabControl)ctrl;
                    tc.ForeColor = ThemeColor.darkColor;

                    foreach (Control control in tc.Controls)
                    {
                        if (control.GetType() == typeof(TabPage))
                        {
                            TabPage tabPage = control as TabPage;
                            tabPage.ForeColor = ThemeColor.darkColor;

                            foreach (Control ct in tabPage.Controls)
                            {
                                if (ct.GetType() == typeof(BunifuButton))
                                {
                                    BunifuButton BunifuButton = (BunifuButton)ct;
                                    BunifuButton.OnDisabledState.BorderColor = ThemeColor.darkColor;
                                    BunifuButton.OnDisabledState.FillColor = ThemeColor.primaryColor;
                                    BunifuButton.OnDisabledState.ForeColor = Color.White;

                                    BunifuButton.onHoverState.BorderColor = ThemeColor.darkColor;
                                    BunifuButton.onHoverState.FillColor = ThemeColor.darkColor;
                                    BunifuButton.onHoverState.ForeColor = Color.White;

                                    BunifuButton.OnIdleState.BorderColor = ThemeColor.darkColor;
                                    BunifuButton.OnIdleState.FillColor = ThemeColor.primaryColor;
                                    BunifuButton.OnIdleState.ForeColor = Color.White;

                                    BunifuButton.OnPressedState.BorderColor = ThemeColor.darkColor;
                                    BunifuButton.OnPressedState.FillColor = ThemeColor.primaryColor;
                                    BunifuButton.OnPressedState.ForeColor = Color.White;
                                    BunifuButton.AutoRoundBorders = roundCorners;
                                    //BunifuButton.AutoGenerateColors = true;
                                    BunifuButton.IdleBorderRadius = borderRadius;
                                    BunifuButton.Font = new Font(BunifuButton.Font, BunifuButton.Font.Style | FontStyle.Bold);

                                    BunifuButton.Refresh();
                                }
                                if (ct.GetType() == typeof(Label))
                                {
                                    Label label = (Label)ct;
                                    label.ForeColor = ThemeColor.darkColor;
                                }
                            }
                        }
                    }
                }
                if (ctrl.GetType() == typeof(Button))
                {
                    Button btn = (Button)ctrl;
                    btn.BackColor = ThemeColor.primaryColor;
                    btn.ForeColor = Color.White;
                    btn.FlatAppearance.BorderColor = ThemeColor.darkColor;
                }
                if (ctrl.GetType() == typeof(Label))
                {
                    Label label = (Label)ctrl;
                    label.ForeColor = ThemeColor.darkColor;
                }
                if (ctrl.GetType() == typeof(MaskedTextBox))
                {
                    MaskedTextBox maskedTextBox = (MaskedTextBox)ctrl;
                }
                if (ctrl.GetType() == typeof(BunifuButton))
                {
                    BunifuButton btn = (BunifuButton)ctrl;

                    btn.OnDisabledState.BorderColor = ThemeColor.darkColor;
                    btn.OnDisabledState.FillColor = ThemeColor.primaryColor;
                    btn.OnDisabledState.ForeColor = Color.White;

                    btn.onHoverState.BorderColor = ThemeColor.darkColor;
                    btn.onHoverState.FillColor = ThemeColor.darkColor;
                    btn.onHoverState.ForeColor = Color.White;

                    btn.OnIdleState.BorderColor = ThemeColor.darkColor;
                    btn.OnIdleState.FillColor = ThemeColor.primaryColor;
                    btn.OnIdleState.ForeColor = Color.White;

                    btn.OnPressedState.BorderColor = ThemeColor.darkColor;
                    btn.OnPressedState.FillColor = ThemeColor.primaryColor;
                    btn.OnPressedState.ForeColor = Color.White;
                    btn.Font = new Font(btn.Font, btn.Font.Style | FontStyle.Bold);
                    btn.Cursor = Cursors.Hand;
                    btn.AutoRoundBorders = roundCorners;
                    //btn.AutoGenerateColors = true;
                    btn.IdleBorderRadius = borderRadius;
                    btn.Invalidate();
                    btn.Refresh();
                }
                if (ctrl.GetType() == typeof(BunifuButton2))
                {
                    BunifuButton2 btn = (BunifuButton2)ctrl;

                    btn.OnDisabledState.BorderColor = ThemeColor.darkColor;
                    btn.OnDisabledState.FillColor = ThemeColor.primaryColor;
                    btn.OnDisabledState.ForeColor = Color.White;

                    btn.onHoverState.BorderColor = ThemeColor.darkColor;
                    btn.onHoverState.FillColor = ThemeColor.darkColor;
                    btn.onHoverState.ForeColor = Color.White;

                    btn.OnIdleState.BorderColor = ThemeColor.darkColor;
                    btn.OnIdleState.FillColor = ThemeColor.primaryColor;
                    btn.OnIdleState.ForeColor = Color.White;

                    btn.OnPressedState.BorderColor = ThemeColor.darkColor;
                    btn.OnPressedState.FillColor = ThemeColor.primaryColor;
                    btn.OnPressedState.ForeColor = Color.White;
                    btn.Font = new Font(btn.Font, btn.Font.Style | FontStyle.Bold);
                    btn.Cursor = Cursors.Hand;
                    btn.AutoRoundBorders = roundCorners;
                    //btn.AutoGenerateColors = true;
                    btn.IdleBorderRadius = borderRadius;
                    btn.Invalidate();
                    btn.Refresh();
                }
                /* if (ctrl.GetType() == typeof(BunifuLabel))
                  {
                      BunifuLabel label = (BunifuLabel)ctrl;
                      label.ForeColor = ThemeColor.SecondaryColor;
                  }*/
                if (ctrl.GetType() == typeof(BunifuDatePicker))
                {
                    BunifuDatePicker DatePicker = (BunifuDatePicker)ctrl;
                    DatePicker.BorderColor = ThemeColor.darkColor;
                }
                if (ctrl.GetType() == typeof(BunifuCheckBox))
                {
                    BunifuCheckBox chkbox = (BunifuCheckBox)ctrl;
                    chkbox.OnCheck.CheckBoxColor = ThemeColor.darkColor;
                    chkbox.OnCheck.BorderColor = ThemeColor.primaryColor;
                    chkbox.OnHoverChecked.CheckBoxColor = ThemeColor.lightColor;
                    chkbox.OnHoverUnchecked.CheckBoxColor = ThemeColor.lightColor;
                }
                if (ctrl.GetType() == typeof(BunifuTextBox))
                {
                    BunifuTextBox textBox = (BunifuTextBox)ctrl;
                    textBox.BorderColorActive = ThemeColor.darkColor;
                    textBox.BorderColorHover = ThemeColor.primaryColor;
                    textBox.BorderColorIdle = ThemeColor.darkColor;
                }
                if (ctrl.GetType() == typeof(BunifuDropdown))
                {
                    BunifuDropdown dropdownList = (BunifuDropdown)ctrl;
                    dropdownList.IndicatorColor = ThemeColor.darkColor;
                    dropdownList.ItemBackColor = ThemeColor.darkColor;
                    dropdownList.ItemForeColor = Color.White;
                    dropdownList.ItemHighLightColor = ThemeColor.primaryColor;
                    dropdownList.BorderColor = ThemeColor.darkColor;
                }
                if (ctrl.GetType() == typeof(BunifuRadioButton))
                {
                    BunifuRadioButton radioButton = (BunifuRadioButton)ctrl;
                    radioButton.RadioColor = ThemeColor.primaryColor;
                    radioButton.OutlineColor = Color.Black;
                    radioButton.OutlineColorUnchecked = Color.Black;
                }
                if (ctrl.GetType() == typeof(BunifuDataGridView))
                {
                    BunifuDataGridView dgv = (BunifuDataGridView)ctrl;

                    dgv.CellBorderStyle = DataGridViewCellBorderStyle.Single;
                    dgv.GridColor = ThemeColor.primaryColor;

                    dgv.ColumnHeadersDefaultCellStyle.Font = new Font(dgv.Font.FontFamily, 10, FontStyle.Bold);
                    dgv.ColumnHeadersDefaultCellStyle.SelectionBackColor = ThemeColor.darkColor;
                    dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                    dgv.DefaultCellStyle.SelectionBackColor = ThemeColor.lightColor;
                    dgv.ColumnHeadersDefaultCellStyle.BackColor = ThemeColor.primaryColor;
                    dgv.Margin = new Padding(0);
                    dgv.Padding = new Padding(0);
                    dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                    //dgv.ForeColor = ThemeColor.SecondaryColor;
                    dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.White;
                }
                if (ctrl.GetType() == typeof(RadioButton))
                {
                    RadioButton rb = (RadioButton)ctrl;
                    rb.ForeColor = ThemeColor.darkColor;
                }
                if (ctrl.GetType() == typeof(CheckBox))
                {
                    CheckBox cb = (CheckBox)ctrl;
                    cb.ForeColor = ThemeColor.darkColor;
                }
                if (ctrl.GetType() == typeof(ComboBox))
                {
                    ComboBox cb = (ComboBox)ctrl;
                    cb.ForeColor = ThemeColor.darkColor;
                }
                if (ctrl.GetType() == typeof(ListBox))
                {
                    ListBox lb = (ListBox)ctrl;
                    lb.ForeColor = ThemeColor.darkColor;
                }
                if (ctrl.GetType() == typeof(CheckedListBox))
                {
                    CheckedListBox clb = (CheckedListBox)ctrl;
                    clb.ForeColor = ThemeColor.darkColor;
                }
                if (ctrl.GetType() == typeof(BunifuGroupBox))
                {
                    BunifuGroupBox cb = (BunifuGroupBox)ctrl;
                    foreach (Control control in cb.Controls)
                    {
                        if (control.GetType() == typeof(BunifuRadioButton))
                        {
                            BunifuRadioButton radioButton = (BunifuRadioButton)control;
                            radioButton.RadioColor = ThemeColor.primaryColor;
                            radioButton.OutlineColor = Color.Black;
                            radioButton.OutlineColorUnchecked = Color.Black;
                        }
                        if (control.GetType() == typeof(BunifuDropdown))
                        {
                            BunifuDropdown dropdownList = (BunifuDropdown)control;
                            dropdownList.IndicatorColor = ThemeColor.darkColor;
                            dropdownList.ItemBackColor = ThemeColor.darkColor;
                            dropdownList.ItemForeColor = Color.White;
                            dropdownList.ItemHighLightColor = ThemeColor.primaryColor;
                            dropdownList.BorderColor = ThemeColor.darkColor;
                        }
                    }
                }
                if (ctrl.GetType() == typeof(BunifuFormControlBox))
                {
                    BunifuFormControlBox controlBox = (BunifuFormControlBox)ctrl;
                    controlBox.CloseBoxOptions.HoverColor = ThemeColor.darkColor;
                    controlBox.CloseBoxOptions.IconHoverColor = Color.Black;
                    controlBox.CloseBoxOptions.IconPressedColor = Color.Black;
                    controlBox.CloseBoxOptions.PressedColor = ThemeColor.darkColor;
                    controlBox.MaximizeBoxOptions.HoverColor = ThemeColor.darkColor;
                    controlBox.MaximizeBoxOptions.PressedColor = ThemeColor.darkColor;
                    controlBox.MinimizeBoxOptions.HoverColor = ThemeColor.darkColor;
                    controlBox.MinimizeBoxOptions.PressedColor = ThemeColor.darkColor;
                }
                if (ctrl.GetType() == typeof(CheckedListBox))
                {
                    CheckedListBox checkedListBox = (CheckedListBox)ctrl;
                    checkedListBox.ForeColor = ThemeColor.darkColor;
                }
                if (ctrl.GetType() == typeof(BunifuFormControlBox))
                {
                    BunifuFormControlBox controlBox = (BunifuFormControlBox)ctrl;
                    controlBox.BackColor = Color.White;
                    controlBox.CloseBoxOptions.HoverColor = Color.White;
                    controlBox.CloseBoxOptions.IconHoverColor = ThemeColor.darkColor;
                    controlBox.CloseBoxOptions.IconPressedColor = ThemeColor.primaryColor;
                    controlBox.CloseBoxOptions.PressedColor = Color.White;
                }
            }
        }
        public static void applyThemeLite(Form form)
        {
            form.AutoScaleMode = AutoScaleMode.Dpi;

            foreach (Control ctrl in form.Controls)
            {
                if (ctrl is Panel pbl)
                {
                    pbl.BackColor = ThemeColor.primaryColor;

                    foreach (Control innerCtrl in pbl.Controls)
                    {
                        if (innerCtrl is BunifuFormControlBox controlBox)
                        {
                            controlBox.BackColor = ThemeColor.primaryColor;
                            controlBox.CloseBoxOptions.HoverColor = ThemeColor.darkColor;
                            controlBox.CloseBoxOptions.IconHoverColor = Color.Black;
                            controlBox.CloseBoxOptions.IconPressedColor = Color.Black;
                            controlBox.CloseBoxOptions.PressedColor = ThemeColor.darkColor;
                            controlBox.MaximizeBoxOptions.HoverColor = ThemeColor.darkColor;
                            controlBox.MaximizeBoxOptions.PressedColor = ThemeColor.darkColor;
                            controlBox.MinimizeBoxOptions.HoverColor = ThemeColor.darkColor;
                            controlBox.MinimizeBoxOptions.PressedColor = ThemeColor.darkColor;
                        }
                    }
                }
                if (ctrl.GetType() == typeof(BunifuButton))
                {
                    BunifuButton btn = (BunifuButton)ctrl;

                    btn.OnDisabledState.BorderColor = ThemeColor.darkColor;
                    btn.OnDisabledState.FillColor = ThemeColor.primaryColor;
                    btn.OnDisabledState.ForeColor = Color.White;

                    btn.onHoverState.BorderColor = ThemeColor.darkColor;
                    btn.onHoverState.FillColor = ThemeColor.darkColor;
                    btn.onHoverState.ForeColor = Color.White;

                    btn.OnIdleState.BorderColor = ThemeColor.darkColor;
                    btn.OnIdleState.FillColor = ThemeColor.primaryColor;
                    btn.OnIdleState.ForeColor = Color.White;

                    btn.OnPressedState.BorderColor = ThemeColor.darkColor;
                    btn.OnPressedState.FillColor = ThemeColor.primaryColor;
                    btn.OnPressedState.ForeColor = Color.White;
                    btn.Font = new Font(btn.Font, btn.Font.Style | FontStyle.Bold);
                    btn.Cursor = Cursors.Hand;
                    //btn.IdleBorderRadius = 12;

                    // btn.AutoRoundBorders = true;
                    // btn.AutoGenerateColors = true;
                    btn.Invalidate();
                    btn.Refresh();
                }
                if (ctrl.GetType() == typeof(BunifuButton2))
                {
                    BunifuButton2 btn = (BunifuButton2)ctrl;

                    btn.OnDisabledState.BorderColor = ThemeColor.darkColor;
                    btn.OnDisabledState.FillColor = ThemeColor.primaryColor;
                    btn.OnDisabledState.ForeColor = Color.White;

                    btn.onHoverState.BorderColor = ThemeColor.darkColor;
                    btn.onHoverState.FillColor = ThemeColor.darkColor;
                    btn.onHoverState.ForeColor = Color.White;

                    btn.OnIdleState.BorderColor = ThemeColor.darkColor;
                    btn.OnIdleState.FillColor = ThemeColor.primaryColor;
                    btn.OnIdleState.ForeColor = Color.White;

                    btn.OnPressedState.BorderColor = ThemeColor.darkColor;
                    btn.OnPressedState.FillColor = ThemeColor.primaryColor;
                    btn.OnPressedState.ForeColor = Color.White;
                    btn.Font = new Font(btn.Font, btn.Font.Style | FontStyle.Bold);
                    btn.Cursor = Cursors.Hand;
                    //btn.IdleBorderRadius = 12;

                    // btn.AutoRoundBorders = true;
                    //btn.AutoGenerateColors = true;
                    btn.Invalidate();
                    btn.Refresh();
                }
                if (ctrl.GetType() == typeof(BunifuTextBox))
                {
                    BunifuTextBox textBox = (BunifuTextBox)ctrl;
                    textBox.BorderColorActive = ThemeColor.darkColor;
                    textBox.BorderColorHover = ThemeColor.primaryColor;
                    textBox.BorderColorIdle = ThemeColor.darkColor;
                }
                if (ctrl.GetType() == typeof(BunifuDropdown))
                {
                    BunifuDropdown dropdownList = (BunifuDropdown)ctrl;
                    dropdownList.IndicatorColor = ThemeColor.darkColor;
                    dropdownList.ItemBackColor = ThemeColor.darkColor;
                    dropdownList.ItemForeColor = Color.White;
                    dropdownList.ItemHighLightColor = ThemeColor.primaryColor;
                    dropdownList.BorderColor = ThemeColor.darkColor;
                }
                if (ctrl.GetType() == typeof(BunifuFormControlBox))
                {
                    BunifuFormControlBox controlBox = (BunifuFormControlBox)ctrl;
                    controlBox.CloseBoxOptions.HoverColor = ThemeColor.darkColor;
                    controlBox.CloseBoxOptions.IconHoverColor = Color.Black;
                    controlBox.CloseBoxOptions.IconPressedColor = Color.Black;
                    controlBox.CloseBoxOptions.PressedColor = ThemeColor.darkColor;
                    controlBox.MaximizeBoxOptions.HoverColor = ThemeColor.darkColor;
                    controlBox.MaximizeBoxOptions.PressedColor = ThemeColor.darkColor;
                    controlBox.MinimizeBoxOptions.HoverColor = ThemeColor.darkColor;
                    controlBox.MinimizeBoxOptions.PressedColor = ThemeColor.darkColor;
                }
            }
        }

        /*  private static void setProcessDpiAwareness()
        {
            try
            {
                if (Environment.OSVersion.Version.Major >= 6) // Windows Vista and later
                {
                    SetProcessDPIAware(); // Older method
                    SetProcessDpiAwareness(PROCESS_DPI_AWARENESS.PROCESS_PER_MONITOR_DPI_AWARE);
                }
            }
            catch { /* Ignore exceptions 
    }
}

      [DllImport("user32.dll")]
        private static extern bool SetProcessDPIAware();

        [DllImport("shcore.dll")]
        private static extern int SetProcessDpiAwareness(PROCESS_DPI_AWARENESS value);

        private enum PROCESS_DPI_AWARENESS
        {
            PROCESS_DPI_UNAWARE = 0,
            PROCESS_SYSTEM_DPI_AWARE = 1,
            PROCESS_PER_MONITOR_DPI_AWARE = 2
        }*/
    }
}
