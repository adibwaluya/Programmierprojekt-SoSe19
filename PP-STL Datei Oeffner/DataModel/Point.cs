using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class Point
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        public Point(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Point() { }


        // Points have the same HashCode if they're in the same coordinates
        public override int GetHashCode()
        {
            return X.GetHashCode() ^ Y.GetHashCode() ^ Z.GetHashCode();
        }

        //  two points would not be counted as two points if they're located in the same position
        // (two points with the same coordinates are equal one point)
        public override bool Equals(object obj)
        {
            var point = obj as Point;
            if (obj == null) return false;
            if (point.X == X && point.Y == Y && point.Z == Z)
            {
                return point.X == X && point.Y == Y && point.Z == Z;
            }

            return false;
        }

    }
}
