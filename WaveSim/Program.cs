using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace WaveSim
{
    class Program
    {
        static void Main()
        {
            using (WaveSimWindow simWin = new WaveSimWindow(800, 600, "Wave Simulator"))
            {
                List<float> vertices = new List<float>() {
                     0.5f,  0.5f, 0.0f,  // top right
                     0.5f, -0.5f, 0.0f,  // bottom right
                    -0.5f,  0.5f, 0.0f,   // top left
                     0.5f, -0.5f, 0.0f,  // bottom right
                    -0.5f, -0.5f, 0.0f,  // bottom left
                    -0.5f,  0.5f, 0.0f   // top left
                };
                simWin.Vertices.AddRange(vertices);
                simWin.BindObjects();

                simWin.Run(60);
            }
        }
    }
}
