using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StlExport;
using ViewModel;
using StlImport;
using ErrorHandling;
using DataModel;


namespace View
{
    class TestView: IStlExport, IViewModel, IStlImport, IErrorHandling, IDataModel
    {
        private string DataName;
        private int Type;
        private string Locate;

        public void StlDataName(string NewName)
        {
            DataName = NewName;
        }

        public void StlDataType(int TheType)
        {
            Type = TheType;
        }

        public void StlSaveLocation(string NewLocation)
        {
            Locate = NewLocation;
        }
    }
}