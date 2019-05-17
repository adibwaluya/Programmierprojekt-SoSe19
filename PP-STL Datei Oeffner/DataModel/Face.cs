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

        public int FirstEdge { get; set; }
        public int SecondEdge { get; set; }
        public int ThirdEdge { get; set; }

        
    }
}
