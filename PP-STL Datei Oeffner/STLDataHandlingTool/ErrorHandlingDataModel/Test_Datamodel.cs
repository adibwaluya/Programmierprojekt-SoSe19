﻿using System;
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
            dm.AddPoint(0, 0, 0);
            dm.AddPoint(0, 4, 0);
            dm.AddPoint(4, 4, 0);
            dm.AddPoint(4, 0, 4);
            dm.AddPoint(2, 2, 0);
            dm.AddPoint(2, 2, 3);

            dm.AddEdge(0, 1);//0
            dm.AddEdge(1, 2);
            dm.AddEdge(2, 3);
            dm.AddEdge(3, 0);//3

            dm.AddEdge(0, 4);//4
            dm.AddEdge(2, 4);
            dm.AddEdge(1, 4);
            dm.AddEdge(0, 2);//7

            dm.AddEdge(0, 5);//8
            dm.AddEdge(1, 5);
            dm.AddEdge(2, 5);
            dm.AddEdge(3, 5);//11

            Console.WriteLine(dm.edges.GetEdge(0).FaceIDs.Count());
            dm.AddFace(0, 4, 6, new Normal(0, 1, 4));
            Console.WriteLine(dm.edges.GetEdge(0).FaceIDs.Count());
            dm.AddFace(1, 5, 6, new Normal(1, 2, 4));
            Console.WriteLine(dm.edges.GetEdge(0).FaceIDs.Count());
            dm.AddFace(2, 3, 7, new Normal(0, 2, 3));
            Console.WriteLine(dm.edges.GetEdge(0).FaceIDs.Count());

            dm.AddFace(0, 8, 9, new Normal(0, 1, 5));
            Console.WriteLine(dm.edges.GetEdge(0).FaceIDs.Count());
            dm.AddFace(1, 9, 10, new Normal(1, 2, 5));
            Console.WriteLine(dm.edges.GetEdge(0).FaceIDs.Count());
            dm.AddFace(2, 10, 11, new Normal(2, 3, 5));
            Console.WriteLine(dm.edges.GetEdge(0).FaceIDs.Count());
            dm.AddFace(3, 11, 8, new Normal(3, 0, 5));
            Console.WriteLine(dm.edges.GetEdge(0).FaceIDs.Count());
        }
    }
}