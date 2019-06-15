using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel;

namespace ErrorHandling
{
    public class IdentifyBodies
    {
        private IList<int> NextFaces = new List<int>();

        public void FindBodies(DataStructure dm)
        {
            DefineBody(dm, 0, 1);
        }

        private void LabelFaces(Edge edge, DataStructure dm, int bodyID)
        {
            foreach (int faceNumber in edge.FaceIDs)
            {
                dm.faces.GetFace(faceNumber).bodyID = bodyID;
                NextFaces.Add(faceNumber);
            }
        }

        private void DefineBody(DataStructure dm, int startID, int bodyID)
        {
            Face currentFace;
            Edge firstEdge;
            Edge secondEdge;
            Edge thirdEdge;
            NextFaces.Add(startID);

            foreach (int currentFaceNumber in NextFaces)
            {
                currentFace = dm.faces.GetFace(currentFaceNumber);

                if (currentFace.bodyID != 0)
                {
                    continue;
                }

                firstEdge = dm.edges.GetEdge(currentFace.FirstEdge);
                secondEdge = dm.edges.GetEdge(currentFace.SecondEdge);
                thirdEdge = dm.edges.GetEdge(currentFace.ThirdEdge);

                LabelFaces(firstEdge, dm, bodyID);
                LabelFaces(secondEdge, dm, bodyID);
                LabelFaces(thirdEdge, dm, bodyID);

                NextFaces.Remove(currentFaceNumber);
            }

        }
    }
}
