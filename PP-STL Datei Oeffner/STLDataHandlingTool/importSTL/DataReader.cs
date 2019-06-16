using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data;
using System.Threading;
using System.Globalization;
using System.Reflection;

namespace importSTL
{
    public class DataReader
    {
        public string path;
        private enum FileType { NONE, BINARY, ASCII };

        private FileType GetFileType(string filePath)
        {
            FileType stlFileType = FileType.NONE;

            if (File.Exists(filePath))
            {
                int lineCount = 0;
                lineCount = File.ReadLines(filePath).Count();

                string firstLine = File.ReadLines(filePath).First();

                string endLine = File.ReadLines(filePath).Skip(lineCount - 1).Take(1).First() +
                                 File.ReadLines(filePath).Skip(lineCount - 2).Take(1).First();

                if ((firstLine.IndexOf("solid") != -1) & (endLine.IndexOf("endsolid") != -1))
                {
                    stlFileType = FileType.ASCII;
                }
                else
                {
                    stlFileType = FileType.BINARY;
                }
            }
            else
            {
                stlFileType = FileType.NONE;
            }

            return stlFileType;
        }

        private DataModel.Point FromStrings(string s1, string s2, string s3)
        {
            if (double.TryParse(s1, out double d1))
                if (double.TryParse(s2, out double d2))
                    if (double.TryParse(s3, out double d3))
                    {
                        return new DataModel.Point(d1, d2, d3);
                    }
            return null;

        }

        private DataModel.DataStructure ReadASCIIFile(string stlPath)
        {
            DataModel.DataStructure dm = new DataModel.DataStructure();
            CultureInfo ci = System.Threading.Thread.CurrentThread.CurrentCulture;
            System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo("en");

            string[] lines = File.ReadAllLines(stlPath);
            DataModel.Point normal;
            DataModel.Point[] points = new DataModel.Point[3];
            int idxPoint = -1;

            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                // Trim() removes all whitespace characters from the beginning and end of the string
                // Replace() will replace designated characters with the given replacement
                // In this case, replace to " " 
                string[] parts = line.Split(' ');
                if (parts.Length == 0) continue;
                switch (parts[0])
                {
                    case "facet":
                        if (parts.Length != 5)
                        {
                            //Fehlermeldung or such (?)
                        }
                        normal = FromStrings(parts[2], parts[3], parts[4]);
                        idxPoint = 0;
                        break;
                    case "vertex":
                        if (parts.Length != 4)
                        {
                            //Fehlermeldung or such (?)
                        }
                        if (idxPoint > 2 || idxPoint == -1)
                        {

                        }
                        points[idxPoint++] = FromStrings(parts[1], parts[2], parts[3]);
                        break;
                    case "endfacet":
                        if (idxPoint != 3)
                        { }

                        //Punkte in pointlist, face erzeugen
                        int p1 = dm.points.AddOrGetPoint(points[0]);
                        int p2 = dm.points.AddOrGetPoint(points[1]);
                        int p3 = dm.points.AddOrGetPoint(points[2]);
                        int p2, p3; //Noch nicht fertig
                        DataModel.Edge e1 = new DataModel.Edge(p1, p2, dm);
                        int ei1 = dm.edges.AddOrGetEdge(e1);
                        idxPoint = -1;
                        break;
                }
            }
            System.Threading.Thread.CurrentThread.CurrentCulture = ci;
        }

    }
}
