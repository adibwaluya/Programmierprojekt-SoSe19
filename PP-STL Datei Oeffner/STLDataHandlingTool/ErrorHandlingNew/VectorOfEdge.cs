using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErrorHandling
{
    public class VectorOfEdge
    {
        double currentVectorY;
        double currentVectorZ;
        public List<int> edgeIDList = new List<int>();
        public void addCoordinates(double _currentVectorY, double _currentVectorZ)
        {
            currentVectorY = _currentVectorY;
            currentVectorZ = _currentVectorZ;
        }
    }
}
