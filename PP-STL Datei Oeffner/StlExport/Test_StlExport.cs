using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using DataModel;

namespace StlExport: IEnumerable
{
    public class Test_StlExport
    {
        // Collect all coordinates from PointList
        DataModel.DataModel dm = new DataModel.DataModel();
        List<Point> ListOfPoints;

        //public void listCoordinates()
        //{
        //    foreach (Point p in dm.points.AddOrGetPoint(p))
        //    {
        //        ListOfPoints.Add(p);
        //    }

        //}
        // Collect all point-normal from Normal
        List<Normal> ListOfNormals;

        public void listNormal()
        {
            foreach (Point pts in ListOfPoints)
            {
                ListOfNormals.Add(Normal.FromPoint(pts));
            }
        }

        // Compile as one STL File
        public void AsFile(List<Point> pts, List<Normal> norms)
        {
            File.
        }
    }
}