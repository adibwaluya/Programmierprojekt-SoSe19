using DataModel;
using ErrorHandlingNew;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErrorHandling
{
    class Program
    {
        static void Main(string[] args)
        {
            Test_Datamodel datamodel1 = new Test_Datamodel();
            datamodel1.createDatamodel();
        }
    }
}
