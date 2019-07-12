/*******************************************************************************************
 * Copyright (c) <2019><Michael Kaip, Maximilian Mews, Michael Reno, Adib Ghassani Waluya> *
 *******************************************************************************************/

// The aim of this class is to provide a data type, in which all vertex related information
// can be stored together.

#region Using directives

using System.Drawing;
using OpenTK;


#endregion

namespace OpenGlUserControl 
{
    public struct Vertex
    {
        public Vector3d Point { get; set; }
        public Vector4 Color { get; set; }

        // If a new property is added, it has to be changed only once and will be reflected everywhere else
        public static int SizeInBytes => Vector3d.SizeInBytes + Vector4.SizeInBytes; 


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