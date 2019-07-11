using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class Face
    {
        // bodyID Implemented by Maximilian
        public int bodyID = 0;

        public Face(int firstEdge, int secondEdge, int thirdEdge, Normal n, DataStructure dm)
        {
            FirstEdge = firstEdge;
            SecondEdge = secondEdge;
            ThirdEdge = thirdEdge;
            N = n;
            Datamodel = dm;
        }

        public Face(Point firstPoint, Point secondPoint, Point thirdPoint, Normal n, DataStructure dm)
        {
            FirstPoint = firstPoint;
            SecondPoint = secondPoint;
            ThirdPoint = thirdPoint;
            N = n;
            Datamodel = dm;
        }

        public Point FirstPoint { get; set; }
        public Point SecondPoint { get; set; }
        public Point ThirdPoint { get; set; }

        public int FirstEdge { get; set; }
        public int SecondEdge { get; set; }
        public int ThirdEdge { get; set; }

        public int[] iEdges
        {
            get
            {
                Dictionary<Point, int> mm_pts = new Dictionary<Point, int>();
                foreach (var item in Edges)
                {
                    mm_pts[item.P1] = item.StartPoint;
                    mm_pts[item.P2] = item.EndPoint;
                }

                return mm_pts.Values.ToArray();
            }
        }

        public List<Edge> Edges
        {
            get
            {
                List<Edge> mm_ret = new List<Edge>();
                mm_ret.Add(Datamodel.edges.GetEdge(FirstEdge));
                mm_ret.Add(Datamodel.edges.GetEdge(SecondEdge));
                mm_ret.Add(Datamodel.edges.GetEdge(ThirdEdge));
                return mm_ret;
            }
        }

        public List<Point> Points
        {
            get
            {
                

                HashSet<Point> mm_set = new HashSet<Point>();
                foreach(var item in Edges)
                {
                    mm_set.Add(item.P1);
                    mm_set.Add(item.P2);
                }

                List<Point> mm_ret = new List<Point>();
                foreach (var pt in mm_set)
                {
                    mm_ret.Add(pt);
                }

                return mm_ret;
            }
        }
        
        public Normal N { get; set; }
        public DataStructure Datamodel { get; set; }

        // For STL Export
        public Normal NormFromFace(Face p)
        {
            return new Normal()
            {
                X = p.N.X,
                Y = p.N.Y,
                Z = p.N.Z,
            };
        }

    }
}
