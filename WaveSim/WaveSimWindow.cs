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

        private int VertexBufferObject;
        private Shader Shader0;

        public WaveSimWindow(int width, int height, string title) : base(width, height, GraphicsMode.Default, title)
        {
            VertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);
        }

        public void BindObjects()
        {            
            GL.BufferData(BufferTarget.ArrayBuffer, Vertices.Count * sizeof(float), Vertices.ToArray(), BufferUsageHint.DynamicDraw);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            KeyboardState input = Keyboard.GetState();

            if (input.IsKeyDown(Key.Escape))
            {
                Exit();
            }

            base.OnUpdateFrame(e);
        }

        protected override void OnLoad(EventArgs e)
        {
            GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);
            Shader0 = new Shader(@"../../shader.vert", @"../../shader.frag");

            

            base.OnLoad(e);
        }

        protected override void OnUnload(EventArgs e)
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.DeleteBuffer(VertexBufferObject);
            Shader0.Dispose();

            base.OnUnload(e);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);

            Shader0.Use();
            for (int i = 0; i < Vertices.Count; i += 3)
            {
                GL.DrawArrays(PrimitiveType.Triangles, i, 3);
            }

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
