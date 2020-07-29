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

                Helper.AppendRectPrism(-5f, 0.005f, -5f, 0.01f, 1f, 0.01f, 1f, 0.7f, 0.1f, 1f);

                Helper.UpdateVertices();

                SimWin.VSync = VSyncMode.Adaptive;
                SimWin.Run(60, 0);
            }
        }
    }
}
