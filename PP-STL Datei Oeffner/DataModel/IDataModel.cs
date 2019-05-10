using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    interface IDataModel
    {
        Object getPoints();
        Object getEdges();
        Object getSurfaces();
        Object getBodies();

        Object setPoint();
        Object setEdge();
        Object setSurface();

        Object addPoint();
        Object addEdge();
        Object addSurface();
    }
}
