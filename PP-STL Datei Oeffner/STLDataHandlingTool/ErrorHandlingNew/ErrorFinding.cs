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

            //Face face1 = dm.faces.GetFace(0);
            //List<Edge> listOfEdges = new List<Edge>();
            //listOfEdges = face1.Edges;
            //Edge currentEdge = listOfEdges[0];
            //Console.WriteLine(currentEdge.P1.Z);
            //int FaceOfEdge = currentEdge.FaceIDs[0];
            //Console.WriteLine(FaceOfEdge);
            //Console.WriteLine(currentEdge.FaceIDs.Count);

            // Hier geht's los

            Edge currentEdge;
            int numberOfFaces;

            for (int currentEdgeNumber = 0; dm.edges.GetEdge(currentEdgeNumber) != null; currentEdgeNumber++)
            {
                currentEdge = dm.edges.GetEdge(currentEdgeNumber);

                numberOfFaces = currentEdge.FaceIDs.Count;      // Anzahl der angrenzenden Flächen wird gezählt

                if (numberOfFaces == 0)
                {
                    Console.WriteLine("Faulty");
                    currentEdge.faulty = true;
                }
                else if (numberOfFaces == 1)
                {
                    Console.WriteLine("potentiallyFaulty");
                    currentEdge.potentiallyFaulty = true;
                }
                else
                {
                    Console.WriteLine("not Faulty");
                    currentEdge.faulty = false;
                }
            }
        }
    }
}