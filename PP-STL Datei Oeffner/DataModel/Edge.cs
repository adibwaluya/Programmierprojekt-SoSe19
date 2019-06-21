using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class Edge
    {
        /// <summary>
        /// potentiallyFaulty, faulty and FaceIDs and ring. Implemented by Maximilian
        /// </summary>
        private bool _ring;
        private bool _potentiallyFaulty;
        private bool _faulty;
        public IList<int> FaceIDs = new List<int>();

        public bool ring
        {
            get
            {
                return _ring;
            }
            set
            {
                _ring = value;
            }
        }
        public bool potentiallyFaulty
        {
            get
            {
                return _potentiallyFaulty;
            }
            set
            {
                _potentiallyFaulty = value;
                if (value)
                {
                    _faulty = false;
                }
            }
        }
        public bool faulty
        {
            get
            {
                return _faulty;
            }
            set
            {
                _faulty = value;
                _potentiallyFaulty = false;
                _ring = false;
            }
        }


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

        // Doesn't work yet. Still need improvement
        public override int GetHashCode()
        {

            return StartPoint.GetHashCode() ^ EndPoint.GetHashCode() ^ model.GetHashCode();
        }


        // Two edges would not be counted as two edges if they both have similar point IDs
        // Two edges with two almost similar pointIDs (e.g. 1,2 and 2,1) are equal
        public override bool Equals(object obj)
        {
            var edge = obj as Edge;
            if (obj == null) return false;
            if (edge.StartPoint == StartPoint ^ edge.StartPoint == EndPoint && edge.EndPoint == EndPoint ^ edge.EndPoint == StartPoint)
            {
                return edge.StartPoint == StartPoint ^ edge.StartPoint == EndPoint && edge.EndPoint == EndPoint ^ edge.EndPoint == StartPoint;
            }


            return false;
        }

    }
}
