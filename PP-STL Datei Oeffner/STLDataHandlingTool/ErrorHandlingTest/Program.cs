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
            DataModel.DataStructure dm = new DataModel.DataStructure();
            test_datamodel1.FillDatamodel(dm);

            ErrorFinding errorFinding = new ErrorFinding();
            errorFinding.findError(dm);

            Console.ReadLine();

        }
    }
}