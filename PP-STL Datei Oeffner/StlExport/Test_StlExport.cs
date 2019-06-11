using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using DataModel;
using System.Collections;

namespace StlExport
{
    public class Test_StlExport: IEnumerable
    {
        // Collect all coordinates from PointList
        DataModel.DataStructure dm = new DataModel.DataStructure();
        List<Point> ListOfPoints;
        int x;

        //public void listCoordinates()
        //{
        //    foreach (Point p in dm.points)
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
            
        }


        // IEnumerator
        public IEnumerator<Point> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}