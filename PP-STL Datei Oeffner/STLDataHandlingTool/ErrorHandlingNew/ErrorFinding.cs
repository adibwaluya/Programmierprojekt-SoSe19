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
        public int edgeListLength;
        // Test

        //Face face1 = dm.faces.GetFace(0);
        //List<Edge> listOfEdges = new List<Edge>();
        //listOfEdges = face1.Edges;
        //Edge currentEdge = listOfEdges[0];
        //Console.WriteLine(currentEdge.P1.Z);
        //int FaceOfEdge = currentEdge.FaceIDs[0];
        //Console.WriteLine(FaceOfEdge);
        //Console.WriteLine(currentEdge.FaceIDs.Count);


        public void FindError(DataStructure dm)
        {
            if (simpleErrorFinding(dm) > 2)
            {
                advancedErrorFinding(dm);
            }
            markPotentiallyFaultyEdgesAsFaulty(dm);
        }

        private void markPotentiallyFaultyEdgesAsFaulty(DataStructure dm)
        {
            for (int currentEdgeNumber = 0; dm.edges.GetEdge(currentEdgeNumber) != null; currentEdgeNumber++)
            {
                if (dm.edges.GetEdge(currentEdgeNumber).potentiallyFaulty)
                {
                    dm.edges.GetEdge(currentEdgeNumber).faulty = true;
                    dm.edges.GetEdge(currentEdgeNumber).potentiallyFaulty = false;
                }
            }
        }

        private void advancedErrorFinding(DataStructure dm)
        {
            Edge currentEdge;
            Point point1;
            Point point2;
            double[,] vectorArray = new double[3, edgeListLength];
            int number = 0;
            double currentVectorY;
            double currentVectorZ;

            for (int currentEdgeNumber = 0; dm.edges.GetEdge(currentEdgeNumber) != null; currentEdgeNumber++)
            {
                currentEdge = dm.edges.GetEdge(currentEdgeNumber);
                if (currentEdge.potentiallyFaulty)
                {
                    point1 = currentEdge.P1;
                    point2 = currentEdge.P2;
                    vectorArray[0, number] = currentEdgeNumber+1;
                    vectorArray[1, number] = Math.Abs(point1.Y - point2.Y) / Math.Abs(point1.X - point2.X); // Die X Position ist immer 1 und wird daher nicht im Array angegeben
                    vectorArray[2, number] = Math.Abs(point1.Z - point2.Z) / Math.Abs(point1.X - point2.X);
                    number++;
                }
            }
            for (int currentNumber = 0; vectorArray[0, currentNumber] != 0; currentNumber++)
            {
                currentVectorY = vectorArray[1, currentNumber];
                currentVectorZ = vectorArray[2, currentNumber];
                for (int currentNumber2 = 0; currentNumber2 < edgeListLength; currentNumber2++)
                {
                    //currentVectorY == vectorArray[1, currentNumber2];
                    //currentVectorZ == vectorArray[2, currentNumber2];
                }
            }
        }

        private void makeVectorsFromPotentiallyFaultyEdges()
        {
            throw new NotImplementedException();
        }

        private int simpleErrorFinding(DataStructure dm)
        {
            Edge currentEdge;
            int numberOfFaces;
            int potentiallyFaultyCounter = 0;

            for (int currentEdgeNumber = 0; dm.edges.GetEdge(currentEdgeNumber) != null; currentEdgeNumber++)
            {
                currentEdge = dm.edges.GetEdge(currentEdgeNumber);

                numberOfFaces = currentEdge.FaceIDs.Count;      // Anzahl der angrenzenden Flächen wird gezählt

                edgeListLength = currentEdgeNumber+1;

                if (numberOfFaces == 0)
                {
                    Console.WriteLine("Faulty");
                    currentEdge.faulty = true;
                }
                else if (numberOfFaces == 1)
                {
                    Console.WriteLine("potentiallyFaulty");
                    currentEdge.potentiallyFaulty = true;
                    potentiallyFaultyCounter++;
                }
                else
                {
                    Console.WriteLine("not Faulty");
                    currentEdge.faulty = false;
                }
            }
            
            return potentiallyFaultyCounter;
        }
    }
}