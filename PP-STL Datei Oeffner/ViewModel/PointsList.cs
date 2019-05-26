using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    class PointsList : Point
    {
        public List<Point> ListOfPoints { get; set; }

        public PointsList()
        {
            ListOfPoints = InitializingPointsList();
        }

        private List<Point> InitializingPointsList()
        {
            ListOfPoints.Add(new Point(1.000000e+01, -1.000000e+01, -1.000000e+01));
            ListOfPoints.Add(new Point(1.000000e+01, -1.000000e+01, 1.000000e+01));
            ListOfPoints.Add(new Point(-1.000000e+01, -1.000000e+01, -1.000000e+01));
            ListOfPoints.Add(new Point(-1.000000e+01, -1.000000e+01, -1.000000e+01));
            ListOfPoints.Add(new Point(1.000000e+01, -1.000000e+01, 1.000000e+01));
            ListOfPoints.Add(new Point(-1.000000e+01, -1.000000e+01, 1.000000e+01));
            //
            ListOfPoints.Add(new Point(1.000000e+01, 1.000000e+01, -1.000000e+01));
            ListOfPoints.Add(new Point(1.000000e+01, 1.000000e+01, 1.000000e+01));
            ListOfPoints.Add(new Point(1.000000e+01, -1.000000e+01, -1.000000e+01));
            ListOfPoints.Add(new Point(1.000000e+01, -1.000000e+01, -1.000000e+01));
            ListOfPoints.Add(new Point(1.000000e+01, 1.000000e+01, 1.000000e+01));
            ListOfPoints.Add(new Point(1.000000e+01, -1.000000e+01, 1.000000e+01));
            // 
            ListOfPoints.Add(new Point(1.000000e+01, 1.000000e+01, 1.000000e+01));
            ListOfPoints.Add(new Point(-1.000000e+01, 1.000000e+01, 1.000000e+01));
            ListOfPoints.Add(new Point(1.000000e+01, -1.000000e+01, 1.000000e+01));
            ListOfPoints.Add(new Point(1.000000e+01, -1.000000e+01, 1.000000e+01));
            ListOfPoints.Add(new Point(-1.000000e+01, 1.000000e+01, 1.000000e+01));
            ListOfPoints.Add(new Point(-1.000000e+01, -1.000000e+01, 1.000000e+01));
            //
            ListOfPoints.Add(new Point(-1.000000e+01, 1.000000e+01, 1.000000e+01));
            ListOfPoints.Add(new Point(-1.000000e+01, 1.000000e+01, -1.000000e+01));
            ListOfPoints.Add(new Point(-1.000000e+01, -1.000000e+01, 1.000000e+01));
            ListOfPoints.Add(new Point(-1.000000e+01, -1.000000e+01, 1.000000e+01));
            ListOfPoints.Add(new Point(-1.000000e+01, 1.000000e+01, -1.000000e+01));
            ListOfPoints.Add(new Point(-1.000000e+01, -1.000000e+01, -1.000000e+01));
            //
            ListOfPoints.Add(new Point(-1.000000e+01, 1.000000e+01, -1.000000e+01));
            ListOfPoints.Add(new Point(1.000000e+01, 1.000000e+01, -1.000000e+01));
            ListOfPoints.Add(new Point(-1.000000e+01, -1.000000e+01, -1.000000e+01));
            ListOfPoints.Add(new Point(-1.000000e+01, -1.000000e+01, -1.000000e+01));
            ListOfPoints.Add(new Point(1.000000e+01, 1.000000e+01, -1.000000e+01));
            ListOfPoints.Add(new Point(1.000000e+01, -1.000000e+01, -1.000000e+01));
            //
            ListOfPoints.Add(new Point(-1.000000e+01, 1.000000e+01, -1.000000e+01));
            ListOfPoints.Add(new Point(-1.000000e+01, 1.000000e+01, 1.000000e+01));
            ListOfPoints.Add(new Point(1.000000e+01, 1.000000e+01, -1.000000e+01));
            ListOfPoints.Add(new Point(1.000000e+01, 1.000000e+01, -1.000000e+01));
            ListOfPoints.Add(new Point(-1.000000e+01, 1.000000e+01, 1.000000e+01));
            ListOfPoints.Add(new Point(1.000000e+01, 1.000000e+01, 1.000000e+01));

            return ListOfPoints;
        }

        public static implicit operator List<object>(PointsList v)
        {
            throw new NotImplementedException();
        }
    }
}
