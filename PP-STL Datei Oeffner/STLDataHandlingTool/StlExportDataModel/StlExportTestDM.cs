using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel;

namespace StlExportDataModel
{
    public class StlExportTestDM
    {
        public void FillDatamodel(DataStructure dm)
        {
            dm.AddPoint(11, 22, 33);
            dm.AddPoint(20, 10, 30);
            dm.AddPoint(1, 2, 3);

            dm.AddPoint(4, 6, 8);
            dm.AddPoint(11, 22, 33);
            dm.AddPoint(20, 10, 30);

            dm.AddPoint(1, 2, 3);
            dm.AddPoint(4, 6, 8);
            dm.AddPoint(11, 22, 33);

            dm.AddPoint(11, 22, 33);
            dm.AddPoint(20, 10, 30);
            dm.AddPoint(1, 2, 3);

            dm.AddPoint(4, 6, 8);
            dm.AddPoint(11, 22, 33);
            dm.AddPoint(20, 10, 30);

            dm.AddPoint(1, 2, 3);
            dm.AddPoint(4, 6, 8);
            dm.AddPoint(11, 22, 33);

            dm.AddEdge(0, 1);
            dm.AddEdge(1, 2);
            dm.AddEdge(2, 3);
            dm.AddEdge(3, 0);
            dm.AddEdge(3, 1);

            Normal norm1 = new Normal(0, 1, 1);
            Normal norm2 = new Normal(1, 0, 0);
            Normal norm3 = new Normal(1, 1, 1);
            Normal norm4 = new Normal(1, 1, 0);
            Normal norm5 = new Normal(1, 1, 1);
            Normal norm6 = new Normal(1, 0, 0);


            dm.AddFace(3, 1, 2, norm1);
            dm.AddFace(0, 1, 2, norm2);
            dm.AddFace(3, 1, 2, norm3);
            dm.AddFace(0, 1, 2, norm4);
            dm.AddFace(3, 1, 2, norm5);
            dm.AddFace(0, 1, 2, norm6);
        }
    }
}
