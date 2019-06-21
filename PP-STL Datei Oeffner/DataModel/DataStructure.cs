using System.Collections.Generic;
using System.Windows.Media.Media3D;

namespace DataModel
{
    public class DataStructure
    {
        public PointList points = new PointList(); //List of Points
        public EdgeList edges = new EdgeList(); //List of Edges
        public FaceList faces = new FaceList(); //List of Faces

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

        public static int VerticesCount; // (modelDataPoints (vertices + normals) ) - (number of normals)

        /// <summary>
        /// Used to get the the data points and normals, which are stored in the DataStructure.
        /// </summary>
        /// <returns>A List of Point3D (double X, double Y, double Z). </returns>
        public  List<Point3D> GetDataPointsList3D()
        {
            var modelDataPoints = new List<Point3D>();

            for(var i = 0; i < faces.m_int2Face.Count; i++)
            {
                for (var j = 0; j < 3; j++)
                {
                    modelDataPoints.Add(new Point3D(faces.m_int2Face[i].Points[j].X, faces.m_int2Face[i].Points[j].Y, faces.m_int2Face[i].Points[j].Z)); // Adding the vertices
                }
                modelDataPoints.Add(new Point3D(faces.m_int2Face[i].N.X, faces.m_int2Face[i].N.Y, faces.m_int2Face[i].N.Z)); // Adding the normal vector
            }

            VerticesCount = modelDataPoints.Count - (modelDataPoints.Count / 4);

            return modelDataPoints;
        }
    }
}

