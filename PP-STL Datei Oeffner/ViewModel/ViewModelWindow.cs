using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace ViewModel
{
     class ViewModelWindow : VertexBuffer
    {
        #region Members

        private int Width { get; set; } // Width of the window
        private int Height { get; set; } // Height of the window
        private const float AngleOfProjection = 45.0f; // Angle for perspective projection mode
        //private float _rotation = 0.0f; // Used to apply the rotation 
        private readonly GameWindow _window;
        private static int _vbo;

        //private static VertexBuffer _vertBuffer; 
        private static VertexBuffer _vertBuffer;
        //private float _rotation = 45.0f;

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

        private void Start()
        {
            _window.Load += Window_Load;
            _window.RenderFrame += Window_RenderFrame;
            _window.Resize += ResizeWindow;
            _window.Run(1.0/60); // Frequency of rendering (per second)
        }

        /*
         * Initializing the Viewport - the area in which objects are going to be rendered inside the window.
         * Multiple viewports are possible!!!
         */
        private void ResizeWindow(object sender, EventArgs e)
        {
            /*
             * Initializing the viewport
             */
            GL.Viewport(0, 0, _window.Width, _window.Height);
            GL.MatrixMode(MatrixMode.Projection); //Initializes the projection matrix.
            GL.LoadIdentity(); // Resets the state of the current matrix.

            /*
             * Setup for perspective projection (3D) (another possibility, orthographic projection (2D), is also available:
             * GL.Ortho();)
             */
            var perspectiveProjectionMatrix = Matrix4.CreatePerspectiveFieldOfView(AngleOfProjection * (float)Math.PI / 180, 
                _window.Width*1.0f/_window.Height, 1.0f, 200.0f);

            GL.LoadMatrix(ref perspectiveProjectionMatrix);

            GL.MatrixMode(MatrixMode.Modelview);
        }

        private static void Window_Load(object sender, EventArgs e)
        {
            GL.ClearColor(0f, 0f, 0f, 0f);
            GL.Enable(EnableCap.DepthTest);

            /*
             * Instantiating a vertex buffer object, which holds the vertex data gonna be drawn in the window
             */
             _vertBuffer = new VertexBuffer();
            //_vertexBuffer = new Vector3d[36]
            //{
            //    // 
            //    new Vector3d(1.000000e+01, -1.000000e+01, -1.000000e+01), 
            //    new Vector3d(1.000000e+01, -1.000000e+01, 1.000000e+01), 
            //    new Vector3d(-1.000000e+01, -1.000000e+01, -1.000000e+01), 
            //    new Vector3d(-1.000000e+01, -1.000000e+01, -1.000000e+01), 
            //    new Vector3d(1.000000e+01, -1.000000e+01, 1.000000e+01), 
            //    new Vector3d(-1.000000e+01, -1.000000e+01, 1.000000e+01),
            //    // 
            //    new Vector3d(1.000000e+01, 1.000000e+01, -1.000000e+01), 
            //    new Vector3d(1.000000e+01, 1.000000e+01, 1.000000e+01), 
            //    new Vector3d(1.000000e+01, -1.000000e+01, -1.000000e+01),
            //    new Vector3d(1.000000e+01, -1.000000e+01, -1.000000e+01), 
            //    new Vector3d(1.000000e+01, 1.000000e+01, 1.000000e+01),
            //    new Vector3d(1.000000e+01, -1.000000e+01, 1.000000e+01),
            //    // 
            //    new Vector3d(1.000000e+01, 1.000000e+01, 1.000000e+01), 
            //    new Vector3d(-1.000000e+01, 1.000000e+01, 1.000000e+01), 
            //    new Vector3d(1.000000e+01, -1.000000e+01, 1.000000e+01),
            //    new Vector3d(1.000000e+01, -1.000000e+01, 1.000000e+01), 
            //    new Vector3d(-1.000000e+01, 1.000000e+01, 1.000000e+01), 
            //    new Vector3d(-1.000000e+01, -1.000000e+01, 1.000000e+01), 
            //    //
            //    new Vector3d(-1.000000e+01, 1.000000e+01, 1.000000e+01),
            //    new Vector3d(-1.000000e+01, 1.000000e+01, -1.000000e+01),
            //    new Vector3d(-1.000000e+01, -1.000000e+01, 1.000000e+01),
            //    new Vector3d(-1.000000e+01, -1.000000e+01, 1.000000e+01),
            //    new Vector3d(-1.000000e+01, 1.000000e+01, -1.000000e+01),
            //    new Vector3d(-1.000000e+01, -1.000000e+01, -1.000000e+01),
            //    //
            //    new Vector3d(-1.000000e+01, 1.000000e+01, -1.000000e+01),
            //    new Vector3d(1.000000e+01, 1.000000e+01, -1.000000e+01), 
            //    new Vector3d(-1.000000e+01, -1.000000e+01, -1.000000e+01), 
            //    new Vector3d(-1.000000e+01, -1.000000e+01, -1.000000e+01), 
            //    new Vector3d(1.000000e+01, 1.000000e+01, -1.000000e+01), 
            //    new Vector3d(1.000000e+01, -1.000000e+01, -1.000000e+01), 
            //    //
            //    new Vector3d(-1.000000e+01, 1.000000e+01, -1.000000e+01), 
            //    new Vector3d(-1.000000e+01, 1.000000e+01, 1.000000e+01), 
            //    new Vector3d(1.000000e+01, 1.000000e+01, -1.000000e+01),
            //    new Vector3d(1.000000e+01, 1.000000e+01, -1.000000e+01),
            //    new Vector3d(-1.000000e+01, 1.000000e+01, 1.000000e+01),
            //    new Vector3d(1.000000e+01, 1.000000e+01, 1.000000e+01),
            //};

            /*
             * Generating a vertex buffer object, that can be passed into
             */
            _vbo = GL.GenBuffer(); // Generates the buffer and passes back a pointer for it
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vbo); // Binding the buffer
            GL.BufferData<Vector3d>(BufferTarget.ArrayBuffer,(IntPtr)(Vector3d.SizeInBytes * BufferData.Length),BufferData,
                BufferUsageHint.StaticDraw);// Passing the data to OpenGL
        }

        private void Window_RenderFrame(object sender, FrameEventArgs e)
        {
            #region Loading Idendity and clearing Buffers

            GL.LoadIdentity();
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            #endregion

            //GL.PushMatrix(); // Saving the Matrix state

            #region Drawing the 1st Object to a certain position in the coordinate system

            /************************************************************************
             * Drawing the 1st Object to a certain position in the coordinate system
             ************************************************************************/

            //// Changes the origin of the coordinate system
            //GL.Translate(0.0, 0.0, -200.0); // Can also be called with a vector of three elements

            //// Rotating
            //GL.Rotate(_rotation, 1.0, 0.0, 0.0); // along the x-axis
            //GL.Rotate(_rotation, 1.0, 0.0, 1.0); // (Angle of rotation, 3D-Vector along which the rotation takes place)

            //// Scaling
            //GL.Scale(1.0, 1.0, 1.0);

            ////Any function which draws something on the screen has to be called between "Clear()" and "SwapBuffers()"
            //DrawModel();

            //#endregion

            //GL.PopMatrix(); //Redrawing the Matrix state with the 1st Object
            //GL.PushMatrix(); // Saving the Matrix state

            #endregion

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

            //GL.PopMatrix(); //Redrawing the Matrix state with the 2nd Object

            // Changes the origin of the coordinate system
            GL.Translate(0.0, 0.0, -100.0); // Can also be called with a vector of three elements

            //// Rotating
            //GL.Rotate(_rotation, 1.0, 0.0, 0.0); // along the x-axis
            //GL.Rotate(_rotation, 1.0, 0.0, 1.0); // (Angle of rotation, 3D-Vector along which the rotation takes place)


            /*
             * 1) EnableClientState(): Defines, that vertices have certain positions
             * 2) Binding the buffer
             * 3) Enabling the vertex buffer
             * 4) Setting a pointer that OpenGL knows, where to find each vertex
             */
            GL.EnableClientState(ArrayCap.VertexArray);
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vbo);
            GL.VertexPointer(3, VertexPointerType.Double, Vector3d.SizeInBytes, 0);

            GL.DrawArrays(PrimitiveType.Triangles, 0, BufferData.Length);

            GL.Flush();
            _window.SwapBuffers();

            //_rotation = _rotation + 0.1f;

            //if (_rotation > 360)
            //{
            //    _rotation -= 360;
            //}
        }
 






        #region DrawModel() Method (old stuff!!!)

        //private static void DrawModel()
        //{
        //    //GL.Begin(PrimitiveType.Quads);

        //    //GL.Color3(1.0, 1.0, 1.0);
        //    //// front side of the cube
        //    ////GL.Normal3(0.0, 0.0, 1.0); // Needed for lightning effects
        //    //GL.Color3(1.0, 0.0, 0.0);
        //    //GL.Vertex3(-10.0, 10.0, 10.0);
        //    //GL.Vertex3(-10.0, 10.0, -10.0);
        //    //GL.Vertex3(-10.0, -10.0, -10.0);
        //    //GL.Vertex3(-10.0, -10.0, 10.0);

        //    //// back side of the cube

        //    ////GL.Normal3(0.0, 0.0, -1.0); // Needed for lightning effects
        //    //GL.Color3(0.0, 1.0, 0.0);
        //    //GL.Vertex3(10.0, 10.0, 10.0);
        //    //GL.Vertex3(10.0, 10.0, -10.0);
        //    //GL.Vertex3(10.0, -10.0, -10.0);
        //    //GL.Vertex3(10.0, -10.0, 10.0);

        //    //// top side of the cube
        //    ////GL.Normal3(0.0, 1.0, 0.0); // Needed for lightning effects
        //    //GL.Color3(0.0, 0.0, 1.0);
        //    //GL.Vertex3(10.0, -10.0, 10.0);
        //    //GL.Vertex3(10.0, -10.0, -10.0);
        //    //GL.Vertex3(-10.0, -10.0, -10.0);
        //    //GL.Vertex3(-10.0, -10.0, 10.0);

        //    //// bottom side of the cube
        //    ////GL.Normal3(0.0, -1.0, 0.0); // Needed for lightning effects
        //    //GL.Color3(1.0, 1.0, 0.0);
        //    //GL.Vertex3(10.0, 10.0, 10.0);
        //    //GL.Vertex3(10.0, 10.0, -10.0);
        //    //GL.Vertex3(-10.0, 10.0, -10.0);
        //    //GL.Vertex3(-10.0, 10.0, 10.0);

        //    //// right side of the cube
        //    ////GL.Normal3(1.0, 0.0, 0.0); // Needed for lightning effects
        //    //GL.Color3(1.0, 0.0, 1.0);
        //    //GL.Vertex3(10.0, 10.0, -10.0);
        //    //GL.Vertex3(10.0, -10.0, -10.0);
        //    //GL.Vertex3(-10.0, -10.0, -10.0);
        //    //GL.Vertex3(-10.0, 10.0, -10.0);

        //    //// left side of the cube
        //    ////GL.Normal3(-1.0, 0.0, 0.0); // Needed for lightning effects
        //    //GL.Color3(0.0, 1.0, 1.0);
        //    //GL.Vertex3(10.0, 10.0, 10.0);
        //    //GL.Vertex3(10.0, -10.0, 10.0);
        //    //GL.Vertex3(-10.0, -10.0, 10.0);
        //    //GL.Vertex3(-10.0, 10.0, 10.0);
        //    //GL.End();

        //    //GL.End();
        //}

        #endregion
    }
}
