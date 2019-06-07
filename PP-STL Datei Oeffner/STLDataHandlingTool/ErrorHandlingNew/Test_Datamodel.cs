using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel;

namespace ErrorHandling
{
    public class Test_Datamodel
    {
        public void createDatamodel()
        {
            DataModel.DataModel dm = new DataModel.DataModel();
            dm.AddPoint(1, 2, 42);
            dm.AddPoint(2, 2, 2);
            dm.AddPoint(3, 2, 3);
            dm.AddEdge(0, 1);
            dm.AddEdge(1, 2);
            dm.AddEdge(2, 0);
            Normal normalVector = new Normal(1, 2, 3);
            dm.AddFace(0, 1, 2, normalVector);

            //das gehört nicht hier hin, sonder zu ErrorFinding:

            Face face1 = dm.faces.GetFace(0);
            List<Edge> listOfEdges = new List<Edge>();
            listOfEdges = face1.Edges;
            Edge currentEdge = listOfEdges[0];
            Console.WriteLine(currentEdge.P1.Z);

            int numberOfFaces;
            bool potentiallyFaulty;
            bool Faulty;

            for (int currentEdgeNumber = 0; dm.edges.GetEdge(currentEdgeNumber) != null; currentEdgeNumber++)
            {
                currentEdge = dm.edges.GetEdge(currentEdgeNumber);

                Console.WriteLine(currentEdge.P1.Z);
                numberOfFaces = currentEdgeNumber;      // Anzahl der angrenzenden Flächen wird gezählt (noch nicht im Datenmodell implementiert)

                if (numberOfFaces == 0)
                {
                    Console.WriteLine("Faulty");
                    //currentEdge is Faulty
                }
                else if (numberOfFaces == 1)
                {
                    Console.WriteLine("potentiallyFaulty");
                    //currentEdge is potentiallyFaulty
                }
                else
                {
                    Console.WriteLine("not Faulty");
                    //currentEdge is not Faulty
                }
            }


            Console.ReadLine();
        }
    }
}

