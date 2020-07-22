using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace WaveSim
{
    class WaveMachine
    {
        private WaveSimWindow SimWin;

        public WaveMachine()
        {
            Timer bindTimer = new Timer()
            {
                Interval = 1000
            };
            bindTimer.Elapsed += BindTimer_Elapsed;

            SimWin = new WaveSimWindow(800, 600, "Wave Simulator");

            List<float> vertices = new List<float>() {
                     0.5f,  0.5f, 0.0f,  // top right
                     0.5f, -0.5f, 0.0f,  // bottom right
                    -0.5f,  0.5f, 0.0f,   // top left
                     0.5f, -0.5f, 0.0f,  // bottom right
                    -0.5f, -0.5f, 0.0f,  // bottom left
                    -0.5f,  0.5f, 0.0f   // top left
                };
            SimWin.Vertices.AddRange(vertices);
            //SimWin.BindObjects();
            bindTimer.Start();
            SimWin.Run(60);

            //using (SimWin = new WaveSimWindow(800, 600, "Wave Simulator"))
            //{
            //    List<float> vertices = new List<float>() {
            //         0.5f,  0.5f, 0.0f,  // top right
            //         0.5f, -0.5f, 0.0f,  // bottom right
            //        -0.5f,  0.5f, 0.0f,   // top left
            //         0.5f, -0.5f, 0.0f,  // bottom right
            //        -0.5f, -0.5f, 0.0f,  // bottom left
            //        -0.5f,  0.5f, 0.0f   // top left
            //    };
            //    SimWin.Vertices.AddRange(vertices);


            //    SimWin.Run(60);
            //}
        }

        private void BindTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Debug.WriteLine("tick-bind");
            SimWin.BindObjects();
        }
    }
}
