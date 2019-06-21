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
            Edge currentEdge;
            Point point1;
            Point point2;
            double currentVectorX;
            double currentVectorY;
            double currentVectorZ;
            List<VectorOfEdge> vectorList = new List<VectorOfEdge>();
            bool noObjectYet;

            // Richtung der Edges, die potentiell falsch sind ermitteln und Edges mit gleicher Richtung in eine Liste einordnen
            for (int currentEdgeNumber = 0; dm.edges.GetEdge(currentEdgeNumber) != null; currentEdgeNumber++)
            {
                currentEdge = dm.edges.GetEdge(currentEdgeNumber);
                if (currentEdge.potentiallyFaulty)
                {
                    noObjectYet = true;
                    point1 = currentEdge.P1;
                    point2 = currentEdge.P2;
                    currentVectorX = point1.X - point2.X;
                    currentVectorY = point1.Y - point2.Y;

                    if ((currentVectorX == 0) && (currentVectorY != 0))             // Sonderfall wenn Vektor paralell zur yz-Ebene liegt
                    {
                        currentVectorZ = (point1.Z - point2.Z) / (currentVectorY);

                    }
                    else if ((currentVectorX == 0) && (currentVectorY == 0))        // Sonderfall wenn Vektor paralell zur z-Achse liegt
                    {
                        currentVectorZ = 1;
                    }
                    else                                                            // Standardfall. currentVectorY und currentVectorZ geben an, wo der Wert auf der y und z-Achse bei x=1 liegt.
                    {
                        currentVectorY = currentVectorY / currentVectorX;
                        currentVectorZ = (point1.Z - point2.Z) / currentVectorX;
                    }

                    // Wenn bereits eine Edge mit dem gleichen Vektor gefunden wurde, wird die aktuelle Edge in der Liste hinzugefügt. Wenn nicht, wird ein neues Objekt mit den Vektoren erzeugt.
                    foreach (VectorOfEdge vector in vectorList)
                    {
                        if (vector.vectorY == currentVectorY && vector.vectorZ == currentVectorZ)
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
                        vectorOfEdge.edgeIDList.Add(currentEdgeNumber);
                    }
                }
            }
            // Ring bilden
            Point startPoint;
            Point currentEndPoint;
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
                else if (state == "notFaulty")
                {
                    currentEdge.faulty = false;
                }
            }
        }

        /// <summary>
        /// Einzellne Kanten werden als falsch erkannt. Kanten mit mehreren Flächen werden als korrekt erkannt. Kanten mit nur einer Fläche sind entweder falsch oder Sonderfälle (=potentiell falsch).
        /// </summary>
        /// <param name="dm">
        /// Instanz des Datenmodells
        /// </param>
        /// <returns>
        /// Anzahl der potentiell falschen Kanten
        /// </returns>
        private int SimpleErrorFinding(DataStructure dm)
        {
            Edge currentEdge;
            int numberOfFaces;
            int potentiallyFaultyCounter = 0;

            for (int currentEdgeNumber = 0; dm.edges.GetEdge(currentEdgeNumber) != null; currentEdgeNumber++)
            {
                currentEdge = dm.edges.GetEdge(currentEdgeNumber);
                numberOfFaces = currentEdge.FaceIDs.Count;          // Anzahl der angrenzenden Flächen

                if (numberOfFaces == 0)
                {
                    currentEdge.faulty = true;
                }
                else if (numberOfFaces == 1)
                {
                    currentEdge.potentiallyFaulty = true;
                    potentiallyFaultyCounter++;
                }
                else
                {
                    currentEdge.faulty = false;
                }
            }
            return potentiallyFaultyCounter;
        }
    }
}