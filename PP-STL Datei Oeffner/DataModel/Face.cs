using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    class Face
    {
        public Face(int firstEdge, int secondEdge, int thirdEdge)
        {
            FirstEdge = firstEdge;
            SecondEdge = secondEdge;
            ThirdEdge = thirdEdge;
        }

        public Face(Point p1, Point p2, Point p3, Normal nx, Normal ny, Normal nz)
        {
            P1 = p1;
            P2 = p2;
            P3 = p3;
            NX = nx;
            NY = ny;
            NZ = nz;
        }

        public int FirstEdge { get; set; }
        public int SecondEdge { get; set; }
        public int ThirdEdge { get; set; }
        public Point P1 { get; set; }
        public Point P2 { get; set; }
        public Point P3 { get; set; }
        public Normal NX { get; set; }
        public Normal NY { get; set; }
        public Normal NZ { get; set; }


    }
}
