using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PP_Projekt_Setup
{
    public interface IStlSpeichern
    {
        void StlDataName(string Name);
        void StlSaveLocation(string Location);
        void StlDataType(int Typ);
    }
}
