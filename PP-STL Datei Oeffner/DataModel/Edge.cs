using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    class Edge
    {
        public Edge(int startPoint, int endPoint)
        {

        }

        public Edge(Point p1, Point p2)
        {
            P1 = p1;
            P2 = p2;
        }

        public int StartPoint { get; set; }
        public int EndPoint { get; set; }
        public Point P1 { get; set; }
        public Point P2 { get; set; }
        public List<Point> Points { get; set; }
    }
}
