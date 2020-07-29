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

                Helper.AppendRectPrism(-5f, 0.005f, -5f, 0.01f, 1f, 0.01f, 1f, 0.7f, 0.1f, 1f);

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

            //for (int i = 0; i < 1000; i++)
            //{                
            //    for (int j = 0; j < 1000; j++)
            //    {
                    //float m = (float)Math.Sin((t * 2f) - (i / 100f));
                    //float m = (float)Math.Sin((t * 1f) - (Math.Sqrt(Math.Pow((i - 500f) / 80f, 2) + Math.Pow((j - 500f) / 80f, 2))));
                    //float m = (float)Math.Sin((t * 2f) - ((i - 500f) / 100f));
                    magnitudes.Add(t);
                    //magnitudes.Add((i * 0.002f) + (j * 0.002f));
                    //Debug.WriteLine(i + j);
            //    }
            //}
            //magnitudes.Add(0.5f);
            SimWin.MagnitudeValues = magnitudes;
        }
    }
}
