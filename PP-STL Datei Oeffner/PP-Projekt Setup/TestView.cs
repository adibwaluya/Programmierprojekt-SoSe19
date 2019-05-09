using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StlExport;
using ViewModel;
using StlImport;
using ErrorHandling;

namespace View
{
    class TestView: IStlExport, IViewModel, IStlImport, IErrorHandling
    {
    }
}