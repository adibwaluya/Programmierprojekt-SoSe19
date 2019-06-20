using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using DataModel;
using System.Collections;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Threading;
using StlExportDataModel;
using DataModel;

namespace StlExport
{
    public class DataWriter
    {
        // Collect all coordinates from PointList in test environment (from View)
        //DataStructure dm = new DataStructure();
        StlExportTestDM testDM = new StlExportTestDM();

        // Indentation as strings
        readonly string indent = String.Join("    ", new String[4]);
        readonly string indent2 = String.Join("    ", new String[8]);

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

                for (int i = 0; i < testDM.ListOfPoints.Count; i = i + 3)
                {
                    //All normal and points as e-sign exponent format
                    string nXasE = testDM.ListOfNormals[i].X.ToString("E");
                    string nYasE = testDM.ListOfNormals[i].Y.ToString("E");
                    string nZasE = testDM.ListOfNormals[i].Z.ToString("E");

                    string iXasE = testDM.ListOfPoints[i].X.ToString("E"); // for i
                    string iYasE = testDM.ListOfPoints[i].Y.ToString("E");
                    string iZasE = testDM.ListOfPoints[i].Z.ToString("E");

                    string i1XasE = testDM.ListOfPoints[i + 1].X.ToString("E"); // for i + 1
                    string i1YasE = testDM.ListOfPoints[i + 1].Y.ToString("E");
                    string i1ZasE = testDM.ListOfPoints[i + 1].Z.ToString("E");

                    string i2XasE = testDM.ListOfPoints[i + 2].X.ToString("E"); // for i + 2
                    string i2YasE = testDM.ListOfPoints[i + 2].Y.ToString("E");
                    string i2ZasE = testDM.ListOfPoints[i + 2].Z.ToString("E");

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
                Console.WriteLine("Exception: " + e.Message);
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
                    txtWriter.Write(Header);

                    // UINT32 – Number of triangles
                    txtWriter.Write(((testDM.ListOfPoints.Count) / 3)); // A triangle consists of 3 points

                    // foreach triangle
                    for (int i = 0; i < testDM.ListOfPoints.Count; i = i + 3)
                    {
                        //All normal and points as e-sign exponent format
                        string nXasE = testDM.ListOfNormals[i].X.ToString("E");
                        string nYasE = testDM.ListOfNormals[i].Y.ToString("E");
                        string nZasE = testDM.ListOfNormals[i].Z.ToString("E");

                        string iXasE = testDM.ListOfPoints[i].X.ToString("E"); // for i
                        string iYasE = testDM.ListOfPoints[i].Y.ToString("E");
                        string iZasE = testDM.ListOfPoints[i].Z.ToString("E");

                        string i1XasE = testDM.ListOfPoints[i + 1].X.ToString("E"); // for i + 1
                        string i1YasE = testDM.ListOfPoints[i + 1].Y.ToString("E");
                        string i1ZasE = testDM.ListOfPoints[i + 1].Z.ToString("E");

                        string i2XasE = testDM.ListOfPoints[i + 2].X.ToString("E"); // for i + 2
                        string i2YasE = testDM.ListOfPoints[i + 2].Y.ToString("E");
                        string i2ZasE = testDM.ListOfPoints[i + 2].Z.ToString("E");

                        // Write the body of binary STL Data

                        // REAL32[3] – Normal vector
                        txtWriter.Write($"{nXasE} {nYasE} {nZasE} ");
                        // REAL32[3] – Vertex 1
                        txtWriter.Write($"{iXasE} {iYasE} {iZasE} ");
                        // REAL32[3] – Vertex 2
                        txtWriter.Write($"{i1XasE} {i1YasE} {i1ZasE} ");
                        // REAL32[3] – Vertex 3
                        txtWriter.Write($"{i2XasE} {i2YasE} {i2ZasE} ");
                        // UINT16 – Attribute byte count = normally 0
                        txtWriter.Write(0);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }
    }
}