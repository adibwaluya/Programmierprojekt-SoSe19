using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Xceed.Wpf;
using View;
using DataModel;

namespace View
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        public SettingsWindow()
        {
            InitializeComponent();
        }

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

        private void SaveColor_Button_Click(object sender, RoutedEventArgs e)
        {
            Box1.Text = "#" + ClrPcker_Body1.SelectedColor.Value.R.ToString() + ClrPcker_Body1.SelectedColor.Value.G.ToString() + ClrPcker_Body1.SelectedColor.Value.B.ToString();
            Box2.Text = "#" + ClrPcker_Body2.SelectedColor.Value.R.ToString() + ClrPcker_Body2.SelectedColor.Value.G.ToString() + ClrPcker_Body2.SelectedColor.Value.B.ToString();
            Box3.Text = "#" + ClrPcker_Error.SelectedColor.Value.R.ToString() + ClrPcker_Error.SelectedColor.Value.G.ToString() + ClrPcker_Error.SelectedColor.Value.B.ToString();

            Color body1Color = ClrPcker_Body1.SelectedColor.Value;
            Color body2Color = ClrPcker_Body2.SelectedColor.Value;
            Color errorColor = ClrPcker_Error.SelectedColor.Value;
        }

        private void SaveTolerance_Button_Click(object sender, RoutedEventArgs e)
        {
            string setTolerance = Tolerance_Box.Text;
        }
    }
}
