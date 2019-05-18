using System;
using System.Configuration;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace ViewModel
{
    class ViewModelWindow
    {
        private readonly GameWindow window;
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
            GL.Ortho(-50, 50.0, -50.0, 50.0, -10.0, 10.0); //Orthographic projection.
            GL.MatrixMode(MatrixMode.Modelview);
        }

        private void RenderF(object sender, FrameEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);


            //Any function which draws something on the screen has to be called between "Clear()" and "SwapBuffers()"

            //GL.Begin(mode: BeginMode.Triangles); //"Triangles" ist just one of different available primitives.
            //setColor();
            //GL.Vertex2(1.0, 1.0);
            //GL.Vertex2(49.0, 1.0);
            //GL.Vertex2(25.0, 49.0);
            //GL.End();

            GL.Rotate();

            GL.Begin(mode: BeginMode.Quads);
            // front
            //GL.Color3(1.0, 1.0, 0.0);
            GL.Vertex3(-10.0, 10.0, 10.0);
            GL.Vertex3(-10.0, 10.0, -10.0);
            GL.Vertex3(-10.0, -10.0, -10.0);
            GL.Vertex3(-10.0, -10.0, 10.0);

            // back
            //GL.Color3(1.0, 0.0, 1.0);
            GL.Vertex3(10.0, 10.0, 10.0);
            GL.Vertex3(10.0, 10.0, -10.0);
            GL.Vertex3(10.0, -10.0, -10.0);
            GL.Vertex3(10.0, -10.0, 10.0);

            // top
            //GL.Color3(0.0, 1.0, 1.0);
            GL.Vertex3(10.0, -10.0, 10.0);
            GL.Vertex3(10.0, -10.0, -10.0);
            GL.Vertex3(-10.0, -10.0, -10.0);
            GL.Vertex3(-10.0, -10.0, 10.0);

            // bottom
            //GL.Color3(1.0, 0.0, 0.0);
            GL.Vertex3(10.0, 10.0, 10.0);
            GL.Vertex3(10.0, 10.0, -10.0);
            GL.Vertex3(-10.0, 10.0, -10.0);
            GL.Vertex3(-10.0, 10.0, 10.0);

            // right
            //GL.Color3(0.0, 1.0, 0.0);
            GL.Vertex3(10.0, 10.0, -10.0);
            GL.Vertex3(10.0, -10.0, -10.0);
            GL.Vertex3(-10.0, -10.0, -10.0);
            GL.Vertex3(-10.0, 10.0, -10.0);

            // left
            //GL.Color3(0.0, 0.0, 1.0);
            GL.Vertex3(10.0, 10.0, 10.0);
            GL.Vertex3(10.0, -10.0, 10.0);
            GL.Vertex3(-10.0, -10.0, 10.0);
            GL.Vertex3(-10.0, 10.0, 10.0);

            GL.End();

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

        }
    }
}
