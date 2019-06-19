﻿using System;
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
            if (SimpleErrorFinding(dm) > 2)
            {
                AdvancedErrorFinding(dm);
            }
            MarkPotentiallyFaultyEdgesAsFaulty(dm);
        }

        private void MarkPotentiallyFaultyEdgesAsFaulty(DataStructure dm)
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

        private void AdvancedErrorFinding(DataStructure dm)
        {
            Edge current_Edge;
            Point point1;
            Point point2;
            double currentVectorY;
            double currentVectorZ;
            List<VectorOfEdge> vectorList = new List<VectorOfEdge>();
            bool noObjectYet;

            // Richtung der Edges, die potentiell falsch sind ermitteln und Edges mit gleicher Richtung in eine Liste einordnen
            for (int currentEdgeNumber = 0; dm.edges.GetEdge(currentEdgeNumber) != null; currentEdgeNumber++)
            {
                current_Edge = dm.edges.GetEdge(currentEdgeNumber);
                if (current_Edge.potentiallyFaulty)
                {
                    noObjectYet = true;
                    point1 = current_Edge.P1;
                    point2 = current_Edge.P2;
                    currentVectorY = Math.Abs(point1.Y - point2.Y) / Math.Abs(point1.X - point2.X);     // Die X Position ist immer 1 und wird daher nicht im Array angegeben
                    currentVectorZ = Math.Abs(point1.Z - point2.Z) / Math.Abs(point1.X - point2.X);

                   
                    // Edge, die in gleiche Richtung zeigt bereits vorhanden?
                    foreach (VectorOfEdge vector in vectorList)    // Wir laufen durch alle Eintragungen in der Liste durch
                    {
                        if (vector.vectorY == currentVectorY && vector.vectorZ == currentVectorZ)   // Wenn ein Vektor mit der gleichen Richtung vorhanden ist
                        {
                            vector.edgeIDList.Add(currentEdgeNumber);
                            noObjectYet = false;
                            break;
                        }
                    }
                    if (noObjectYet)
                    {
                        VectorOfEdge vectorOfEdge = new VectorOfEdge();
                        vectorList.Add(vectorOfEdge);
                        vectorOfEdge.addCoordinates(currentVectorY, currentVectorZ);
                    }
                }
            }
            // Ring bilden
            Point startPoint;
            Point currentEndPoint;
            Edge currentEdge;
            bool noPotentiallyFaultyEdgesLeft = false;
            bool newStartPoint = true;

            foreach (VectorOfEdge vector in vectorList)
            {
                // falls die nächste foreach Schleife nie durchlaufen wird (was eigentlich nicht passieren kann)
                startPoint = dm.edges.GetEdge(vector.edgeIDList[0]).P1;
                currentEndPoint = dm.edges.GetEdge(vector.edgeIDList[0]).P2;
                currentEdge = dm.edges.GetEdge(0);
                //
                while (!noPotentiallyFaultyEdgesLeft)
                {
                    noPotentiallyFaultyEdgesLeft = true;
                    foreach (int edgeID in vector.edgeIDList)
                    {
                        currentEdge = dm.edges.GetEdge(edgeID);

                        if (currentEdge.potentiallyFaulty)
                        {
                            if (newStartPoint)
                            {
                                startPoint = currentEdge.P1;
                                currentEndPoint = currentEdge.P2;
                                newStartPoint = false;
                            }
                            noPotentiallyFaultyEdgesLeft = false;
                        }

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
                    }
                    if (currentEndPoint == startPoint)
                    {
                        MarkRingEdges(dm, "notFaulty", vector.edgeIDList);
                        newStartPoint = true;
                    }
                    // was, wenn keine weitere passende Edge gefunden wird?
                    else if (currentEdge.ring == false)
                    {
                        MarkRingEdges(dm, "faulty", vector.edgeIDList);
                        newStartPoint = true;
                    }
                }
                Console.WriteLine();
            }
        }

        private void MarkRingEdges(DataStructure dm, string state, List<int> list)
        {
            Edge currentEdge;

            foreach (int edgeID in list)
            {
                currentEdge = dm.edges.GetEdge(edgeID);
                currentEdge.ring = false;
                currentEdge.potentiallyFaulty = false;
                if (state == "faulty")
                {
                    currentEdge.faulty = true;
                }
                else if(state == "notFaulty")
                {
                    currentEdge.faulty = false;
                }
            }
        }

        private int SimpleErrorFinding(DataStructure dm)
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