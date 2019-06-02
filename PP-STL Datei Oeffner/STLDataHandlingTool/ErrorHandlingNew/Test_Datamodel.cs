using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel;

namespace ErrorHandlingNew
{
    class Test_Datamodel
    {
        public void createDatamodel()
        {
            DataModel.DataModel dm = new DataModel.DataModel();
            dm.AddPoint(1, 2, 42);
            dm.AddPoint(2, 2, 3);
            dm.AddPoint(3, 2, 3);
            dm.AddEdge(0, 1);
            dm.AddEdge(1, 2);
            dm.AddEdge(2, 0);
            Normal normalVector = new Normal();
            dm.AddFace(0, 1, 2, normalVector, dm);

            //das gehört nicht hier hin, sonder zu ErrorFinding:

            Face face1 = dm.faces.GetFace(0);
            List<Edge> listOfEdges = new List<Edge>();
            listOfEdges = face1.Edges;
            Edge edge1 = listOfEdges[0];
            Console.WriteLine(edge1.P1.Z);


            Console.ReadLine();
        }
    }
}

