using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace ViewModel
{
    public class VertexBuffer 
    {

        #region Members

        public static Vector3d[] BufferData;
        public double Length { get; }


        #endregion

        #region Constructor

        public VertexBuffer() // Takes a parameter of type List<Point> ListOfPoints
        {
            var tmp = new PointsList();

            BufferData = new Vector3d[tmp.ListOfPoints.Count];

            for (var i = 0; i < tmp.ListOfPoints.Count; i++)
            {
                BufferData[i] = new Vector3d(tmp.ListOfPoints.ElementAt(i).X, tmp.ListOfPoints.ElementAt(i).Y, tmp.ListOfPoints.ElementAt(i).Z);
            }

            Length = BufferData.Length;
        }

        #endregion
    }
}
