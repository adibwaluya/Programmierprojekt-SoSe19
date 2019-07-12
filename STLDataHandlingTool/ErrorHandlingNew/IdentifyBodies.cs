/*******************************************************************************************
 * Copyright (c) <2019><Michael Kaip, Maximilian Mews, Michael Reno, Adib Ghassani Waluya> *
 *******************************************************************************************/
using System.Collections.Generic;
using DataModel;

namespace ErrorHandling
{
    /// <summary>
    /// Gibt jeder Face eine Nummer. Jede Nummer repräsentiert die Zugehörigkeit zu einem Körper. Ein Körper ist eine zusammenhängende Struktur aus Flächen.
    /// </summary>
    public class IdentifyBodies
    {
        private IList<int> NextFaces = new List<int>();
        
        public void FindBodies(DataStructure dm)    // Geht alle Flächen durch. Gehört die aktuelle Fläche noch nicht zu einem Körper wird "DefineBody" verwendet.
        {
            int bodyNumber = 1;

            for (int currentFaceNumber = 0; dm.faces.GetFace(currentFaceNumber) != null; currentFaceNumber++)
            {
                if (dm.faces.GetFace(currentFaceNumber).bodyID == 0)
                {
                    DefineBody(dm, currentFaceNumber, bodyNumber);
                    bodyNumber++;
                }
            }
        }

        private void AddNeighborFacesToNextFaces(Edge edge)    // Fläche wird Nummer von Körper zugeordnet und angrenzende Flächen werden in die Liste eingefügt
        {
            foreach (int faceNumber in edge.FaceIDs)
            {
                NextFaces.Add(faceNumber);
            }
        }

        private void DefineBody(DataStructure dm, int startID, int bodyID)  // Geht von einer Fläche aus alle angrenzenden Flächen durch und ordnet sie einem Körper zu.
        {
            Face currentFace;
            NextFaces.Add(startID); // Anfangsfläche wird in Liste eingefügt

            for (int i = 0; i < NextFaces.Count; i++)
            {
                currentFace = dm.faces.GetFace(NextFaces[i]);

                if (currentFace.bodyID == 0)    // Wenn die aktuelle Fläche bereits markiert wurde wird zur nächsten Fläche gegangen
                {
                    AddNeighborFacesToNextFaces(dm.edges.GetEdge(currentFace.FirstEdge));
                    AddNeighborFacesToNextFaces(dm.edges.GetEdge(currentFace.SecondEdge));
                    AddNeighborFacesToNextFaces(dm.edges.GetEdge(currentFace.ThirdEdge));
                    currentFace.bodyID = bodyID;
                }
            }
            NextFaces.Clear();
        }
    }
}
