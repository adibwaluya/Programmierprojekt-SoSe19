using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Windows.Media.Media3D;
using DataModel;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using Point = System.Drawing.Point;

namespace OpenGlUserControl
{
    public partial class WinFormsControl : UserControl
    {
        private GLControl _glControl;
        private Vector3d[] _vertexBuffer;
        private int _vertexBufferObject;

        // Singleton
        private static WinFormsControl _winFormsControlInstance;
        public static WinFormsControl Instance1
        {
            get
            {
                if (_winFormsControlInstance != null) return _winFormsControlInstance;
                _winFormsControlInstance = new WinFormsControl
                {
                    Dock = DockStyle.Fill
                };

                return _winFormsControlInstance;
            }
        }


        public WinFormsControl()
        {
            InitializeComponent();
        }


        private void WinFormsControl_Load(object sender, EventArgs e)
        {
            _glControl = new GLControl()
            {
                Dock = DockStyle.Fill,
                VSync = true
            };

            // Adding the glControl to the panel inside WinFormsControl
            WinformsControlPanel.Controls.Add(_glControl);

            // Event Handlers
            _glControl.Resize += _glControl_Resize;
 
        }

        public float ViewAngle = MathHelper.PiOver4; // 45 degree (in radian)
        public float DistanceToNearClipPlane = 1.0f;
        public float DistanceToFarClipPlane = 10.0f;
        private void _glControl_Resize(object sender, EventArgs e) 
        {
            
            /***********************
             * Setting the viewport
             ***********************/
            GL.Viewport(Point.Empty, _glControl.Size);
            GL.MatrixMode(MatrixMode.Projection);

            /******************************************************************************
             * Makes sure, that only pixels get drawn, which are not hided by other pixels.
             ******************************************************************************/
            GL.Enable(cap: EnableCap.DepthTest);  

            /*************************************************************************************************
             * Creating a perspective projection matrix, to transform the camera space into the raster space.
             *************************************************************************************************/
            var perspectiveProjection = Matrix4.CreatePerspectiveFieldOfView(ViewAngle, 
                _glControl.Width*1.0f / _glControl.Height, DistanceToNearClipPlane, DistanceToFarClipPlane);
            GL.LoadMatrix(ref perspectiveProjection);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();
        }

        public Vector3 CameraLocation = new Vector3(5f, 5f, 5f);
        public Vector3 LookingAt = new Vector3(0f, 0f, 0f); // Looks at the center of the coordinate system
        public Vector3 CameraUpside = new Vector3(0f, 1f, 0f);

        private void Render(ColorRGB backgroundColor, ColorRGB foregroundColor)
        {
            _glControl.MakeCurrent();

            GL.ClearColor(backgroundColor.R, backgroundColor.G, backgroundColor.B, 0f);
            GL.ClearDepth(1);

            /***********************************************************************************************
             * Resetting the depth and the color buffer in order to clean it up, before rendering new stuff.
             ***********************************************************************************************/
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            /**************************************************
             * Translating the origin of the coordinate system
             **************************************************/
            GL.Translate(0.0,0.0,-10.0);

            /************************
             * Setup for the camera
             ************************/
            Matrix4 lookAt = Matrix4.LookAt(CameraLocation,LookingAt, CameraUpside);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref lookAt);


            _vertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(Vector3d.SizeInBytes * DataStructure.VerticesCount),
                _vertexBuffer, BufferUsageHint.StaticDraw); // DynamicDraw is another option!!!!

            GL.EnableClientState(ArrayCap.VertexArray);
            GL.VertexPointer(3, VertexPointerType.Double, Vector3d.SizeInBytes, 0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);

            //GL.EnableClientState(ArrayCap.NormalArray);
            //GL.NormalPointer(NormalPointerType.Double, Vector3d.SizeInBytes,Vector3d.SizeInBytes);

            GL.Color3(foregroundColor.R, foregroundColor.G, foregroundColor.B);
            GL.DrawArrays(PrimitiveType.Triangles, 0, DataStructure.VerticesCount);

            _glControl.SwapBuffers();
        }

        

        public void DrawModel(List<Point3D> dataPoints, ColorRGB backgroundColor, ColorRGB foregroundColor)
        {
            InitializeVertexBuffer(dataPoints, foregroundColor);

            Render(backgroundColor, foregroundColor);
        }

        /// <summary>
        /// Used for initializing the VertexBuffer, which stores the data to be rendered in the GPU.
        /// </summary>
        /// <param name="dataPoints">List of Point3D (double X, double Y double Z). Data points to be rendered.</param>
        /// <param name="foregroundColor">The defined foreground color.</param>
        private void InitializeVertexBuffer(List<Point3D> dataPoints, ColorRGB foregroundColor)
        {

            if (dataPoints == null) throw new NullReferenceException();
        
            if (dataPoints.GetType() != typeof(List<Point3D>))
                throw new ArgumentException("Parameter does not match the specified data type.");

            if (foregroundColor == null) throw new ArgumentNullException(nameof(foregroundColor));

            _vertexBuffer = new Vector3d[dataPoints.Count+(dataPoints.Count-(dataPoints.Count/4 - 1))];

            var min = GetBoundingBoxMinValue(dataPoints);
            var max = GetBoundingBoxMaxValue(dataPoints);

            for (var i = 0; i < dataPoints.Count; i = i+4) 
            {
                for (var j = 0; j < _vertexBuffer.Length; j = j+5)
                {
                    var normalizedPoint = Normalize(dataPoints[i], max, min);
                    _vertexBuffer[j] = new Vector3d(normalizedPoint.X, normalizedPoint.Y, normalizedPoint.Z); // Adding the 1st vertex

                    normalizedPoint = Normalize(dataPoints[i+1], max, min);
                    _vertexBuffer[j+1] = new Vector3d(normalizedPoint.X, normalizedPoint.Y, normalizedPoint.Z); // Adding the 2nd vertex

                    normalizedPoint = Normalize(dataPoints[i+2], max, min);
                    _vertexBuffer[j+2] = new Vector3d(normalizedPoint.X, normalizedPoint.Y, normalizedPoint.Z); // Adding the 3rd vertex

                    _vertexBuffer[j+3] = new Vector3d(dataPoints[i+3].X, dataPoints[i+3].Y, dataPoints[i+ 3].Z); // Adding the normal vector
                }
            }

            for (var k = 4; k < _vertexBuffer.Length; k = k+5)
            {
                _vertexBuffer[k] = new Vector3d(foregroundColor.R, foregroundColor.G, foregroundColor.B); // Adding the vertex color
            }
        }

        /// <summary>
        /// Used for normalizing the coordinates of the given data points within a range of -1 and 1.
        /// </summary>
        /// <param name="point">Point of type Point3D</param>
        /// <param name="max">Max. bounding box value.</param>
        /// <param name="min">Min bounding box value</param>
        /// <returns></returns>
        private static Point3D Normalize(Point3D point, Point3D max, Point3D min)
        {
            var greatestDiff = Math.Max(Math.Max(max.X-min.X, max.Y-min.Y), max.Z-min.Z);
            if (greatestDiff == 0.0f) greatestDiff = 1;
            var ret = new Point3D
            {
                X = (point.X - 0.5 * (max.X + min.X)) / greatestDiff,
                Y = (point.Y - 0.5 * (max.Y + min.Y)) / greatestDiff,
                Z = (point.Z - 0.5 * (max.Z + min.Z)) / greatestDiff
            };

            return ret;
        }

        /// <summary>
        /// Used to get the max bounding Box value from the give data points.
        /// </summary>
        /// <param name="dataPoints">Data points of type List Point3D.</param>
        /// <returns>Max. bounding box value.</returns>
        public Point3D GetBoundingBoxMaxValue(List<Point3D> dataPoints)
        {
            Point3D maxBoundingBoxValue = new Point3D(double.NegativeInfinity, Double.NegativeInfinity, Double.NegativeInfinity);

            foreach (var point in dataPoints)
            {
                if (point.X > maxBoundingBoxValue.X)
                {
                    maxBoundingBoxValue.X = point.X;
                }
                if (point.Y > maxBoundingBoxValue.Y)
                {
                    maxBoundingBoxValue.Y = point.Y;
                }
                if (point.Z > maxBoundingBoxValue.Z)
                {
                    maxBoundingBoxValue.Z = point.Z;
                }
            }
            return maxBoundingBoxValue;
        }

        /// <summary>
        /// Used to get the min bounding Box value from the give data points.
        /// </summary>
        /// <param name="dataPoints">Data points of type List Point3D.</param>
        /// <returns>Min. bounding box value.</returns>
        public Point3D GetBoundingBoxMinValue(List<Point3D> dataPoints)
        {
            Point3D minBoundingBoxValue = new Point3D(double.PositiveInfinity, Double.PositiveInfinity, Double.PositiveInfinity);

            foreach (var point in dataPoints)
            {
                if (point.X < minBoundingBoxValue.X)
                {
                    minBoundingBoxValue.X = point.X;
                }
                if (point.Y < minBoundingBoxValue.Y)
                {
                    minBoundingBoxValue.Y = point.Y;
                }
                if (point.Z < minBoundingBoxValue.Z)
                {
                    minBoundingBoxValue.Z = point.Z;
                }
            }
            return minBoundingBoxValue;
        }
    }
}
