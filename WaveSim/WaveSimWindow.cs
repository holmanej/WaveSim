using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Drawing2D;
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
        public List<Matrix4> Transform = new List<Matrix4>();
        public Matrix4 Model;
        public Matrix4 View;
        public Matrix4 Projection;

        private int VertexBufferObject;
        private int ElementBufferObject;
        private Shader Shader0;

        private float y = 0;

        public WaveSimWindow(int width, int height, string title) : base(width, height, GraphicsMode.Default, title)
        {
            VertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);

            ElementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBufferObject);
        }

        private void BufferObjects()
        {
            GL.BufferData(BufferTarget.ArrayBuffer, Vertices.Count * sizeof(float), Vertices.ToArray(), BufferUsageHint.DynamicDraw);

            int loc = Shader0.GetAttribLoc("vPosition");
            GL.EnableVertexAttribArray(loc);
            GL.VertexAttribPointer(loc, 3, VertexAttribPointerType.Float, false, 10 * sizeof(float), 0);

            loc = Shader0.GetAttribLoc("vNormal");
            GL.EnableVertexAttribArray(loc);
            GL.VertexAttribPointer(loc, 3, VertexAttribPointerType.Float, false, 10 * sizeof(float), 3 * sizeof(float));

            loc = Shader0.GetAttribLoc("vColor");
            GL.EnableVertexAttribArray(loc);
            GL.VertexAttribPointer(loc, 4, VertexAttribPointerType.Float, false, 10 * sizeof(float), 6 * sizeof(float));
        }

        private void SetAngle(float x, float y, float z)
        {
            Transform[2] = Matrix4.CreateRotationX(x);
            Transform[3] = Matrix4.CreateRotationY(y);
            Transform[4] = Matrix4.CreateRotationZ(z);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            KeyboardState input = Keyboard.GetState();

            if (input.IsKeyDown(Key.Escape))
            {
                Exit();
            }

            if (input.IsKeyDown(Key.Left))
            {
                y -= 0.1f;
            }

            if (input.IsKeyDown(Key.Right))
            {
                y += 0.1f;
            }

            SetAngle(0f, y, 0);
            BufferObjects();

            base.OnUpdateFrame(e);
        }

        protected override void OnLoad(EventArgs e)
        {
            GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);
            GL.Enable(EnableCap.DepthTest);

            Transform = new List<Matrix4>()
            {
                Matrix4.CreateTranslation(0f, 0f, 0f),
                Matrix4.CreateScale(1f, 1f, 1f),
                Matrix4.CreateRotationX(0),
                Matrix4.CreateRotationY(0),
                Matrix4.CreateRotationZ(0)
            };

            Shader0 = new Shader(@"../../shader.vert", @"../../shader.frag");
            Shader0.Use();

            Model = Matrix4.CreateRotationX(15f * 3.14f / 180);
            View = Matrix4.CreateTranslation(0f, 0f, -3f);
            Projection = Matrix4.CreatePerspectiveFieldOfView(45f * 3.14f / 180f, Width / (float)Height, 0.1f, 100f);            

            base.OnLoad(e);
        }

        protected override void OnUnload(EventArgs e)
        {
            GL.DeleteBuffer(VertexBufferObject);
            GL.DeleteBuffer(ElementBufferObject);
            Shader0.Dispose();

            base.OnUnload(e);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            Shader0.Use();
            Shader0.SetTransform(Transform[0], Transform[1], Transform[2], Transform[3], Transform[4]);

            //View = Matrix4.CreateTranslation(0, 0f, y);
            //Debug.WriteLine(y);

            Shader0.SetMatrix4("model", Model);
            Shader0.SetMatrix4("view", View);
            Shader0.SetMatrix4("projection", Projection);

            GL.DrawArrays(PrimitiveType.Triangles, 0, Vertices.Count);

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
