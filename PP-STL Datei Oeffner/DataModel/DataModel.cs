using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    class DataModel
    {
        //ID registrieren
        //von der ID zum Punkt
        Dictionary<int, Point> pointIds;
        Dictionary<int, Edge> edgeIds;
        Dictionary<int, Face> faceIds;

        //vom Punkt zur ID
        Dictionary<Point, int> pointIds2;
        Dictionary<Edge, int> edgeIds2;
        Dictionary<Face, int> faceIds2;

        public List<Point> PointList
        {
            get
            {
                return pointIds.Values.ToList();
            }
        }

        public List<Edge> EdgeList
        {
            get
            {
                return edgeIds.Values.ToList();
            }
        }

        public List<Face> FaceList
        {
            get
            {
                return faceIds.Values.ToList();
            }
        }

        public int AddPoint(double x, double y, double z)
        {
            Point p = new Point(x, y, z);
            return AddPoint(p);
        }

        public int AddPoint(Point p)
        {
            if (pointIds2.ContainsKey(p))
            {
                return pointIds2[p];
            }
            int key = pointIds2.Count;
            pointIds2[p] = key;
            pointIds[key] = p;
            return key;
        }

        public int AddEdge(Edge p)
        {
            if (edgeIds2.ContainsKey(p))
            {
                return edgeIds2[p];
            }
            int key = edgeIds2.Count;
            edgeIds2[p] = key;
            edgeIds[key] = p;
            return key;
        }

        public int AddFace(Face p)
        {
            if (faceIds2.ContainsKey(p))
            {
                return faceIds2[p];
            }
            int key = faceIds2.Count;
            faceIds2[p] = key;
            faceIds[key] = p;
            return key;
        }
    }
}
