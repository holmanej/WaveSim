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
                updateTimer.Start();

                //Helper.AppendRectPrism(-1f, 0f, -1f, 2f, 0.01f, 2f, 1f, 0.5f, 0.2f, 1f);
                //double t = SimWin.simTime;

                //for (int i = 0; i < 100; i++)
                //{
                //    Helper.AppendRectPrism(-1f + i * 0.02f, 0.01f, 0f, 0.02f, (float)Math.Sin(t - (i / -6.28f * 0.8f) / 2f) * 1f, 0.02f, 1f, 0.7f, 0.1f, 1f);
                //}

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
            Helper.ClearPrimitiveLists();
            Helper.AppendRectPrism(-1f, 0f, -1f, 2f, 0.01f, 2f, 1f, 0.5f, 0.2f, 1f);

            double t = SimWin.simTime;

            for (int i = 0; i < 100; i++)
            {
                Helper.AppendRectPrism(-1f + i * 0.02f, 0.01f, 0f, 0.02f, (float)Math.Sin(t - (i / -6.28f * 0.8f) / 2f) * 1f, 0.02f, 1f, 0.7f, 0.1f, 1f);
            }
        }
    }
}
