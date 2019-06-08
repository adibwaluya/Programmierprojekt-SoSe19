using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel;
using ErrorHandlingTest;

namespace ErrorHandling
{
    class ErrorFinding
    {
        //Wie kann ich hier auf die Instanz vom Datamodel zugreifen, die in der Klasse ErrorHandling erzeugt wird?

        public void findError(DataModel.DataModel dm1)
        {
            Face face1 = dm1.faces.GetFace(0);
            List<Edge> listOfEdges = new List<Edge>();
            listOfEdges = face1.Edges;
            Edge currentEdge = listOfEdges[0];
            Console.WriteLine(currentEdge.P1.Z);
        }


    }
}