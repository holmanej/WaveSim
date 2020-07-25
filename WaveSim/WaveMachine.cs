using OpenTK;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace WaveSim
{
    class WaveMachine
    {
        private WaveSimWindow SimWin;
        private PrimitiveHelper Helper;
        private float angle = 0;

        public WaveMachine()
        {
            using (SimWin = new WaveSimWindow(800, 600, "Wave Simulator"))
            {
                Helper = new PrimitiveHelper(SimWin);

                //List<float> vertices = new List<float>() {
                //    0f, 0.5f, 0f,
                //    -0.5f, -0.5f, 0f,
                //    0.5f, -0.5f, 0.0f,
                //}
                //Helper.AppendTriangle(vertices);
                //Helper.AppendSquare(-0.5f, -0.5f, 0.4f, 0.4f);

                Helper.AppendRectPrism(-0.1f, -0.1f, -0.1f, 0.2f, 0.2f, 0.2f);

                Timer rTimer = new Timer()
                {
                    Interval = 10
                };
                rTimer.Elapsed += RTimer_Elapsed;
                rTimer.Start();

                

                SimWin.VSync = VSyncMode.Adaptive;
                SimWin.Run(60, 0);
            }
        }

        private void RTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            angle += 1;

            List<Matrix4> transform = new List<Matrix4>()
            {
                Matrix4.CreateTranslation(0f, 0f, 0f),
                Matrix4.CreateScale(1f, 1f, 1f),
                Matrix4.CreateRotationX(20f * 3.14f / 180f),
                Matrix4.CreateRotationY(-angle * 3.14f / 180f),
                Matrix4.CreateRotationZ(0 * 3.14f / 180f)
            };

            SimWin.Transform = transform;
        }
    }
}
