using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel;

namespace ErrorHandlingDataModel
{
    public class Test_Datamodel
    {
        public void FillDatamodel(DataStructure dm)
        {
            dm.AddPoint(1, 2, 42);
            dm.AddPoint(2, 2, 2);
            dm.AddPoint(3, 2, 3);
            dm.AddPoint(9, 8, 7);
            dm.AddEdge(0, 1);
            dm.AddEdge(1, 2);
            dm.AddEdge(2, 0);
            dm.AddEdge(3, 0);
            dm.AddEdge(3, 1);
            //Normal normalVector = new Normal(1, 2, 3);
            dm.AddFace(3, 1, 2, new Normal(1, 2, 3));
            dm.AddFace(0, 1, 2, new Normal(1, 2, 3));
        }
    }
}