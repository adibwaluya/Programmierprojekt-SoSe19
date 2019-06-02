using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class DataModel
    {
        public PointList points = new PointList();
        public EdgeList edges = new EdgeList();
        public FaceList faces = new FaceList();

        public int AddPoint(double x, double y, double z)
        {
            Point p = new Point(x, y, z);
            return points.AddOrGetPoint(p);
        }

        public int AddEdge(int pt1, int pt2)
        {
            Edge e = new Edge(pt1, pt2, this);
            return edges.AddOrGetEdge(e);
        }

        public int AddFace(int e1, int e2, int e3, Normal n)
        {
            Face f = new Face(e1, e2, e3, n, this);
            return faces.AddOrGetFace(f);
        }


    }
}

