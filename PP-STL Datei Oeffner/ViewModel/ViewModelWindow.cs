using System;
using System.Configuration;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace ViewModel
{
    class ViewModelWindow
    {
        private readonly GameWindow window;
        private float AngleOfProjection = 45.0f; // Angle for perspective projection mode
        private float rotation = 0.0f; // Used to apply the rotation

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
            window.Run(1.0/60); // Frequency of rendering (per second)
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
            Matrix4 perspectiveProjectionMatrix = Matrix4.CreatePerspectiveFieldOfView(AngleOfProjection * (float)Math.PI / 180, 
                window.Width/window.Height, 1.0f, 100.0f); // Angle in rad
            GL.LoadMatrix(ref perspectiveProjectionMatrix);
            
            GL.MatrixMode(MatrixMode.Modelview);
        }
         
        private void RenderF(object sender, FrameEventArgs e)
        {
            GL.LoadIdentity();
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            // Changes the origin of the coordinate system
            GL.Translate(0.0, 0.0, -50.0); // Can also be called with a vector of three elements

            // Rotating
            GL.Rotate(rotation, 1.0, 0.0, 0.0); // along the x-axis
            GL.Rotate(rotation, 1.0, 0.0, 1.0); // (Angle of rotation, 3D-Vector along which the rotation takes place)

            // Scaling
            GL.Scale(0.5, 1.0, 1.0);


            //Any function which draws something on the screen has to be called between "Clear()" and "SwapBuffers()"
            GL.Begin(BeginMode.Quads);

            // front side of the cube
            GL.Color3(1.0, 0.0, 0.0);
            GL.Vertex3(-10.0, 10.0, 10.0);
            GL.Vertex3(-10.0, 10.0, -10.0);
            GL.Vertex3(-10.0, -10.0, -10.0);
            GL.Vertex3(-10.0, -10.0, 10.0);

            // back side of the cube
            GL.Color3(0.0, 1.0, 0.0);
            GL.Vertex3(10.0, 10.0, 10.0);
            GL.Vertex3(10.0, 10.0, -10.0);
            GL.Vertex3(10.0, -10.0, -10.0);
            GL.Vertex3(10.0, -10.0, 10.0);

            // top side of the cube
            GL.Color3(0.0, 0.0, 1.0);
            GL.Vertex3(10.0, -10.0, 10.0);
            GL.Vertex3(10.0, -10.0, -10.0);
            GL.Vertex3(-10.0, -10.0, -10.0);
            GL.Vertex3(-10.0, -10.0, 10.0);

            // bottom side of the cube
            GL.Color3(1.0, 1.0, 0.0);
            GL.Vertex3(10.0, 10.0, 10.0);
            GL.Vertex3(10.0, 10.0, -10.0);
            GL.Vertex3(-10.0, 10.0, -10.0);
            GL.Vertex3(-10.0, 10.0, 10.0);

            // right side of the cube
            GL.Color3(1.0, 0.0, 1.0);
            GL.Vertex3(10.0, 10.0, -10.0);
            GL.Vertex3(10.0, -10.0, -10.0);
            GL.Vertex3(-10.0, -10.0, -10.0);
            GL.Vertex3(-10.0, 10.0, -10.0);

            // left side of the cube
            GL.Color3(0.0, 1.0, 1.0);
            GL.Vertex3(10.0, 10.0, 10.0);
            GL.Vertex3(10.0, -10.0, 10.0);
            GL.Vertex3(-10.0, -10.0, 10.0);
            GL.Vertex3(-10.0, 10.0, 10.0);
            GL.End();

            window.SwapBuffers();

            //rotation += 0.2f;
            rotation = rotation + 0.1f;

            if (rotation > 360)
            {
                rotation -= 360;
            }
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
