using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class Normal : Point
    {
        public Normal(double x, double y, double z) : base(x, y, z) { }
        public Normal() : base() { }

        public static Normal FromPoint(Point p)
        {
            if (p == null)
                return null;

            return new Normal()
            {
                X = p.X,
                Y = p.Y,
                Z = p.Z,
            };
        }
    }
}
