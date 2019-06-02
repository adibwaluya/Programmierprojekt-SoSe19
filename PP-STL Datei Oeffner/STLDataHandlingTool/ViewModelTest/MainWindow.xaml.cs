using System.Windows;
using OpenGlUserControl;
using System.Windows.Forms;
using System.Windows.Forms.Integration;


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

        // EventHandler for Btn in MAinWindow
        private void CreateAndShowModel_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.MessageBox.Show("Hello World");
        }
    }
}