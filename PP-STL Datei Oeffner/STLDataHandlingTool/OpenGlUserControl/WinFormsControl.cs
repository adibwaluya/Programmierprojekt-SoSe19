using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Media.Media3D;
using DataModel;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using Color = System.Drawing.Color;
using Point = System.Drawing.Point;

namespace OpenGlUserControl
{
    public partial class WinFormsControl : UserControl
    {
        private GLControl _glControl;

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
            /*
             * Setting the viewport
             */
            GL.Viewport(Point.Empty, _glControl.Size);
            GL.MatrixMode(MatrixMode.Projection);

            /*
             * Makes sure, that only pixels get drawn, which are not hided by other pixels.
             */
            GL.Enable(cap: EnableCap.DepthTest);  

            /*
             * Creating a perspective projection matrix, to transform the camera space into the raster space.
             */
            var perspectiveProjection = Matrix4.CreatePerspectiveFieldOfView(ViewAngle, 
                _glControl.Width*1.0f / _glControl.Height, DistanceToNearClipPlane, DistanceToFarClipPlane);
            GL.LoadMatrix(ref perspectiveProjection);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();
        }

        public Vector3 CameraLocation = new Vector3(5f, 5f, 5f);
        public Vector3 LookingAt = new Vector3(0f, 0f, 0f); // Looks at the center of the coordinate system
        public Vector3 CameraUpside = new Vector3(0f, 1f, 0f);

        private int VertexBufferId;

        private void Render()
        {
            _glControl.MakeCurrent();

            GL.ClearColor(Color.DarkGreen);
            GL.ClearDepth(1); // ??????????????? Whats this actually ??????????????????

            /*
             * Resetting the depth and the color buffer in order to clean it up, before rendering new stuff.
             */
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            /*
             * Translating the origin of the coordinate system
             */
            GL.Translate(0.0,0.0,-10.0);

            /*
             * Setup for the camera
             */
            Matrix4 lookAt = Matrix4.LookAt(CameraLocation,LookingAt, CameraUpside);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref lookAt);


            // ==> Continue here...

            

            //_vertexBufferObject = GL.GenBuffer();
            //GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
            //GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(Vector3d.SizeInBytes * DataStructure.VerticesCount),
            //    _vertexBuffer, BufferUsageHint.StaticDraw); // DynamicDraw is another option!!!!

            //GL.EnableClientState(ArrayCap.VertexArray);
            //GL.VertexPointer(3, VertexPointerType.Double, Vector3d.SizeInBytes, 0);
            //GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);

            ////GL.EnableClientState(ArrayCap.NormalArray);
            ////GL.NormalPointer(NormalPointerType.Double, Vector3d.SizeInBytes,Vector3d.SizeInBytes);

            //GL.Color3(foregroundColor.R, foregroundColor.G, foregroundColor.B);
            //GL.DrawArrays(PrimitiveType.Triangles, 0, DataStructure.VerticesCount);

            _glControl.SwapBuffers();
        }

        public void DrawModel(Tuple<Vertex[], Normal[], uint[], uint[]> modelData)
        {
            InitializeBuffers(modelData);

            Render();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataPoints"></param>
        private void InitializeBuffers(Tuple<Vertex[], Normal[], uint[], uint[]> dataPoints)
        {
            InitializeVertexBuffer(dataPoints.Item1);
            InitializeNormalBuffer(dataPoints.Item2);
            InitializeFaceIndexArray(dataPoints.Item3);
            InitializeEdgeIndexArray(dataPoints.Item4);

            #region bounding box calculations

            //var min = GetBoundingBoxMinValue(dataPoints);
            //var max = GetBoundingBoxMaxValue(dataPoints);

            //for (var i = 0; i < dataPoints.Count; i = i+4) 
            //{
            //    for (var j = 0; j < VertexBuffer.Length; j = j+5)
            //    {
            //        var normalizedPoint = Normalize(dataPoints[i], max, min);
            //        VertexBuffer[j] = new Vector3d(normalizedPoint.X, normalizedPoint.Y, normalizedPoint.Z); // Adding the 1st vertex

            //        normalizedPoint = Normalize(dataPoints[i+1], max, min);
            //        VertexBuffer[j+1] = new Vector3d(normalizedPoint.X, normalizedPoint.Y, normalizedPoint.Z); // Adding the 2nd vertex

            //        normalizedPoint = Normalize(dataPoints[i+2], max, min);
            //        VertexBuffer[j+2] = new Vector3d(normalizedPoint.X, normalizedPoint.Y, normalizedPoint.Z); // Adding the 3rd vertex

            //        VertexBuffer[j+3] = new Vector3d(dataPoints[i+3].X, dataPoints[i+3].Y, dataPoints[i+ 3].Z); // Adding the normal vector
            //    }
            //}
            #endregion
        }



        public Vertex[] VertexBuffer;

        private void InitializeVertexBuffer(Vertex[] vertices)
        {
            if (vertices == null) throw new NullReferenceException();

            if (vertices.GetType() != typeof(List<DataModel.Point>))
                throw new ArgumentException("Parameter does not match the specified data type.");

            //VertexBuffer = new Vector3d[vertices.Count + (vertices.Count - (vertices.Count / 4 - 1))];
        }

        public Vector3d[] NormalBuffer;

        private static void InitializeNormalBuffer(Normal[] normals)
        {
            throw new NotImplementedException();
        }

        public uint[] FaceIndices;

        private static void InitializeFaceIndexArray(uint[] faces)
        {
            throw new NotImplementedException();
        }

        public uint[] EdgeIndices;

        private static void InitializeEdgeIndexArray(uint[] edges)
        {
            throw new NotImplementedException();
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

        /// <summary>
        /// Used to get the data from data structure, needed to render the model in OpenGL
        /// </summary>
        /// <param name="ds">An object of Type DataStructure</param>
        /// <returns>A Tuple of data points list (double x, double y, double z), normal vector List (double x, double y, double z), face index array (uint[] and edge index array (uint[]) </returns>
        public Tuple<Vertex[], Normal[], uint[], uint[]> GetModelDataForRendering(DataStructure ds)
        {
            var vertices = GetVertices(ds);
            var normalVectors = GetNormals(ds);
            var faceList = GetFaceList(ds);
            var edgeList = GetEdgeList(ds);


            return Tuple.Create(vertices, normalVectors, faceList, edgeList);
        }

        public static Color VertexColor = Color.DarkBlue;

        private static Vertex[] GetVertices(DataStructure ds) 
        {
            var vertices = new Vertex[ds.points.m_int2pt.Count];

            for (var i = 0; i < ds.points.m_int2pt.Count; i++)
            {
                vertices[i] = new Vertex(new Vector3d(ds.points.m_int2pt[i].X, ds.points.m_int2pt[i].Y, ds.points.m_int2pt[i].Z), VertexColor);
            }

            return vertices;
        }

        private static Normal[] GetNormals(DataStructure ds)
        {
            var normalVectors = new Normal[ds.faces.m_int2Face.Count];

            for (var i = 0; i < ds.faces.m_int2Face.Count; i++)
            {
                normalVectors[i] = new Normal(new Vector3d(ds.faces.m_int2Face[i].N.X, ds.faces.m_int2Face[i].N.Y, ds.faces.m_int2Face[i].N.Z));
            }

            return normalVectors;
        }

        // Contains the indices of vertices for each face (3 per face)
        private static uint[] GetFaceList(DataStructure ds)
        {
            var faceList = new uint[(ds.faces.m_int2Face.Count * 3)];

            for(var i = 0; i < (ds.faces.m_int2Face.Count * 3); i = i+3)
            {
                foreach (var face in ds.faces.m_int2Face)
                {
                    faceList[i] = (uint)face.Value.Points.IndexOf(face.Value.Points[0]);
                    faceList[i + 1] = (uint)face.Value.Points.IndexOf(face.Value.Points[1]);
                    faceList[i + 2] = (uint)face.Value.Points.IndexOf(face.Value.Points[2]);
                }
            }

            return faceList;
        }

        private static uint[] GetEdgeList(DataStructure ds)
        {
            throw new NotImplementedException();
        }
    }
}
