using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace ViewModel
{
    class VertexBuffer 
    {

        #region Members

        public static Vector3d[] _vertexBuffer; 


        #endregion

        #region Constructor

        public VertexBuffer() // Takes a parameter of type List<Point> ListOfPoints
        {
            var listOfPoints = new List<Point> // Creating a sample of List<Point> points and populating it with sample data
            {
                new PointsList()
            };

            _vertexBuffer = new Vector3d[listOfPoints.Count];

            for (var i = 0; i < listOfPoints.Count; i++)
            {
                _vertexBuffer[i] = new Vector3d(listOfPoints.ElementAt(i).X, listOfPoints.ElementAt(i).Y, listOfPoints.ElementAt(i).Z);
            }

            

        }

        #endregion
    }
}
