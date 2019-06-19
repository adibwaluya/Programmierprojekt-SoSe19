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
using View;

namespace StlExport
{
    public class DataWriter
    {
        // Collect all coordinates from PointList in test environment (from View)
        DataModel.DataStructure dm = new DataModel.DataStructure();
        View.ExportTest_DataModel testDM = new ExportTest_DataModel();

        // Indentation as strings
        readonly string indent = String.Join("    ", new String[4]);
        readonly string indent2 = String.Join("    ", new String[8]);

        // Compile as one STL File
        // This one is as ASCII file
        private void AsAsciiFile(string filePath) //TODO: Data Model as parameter and Exception as return type?
        {
            StreamWriter txtWriter = null;
            try
            {
                // Add file name and location to StreamWriter
                txtWriter = new StreamWriter(filePath);

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
        private void AsBinaryFile(string File)
        {
            using (var txtWriter = new BinaryWriter(System.IO.File.OpenWrite(File), Encoding.ASCII))
            {
                // Encode the header of the binary file as ASCII and set the buffer to 80 bytes
                string HeaderAsString = File;
                byte[] Header = new byte[80];
                Encoding.ASCII.GetBytes(HeaderAsString, 0, HeaderAsString.Length, Header, 0);
                txtWriter.Write(Header);

                // UINT32 – Number of triangles
                txtWriter.Write((testDM.ListOfPoints.Count / 3)); // A triangle consists of 3 points

                // foreach triangle
                // REAL32[3] – Normal vector
                // REAL32[3] – Vertex 1
                // REAL32[3] – Vertex 2
                // REAL32[3] – Vertex 3
                // UINT16 – Attribute byte count
            }

            //using (StreamWriter txtWriter = new StreamWriter(File))
            //{
            //    try
            //    {
            //        // Binary file starts here
            //        // Header
            //        txtWriter.WriteLine(File); //TODO: Change header to file's name?

            //        BitConverter.DoubleToInt64Bits(ListOfPoints.Count);
            //        // Total number of triangles
            //        txtWriter.WriteLine();

            //        //TODO: finish binary files

            //    }
            //    catch (Exception e)
            //    {
            //        Console.WriteLine(e);
            //        throw;
            //    }
            //}
            
        }


    }
}