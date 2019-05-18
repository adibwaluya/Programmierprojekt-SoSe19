using System;

using OpenTK;

namespace ViewModel
{
    class Program
    {
        static void Main(string[] args)
        {
            GameWindow window = new GameWindow(500,500);
            ViewModelWindow modelWindow = new ViewModelWindow(window);
        }
    }
}
