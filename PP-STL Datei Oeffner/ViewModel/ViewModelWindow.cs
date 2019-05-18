using System;
using System.Configuration;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace ViewModel
{
    class ViewModelWindow
    {
        private readonly GameWindow window;
        private float ProjectionAngle = 45.0f; // Angle for perspective projection mode

        public ViewModelWindow(GameWindow window)
        {
            this.window = window;
            Start();
        }

        private void Start()
        {
            window.Load += Loaded;
            window.RenderFrame += RenderF;
            window.Resize += ResizeWindow;
            window.Run(1.0/60); // Frequency
        }

        /*
         * Initializing the Viewport - the area in which objects are going to be rendered inside the window.
         * Multiple viewports are poosible!!!
         */
        private void ResizeWindow(object sender, EventArgs e)
        {
            GL.Viewport(0, 0, window.Width, window.Height);
            GL.MatrixMode(MatrixMode.Projection); //Initializes the projection matrix.
            GL.LoadIdentity(); // Resets the state of the current matrix.

            // Setup for the perspective projection (3D) (another possibility, orthographic projection (2D), is also available: GL.Ortho();)
            Matrix4 matrix = Matrix4.CreatePerspectiveFieldOfView(ProjectionAngle * (float)Math.PI / 180, 
                window.Width/window.Height, 1.0f, 100.0f);
            GL.LoadMatrix(ref matrix);

            GL.MatrixMode(MatrixMode.Modelview);
        }
         
        private void RenderF(object sender, FrameEventArgs e)
        {
            GL.LoadIdentity();
            GL.Clear(ClearBufferMask.ColorBufferBit);

            //Any function which draws something on the screen has to be called between "Clear()" and "SwapBuffers()"


            window.SwapBuffers();
        }

        /*
         * Checks if user settings according to the color are available - otherwise sets the color to default.
         */
        private void setColor()
        {
            GL.Color3(0.151, 0.255, 0.255); // default color values
        }

        /*
         * Clearing the color of the frame and setting it to its default value
         */
        private void Loaded(object sender, EventArgs e)
        {
            GL.ClearColor(0f, 0f, 0f, 0f);
            GL.Enable(EnableCap.DepthTest);

        }
    }
}
