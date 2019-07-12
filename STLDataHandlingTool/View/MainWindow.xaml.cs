/*******************************************************************************************
 * Copyright (c) <2019><Michael Kaip, Maximilian Mews, Michael Reno, Adib Ghassani Waluya> *
 *******************************************************************************************/

using System;
using System.Diagnostics;
using System.Text;
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
        // Creating data structure for all components to use
        static DataStructure STLFile = new DataStructure();
        public DataStructure STL { get { return STLFile; } }

        public MainWindow()
        {
            InitializeComponent();
        }

        #region ClickEvent
        #region Save
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
            // Instantiate DataWriter to access its methods
            DataWriter dw = new DataWriter();

            // Requirements to save the file
            bool reqA = saveDlg.ShowDialog() == true;   // To open the Save Dialog
            bool reqB = saveDlg.FilterIndex == 1;       // If the user wants to save the file as an ASCII STL File
            if (STLFile != null)
            {
                if (reqA && reqB)
                {
                    // AsAsciiFile here with SaveDlg.Filename and data model as parameter
                    dw.AsAsciiFile(saveDlg.FileName, STLFile);
                    MessageBox.Show("File saved as an ASCII STL File.", "Successful!");
                }
                else // If the user wants to save the DataStructure as a binary STL File (FilterIndex == 2)
                {
                    // AsBinaryFile here with SaveDlg.Filename and data model as parameter
                    dw.AsBinaryFile(saveDlg.FileName, STLFile);
                    MessageBox.Show("File saved as a binary STL File.", "Successful!");
                }
            }
            else
            {
                MessageBox.Show("No STL file exist. \nPlease open a valid file first.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion
        #region Open
        // Open file
        private void Open_OnClick(object sender, RoutedEventArgs e)
        {
            // Set the button as an open file dialog when clicked
            OpenFileDialog openDlg = new OpenFileDialog
            {
                // Set the title of the pop-up window
                Title = "Browse STL Data",
                // Set the filter so the user can only open STL files
                Filter = "STL File (*.stl) | *.stl;*.txt",
                // Set the initial directory of open file to My Documents
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
            };
            try
            {
                // Requirements to open the file
                if (openDlg.ShowDialog() == true)
                {
                    // Connect to STL Import
                    importSTL.DataReader read = new importSTL.DataReader(openDlg.FileName);
                    STLFile = read.ReadFile();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occured: " + ex, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion
        #region ErrorHandling
        // Error Handling
        private void ErrorHandle_OnClick(object sender, RoutedEventArgs e)
        {
            if (STLFile != null)
            {
                if (SettingsWindow.settings != null)
                {
                    try
                    {
                        // take colors from data model
                        var errorColor = System.Drawing.Color.FromArgb(
                            SettingsWindow.settings.ErrorColor.A, SettingsWindow.settings.ErrorColor.R,
                            SettingsWindow.settings.ErrorColor.G, SettingsWindow.settings.ErrorColor.B);

                        // set the timer for info box
                        Stopwatch timePassed = new Stopwatch();
                        timePassed.Start();

                        ErrorHandling.ErrorFinding error = new ErrorHandling.ErrorFinding();
                        error.FindError(STLFile, errorColor);

                        // Information for the info box
                        StringBuilder sb = new StringBuilder();
                        for (int i = 0; STLFile.edges.GetEdge(i) != null; i++)
                        {
                            if (STLFile.edges.GetEdge(i).CurrentCondition != DataModel.Edge.Condition.NotFaulty)
                            {
                                sb.AppendLine("ID " + Convert.ToString(i) + " " + Convert.ToString(STLFile.edges.GetEdge(i).CurrentCondition));
                            }
                        }
                        timePassed.Stop();
                        sb.AppendLine("Edges not Listed here are not faulty");
                        sb.AppendLine("Time Passed: " + timePassed.Elapsed);
                        Info_Box_Error.Text = Convert.ToString(sb);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("An error occured: " + ex, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("No error color chosen. \nPlease choose a valid color first.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

                }
            }
            else
            {
                MessageBox.Show("No STL file exist. \nPlease open a valid file first.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion
        #region Setting
        // Setting
        private void Settings_OnClick(object sender, RoutedEventArgs e)
        {
            // Open new window
            SettingsWindow setWindow = new SettingsWindow();
            setWindow.ShowDialog();
        }
        #endregion
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
