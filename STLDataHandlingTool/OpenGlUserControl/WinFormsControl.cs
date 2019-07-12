/*******************************************************************************************
 * Copyright (c) <2019><Michael Kaip, Maximilian Mews, Michael Reno, Adib Ghassani Waluya> *
 *******************************************************************************************/

#region Using Directives

using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Windows.Media.Media3D;
using DataModel;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using Color = System.Drawing.Color;
using Point = System.Drawing.Point;

#endregion

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

        // Resize event handler
        private void _glControl_Resize(object sender, EventArgs e) 
        {
            // Setting the viewport
            GL.Viewport(Point.Empty, _glControl.Size);
            GL.MatrixMode(MatrixMode.Projection);

            // Makes sure, that only pixels get drawn, which are not hided by other pixels.
            GL.Enable(cap: EnableCap.DepthTest);

            // Creating a perspective projection matrix, to transform the camera space into the raster space.
            var perspectiveProjection = Matrix4.CreatePerspectiveFieldOfView(ViewAngle, 
                _glControl.Width*1.0f / _glControl.Height, DistanceToNearClipPlane, DistanceToFarClipPlane);
            GL.LoadMatrix(ref perspectiveProjection);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();
        }

        // Helper for moving the camera around
        public Vector3 CameraLocation
        {
            get => _cameraLocation;
            set
            {
                _cameraLocation = value;
                Render();
            }
        }

        public Vector3 LookingAt = new Vector3(0f, 0f, 0f); // Looks at the center of the coordinate system
        public Vector3 CameraUpside = new Vector3(0f, 1f, 0f);

        private int _vertexBufferObject;
        private int _indexBufferObject;

        #region LookAt Method (not in use!)

        private static Matrix4 LookAt(Vector3 from, Vector3 to, Vector3 tmp )
        {
            if (tmp.Length == 0) tmp = new Vector3(0, 1, 0);
            var forward = Vector3.Normalize(from - to);
            var right = Vector3.Cross(tmp.Normalized() , forward);
            var up = Vector3.Cross(forward, right);

            var camToWorld = Matrix4.Identity;

            camToWorld[0,0] = right.X; 
            camToWorld[0,1] = right.Y; 
            camToWorld[0,2] = right.Z; 
            camToWorld[1,0] = up.X; 
            camToWorld[1,1] = up.Y; 
            camToWorld[1,2] = up.Z; 
            camToWorld[2,0] = forward.X; 
            camToWorld[2,1] = forward.Y; 
            camToWorld[2,2] = forward.Z; 
 
            camToWorld[3,0] = from.X; 
            camToWorld[3,1] = from.Y; 
            camToWorld[3,2] = from.Z; 
 
            return camToWorld; 
        }

        #endregion

        // Method for rendering
        private void Render()
        {
            _glControl.MakeCurrent();

            GL.ClearColor(BackColor);

            //Resetting the depth and the color buffer in order to clean it up, before rendering new stuff.
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            // Translating the origin of the coordinate system
            GL.Translate(0.0,0.0,0.0);

            // Camera setup
            //Matrix4 lookAt = Matrix4.LookAt(CameraLocation, LookingAt, CameraUpside);
            Matrix4 lookAt = LookAt(CameraLocation, LookingAt, CameraUpside);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref lookAt);
            //GL.LoadIdentity();

            // Passing vertex data to the GPU
            _vertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(Vertex.SizeInBytes * VertexBuffer.Length),
                VertexBuffer, BufferUsageHint.StaticDraw);
            GL.EnableClientState(ArrayCap.VertexArray);
            GL.VertexPointer(3, VertexPointerType.Double, Vertex.SizeInBytes, (IntPtr)0);
            GL.ColorPointer(4, ColorPointerType.Float, Vertex.SizeInBytes, (IntPtr)(Vector3d.SizeInBytes));

            // Passing face indices to the GPU
            _indexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer , _indexBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, (IntPtr)(sizeof(uint) * FaceIndexBuffer.Length), 
                FaceIndexBuffer, BufferUsageHint.StaticDraw);
            GL.EnableClientState(ArrayCap.ColorArray); 

            GL.DrawElements(PrimitiveType.Triangles, FaceIndexBuffer.Length, DrawElementsType.UnsignedByte,0);

            GL.Flush();

            _glControl.SwapBuffers();
        }

        /// <summary>
        /// Draws the 3D-Model on the screen.
        /// </summary>
        /// <param name="ds">An object of type DataStructure.</param>
        /// <param name="backgroundColor">The background color of type Color, which is used for rendering.</param>
        /// <param name="foregroundColor">he foreground color of type Color, which is used for rendering</param>
        public void DrawModel(DataStructure ds, Color backgroundColor, Color foregroundColor)
        {
            if (ds == null) throw new ArgumentNullException(nameof(ds));

            GetModelDataForRendering_And_InitializeBuffers(ds, foregroundColor);
            BackColor = backgroundColor;
            Render();
        }

    
        #region Helpers for bounding Box calculations and normalizing vertices from the given data model. Might be useful in some cases...

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

        #endregion

        // Buffer data members
        public Vertex[] VertexBuffer;
        public Normal[] NormalBuffer;
        public uint[] FaceIndexBuffer;
        public uint[] EdgeIndexBuffer;
        private Vector3 _cameraLocation = new Vector3(5f, 5f, 5f);


        // Used to get the data from data structure, needed to render the model in OpenGL. Buffers for Rendering are going to initialized accordingly.
        private void GetModelDataForRendering_And_InitializeBuffers(DataStructure ds, Color foregroundColor)
        {
            VertexBuffer = GetVertices(ds, foregroundColor); 
            NormalBuffer = GetNormals(ds);
            FaceIndexBuffer = GetFaceIndices(VertexBuffer,ds);
            EdgeIndexBuffer = GetEdgeIndices(ds);
        }
    

        private static Vertex[] GetVertices(DataStructure ds, Color foregroundColor) 
        {
            var vertices = new Vertex[ds.points.m_int2pt.Count];

            for (var i = 0; i < ds.points.m_int2pt.Count; i++)
            {
                vertices[i] = new Vertex(new Vector3d(ds.points.m_int2pt[i].X, ds.points.m_int2pt[i].Y, ds.points.m_int2pt[i].Z), foregroundColor);
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


        private static uint[] GetFaceIndices(Vertex[] vertices, DataStructure ds)
        {
            var tmpFaceList = new List<uint>();
            //var faceList = new uint[(ds.faces.m_int2Face.Count * 3)]; // Contains the vertex indices for each face
            var tmpPointsList = new DataModel.Point[3];

            foreach (var face in ds.faces.m_int2Face) // For each element in m_int2face "DO"
            {
                face.Value.Points.CopyTo(tmpPointsList); // Getting the points of the current face and save them back temporarily

                foreach(var point in tmpPointsList) 
                {
                    for (var i = 0; i < vertices.Length; i++)
                    {
                        if (point.X == vertices[i].Point.X &
                            point.Y == vertices[i].Point.Y &
                            point.Z == vertices[i].Point.Z)
                        {
                            tmpFaceList.Add((uint) i);
                        }
                    }
                }
            }

            return tmpFaceList.ToArray();
        }


        private static uint[] GetEdgeIndices(DataStructure ds) 
        {
            var tmpEdgeList = new List<uint>();

            foreach (var edge in ds.edges.m_int2edge)
            {
                tmpEdgeList.Add((uint)edge.Value.StartPoint);
                tmpEdgeList.Add((uint)edge.Value.EndPoint);
            }

            return tmpEdgeList.ToArray();
        }
    }
}
