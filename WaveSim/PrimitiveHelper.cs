using OpenTK;
using OpenTK.Graphics.ES20;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaveSim
{
    class PrimitiveHelper
    {
        private List<float> Vertices = new List<float>();

        private WaveSimWindow Window;

        public PrimitiveHelper(WaveSimWindow window)
        {
            Window = window;
        }

        public void ClearPrimitiveLists()
        {
            Vertices.Clear();
            Window.Vertices = Vertices;
        }

        public void AppendTriangle(List<float> vertices)
        {
            Vertices.AddRange(vertices);
            Window.Vertices = Vertices;
        }

        public void AppendRectPrism(float x, float y, float z, float w, float h, float d, float r, float b, float g, float a)
        {
            List<float> vertices = new List<float>()
            {
                x, y, z,              0f, 0f, z * 2,        r, b, g, a,		// 0
                x + w, y, z,          0f, 0f, z * 2,        r, b, g, a,		// 1
                x, y + h, z,          0f, 0f, z * 2,        r, b, g, a,		// 2
                x + w, y, z,          0f, 0f, z * 2,        r, b, g, a,		// 1
                x, y + h, z,          0f, 0f, z * 2,        r, b, g, a,		// 2
                x + w, y + h, z,      0f, 0f, z * 2,        r, b, g, a,		// 3
						
                x, y, z + d,          0f, 0f, (z + d) * 2,  r, b, g, a,		// 4
                x + w, y, z + d,      0f, 0f, (z + d) * 2,  r, b, g, a,		// 5
                x, y + h, z + d,      0f, 0f, (z + d) * 2,  r, b, g, a,		// 6
                x + w, y, z + d,      0f, 0f, (z + d) * 2,  r, b, g, a,		// 5
                x, y + h, z + d,      0f, 0f, (z + d) * 2,  r, b, g, a,		// 6
                x + w, y + h, z + d,  0f, 0f, (z + d) * 2,  r, b, g, a,		// 7
						
                x, y, z,              x * 2, 0f, 0f,        r, b, g, a,		// 0
                x, y + h, z,          x * 2, 0f, 0f,        r, b, g, a,		// 2
                x, y, z + d,          x * 2, 0f, 0f,        r, b, g, a,		// 4
                x, y + h, z,          x * 2, 0f, 0f,        r, b, g, a,		// 2
                x, y, z + d,          x * 2, 0f, 0f,        r, b, g, a,		// 4
                x, y + h, z + d,      x * 2, 0f, 0f,        r, b, g, a,		// 6
						
                x + w, y, z,          (x + w) * 2, 0f, 0f,  r, b, g, a,		// 1
                x + w, y + h, z,      (x + w) * 2, 0f, 0f,  r, b, g, a,		// 3
                x + w, y, z + d,      (x + w) * 2, 0f, 0f,  r, b, g, a,		// 5
                x + w, y + h, z,      (x + w) * 2, 0f, 0f,  r, b, g, a,		// 3
                x + w, y, z + d,      (x + w) * 2, 0f, 0f,  r, b, g, a,		// 5
                x + w, y + h, z + d,  (x + w) * 2, 0f, 0f,  r, b, g, a,		// 7
						
                x, y + h, z,          0f, (y + h) * 2, 0f,  r, b, g, a,		// 2
                x + w, y + h, z,      0f, (y + h) * 2, 0f,  r, b, g, a,		// 3
                x, y + h, z + d,      0f, (y + h) * 2, 0f,  r, b, g, a,		// 6
                x + w, y + h, z,      0f, (y + h) * 2, 0f,  r, b, g, a,		// 3
                x, y + h, z + d,      0f, (y + h) * 2, 0f,  r, b, g, a,		// 6
                x + w, y + h, z + d,  0f, (y + h) * 2, 0f,  r, b, g, a,		// 7
						
                x, y, z,              0f, y * 2, 0f,        r, b, g, a,		// 0
                x + w, y, z,          0f, y * 2, 0f,        r, b, g, a,		// 1
                x, y, z + d,          0f, y * 2, 0f,        r, b, g, a,		// 4
                x + w, y, z,          0f, y * 2, 0f,        r, b, g, a,		// 1
                x, y, z + d,          0f, y * 2, 0f,        r, b, g, a,		// 4
                x + w, y, z + d,      0f, y * 2, 0f,        r, b, g, a 		// 5
            };

            //    vertices = new List<float>()
            //    {
            //        // Position          Normal
            //        -0.5f, -0.5f, -0.5f,  0.0f,  0.0f, -1.0f, // Front face
            //     0.5f, -0.5f, -0.5f,  0.0f,  0.0f, -1.0f,
            //     0.5f,  0.5f, -0.5f,  0.0f,  0.0f, -1.0f,
            //     0.5f,  0.5f, -0.5f,  0.0f,  0.0f, -1.0f,
            //    -0.5f,  0.5f, -0.5f,  0.0f,  0.0f, -1.0f,
            //    -0.5f, -0.5f, -0.5f,  0.0f,  0.0f, -1.0f,

            //    -0.5f, -0.5f,  0.5f,  0.0f,  0.0f,  1.0f, // Back face
            //     0.5f, -0.5f,  0.5f,  0.0f,  0.0f,  1.0f,
            //     0.5f,  0.5f,  0.5f,  0.0f,  0.0f,  1.0f,
            //     0.5f,  0.5f,  0.5f,  0.0f,  0.0f,  1.0f,
            //    -0.5f,  0.5f,  0.5f,  0.0f,  0.0f,  1.0f,
            //    -0.5f, -0.5f,  0.5f,  0.0f,  0.0f,  1.0f,

            //    -0.5f,  0.5f,  0.5f, -1.0f,  0.0f,  0.0f, // Left face
            //    -0.5f,  0.5f, -0.5f, -1.0f,  0.0f,  0.0f,
            //    -0.5f, -0.5f, -0.5f, -1.0f,  0.0f,  0.0f,
            //    -0.5f, -0.5f, -0.5f, -1.0f,  0.0f,  0.0f,
            //    -0.5f, -0.5f,  0.5f, -1.0f,  0.0f,  0.0f,
            //    -0.5f,  0.5f,  0.5f, -1.0f,  0.0f,  0.0f,

            //     0.5f,  0.5f,  0.5f,  1.0f,  0.0f,  0.0f, // Right face
            //     0.5f,  0.5f, -0.5f,  1.0f,  0.0f,  0.0f,
            //     0.5f, -0.5f, -0.5f,  1.0f,  0.0f,  0.0f,
            //     0.5f, -0.5f, -0.5f,  1.0f,  0.0f,  0.0f,
            //     0.5f, -0.5f,  0.5f,  1.0f,  0.0f,  0.0f,
            //     0.5f,  0.5f,  0.5f,  1.0f,  0.0f,  0.0f,

            //    -0.5f, -0.5f, -0.5f,  0.0f, -1.0f,  0.0f, // Bottom face
            //     0.5f, -0.5f, -0.5f,  0.0f, -1.0f,  0.0f,
            //     0.5f, -0.5f,  0.5f,  0.0f, -1.0f,  0.0f,
            //     0.5f, -0.5f,  0.5f,  0.0f, -1.0f,  0.0f,
            //    -0.5f, -0.5f,  0.5f,  0.0f, -1.0f,  0.0f,
            //    -0.5f, -0.5f, -0.5f,  0.0f, -1.0f,  0.0f,

            //    -0.5f,  0.5f, -0.5f,  0.0f,  1.0f,  0.0f, // Top face
            //     0.5f,  0.5f, -0.5f,  0.0f,  1.0f,  0.0f,
            //     0.5f,  0.5f,  0.5f,  0.0f,  1.0f,  0.0f,
            //     0.5f,  0.5f,  0.5f,  0.0f,  1.0f,  0.0f,
            //    -0.5f,  0.5f,  0.5f,  0.0f,  1.0f,  0.0f,
            //    -0.5f,  0.5f, -0.5f,  0.0f,  1.0f,  0.0f
            //};

            Vertices.AddRange(vertices);
            Window.Vertices = Vertices;
        }

    }
}
