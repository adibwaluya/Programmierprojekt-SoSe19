using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace ViewModel
{
    class Program
    {
        static void Main(string[] args)
        {
            GameWindow window = new GameWindow(500,500);

            ViewModelWindow viewModelWindow = new ViewModelWindow(window);
        }
    }
}
