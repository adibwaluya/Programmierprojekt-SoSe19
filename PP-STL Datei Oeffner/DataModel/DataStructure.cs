using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class DataStructure
    {
        public PointList points = new PointList(); //List of Points
        public EdgeList edges = new EdgeList(); //List of Edges
        public FaceList faces = new FaceList(); //List of Faces

        // Singleton --> MK: Implemented to have an access to FaceList, while developing my component. 
        private static DataStructure _dataStructure; 
        public static DataStructure DataStructureInstance 
        {
            get
            {
                if (_dataStructure != null) return _dataStructure;
                _dataStructure = new DataStructure(); 

                return _dataStructure;
            }
            set => _dataStructure = value; // Setter is only need for creating a test-datamodel
        }

        /* User adds Point by giving its coordinates */
        public int AddPoint(double x, double y, double z)
        {
            Point p = new Point(x, y, z);
            return points.AddOrGetPoint(p);
        }

        /* User adds Edge by giving its point IDs */
        public int AddEdge(int pt1, int pt2)
        {
            Edge e = new Edge(pt1, pt2, this);
            return edges.AddOrGetEdge(e);
        }

        /* User adds Face by giving its Edge IDs and Normal */
        public int AddFace(int e1, int e2, int e3, Normal n)
        {
            Face f = new Face(e1, e2, e3, n, this);
            
            // Adds FaceID to the list of every edge. Implemented by Maximilian
            edges.GetEdge(e1).FaceIDs.Add(faces.AddOrGetFace(f));
            edges.GetEdge(e2).FaceIDs.Add(faces.AddOrGetFace(f));
            edges.GetEdge(e3).FaceIDs.Add(faces.AddOrGetFace(f));

            return faces.AddOrGetFace(f);
        }

        /// <summary>
        /// Gets the maximum x-coordinate value from the datamodel.
        /// </summary>
        /// <returns>maxValueX</returns>
        public double GetMaxValueX()
        {
            var maxValueX = double.NegativeInfinity;

            for (var i = 0; i < points.m_int2pt.Count; i++)
            {
                if (points.GetPoint(i).X > maxValueX)
                {
                    maxValueX = points.GetPoint(i).X;
                }
            }
            return maxValueX;
        }

        /// <summary>
        /// Gets the maximum y-coordinate value from the datamodel.
        /// </summary>
        /// <returns>maxValueY</returns>
        public double GetMaxValueY()
        {
            var maxValueY = double.NegativeInfinity;

            for (var i = 0; i < points.m_int2pt.Count; i++)
            {
                if (points.GetPoint(i).Y > maxValueY)
                {
                    maxValueY = points.GetPoint(i).Y;
                }
            }
            return maxValueY;
        }

        /// <summary>
        /// Gets the maximum z-coordinate value from the datamodel.
        /// </summary>
        /// <returns>maxValueX</returns>
        public double GetMaxValueZ()
        {
            var maxValueZ = double.NegativeInfinity;

            for (var i = 0; i < points.m_int2pt.Count; i++)
            {
                if (points.GetPoint(i).Y > maxValueZ)
                {
                    maxValueZ = points.GetPoint(i).Y;
                }
            }
            return maxValueZ;
        }

        /// <summary>
        /// Gets the minimum x-coordinate value from the datamodel.
        /// </summary>
        /// <returns>minValueX</returns>
        public double GetMinValueX()
        {
            var minValueX = double.PositiveInfinity;

            for (var i = 0; i < points.m_int2pt.Count; i++)
            {
                if (points.GetPoint(i).X < minValueX)
                {
                    minValueX = points.GetPoint(i).X;
                }
            }
            return minValueX;
        }

        /// <summary>
        /// Gets the minimum y-coordinate value from the datamodel.
        /// </summary>
        /// <returns>minValueY</returns>
        public double GetMinValueY()
        {
            var minValueY = double.PositiveInfinity;

            for (var i = 0; i < points.m_int2pt.Count; i++)
            {
                if (points.GetPoint(i).Y < minValueY)
                {
                    minValueY = points.GetPoint(i).X;
                }
            }
            return minValueY;
        }

        /// <summary>
        /// Gets the minimum z-coordinate value from the datamodel.
        /// </summary>
        /// <returns>minValueZ</returns>
        public double GetMinValueZ()
        {
            var minValueZ = double.PositiveInfinity;

            for (var i = 0; i < points.m_int2pt.Count; i++)
            {
                if (points.GetPoint(i).Y < minValueZ)
                {
                    minValueZ = points.GetPoint(i).X;
                }
            }
            return minValueZ;
        }


        public delegate void DataStructureCreatedEventHandler(object sender, EventArgs e);

        public event DataStructureCreatedEventHandler DataStructureCreated;

        protected virtual void OnDataStructureCreated()
        {
            DataStructureCreated?.Invoke(this, EventArgs.Empty);
        }
    }
}

