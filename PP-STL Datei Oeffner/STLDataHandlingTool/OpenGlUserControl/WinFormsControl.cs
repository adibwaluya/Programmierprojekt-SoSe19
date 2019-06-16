using System;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using DataModel;

namespace OpenGlUserControl
{
    public partial class WinFormsControl : UserControl
    {
        private GLControl _glControl;
        private Vector3d[] _vertexBuffer;
        private int _vertexBufferObject;
        //private uint[] _defaultColor = new uint[3];

        // Singleton
        private static WinFormsControl _instance1;
        public static WinFormsControl Instance1
        {
            get
            {
                if (_instance1 != null) return _instance1;
                _instance1 = new WinFormsControl
                {
                    Dock = DockStyle.Fill
                };

                return _instance1;
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

            _glControl.Paint += _glControl_Paint;
            _glControl.Resize += _glControl_Resize;

            GL.ClearColor(Color.DarkGray);
            GL.Enable(EnableCap.DepthTest);

        }

        private void _glControl_Paint(object sender, PaintEventArgs e) 
        {
            Render();
        }

        private void _glControl_Resize(object sender, EventArgs e) 
        {
            GL.Viewport(Point.Empty, _glControl.Size);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            var perspectiveProjection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, 
                _glControl.Width*1.0f / _glControl.Height, 1.0f, 10.0f);
            GL.LoadMatrix(ref perspectiveProjection);
            GL.MatrixMode(MatrixMode.Modelview);
        }

        private void Render()
        {
            _glControl.MakeCurrent();
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.Translate(0.0,0.0,-10.0);// Translates the origin of the Matrix. The bigger z, the smaller the object.

            Matrix4 lookAt = Matrix4.LookAt(5, 5, 5, 0, 0, 0, 0, 1, 0);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref lookAt);
            //GL.MultMatrix(scalar); // Multiplies the current Matrix with a given scalar

            GL.Scale(1.0,1.0,1.0); // Scaling

            GetDataFromDataModel(); // Getting the Data from DataModel and loading them into the _vertexBuffer

            _vertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
            GL.BufferData<Vector3d>(BufferTarget.ArrayBuffer, (IntPtr)(Vector3d.SizeInBytes * _vertexBuffer.Length),
                _vertexBuffer, BufferUsageHint.StaticDraw); // DynamicDraw is another option!!!!

            GL.EnableClientState(ArrayCap.VertexArray);
            //GL.EnableClientState(ArrayCap.NormalArray);
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
            GL.VertexPointer(3, VertexPointerType.Double, Vector3d.SizeInBytes, 0);

            GL.Color4(Color.FromArgb(209, 241, 241));
            GL.DrawArrays(PrimitiveType.Triangles, 0, _vertexBuffer.Length);

            _glControl.SwapBuffers();
        }

        /// <summary>
        /// Gets the Data from Datamodel and assigns them to _vertexBuffer (Member of this class!)
        /// </summary>
        private void GetDataFromDataModel()
        {
            if (DataStructure.DataStructureInstance == null) return;

            _vertexBuffer = new Vector3d[DataStructure.DataStructureInstance.points.m_int2pt.Count];

            var maxValueX = DataStructure.DataStructureInstance.GetMaxValueX();
            var maxValueY = DataStructure.DataStructureInstance.GetMaxValueY();
            var maxValueZ = DataStructure.DataStructureInstance.GetMaxValueZ();

            var minValueX = DataStructure.DataStructureInstance.GetMinValueX();
            var minValueY = DataStructure.DataStructureInstance.GetMinValueY();
            var minValueZ = DataStructure.DataStructureInstance.GetMinValueZ();

            for (var i = 0; i < DataStructure.DataStructureInstance.points.m_int2pt.Count; i++)
            {
                var normalizedX = NormalizeX(DataStructure.DataStructureInstance.points.GetPoint(i).X, maxValueX,
                    minValueX);
                var normalizedY = NormalizeY(DataStructure.DataStructureInstance.points.GetPoint(i).Y, maxValueY,
                    minValueY);
                var normalizedZ = NormalizeZ(DataStructure.DataStructureInstance.points.GetPoint(i).Z, maxValueZ,
                    minValueZ);

                _vertexBuffer[i] = new Vector3d(normalizedX, normalizedY, normalizedZ);
            }
        }

        /// <summary>
        /// Used to normalize the x-coordinate of any given Point within a range of -1 and 1
        /// </summary>
        /// <param name="pointX">The x-coordinate.</param>
        /// <param name="maxValueX">The maxX-value of the given datamodel.</param>
        /// <param name="minValueX">he minX-value of the given datamodel.</param>
        /// <returns>Normalized x-coordinate of the given point.</returns>
        private static double NormalizeX(double pointX, double maxValueX, double minValueX) 
        {
            var centerX = (maxValueX - minValueX) / 2;
            var diff = maxValueX - centerX;

            return (pointX - centerX) / diff;
        }

        /// <summary>
        /// Used to normalize the y-coordinate of any given Point within a range of -1 and 1
        /// </summary>
        /// <param name="pointY">The y-coordinate.</param>
        /// <param name="maxValueY">The maxY-value of the given datamodel.</param>
        /// <param name="minValueY">he minY-value of the given datamodel.</param>
        /// <returns>Normalized y-coordinate of the given point.</returns>
        private static double NormalizeY(double pointY, double maxValueY, double minValueY)
        {
            var centerY = (maxValueY - minValueY) / 2;
            var diff = maxValueY - centerY;

            return (pointY - centerY) / diff;
        }

        /// <summary>
        /// Used to normalize the z-coordinate of any given Point within a range of -1 and 1
        /// </summary>
        /// <param name="pointZ">The y-coordinate.</param>
        /// <param name="maxValueZ">The maxZ-value of the given datamodel.</param>
        /// <param name="minValueZ">he minZ-value of the given datamodel.</param>
        /// <returns>Normalized z-coordinate of the given point.</returns>
        private static double NormalizeZ(double pointZ, double maxValueZ, double minValueZ)
        {
            var centerZ = (maxValueZ - minValueZ) / 2;
            var diff = maxValueZ - centerZ;

            return (pointZ - centerZ) / diff;
        }

    }
}
