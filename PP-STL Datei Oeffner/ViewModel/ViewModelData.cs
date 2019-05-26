using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace ViewModel
{
    class ViewModelData 
    {

        #region Members

        private static int _vbo;
        public static Vector3d[] VertexBuffer; 


        #endregion

        #region Constructor

        public ViewModelData() // Takes a parameter of type List<Point> ListOfPoints
        {
            var listOfPoints = new List<Point> // Creating a sample of List<Point> points and populating it with sample data
            {
                new PointsList()
            };

            VertexBuffer = new Vector3d[listOfPoints.Count];

            for (var i = 0; i < listOfPoints.Count; i++)
            {
                VertexBuffer[i] = new Vector3d(listOfPoints.ElementAt(i).X, listOfPoints.ElementAt(i).Y, listOfPoints.ElementAt(i).Z);
            }

            

        }

        #endregion
    }
}
