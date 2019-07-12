/*******************************************************************************************
 * Copyright (c) <2019><Michael Kaip, Maximilian Mews, Michael Reno, Adib Ghassani Waluya> *
 *******************************************************************************************/

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
        /// FaceIDs List, Condition enum and cycle bool Implemented by Maximilian
        /// </summary>
        private bool _cycle;                            // important for advanced error finding
        public IList<int> FaceIDs = new List<int>();    // list of faceIDs of faces the edge belongs to

        public enum Condition { Faulty, PotentiallyFaulty, NotFaulty }; // condition of the edge
        private Condition _condition;

        public Condition CurrentCondition
        {
            get
            {
                return _condition;
            }
            set
            {
                _condition = value;
                if (value == Condition.Faulty || value == Condition.NotFaulty)
                {
                    _cycle = false;
                }
            }
        }

        public bool cycle
        {
            get
            {
                return _cycle;
            }
            set
            {
                _cycle = value;
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
