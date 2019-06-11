using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace OpenGlUserControl
{
    public partial class WinFormsControl : UserControl
    {
        private static float _angle = 0.0f; 
        private GLControl _glControl;

        private Vector3d[] _vertexBuffer;
        private int _vbo;

        #region ---Creating the GLControl---

        
        private static WinFormsControl _instance1;

        // Singleton
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

            _glControl.Resize += _glControl_Resize;
            _glControl.Paint += _glControl_Paint;


            GL.ClearColor(Color.DimGray);
            GL.Enable(EnableCap.DepthTest);

            _glControl_Resize(_glControl, EventArgs.Empty); // Makes sure, the viewport an projection matrix are going to be correctly set.
        }

        #endregion

        #region Eventhandlers

        private void _glControl_Resize(object sender, EventArgs e)
        {
            if (sender is GLControl glControl && glControl.ClientSize.Height == 0)
            {
                glControl.ClientSize = new Size(glControl.ClientSize.Width, 1);
            }

            Matrix4 perspectiveProjection =
                Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, Width*1.0f / Height, 1.0f, 100.0f);

            GL.MatrixMode(MatrixMode.Projection);

            GL.LoadMatrix(ref perspectiveProjection); 
        }

        private void _glControl_Paint(object sender, PaintEventArgs e)
        {
            Render();
        }

        private void Render()
        {
            Matrix4 lookAt = Matrix4.LookAt(0, 5, 5, 0, 0, 0, 1, 3, 0);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref lookAt);

            //GL.Rotate(_angle, 0.0f, 1.0f, 0.0);
            //_angle += 0.5f;

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GetDataModel(); // Getting the Data from DataModel and loading them into the _vertexBuffer

            _vbo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vbo);
            GL.BufferData<Vector3d>(BufferTarget.ArrayBuffer, (IntPtr)(Vector3d.SizeInBytes * _vertexBuffer.Length),
                _vertexBuffer,BufferUsageHint.StaticDraw); // DynamicDraw!!!

            GL.EnableClientState(ArrayCap.VertexArray);
            //GL.EnableClientState(ArrayCap.NormalArray);
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vbo);
            GL.VertexPointer(3, VertexPointerType.Double, Vector3d.SizeInBytes, 0);

            GL.DrawArrays(PrimitiveType.Triangles, 0, _vertexBuffer.Length);

            _glControl.SwapBuffers();
        }

        #endregion

        #region 

        private void GetDataModel()
        {
          

            _vertexBuffer = new Vector3d[36]
            {
                new Vector3d(1.000000e+01, -1.000000e+01, -1.000000e+01),
                new Vector3d(1.000000e+01, -1.000000e+01, 1.000000e+01),
                new Vector3d(-1.000000e+01, -1.000000e+01, -1.000000e+01),
               
                new Vector3d(-1.000000e+01, -1.000000e+01, -1.000000e+01),
                new Vector3d(1.000000e+01, -1.000000e+01, 1.000000e+01),
                new Vector3d(-1.000000e+01, -1.000000e+01, 1.000000e+01),

                new Vector3d(1.000000e+01, 1.000000e+01, -1.000000e+01),
                new Vector3d(1.000000e+01, 1.000000e+01, 1.000000e+01),
                new Vector3d(1.000000e+01, -1.000000e+01, -1.000000e+0),

                new Vector3d(1.000000e+01, -1.000000e+01, -1.000000e+01),
                new Vector3d(1.000000e+01, 1.000000e+01, 1.000000e+01),
                new Vector3d(1.000000e+01, -1.000000e+01, 1.000000e+01),

                new Vector3d(1.000000e+01, 1.000000e+01, 1.000000e+01),
                new Vector3d(-1.000000e+01, 1.000000e+01, 1.000000e+01),
                new Vector3d(1.000000e+01, -1.000000e+01, 1.000000e+01),

                new Vector3d(1.000000e+01, -1.000000e+01, 1.000000e+01),
                new Vector3d(-1.000000e+01, 1.000000e+01, 1.000000e+01),
                new Vector3d(-1.000000e+01, -1.000000e+01, 1.000000e+01),

                new Vector3d(-1.000000e+01, 1.000000e+01, 1.000000e+01),
                new Vector3d(-1.000000e+01, 1.000000e+01, -1.000000e+01),
                new Vector3d(-1.000000e+01, -1.000000e+01, 1.000000e+01),

                new Vector3d(-1.000000e+01, -1.000000e+01, 1.000000e+01),
                new Vector3d(-1.000000e+01, 1.000000e+01, -1.000000e+01),
                new Vector3d(-1.000000e+01, -1.000000e+01, -1.000000e+01),

                new Vector3d(-1.000000e+01, 1.000000e+01, -1.000000e+01),
                new Vector3d(1.000000e+01, 1.000000e+01, -1.000000e+01),
                new Vector3d(-1.000000e+01, -1.000000e+01, -1.000000e+01),

                new Vector3d(-1.000000e+01, -1.000000e+01, -1.000000e+01),
                new Vector3d(1.000000e+01, 1.000000e+01, -1.000000e+01),
                new Vector3d(1.000000e+01, -1.000000e+01, -1.000000e+01),

                new Vector3d(-1.000000e+01, 1.000000e+01, -1.000000e+01),
                new Vector3d(-1.000000e+01, 1.000000e+01, 1.000000e+01),
                new Vector3d(1.000000e+01, 1.000000e+01, -1.000000e+01),

                new Vector3d(11.000000e+01, 1.000000e+01, -1.000000e+01),
                new Vector3d(-1.000000e+01, 1.000000e+01, 1.000000e+01),
                new Vector3d(1.000000e+01, 1.000000e+01, 1.000000e+01),

            };

        }

        #endregion
    }

}
        