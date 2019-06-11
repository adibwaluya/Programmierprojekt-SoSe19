using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel;
using ErrorHandlingDataModel;

namespace ErrorHandling
{
    public class ErrorFinding
    {

        public void findError(DataStructure dm)
        {
            // Test

            Face face1 = dm.faces.GetFace(0);
            List<Edge> listOfEdges = new List<Edge>();
            listOfEdges = face1.Edges;
            Edge currentEdge = listOfEdges[0];
            Console.WriteLine(currentEdge.P1.Z);


            // Hier geht's los

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
        }


    }
}