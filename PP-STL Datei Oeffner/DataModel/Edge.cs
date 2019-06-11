using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class Edge
    {
        public bool potentiallyFaulty;
        public bool faulty;
        public IList<int> FaceIDs = new List<int>();

        public Edge(int startPoint, int endPoint, DataStructure model)
        {
            StartPoint = startPoint;
            EndPoint = endPoint;
            this.model = model;
        }

        private DataStructure model;
        public int StartPoint { get; set; }
        public int EndPoint { get; set; }
        
        public Point P1
        {
            get
            {
                return model.points.GetPoint(StartPoint);
            }
        }
        public Point P2
        {
            get
            {
                return model.points.GetPoint(EndPoint);
            }
        }
       
    }
}
