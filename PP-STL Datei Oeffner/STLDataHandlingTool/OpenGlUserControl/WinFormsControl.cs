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
    public partial class WinFormsControl: UserControl
    {
        private GLControl _glControl;

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
                VSync = true,
        };


            // Adding the glControl to the panel inside WinFormsControl
            WinformsControlPanel.Controls.Add(_glControl);

            _glControl.Paint += GlControl_Paint;
            _glControl.SizeChanged += GlControl_SizeChanged;
        }

        private void GlControl_Paint(object sender, PaintEventArgs e)
        {
            _glControl.MakeCurrent();
            GL.ClearColor(Color.Blue);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            
            GL.Viewport(Point.Empty, _glControl.Size);

            GL.Begin(PrimitiveType.Triangles);
            GL.Vertex2(-1, -1);
            GL.Vertex2(1, -1);
            GL.Vertex2(0, 1);
            GL.End();

            _glControl.SwapBuffers();

        }

        private void GlControl_SizeChanged(object sender, EventArgs e)
        {
            _glControl.Invalidate();
        }
    }
}
