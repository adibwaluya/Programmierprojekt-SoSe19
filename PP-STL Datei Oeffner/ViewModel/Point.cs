using OpenTK;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using OpenTK.Graphics.OpenGL;

namespace ViewModel
{
    public class Point : object
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }


        public Point(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Point() {}
    }
}