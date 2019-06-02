using System.Windows;
using OpenGlUserControl;


namespace ViewModelTest
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

        }

        private void CreateAndShowModel_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Hello World");
        }

        private void WindowsFormsHost_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}