using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PP_Projekt_Setup
{
    class StlSpeichern: IStlSpeichern
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
