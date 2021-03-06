﻿/*******************************************************************************************
 * Copyright (c) <2019><Michael Kaip, Maximilian Mews, Michael Reno, Adib Ghassani Waluya> *
 *******************************************************************************************/

using System.Windows;
using System.Windows.Media;
using DataModel;

namespace View
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {

        public static UserSettings settings;

        public SettingsWindow()
        {
            InitializeComponent();
        }

        #region Colors
        private void ClrPcker_Body1_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> routedPropertyChangedEventArgs)
        {
            Box1.Text = "#" + ClrPcker_Body1.SelectedColor.Value.R.ToString() + ClrPcker_Body1.SelectedColor.Value.G.ToString() + ClrPcker_Body1.SelectedColor.Value.B.ToString();
        }

        private void ClrPcker_Body2_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> routedPropertyChangedEventArgs)
        {
            Box2.Text = "#" + ClrPcker_Body2.SelectedColor.Value.R.ToString() + ClrPcker_Body2.SelectedColor.Value.G.ToString() + ClrPcker_Body2.SelectedColor.Value.B.ToString();
        }

        private void ClrPcker_Error_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> routedPropertyChangedEventArgs)
        {
            Box3.Text = "#" + ClrPcker_Error.SelectedColor.Value.R.ToString() + ClrPcker_Error.SelectedColor.Value.G.ToString() + ClrPcker_Error.SelectedColor.Value.B.ToString();
        }

        private void ClrPcker_Bckgrnd_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            Box4.Text = "#" + ClrPcker_Bckgrnd.SelectedColor.Value.R.ToString() + ClrPcker_Bckgrnd.SelectedColor.Value.G.ToString() + ClrPcker_Bckgrnd.SelectedColor.Value.B.ToString();
        }

        private void ClrPcker_Frgrnd_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            Box5.Text = "#" + ClrPcker_Frgrnd.SelectedColor.Value.R.ToString() + ClrPcker_Frgrnd.SelectedColor.Value.G.ToString() + ClrPcker_Frgrnd.SelectedColor.Value.B.ToString();
        }

        private void SaveColor_Button_Click(object sender, RoutedEventArgs e)
        {
            Box1.Text = "#" + ClrPcker_Body1.SelectedColor.Value.R.ToString() + ClrPcker_Body1.SelectedColor.Value.G.ToString() + ClrPcker_Body1.SelectedColor.Value.B.ToString();
            Box2.Text = "#" + ClrPcker_Body2.SelectedColor.Value.R.ToString() + ClrPcker_Body2.SelectedColor.Value.G.ToString() + ClrPcker_Body2.SelectedColor.Value.B.ToString();
            Box3.Text = "#" + ClrPcker_Error.SelectedColor.Value.R.ToString() + ClrPcker_Error.SelectedColor.Value.G.ToString() + ClrPcker_Error.SelectedColor.Value.B.ToString();
            Box4.Text = "#" + ClrPcker_Bckgrnd.SelectedColor.Value.R.ToString() + ClrPcker_Bckgrnd.SelectedColor.Value.G.ToString() + ClrPcker_Bckgrnd.SelectedColor.Value.B.ToString();
            Box5.Text = "#" + ClrPcker_Frgrnd.SelectedColor.Value.R.ToString() + ClrPcker_Frgrnd.SelectedColor.Value.G.ToString() + ClrPcker_Frgrnd.SelectedColor.Value.B.ToString();

            Color body1Color = ClrPcker_Body1.SelectedColor.Value;
            Color body2Color = ClrPcker_Body2.SelectedColor.Value;
            Color errorColor = ClrPcker_Error.SelectedColor.Value;
            Color backgroundColor = ClrPcker_Bckgrnd.SelectedColor.Value;
            Color foregroundColor = ClrPcker_Bckgrnd.SelectedColor.Value;

            settings = new UserSettings(backgroundColor, foregroundColor, errorColor);
            this.Close();
        }
        #endregion
    }
}
