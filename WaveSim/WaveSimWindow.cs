using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace WaveSim
{
    class WaveSimWindow : GameWindow
    {
        public List<float> Vertices = new List<float>();
        public List<float> Colors = new List<float>();
        public List<uint> Indices = new List<uint>();
        public List<Matrix4> Transform = new List<Matrix4>();

        private int VertexBufferObject;
        private int ColorBufferObject;
        private int ElementBufferObject;
        private Shader Shader0;

        public WaveSimWindow(int width, int height, string title) : base(width, height, GraphicsMode.Default, title)
        {
            VertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);

            //ColorBufferObject = GL.GenBuffer();
            //GL.BindBuffer(BufferTarget.ArrayBuffer, ColorBufferObject);

            ElementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBufferObject);
        }

        public void BindObjects()
        {            
            GL.BufferData(BufferTarget.ArrayBuffer, Vertices.Count * sizeof(float), Vertices.ToArray(), BufferUsageHint.DynamicDraw);            
            //GL.BufferData(BufferTarget.ArrayBuffer, Colors.Count * sizeof(float), Colors.ToArray(), BufferUsageHint.DynamicDraw);            
            GL.BufferData(BufferTarget.ElementArrayBuffer, Indices.Count * sizeof(uint), Indices.ToArray(), BufferUsageHint.StaticDraw);

            int loc = Shader0.GetAttribLoc("vPosition");
            GL.EnableVertexAttribArray(loc);
            GL.VertexAttribPointer(loc, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);

            //loc = Shader0.GetAttribLoc("vColor");
            //GL.EnableVertexAttribArray(loc);
            //GL.VertexAttribPointer(loc, 4, VertexAttribPointerType.Float, false, 4 * sizeof(float), 0);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            KeyboardState input = Keyboard.GetState();

            if (input.IsKeyDown(Key.Escape))
            {
                Exit();
            }

            BindObjects();

            base.OnUpdateFrame(e);
        }

        protected override void OnLoad(EventArgs e)
        {
            GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);
            GL.Enable(EnableCap.DepthTest);
            Shader0 = new Shader(@"../../shader.vert", @"../../shader.frag");
            

            base.OnLoad(e);
        }

        protected override void OnUnload(EventArgs e)
        {
            GL.DeleteBuffer(VertexBufferObject);
            GL.DeleteBuffer(ColorBufferObject);
            GL.DeleteBuffer(ElementBufferObject);
            Shader0.Dispose();

            base.OnUnload(e);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            Shader0.Use();
            Shader0.SetTransform(Transform[0], Transform[1], Transform[2], Transform[3], Transform[4]);

            GL.DrawElements(PrimitiveType.Triangles, Indices.Count, DrawElementsType.UnsignedInt, 0);

            Context.SwapBuffers();
            base.OnRenderFrame(e);
        }

        protected override void OnResize(EventArgs e)
        {
            GL.Viewport(0, 0, Width, Height);
            base.OnResize(e);
        }
    }
}
