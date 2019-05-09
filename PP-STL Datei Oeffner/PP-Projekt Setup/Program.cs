using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PP_Projekt_Setup
{
    class Program
    {
        static void Main(string[] args)
        {
            StlSpeichern Save = new StlSpeichern();
            Save.StlDataName("Meine geile STL Datei");

        }
    }
}
