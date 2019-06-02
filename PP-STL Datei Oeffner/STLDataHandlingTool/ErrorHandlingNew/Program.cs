using DataModel;
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
            PointList bla = new PointList();
            Point punkt1 = new Point(1,2,3);
            bla.AddOrGetPoint(punkt1);
            Point punkt2 = new Point();
            punkt2 = bla.GetPoint(1);
            Console.WriteLine();
            Console.ReadLine();
        }
    }
}
