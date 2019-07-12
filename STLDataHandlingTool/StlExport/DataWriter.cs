/*******************************************************************************************
 * Copyright (c) <2019><Michael Kaip, Maximilian Mews, Michael Reno, Adib Ghassani Waluya> *
 *******************************************************************************************/

using System;
using System.Text;
using System.IO;
using System.Globalization;
using System.Threading;
using StlExportDataModel;
using DataModel;
using System.Windows;

namespace StlExport
{
    public class DataWriter
    {
        // Collect all coordinates from PointList in test environment (from View)
        //DataStructure dm = new DataStructure();
        DataStructure testDM = new DataStructure();

        // Indentation as strings
        public string indent = String.Join("    ", new String[4]);
        public string indent2 = String.Join("    ", new String[8]);

        // Compile as one STL File
        // This one is as ASCII file
        public void AsAsciiFile(string File, DataStructure dataStructure) //TODO: Data Model as parameter and Exception as return type?
        {
            StreamWriter txtWriter = null;
            try
            {
                // Add file name and location to StreamWriter
                txtWriter = new StreamWriter(File, false);

                // Starting to write the data from here
                // Write an opening line of ASCII STL Data
                txtWriter.WriteLine("solid ");

                // Setting the culture info to make sure the exponents are the same
                CultureInfo current = new CultureInfo("en-US");
                Thread.CurrentThread.CurrentCulture = current;
                Thread.CurrentThread.CurrentUICulture = current;
            
                // Write the body of the STL data
                // with for - loop to iterate every three points
                for (int i = 0; i < dataStructure.points.m_int2pt.Count; i = i + 3)
                {
                    //All normal and points as e-sign exponent format
                    Face newFace = dataStructure.faces.m_int2Face[i];
                    Normal norm = newFace.NormFromFace(dataStructure.faces.m_int2Face[i]);

                    string nXasE = norm.X.ToString("E"); // Normals can also be {0; 0; 0}
                    string nYasE = norm.Y.ToString("E");
                    string nZasE = norm.Z.ToString("E");

                    string iXasE = dataStructure.points.m_int2pt[i].X.ToString("E"); // for i
                    string iYasE = dataStructure.points.m_int2pt[i].Y.ToString("E");
                    string iZasE = dataStructure.points.m_int2pt[i].Z.ToString("E");

                    string i1XasE = dataStructure.points.m_int2pt[i + 1].X.ToString("E"); // for i + 1
                    string i1YasE = dataStructure.points.m_int2pt[i + 1].Y.ToString("E");
                    string i1ZasE = dataStructure.points.m_int2pt[i + 1].Z.ToString("E");

                    string i2XasE = dataStructure.points.m_int2pt[i + 2].X.ToString("E"); // for i + 2
                    string i2YasE = dataStructure.points.m_int2pt[i + 2].Y.ToString("E");
                    string i2ZasE = dataStructure.points.m_int2pt[i + 2].Z.ToString("E");

                    //Write the body of ASCII STL Data
                    txtWriter.WriteLine($"facet normal {nXasE} {nYasE} {nZasE}");
                    txtWriter.WriteLine(indent + "outer loop");
                    txtWriter.WriteLine($"{indent2} vertex {iXasE} {iYasE} {iZasE}");       //coordinates point1
                    txtWriter.WriteLine($"{indent2} vertex {i1XasE} {i1YasE} {i1ZasE}");    //coordinates point2
                    txtWriter.WriteLine($"{indent2} vertex {i2XasE} {i2YasE} {i2ZasE}");    //coordinates point3
                    txtWriter.WriteLine(indent + "endloop");
                    txtWriter.WriteLine("endfacet");
                }

                // Finish the file
                txtWriter.Write("endsolid");

            }
            catch (Exception e)
            {
                MessageBox.Show("A handled exception just occurred: " + e.Message + "\nPlease try again.", "Exception During Export", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            finally
            {
                //Close the file
                if (txtWriter != null) txtWriter.Close();
            }
        }

        // This one is as binary file
        public void AsBinaryFile(string File, DataStructure dataStructure)
        {
            // Create new BinaryWriter
            using (var txtWriter = new BinaryWriter(System.IO.File.OpenWrite(File), Encoding.ASCII))
            {
                try
                {
                    // Setting the culture info to make sure the exponents are the same
                    CultureInfo current = new CultureInfo("en-US");
                    Thread.CurrentThread.CurrentCulture = current;
                    Thread.CurrentThread.CurrentUICulture = current;

                    // Encode the header of the binary file as ASCII and set the buffer to 80 bytes
                    string HeaderAsString = File;
                    byte[] Header = new byte[80];
                    Encoding.ASCII.GetBytes(HeaderAsString, 0, HeaderAsString.Length, Header, 0);
                    txtWriter.Write(Header + " ");

                    // UINT32 – Number of triangles
                    uint totalTriangles = (((UInt32) dataStructure.points.m_int2pt.Count) / 3); // A triangle consists of 3 points
                    txtWriter.Write(totalTriangles.ToString("E") + " ");

                    // foreach triangle
                    // for - loop is used to get a better iteration over the points
                    for (int i = 0; i <= dataStructure.points.m_int2pt.Count; i = i + 3)
                    {
                        //All normal and points as e-sign exponent format
                        Face newFace = dataStructure.faces.m_int2Face[i];
                        Normal norm = newFace.NormFromFace(dataStructure.faces.m_int2Face[i]);

                        uint nX = (UInt32) norm.X;               // Normals can also be {0; 0; 0}
                        uint nY = (UInt32)norm.Y;
                        uint nZ = (UInt32)norm.Z;

                        string nXasE = nX.ToString("E2");
                        string nYasE = nY.ToString("E2");
                        string nZasE = nZ.ToString("E2");

                        uint iX = (UInt32)dataStructure.points.m_int2pt[i].X; // for i
                        uint iY = (UInt32)dataStructure.points.m_int2pt[i].Y;
                        uint iZ = (UInt32)dataStructure.points.m_int2pt[i].Z;
                        string iXasE = iX.ToString("E2");
                        string iYasE = iY.ToString("E2");
                        string iZasE = iZ.ToString("E2");

                        uint i1X = (UInt32)dataStructure.points.m_int2pt[i + 1].X; // for i + 1
                        uint i1Y = (UInt32)dataStructure.points.m_int2pt[i + 1].Y;
                        uint i1Z = (UInt32)dataStructure.points.m_int2pt[i + 1].Z;
                        string i1XasE = i1X.ToString("E2");
                        string i1YasE = i1Y.ToString("E2");
                        string i1ZasE = i1Z.ToString("E2");

                        uint i2X = (UInt32)dataStructure.points.m_int2pt[i + 2].X;
                        uint i2Y = (UInt32)dataStructure.points.m_int2pt[i + 2].Y;
                        uint i2Z = (UInt32)dataStructure.points.m_int2pt[i + 2].Z;
                        string i2XasE = i2X.ToString("E2"); // for i + 2
                        string i2YasE = i2Y.ToString("E2");
                        string i2ZasE = i2Z.ToString("E2");

                        //Write the body of binary STL Data
                        // REAL32[3] four times - normal, vertex 1, vertex 2, vertex 3 and attribute (0)
                        txtWriter.Write($"{nXasE} {nYasE} {nZasE} {iXasE} {iYasE} {iZasE} {i1XasE} {i1YasE} {i1ZasE} {i2XasE} {i2YasE} {i2ZasE} 0 ");
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show("A handled exception just occurred: " + e.Message + "\nPlease try again.", "Exception During Export", MessageBoxButton.OK, MessageBoxImage.Warning);
                    throw;
                }
            }
        }
    }
}