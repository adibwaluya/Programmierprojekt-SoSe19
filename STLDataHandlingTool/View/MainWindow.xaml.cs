/*******************************************************************************************
 * Copyright (c) <2019><Michael Kaip, Maximilian Mews, Michael Reno, Adib Ghassani Waluya> *
 *******************************************************************************************/

using System;
using System.Windows;
using System.Windows.Controls;
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

        #region ClickEvent
        // Save file
        private void Save_OnClick(object sender, RoutedEventArgs e)
        {
            // Set the button as a save file dialog when clicked
            SaveFileDialog saveDlg = new SaveFileDialog
            {
                // Set the filter so the user will know which format of STL will be saved
                Filter = "ASCII STL File (*.stl)|*.stl|Binary STL File (*.stl)|*.stl",
                // Set the initial directory of the saved file to My Documents
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
            };

            #region Test Site
            // Instantiate data writer to access its methods
            StlExportTestDM testDM = new StlExportTestDM();
            DataWriter dw = new DataWriter();
            DataStructure dm = new DataStructure();
            testDM.FillDatamodel(dm);
            #endregion

            // Requirements to save the file
            bool reqA = saveDlg.ShowDialog() == true;   // To open the Save Dialog
            bool reqB = saveDlg.FilterIndex == 1;       // If the user wants to save the file as an ASCII STL File
            if (reqA && reqB)
            {
                // AsAsciiFile here with SaveDlg.Filename and data model as parameter
                dw.AsAsciiFile(saveDlg.FileName, dm);
                MessageBox.Show("File saved as an ASCII STL File.", "Successful!");
            }
            else // If the user wants to save the DataStructure as a binary STL File (FilterIndex == 2)
            {
                // AsBinaryFile here with SaveDlg.Filename and data model as parameter
                dw.AsBinaryFile(saveDlg.FileName, dm);
                MessageBox.Show("File saved as a binary STL File.", "Successful!");
            }
        }

        // Open file
        private void Open_OnClick(object sender, RoutedEventArgs e)
        {
            // Set the button as an open file dialog when clicked
            OpenFileDialog openDlg = new OpenFileDialog
            {
                // Set the filter so the user can only open STL files
                Filter = "STL File (*.stl) | *.stl",
                // Set the initial directory of open file to My Documents
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
            };

            // Requirements to open the file
            if (openDlg.ShowDialog() == true)
            {
                // Connect to STL Import dummy
                StlExportTestDM testDM = new StlExportTestDM();
                DataStructure dm = new DataStructure();
                testDM.FillDatamodel(dm);
            }
        }

        // Error handling
        private void ErrorHandle_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        // Setting
        private void Settings_OnClick(object sender, RoutedEventArgs e)
        {
            // Open new window
            SettingsWindow setWindow = new SettingsWindow();
            setWindow.ShowDialog();
        }

        // Undo
        private void UndoButton_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        // Redo
        private void RedoButton_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
        #endregion

        // Information will come out if the data model is already filled
        private void Information_Boxes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // The related information will be printed on the text box, according to which info the user wants on the drop down list
            if (Information_Boxes.SelectedIndex == 0 && new DataStructure().points.m_int2pt.Count != 0) // To prevent Null Exception
            {
                Info_Box.Text = "The number of points in the data is " + new DataStructure().points.m_int2pt.Count;
            }
            else if (Information_Boxes.SelectedIndex == 1 && new DataStructure().faces.m_int2Face.Count != 0) // To prevent Null Exception
            {
                Info_Box.Text = "The number of faces in the data is " + new DataStructure().faces.m_int2Face.Count;
            }
        }

    }
}
