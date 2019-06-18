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
            Edge current_Edge;
            Point point1;
            Point point2;
            double currentVectorY;
            double currentVectorZ;
            List<VectorOfEdge> vectorList = new List<VectorOfEdge>();

            // Richtung der Edges, die potentiell falsch sind ermitteln und Edges mit gleicher Richtung in eine Liste einordnen
            for (int currentEdgeNumber = 0; dm.edges.GetEdge(currentEdgeNumber) != null; currentEdgeNumber++)
            {
                current_Edge = dm.edges.GetEdge(currentEdgeNumber);
                if (current_Edge.potentiallyFaulty)
                {
                    point1 = current_Edge.P1;
                    point2 = current_Edge.P2;
                    currentVectorY = Math.Abs(point1.Y - point2.Y) / Math.Abs(point1.X - point2.X);     // Die X Position ist immer 1 und wird daher nicht im Array angegeben
                    currentVectorZ = Math.Abs(point1.Z - point2.Z) / Math.Abs(point1.X - point2.X);

                    VectorOfEdge vectorOfEdge = new VectorOfEdge();
                    vectorList.Add(vectorOfEdge);
                    vectorOfEdge.addCoordinates(currentVectorY, currentVectorZ);
                    
                    // Edge, die in gleiche Richtung zeigt bereits vorhanden?
                    foreach (VectorOfEdge vector in vectorList)    // Wir laufen durch alle Eintragungen in der Liste durch
                    {
                        if (vectorOfEdge.edgeIDList[0] == currentVectorY && vectorOfEdge.edgeIDList[1] == currentVectorZ)   // Wenn ein Vektor mit der gleichen Richtung vorhanden ist
                        {
                            vectorOfEdge.edgeIDList.Add(currentEdgeNumber);
                            vectorList.RemoveAt(vectorList.Count - 1);
                            break;
                        }
                    }
                }
            }
            // Ring bilden
            Point startPoint;
            Point currentEndPoint;
            Edge currentEdge = dm.edges.GetEdge(0);

            foreach (VectorOfEdge vector in vectorList)
            {
                startPoint = dm.edges.GetEdge(vector.edgeIDList[0]).P1; //Startpunkt muss der Punkt der ersten Edge sein die noch nicht notFaulty ist
                currentEndPoint = dm.edges.GetEdge(vector.edgeIDList[0]).P1;
                while (true)
                {
                    foreach (int edgeID in vector.edgeIDList)
                    {
                        currentEdge = dm.edges.GetEdge(edgeID);
                        if (currentEndPoint == currentEdge.P1 && currentEdge.ring == false)
                        {
                            currentEdge.ring = true;
                            currentEndPoint = currentEdge.P2;
                            break;
                        }
                        else if (currentEndPoint == currentEdge.P2 && currentEdge.ring == false)
                        {
                            currentEdge.ring = true;
                            currentEndPoint = currentEdge.P1;
                            break;
                        }
                    }// was wenn keine weitere passende Edge gefunden wird?
                    if (currentEndPoint == startPoint)
                    {
                        markRingEdgesAsNotFaulty(dm);
                        break;
                    }
                    else if (currentEdge.ring==false)
                    {

                    }
                }
            }
        }

        private void markRingEdgesAsNotFaulty(DataStructure dm)
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

                edgeListLength = currentEdgeNumber + 1;

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