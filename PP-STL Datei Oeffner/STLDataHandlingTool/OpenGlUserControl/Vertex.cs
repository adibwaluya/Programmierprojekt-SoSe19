﻿using System.Drawing;
using OpenTK;

/*
 * Starting Point of Refactoring and solving problems in the code. The idea is to save vertices and
 * all other dependent information in a struct an access it from there.
 * Decided using a struct here for efficiency reasons (value type!).
 */

namespace OpenGlUserControl 
{
    public struct Vertex
    {
        public Vector3d Point; 
        public Vector4 Color;

        // If a new property is added, it has to be changed only once and will be reflected everywhere else
        public static int SizeInBytes => Vector3d.SizeInBytes + Vector4.SizeInBytes; 
        // expression body (only getter!)

        public Vertex(Vector3d point, Vector4 color)
        {
            Point = point;
            Color = color;
        }

        public Vertex(Vector3d point, Color color)
        {
            Point = point;
            Color = new Vector4(color.R / 255f, color.G / 255f, color.B / 255f, color.A / 255f);
        }
    }
}