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

namespace StlExport
{
    public class DataWriter
    {
        // Collect all coordinates from PointList
        DataModel.DataStructure dm = new DataModel.DataStructure();
        private List<Point> ListOfPoints;

        private void ListCoordinates()
        {
            foreach (Point p in dm.points)
            {
                ListOfPoints.Add(p);
            }
        }

        // Collect all point-normal from Normal
        private List<Normal> ListOfNormals;


        private void ListNormal()
        {
            foreach (Point pts in ListOfPoints)
            {
                ListOfNormals.Add(Normal.FromPoint(pts));
            }
        }

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

                for (int i = 0; i < ListOfPoints.Count; i = i + 3)
                {
                    //All normal and points as e-sign exponent format
                    string nXasE = ListOfNormals[i].X.ToString("E");
                    string nYasE = ListOfNormals[i].Y.ToString("E");
                    string nZasE = ListOfNormals[i].Z.ToString("E");

                    string iXasE = ListOfPoints[i].X.ToString("E"); // for i
                    string iYasE = ListOfPoints[i].Y.ToString("E");
                    string iZasE = ListOfPoints[i].Z.ToString("E");

                    string i1XasE = ListOfPoints[i + 1].X.ToString("E"); // for i + 1
                    string i1YasE = ListOfPoints[i + 1].Y.ToString("E");
                    string i1ZasE = ListOfPoints[i + 1].Z.ToString("E");

                    string i2XasE = ListOfPoints[i + 2].X.ToString("E"); // for i + 2
                    string i2YasE = ListOfPoints[i + 2].Y.ToString("E");
                    string i2ZasE = ListOfPoints[i + 2].Z.ToString("E");

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
                txtWriter.Write((ListOfPoints.Count / 3)); // A triangle consists of 3 points

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

    public class ExportTest_DataModel
    {
        public void FillDatamodel(DataStructure dm)
        {
            dm.AddPoint(11, 22, 33);
            dm.AddPoint(20, 10, 30);
            dm.AddPoint(1, 2, 3);
            dm.AddPoint(4, 6, 8);

            dm.AddEdge(0, 1);
            dm.AddEdge(1, 2);
            dm.AddEdge(2, 3);
            dm.AddEdge(3, 0);
            dm.AddEdge(3, 1);

            Normal norm1 = new Normal(1, 1, 1);
            Normal norm2 = new Normal(1, 2, 3);


            dm.AddFace(3, 1, 2, norm1);
            dm.AddFace(0, 1, 2, norm2);
        }
    }
}