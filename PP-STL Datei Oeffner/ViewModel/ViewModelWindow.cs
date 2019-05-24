using System;
using System.Configuration;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace ViewModel
{
    internal class ViewModelWindow
    {
        #region Members

        private int Width { get; set; } // Width of the window
        private int Height { get; set; } // Height of the window
        private const float AngleOfProjection = 45.0f; // Angle for perspective projection mode
        private float _rotation = 0.0f; // Used to apply the rotation 
        private readonly GameWindow _window;


        #endregion

        #region Constructor

        public ViewModelWindow(int width, int height)
        {
            Width = width;
            Height = height;
            
            _window = new GameWindow(Width, Height);

            Start();
        }

        #endregion

        #region Mehthods for window control

        private void Start()
        {
            _window.Load += Loaded;
            _window.RenderFrame += RenderF;
            _window.Resize += ResizeWindow;
            _window.Run(1.0/60); // Frequency of rendering (per second)
        }

        /*
         * Initializing the Viewport - the area in which objects are going to be rendered inside the window.
         * Multiple viewports are possible!!!
         */
        private void ResizeWindow(object sender, EventArgs e)
        {
            // Initializing the viewport
            GL.Viewport(0, 0, _window.Width, _window.Height);
            GL.MatrixMode(MatrixMode.Projection); //Initializes the projection matrix.
            GL.LoadIdentity(); // Resets the state of the current matrix.

            // Setup for perspective projection (3D) (another possibility, orthographic projection (2D), is also available: GL.Ortho();)
            Matrix4 perspectiveProjectionMatrix = Matrix4.CreatePerspectiveFieldOfView(AngleOfProjection * (float)Math.PI / 180, 
                _window.Width/_window.Height, 1.0f, 200.0f);
            GL.LoadMatrix(ref perspectiveProjectionMatrix);
            GL.MatrixMode(MatrixMode.Modelview);
        }

        private void RenderF(object sender, FrameEventArgs e)
        {
            #region Loading Idendity and clearing Buffers

            GL.LoadIdentity();
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            #endregion

            GL.PushMatrix(); // Saving the Matrix state

            #region Drawing the 1st Object to a certain position in the coordinate system

            /************************************************************************
             * Drawing the 1st Object to a certain position in the coordinate system
             ************************************************************************/

            // Changes the origin of the coordinate system
            GL.Translate(0.0, 0.0, -100.0); // Can also be called with a vector of three elements

            // Rotating
            GL.Rotate(_rotation, 1.0, 0.0, 0.0); // along the x-axis
            GL.Rotate(_rotation, 1.0, 0.0, 1.0); // (Angle of rotation, 3D-Vector along which the rotation takes place)

            // Scaling
            GL.Scale(1.0, 1.0, 1.0);

            //Any function which draws something on the screen has to be called between "Clear()" and "SwapBuffers()"
            DrawModel();

            #endregion

            GL.PopMatrix(); //Redrawing the Matrix state with the 1st Object
            GL.PushMatrix(); // Saving the Matrix state

            #region Drawing the 2nd Object to another position in the coordinate system

            ///**********************************************************************
            // * Drawing the 2nd Object to another position in the coordinate system
            // **********************************************************************/

            //// Changes the origin of the coordinate system
            //GL.Translate(15.0, 0.0, -100.0); // Can also be called with a vector of three elements

            //// Rotating
            //GL.Rotate(_rotation, 1.0, 1.0, 0.0); // along the x-axis
            //GL.Rotate(_rotation, 0.0, 1.0, 1.0); // (Angle of rotation, 3D-Vector along which the rotation takes place)

            //// Scaling
            //GL.Scale(1.0, 1.0, 1.0);

            ////Any function which draws something on the screen has to be called between "Clear()" and "SwapBuffers()"
            //DrawModel();

            #endregion

            GL.PopMatrix(); //Redrawing the Matrix state with the 2nd Object

            _window.SwapBuffers();

            _rotation = _rotation + 0.1f;

            if (_rotation > 360)
            {
                _rotation -= 360;
            }
        }

        /*
         * Checks if user settings according to the color are available - otherwise sets the color to default.
         */
        private void SetColor()
        {
            GL.Color3(0.151, 0.255, 0.255); // default color values
        }

        /*
         * Clearing the color of the frame and setting it to its default value
         */
        private static void Loaded(object sender, EventArgs e)
        {
            GL.ClearColor(0f, 0f, 0f, 0f);
            GL.Enable(EnableCap.DepthTest);
        }

        #endregion

        private static void DrawModel()
        {
            GL.Begin(PrimitiveType.Quads);

            GL.Color3(1.0, 1.0, 1.0);
            // front side of the cube
            //GL.Normal3(0.0, 0.0, 1.0); // Needed for lightning effects
            GL.Color3(1.0, 0.0, 0.0);
            GL.Vertex3(-10.0, 10.0, 10.0);
            GL.Vertex3(-10.0, 10.0, -10.0);
            GL.Vertex3(-10.0, -10.0, -10.0);
            GL.Vertex3(-10.0, -10.0, 10.0);

            // back side of the cube

            //GL.Normal3(0.0, 0.0, -1.0); // Needed for lightning effects
            GL.Color3(0.0, 1.0, 0.0);
            GL.Vertex3(10.0, 10.0, 10.0);
            GL.Vertex3(10.0, 10.0, -10.0);
            GL.Vertex3(10.0, -10.0, -10.0);
            GL.Vertex3(10.0, -10.0, 10.0);

            // top side of the cube
            //GL.Normal3(0.0, 1.0, 0.0); // Needed for lightning effects
            GL.Color3(0.0, 0.0, 1.0);
            GL.Vertex3(10.0, -10.0, 10.0);
            GL.Vertex3(10.0, -10.0, -10.0);
            GL.Vertex3(-10.0, -10.0, -10.0);
            GL.Vertex3(-10.0, -10.0, 10.0);

            // bottom side of the cube
            //GL.Normal3(0.0, -1.0, 0.0); // Needed for lightning effects
            GL.Color3(1.0, 1.0, 0.0);
            GL.Vertex3(10.0, 10.0, 10.0);
            GL.Vertex3(10.0, 10.0, -10.0);
            GL.Vertex3(-10.0, 10.0, -10.0);
            GL.Vertex3(-10.0, 10.0, 10.0);

            // right side of the cube
            //GL.Normal3(1.0, 0.0, 0.0); // Needed for lightning effects
            GL.Color3(1.0, 0.0, 1.0);
            GL.Vertex3(10.0, 10.0, -10.0);
            GL.Vertex3(10.0, -10.0, -10.0);
            GL.Vertex3(-10.0, -10.0, -10.0);
            GL.Vertex3(-10.0, 10.0, -10.0);

            // left side of the cube
            //GL.Normal3(-1.0, 0.0, 0.0); // Needed for lightning effects
            GL.Color3(0.0, 1.0, 1.0);
            GL.Vertex3(10.0, 10.0, 10.0);
            GL.Vertex3(10.0, -10.0, 10.0);
            GL.Vertex3(-10.0, -10.0, 10.0);
            GL.Vertex3(-10.0, 10.0, 10.0);
            GL.End();
        }
    }
}
