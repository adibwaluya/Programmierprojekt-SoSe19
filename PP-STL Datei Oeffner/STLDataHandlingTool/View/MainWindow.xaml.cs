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
using System.Windows.Navigation;
using System.Windows.Shapes;
using DataModel;
using Microsoft.Win32;
using StlExport;
using StlExportDataModel;


namespace View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        //private void ClrPcker_Background_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color> e)
        //{
        //    TextBox.Text = "#" + ClrPcker_Background.SelectedColor.R.ToString() + ClrPcker_Background.SelectedColor.G.ToString() + ClrPcker_Background.SelectedColor.B.ToString();
        //}
        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            // Set the button as a save file dialog when clicked
            SaveFileDialog SaveDlg = new SaveFileDialog();

            // Set the filter so the user will know which format of stl will be saved
            SaveDlg.Filter = "ASCII STL File (*.stl) | *.stl | Binary STL File (*.stl) | *.stl";
            // Set the initial directory of the saved file to My Documents
            SaveDlg.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            // Instantiate data writer to access its methods
            DataWriter dw = new DataWriter();
            DataStructure dm = new DataStructure();
            StlExportTestDM testDM = new StlExportTestDM();
            
            // Requirements to save the file
            if (SaveDlg.ShowDialog() == true & SaveDlg.Filter == "ASCII STL File")
            {
                // AsAsciiFile here with SaveDlg.Filename and data model as parameter
                dw.AsAsciiFile(SaveDlg.FileName, testDM.FillDatamodel(dm));
            }
            else // If the user wants to save the DataStructure as a binary STL File
            {
                // AsBinaryFile here with SaveDlg.Filename and data model as parameter
                dw.AsBinaryFile(SaveDlg.FileName, testDM.FillDatamodel(dm));
            }
        }
    }
}
