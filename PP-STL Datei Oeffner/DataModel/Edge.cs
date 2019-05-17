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

        public int StartPoint { get; set; }
        public int EndPoint { get; set; }
        public List<Point> Points { get; set; }
    }
}
