using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using StlExport;
using ViewModel;
using StlImport;
using ErrorHandling;

namespace View
{
    class Program : IStlExport, IViewModel, IStlImport, IErrorHandling
    {
        static void Main(string[] args)
        {
            userControl();
            findError();
            repair();
            openFile();
            saveFile();

        }
    }
}
