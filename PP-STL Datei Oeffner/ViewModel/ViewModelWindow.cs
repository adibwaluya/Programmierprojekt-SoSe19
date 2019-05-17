using System;
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

        void Start()
        {
            window.Load += Loaded;
            window.Resize += ResizeWindow;
            window.RenderFrame += RenderF;
            window.Run(1.0 / 60.0);
        }

        void Loaded(object o, EventArgs e)
        {
            GL.ClearColor(0.0f,0.0f,0.0f,0.0f);
        }

        void ResizeWindow(object o, EventArgs e)
        {
            GL.Viewport(0, 0, window.Width, window.Height);

        }

        void RenderF(object o, EventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);
            /*
             * Any Function, which ist going to be called from here that draws something on the screen will take affect.
             */
            window.SwapBuffers();
        }
    }
}
