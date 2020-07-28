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

        public WaveMachine()
        {
            using (SimWin = new WaveSimWindow(800, 600, "Wave Simulator"))
            {
                Helper = new PrimitiveHelper(SimWin);

                Timer updateTimer = new Timer()
                {
                    Interval = 20
                };
                updateTimer.Elapsed += UpdateTimer_Elapsed;
                //updateTimer.Start();

                List<float> magnitudes = new List<float>();
                Helper.AppendRectPrism(-1f, 0f, -1f, 2f, 0.01f, 2f, 1f, 0.5f, 0.2f, 1f);
                magnitudes.Add(1f);

                float t = (float)SimWin.simTime;
                
                for (int i = 0; i < 100; i++)
                {
                    //Helper.AppendRectPrism(-1f + i * 0.02f, 0.005f, -1f, 0.02f, (float)Math.Sin((t * 2) + (i / 10f)), 2f, 1f, 0.7f, 0.1f, 1f);
                    Helper.AppendRectPrism(-1f + i * 0.02f, 0.005f, -1f, 0.02f, 1f, 2f, 1f, 0.7f, 0.1f, 1f);
                    //magnitudes.Add((float)Math.Sin((t * 2) + (i / 10f)));
                }

                magnitudes.Add(0.5f);
                SimWin.ScaleValues = magnitudes;
                Helper.UpdateVertices();

                SimWin.VSync = VSyncMode.Adaptive;
                SimWin.Run(60, 0);
            }
        }

        private void UpdateTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            ChangeData();
        }

        private void ChangeData()
        {
            Helper.ClearVertices();
            Helper.AppendRectPrism(-1f, 0f, -1f, 2f, 0.01f, 2f, 1f, 0.5f, 0.2f, 1f);

            float t = (float)SimWin.simTime;

            for (int i = 0; i < 100; i++)
            {
                Helper.AppendRectPrism(-1f + i * 0.02f, 0.005f, -1f, 0.02f, (float)Math.Sin((t * 2) + (i / 10f)), 2f, 1f, 0.7f, 0.1f, 1f);
            }
            Helper.UpdateVertices();
        }
    }
}
