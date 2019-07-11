using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel;

namespace ErrorHandling
{
    public class ErrorFinding
    {
        private double tolerance = 0.1;
        public double Tolerance
        {
            get { return tolerance; }
            set { tolerance = value; }
        }
        public void FindError(DataStructure dm)
        {
            if (SimpleErrorFinding(dm) > 2)
            {
                AdvancedErrorFinding(dm);
            }
            else
            {
                // Restliche potentiell falsche Kanten werden als falsch markiert
                for (int currentEdgeNumber = 0; dm.edges.GetEdge(currentEdgeNumber) != null; currentEdgeNumber++)
                {
                    if (dm.edges.GetEdge(currentEdgeNumber).CurrentCondition == Edge.Condition.PotentiallyFaulty)
                    {
                        dm.edges.GetEdge(currentEdgeNumber).CurrentCondition = Edge.Condition.Faulty;
                        dm.FaultyEdges.Add(currentEdgeNumber);
                    }
                }
            }
        }
        /// <summary>
        /// Für jede potentiell fehlerhafte Kante wird festgestellt, ob sie fehlerhaft oder korrekt ist.
        /// </summary>
        /// <param name="dm">
        /// Instanz des Datenmodells
        /// </param>
        private void AdvancedErrorFinding(DataStructure dm)
        {
            List<VectorOfEdge> vectorList = new List<VectorOfEdge>();

            // Richtung der Edges, die potentiell falsch sind wird ermittelt und Edges mit gleicher Richtung in eine Liste eingeordnet
            FormVectorsOfEdges(dm, vectorList);

            // Es wird ein eindimensionaler, zyklischer Pfad aus parallelen Vektoren gebildet.
            FormCycle(dm, vectorList);
        }

        private void FormVectorsOfEdges(DataStructure dm, List<VectorOfEdge> vectorList)
        {
            Edge currentEdge;
            Point point1;
            Point point2;
            double currentVectorX;
            double currentVectorY;
            double currentVectorZ;
            bool noObjectYet;

            for (int currentEdgeNumber = 0; dm.edges.GetEdge(currentEdgeNumber) != null; currentEdgeNumber++)
            {
                currentEdge = dm.edges.GetEdge(currentEdgeNumber);
                if (currentEdge.CurrentCondition == Edge.Condition.PotentiallyFaulty)
                {
                    noObjectYet = true;
                    point1 = currentEdge.P1;
                    point2 = currentEdge.P2;
                    currentVectorX = point1.X - point2.X;
                    currentVectorY = point1.Y - point2.Y;

                    if (currentVectorX == 0 && currentVectorY != 0)             // Sonderfall, wenn Vektor paralell zur yz-Ebene liegt
                    {
                        currentVectorZ = (point1.Z - point2.Z) / (currentVectorY);

                    }
                    else if (currentVectorX == 0 && currentVectorY == 0)        // Sonderfall, wenn Vektor paralell zur z-Achse liegt
                    {
                        currentVectorZ = 1;
                    }
                    else                                                        // Standardfall. currentVectorY und currentVectorZ geben an, wo der Wert auf der y und z-Achse bei x=1 liegt.
                    {
                        currentVectorY = currentVectorY / currentVectorX;
                        currentVectorZ = (point1.Z - point2.Z) / currentVectorX;
                    }

                    // Wenn bereits eine Edge mit dem gleichen Vektor gefunden wurde, wird die aktuelle Edge in der Liste hinzugefügt. Wenn nicht, wird ein neues Objekt mit den Vektoren erzeugt.
                    foreach (VectorOfEdge vector in vectorList)
                    {
                        if (ApproximatelyEqual(vector.vectorY, vector.vectorZ, currentVectorY, currentVectorZ))
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

        }

        private void FormCycle(DataStructure dm, List<VectorOfEdge> vectorList)
        {
            Edge currentEdge;
            Point startPoint;
            Point currentEndPoint;
            bool noPotentiallyFaultyEdgesLeft;
            bool newStartPointNeeded = true;
            bool foundMatchingEdge = false;

            foreach (VectorOfEdge vector in vectorList)
            {
                // falls die nächste foreach Schleife nie durchlaufen wird (was eigentlich nicht passieren kann) warum bauche ich die nächsten drei Zeilen pls halp
                startPoint = dm.edges.GetEdge(vector.edgeIDList[0]).P1;
                currentEndPoint = dm.edges.GetEdge(vector.edgeIDList[0]).P2;
                //
                noPotentiallyFaultyEdgesLeft = false;

                while (!noPotentiallyFaultyEdgesLeft)       // Am Ende werden alle potentiell falschen Kanten als richtig oder falsch eingeordnet sein.
                {
                    foundMatchingEdge = false;
                    noPotentiallyFaultyEdgesLeft = true;
                    foreach (int edgeID in vector.edgeIDList)
                    {
                        currentEdge = dm.edges.GetEdge(edgeID);

                        if (currentEdge.CurrentCondition == Edge.Condition.PotentiallyFaulty)
                        {
                            noPotentiallyFaultyEdgesLeft = false;

                            if (newStartPointNeeded)
                            {
                                startPoint = currentEdge.P1;
                                currentEndPoint = currentEdge.P2;
                                currentEdge.cycle = true;
                                newStartPointNeeded = false;
                                foundMatchingEdge = true;
                                break;
                            }
                        }
                        else
                        {
                            continue;
                        }

                        // Wenn die momentane Kante an den Endpunkt passt und noch nicht im Pfad ist wird sie hinzugefügt und der andere Punkt der Kante stellt den neuen Endpunkt dar.
                        if (currentEndPoint == currentEdge.P1 && currentEdge.cycle == false)
                        {
                            currentEdge.cycle = true;
                            currentEndPoint = currentEdge.P2;
                            foundMatchingEdge = true;
                            break;
                        }
                        else if (currentEndPoint == currentEdge.P2 && currentEdge.cycle == false)
                        {
                            currentEdge.cycle = true;
                            currentEndPoint = currentEdge.P1;
                            foundMatchingEdge = true;
                            break;
                        }
                    }
                    // Wenn der Pfad geschlossen werden konnte, werden die Kanten des Pfades als nicht fehlerhaft markiert.
                    if (currentEndPoint == startPoint && !noPotentiallyFaultyEdgesLeft)
                    {
                        SetCycleEdges(dm, vector, Edge.Condition.NotFaulty);
                        newStartPointNeeded = true;
                    }
                    // Wenn hingegen keine weitere passende Kante gefunden werden konnte, werden die Kanten des Pfades als fehlerhaft markiert.
                    else if (!foundMatchingEdge && !noPotentiallyFaultyEdgesLeft)
                    {
                        SetCycleEdges(dm, vector, Edge.Condition.Faulty);
                        newStartPointNeeded = true;
                    }
                }
            }
        }

        private bool ApproximatelyEqual(double y1, double z1, double y2, double z2)
        {
            tolerance = (y1 + z1) / 100;
            if (Math.Abs(y1-y2)<=tolerance && Math.Abs(z1 - z2) <= tolerance)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void SetCycleEdges(DataStructure dm, VectorOfEdge vec, Edge.Condition condition)
        {
            foreach (int edgeID in vec.edgeIDList)
            {
                if (dm.edges.GetEdge(edgeID).cycle)
                {
                    dm.edges.GetEdge(edgeID).CurrentCondition = condition;
                    if (condition == Edge.Condition.Faulty)
                    {
                        dm.FaultyEdges.Add(edgeID);
                    }
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
            //
            for (int currentEdgeNumber = 0; dm.edges.GetEdge(currentEdgeNumber) != null; currentEdgeNumber++)
            {
                currentEdge = dm.edges.GetEdge(currentEdgeNumber);
                numberOfFaces = currentEdge.FaceIDs.Count;          // Anzahl der angrenzenden Flächen

                if (numberOfFaces == 0)
                {
                    currentEdge.CurrentCondition = Edge.Condition.Faulty;
                    dm.FaultyEdges.Add(currentEdgeNumber);
                }
                else if (numberOfFaces == 1)
                {
                    currentEdge.CurrentCondition = Edge.Condition.PotentiallyFaulty;
                    potentiallyFaultyCounter++;
                }
                else
                {
                    currentEdge.CurrentCondition = Edge.Condition.NotFaulty;
                }
            }
            return potentiallyFaultyCounter;
        }
    }
}