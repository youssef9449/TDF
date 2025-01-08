using Bunifu.UI.WinForms;
using Bunifu.UI.WinForms.BunifuButton;
using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using TDF.Classes;
using static TDF.Net.Database;

namespace TDF.Net
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            CultureInfo english = new CultureInfo("en-GB");
            CultureInfo.DefaultThreadCurrentUICulture = english;
            CultureInfo.DefaultThreadCurrentCulture = english;
            CultureInfo.CurrentCulture = english;
            CultureInfo.CurrentUICulture = english;
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

        public static void applyTheme(Form form)
        {
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
                            // BunifuButton.AutoRoundBorders = true;
                            // BunifuButton.AutoGenerateColors = true;
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
                                    BunifuButton btn = (BunifuButton)ct;
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

                                    btn.Refresh();
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

                    // btn.AutoRoundBorders = true;
                    // btn.AutoGenerateColors = true;
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
                    // btn.AutoRoundBorders = true;
                    // btn.AutoGenerateColors = true;
                    btn.Invalidate();
                    btn.Refresh();
                }
                if (ctrl.GetType() == typeof(BunifuButton2))
                {
                    BunifuButton2 btn2 = (BunifuButton2)ctrl;

                    btn2.OnDisabledState.BorderColor = ThemeColor.darkColor;
                    btn2.OnDisabledState.FillColor = ThemeColor.primaryColor;
                    btn2.OnDisabledState.ForeColor = Color.White;

                    btn2.onHoverState.BorderColor = ThemeColor.darkColor;
                    btn2.onHoverState.FillColor = ThemeColor.darkColor;
                    btn2.onHoverState.ForeColor = Color.White;

                    btn2.OnIdleState.BorderColor = ThemeColor.darkColor;
                    btn2.OnIdleState.FillColor = ThemeColor.primaryColor;
                    btn2.OnIdleState.ForeColor = Color.White;

                    btn2.OnPressedState.BorderColor = ThemeColor.darkColor;
                    btn2.OnPressedState.FillColor = ThemeColor.primaryColor;
                    btn2.OnPressedState.ForeColor = Color.White;
                    btn2.Font = new Font(btn2.Font, btn2.Font.Style | FontStyle.Bold);
                    // btn2.AutoRoundBorders = true;
                    // btn2.AutoGenerateColors = true;
                    btn2.Invalidate();
                    btn2.Refresh();
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
    }
}
