/*******************************************************************************************
 * Copyright (c) <2019><Michael Kaip, Maximilian Mews, Michael Reno, Adib Ghassani Waluya> *
 *******************************************************************************************/

using System;
using System.Collections.Generic;
using System.Windows.Media.Media3D;

namespace DataModel
{
    public class DataStructure
    {
        public PointList points = new PointList(); //List of Points
        public EdgeList edges = new EdgeList(); //List of Edges
        public FaceList faces = new FaceList(); //List of Faces

        public List<uint> FaultyEdges = new List<uint>(); // List of faulty edges implemented by Maximilian Mews. List will be handed over to OpenGlUserControl for performance reasons

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
    }
}

