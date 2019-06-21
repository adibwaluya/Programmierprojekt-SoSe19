using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ErrorHandlingDataModel;
using DataModel;
using ErrorHandling;


namespace ErrorHandlingTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Test_Datamodel test_datamodel1 = new Test_Datamodel();
            DataStructure dm = new DataStructure();
            test_datamodel1.FillDatamodel(dm);

            ErrorFinding errorFinding = new ErrorFinding();
            errorFinding.FindError(dm);

            IdentifyBodies identifyBodies = new IdentifyBodies();
            identifyBodies.FindBodies(dm);

            //Console.WriteLine(dm.faces.GetFace(0).bodyID);
            //Console.WriteLine(dm.faces.GetFace(1).bodyID);
            //Console.WriteLine(dm.faces.GetFace(2).bodyID);
            //Console.WriteLine(dm.faces.GetFace(3).bodyID);
            //Console.WriteLine(dm.faces.GetFace(4).bodyID);
            //Console.WriteLine(dm.faces.GetFace(5).bodyID);
            //Console.WriteLine(dm.faces.GetFace(6).bodyID);
            //Console.WriteLine(dm.faces.GetFace(7).bodyID);

            for (int i = 0; dm.edges.GetEdge(i) != null; i++)
            {
                Console.WriteLine(dm.edges.GetEdge(i).faulty);
            }

            Console.ReadLine();

        }
    }
}