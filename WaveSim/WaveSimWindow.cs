using System;
using System.Collections.Concurrent;
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
        public float[] Vertices;
        public List<Matrix4> Transform = new List<Matrix4>();
        public List<float> MagnitudeValues = new List<float>();
        public double simTime = 0;

        public Matrix4 Model;
        public Matrix4 View;
        public Matrix4 Projection;

        private int VertexBufferObject;
        private int ElementBufferObject;
        private Shader Shader0;

        private int BufferLength;
        private float yRotation = 0;
        private float xRotation = 0;
        private float Zoom = 0;
        

        public WaveSimWindow(int width, int height, string title) : base(width, height, GraphicsMode.Default, title)
        {
            VertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);

            ElementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBufferObject);

            MouseMove += WaveSimWindow_MouseMove;
            MouseWheel += WaveSimWindow_MouseWheel;
        }

        private void WaveSimWindow_MouseMove(object sender, MouseMoveEventArgs e)
        {
            if (e.Mouse.LeftButton == ButtonState.Pressed)
            {
                yRotation -= e.XDelta / 150f;
                xRotation += e.YDelta / 7f;
            }
        }

        private void WaveSimWindow_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.DeltaPrecise > 0)
            {
                Zoom += 1f;
            }
            else
            {
                Zoom -= 1f;
            }
        }

        public void BufferObjects()
        {
            GL.BufferData(BufferTarget.ArrayBuffer, Vertices.Length * sizeof(float), Vertices, BufferUsageHint.DynamicDraw);
            BufferLength = 36;
            //Debug.WriteLine("bf: " + BufferLength);
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
            simTime += e.Time;

            if (input.IsKeyDown(Key.Escape))
            {
                Exit();
            }

            SetAngle(0f, yRotation, 0);
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

            Model = Matrix4.CreateRotationX(20f * 3.14f / 180);
            View = Matrix4.CreateTranslation(0f, 0f, -5f);
            Projection = Matrix4.CreatePerspectiveFieldOfView(45f * 3.14f / 180f, Width / (float)Height, 0.1f, 100f);

            int loc = Shader0.GetAttribLoc("vPosition");
            GL.EnableVertexAttribArray(loc);
            GL.VertexAttribPointer(loc, 3, VertexAttribPointerType.Float, false, 10 * sizeof(float), 0);

            loc = Shader0.GetAttribLoc("vNormal");
            GL.EnableVertexAttribArray(loc);
            GL.VertexAttribPointer(loc, 3, VertexAttribPointerType.Float, false, 10 * sizeof(float), 3 * sizeof(float));

            loc = Shader0.GetAttribLoc("vColor");
            GL.EnableVertexAttribArray(loc);
            GL.VertexAttribPointer(loc, 4, VertexAttribPointerType.Float, false, 10 * sizeof(float), 6 * sizeof(float));

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

            Model = Matrix4.CreateRotationX((20f + xRotation) * 3.14f / 180);
            View = Matrix4.CreateTranslation(0f, 0f, -5f + Zoom);

            Shader0.SetMatrix4("model", Model);
            Shader0.SetMatrix4("view", View);
            Shader0.SetMatrix4("projection", Projection);
            for (int i = 0; i < MagnitudeValues.Count; i++)
            {
                GL.Uniform1(Shader0.GetUniformLoc("magnitudes[" + i.ToString() + "]"), MagnitudeValues[i]);
            }
            Debug.WriteLine("mag: " + MagnitudeValues.Count);

            GL.DrawArrays(PrimitiveType.Triangles, 0, BufferLength);
            GL.DrawArraysInstanced(PrimitiveType.Triangles, BufferLength, BufferLength, MagnitudeValues.Count);

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
