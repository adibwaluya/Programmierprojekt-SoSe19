using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class Face
    {
        public Face(int firstEdge, int secondEdge, int thirdEdge, Normal n, DataStructure dm)
        {
            FirstEdge = firstEdge;
            SecondEdge = secondEdge;
            ThirdEdge = thirdEdge;
            N = n;
            Datamodel = dm;
        }

        public int FirstEdge { get; set; }
        public int SecondEdge { get; set; }
        public int ThirdEdge { get; set; }
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


    }
}
