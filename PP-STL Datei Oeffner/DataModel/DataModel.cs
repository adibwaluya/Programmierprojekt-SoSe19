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

        //für Konstruktor Datenmodell benötigt
        public Normal n1;
        public Normal n2;
        public Normal n3;
        public Face f;

        public DataModel()
        {
            pointIds = new Dictionary<int, Point>();

            /* 3 Punkt sind vom Dictionary genommen */
            pointIds[0] = new Point();
            pointIds[1] = new Point();
            pointIds[2] = new Point();
            n1 = new Normal();
            n2 = new Normal();
            n3 = new Normal();


            edgeIds = new Dictionary<int, Edge>();

            /* 3 Kanten vom Dictionary genommen, die 2 Punkte enthalten */
            edgeIds[0] = new Edge(pointIds[0], pointIds[1]);
            edgeIds[1] = new Edge(pointIds[0], pointIds[2]);
            edgeIds[2] = new Edge(pointIds[1], pointIds[2]);

            //PointIDs vom Dictionary genommen
            PointFromDictKeys(0, pointIds);
            PointFromDictKeys(1, pointIds);
            PointFromDictKeys(2, pointIds);

            //EdgeIDs vom Dictionary genommen
            EdgeFromDictKeys(0, edgeIds);
            EdgeFromDictKeys(1, edgeIds);
            EdgeFromDictKeys(2, edgeIds);

            //Eine Fläche, die aus 3 Punkten und Normalen besteht
            f = new Face(pointIds[0], pointIds[1], pointIds[2], n1, n2, n3);
        }

        private void PointFromDictKeys(int i, Dictionary<int, Point> dx)
        {
            string from = i + ": ";
            foreach (int ix in dx.Keys)
                from += ix + " ";
            Console.WriteLine(from);

        }
        private void EdgeFromDictKeys(int i, Dictionary<int, Edge> dx)
        {
            string from = i + ": ";
            foreach (int ix in dx.Keys)
                from += ix + " ";
            Console.WriteLine(from);

        }

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
