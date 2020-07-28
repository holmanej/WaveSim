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
                updateTimer.AutoReset = true;
                updateTimer.Elapsed += UpdateTimer_Elapsed;
                updateTimer.Start();

                Helper.AppendRectPrism(-1f, 0f, -1f, 2f, 0.01f, 2f, 1f, 0.5f, 0.2f, 1f);
                Helper.AppendRectPrism(-1f, 0.005f, -1f, 0.02f, 1f, 0.02f, 1f, 0.7f, 0.1f, 1f);

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
            float t = (float)SimWin.simTime;
            List<float> magnitudes = new List<float>();

            for (int i = 0; i < 10; i++)
            {                
                for (int j = 0; j < 100; j++)
                {
                    float m = (float)Math.Sin((t * 2f) - (j / 10f));
                    magnitudes.Add(m);
                    //magnitudes.Add((i * 0.002f) + (j * 0.002f));
                    //Debug.WriteLine(i + j);
                }
            }
            //magnitudes.Add(0.5f);
            SimWin.MagnitudeValues = magnitudes;
        }
    }
}
