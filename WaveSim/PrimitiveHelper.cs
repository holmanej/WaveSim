using OpenTK;
using OpenTK.Graphics.ES20;
using System;
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
        private void UpdateWindowLists()
        {
            Window.Vertices = Vertices;
        }

        public void ClearPrimitiveLists()
        {
            Vertices.Clear();
            UpdateWindowLists();
        }

        public void AppendTriangle(List<float> vertices)
        {
            Vertices.AddRange(vertices);

            UpdateWindowLists();
        }

        public void AppendRectPrism(float x, float y, float z, float w, float h, float d)
        {
            List<float> vertices = new List<float>()
            {
                x, y, z,                // 0
                x + w, y, z,            // 1
                x, y + h, z,            // 2
                x + w, y, z,            // 1
                x, y + h, z,            // 2
                x + w, y + h, z,        // 3

                x, y, z + d,            // 4
                x + w, y, z + d,        // 5
                x, y + h, z + d,        // 6
                x + w, y, z + d,        // 5
                x, y + h, z + d,        // 6
                x + w, y + h, z + d,    // 7

                x, y, z,                // 0
                x, y + h, z,            // 2
                x, y, z + d,            // 4
                x, y + h, z,            // 2
                x + w, y, z + d,        // 5
                x, y + h, z + d,        // 6

                x + w, y, z,            // 1
                x + w, y + h, z,        // 3
                x + w, y, z + d,        // 5
                x + w, y + h, z,        // 3
                x + w, y, z + d,        // 5
                x + w, y + h, z + d,    // 7

                x, y + h, z,            // 2
                x + w, y + h, z,        // 3
                x, y + h, z + d,        // 6
                x + w, y + h, z,        // 3
                x, y + h, z + d,        // 6
                x + w, y + h, z + d,    // 7

                x, y, z,                // 0
                x + w, y, z,            // 1
                x, y, z + d,            // 4
                x + w, y, z,            // 1
                x, y, z + d,            // 4
                x + w, y, z + d         // 5
            };

            vertices = new List<float>()
            {
                // Position          Normal
                -0.5f, -0.5f, -0.5f,  0.0f,  0.0f, -1.0f, // Front face
             0.5f, -0.5f, -0.5f,  0.0f,  0.0f, -1.0f,
             0.5f,  0.5f, -0.5f,  0.0f,  0.0f, -1.0f,
             0.5f,  0.5f, -0.5f,  0.0f,  0.0f, -1.0f,
            -0.5f,  0.5f, -0.5f,  0.0f,  0.0f, -1.0f,
            -0.5f, -0.5f, -0.5f,  0.0f,  0.0f, -1.0f,

            -0.5f, -0.5f,  0.5f,  0.0f,  0.0f,  1.0f, // Back face
             0.5f, -0.5f,  0.5f,  0.0f,  0.0f,  1.0f,
             0.5f,  0.5f,  0.5f,  0.0f,  0.0f,  1.0f,
             0.5f,  0.5f,  0.5f,  0.0f,  0.0f,  1.0f,
            -0.5f,  0.5f,  0.5f,  0.0f,  0.0f,  1.0f,
            -0.5f, -0.5f,  0.5f,  0.0f,  0.0f,  1.0f,

            -0.5f,  0.5f,  0.5f, -1.0f,  0.0f,  0.0f, // Left face
            -0.5f,  0.5f, -0.5f, -1.0f,  0.0f,  0.0f,
            -0.5f, -0.5f, -0.5f, -1.0f,  0.0f,  0.0f,
            -0.5f, -0.5f, -0.5f, -1.0f,  0.0f,  0.0f,
            -0.5f, -0.5f,  0.5f, -1.0f,  0.0f,  0.0f,
            -0.5f,  0.5f,  0.5f, -1.0f,  0.0f,  0.0f,

             0.5f,  0.5f,  0.5f,  1.0f,  0.0f,  0.0f, // Right face
             0.5f,  0.5f, -0.5f,  1.0f,  0.0f,  0.0f,
             0.5f, -0.5f, -0.5f,  1.0f,  0.0f,  0.0f,
             0.5f, -0.5f, -0.5f,  1.0f,  0.0f,  0.0f,
             0.5f, -0.5f,  0.5f,  1.0f,  0.0f,  0.0f,
             0.5f,  0.5f,  0.5f,  1.0f,  0.0f,  0.0f,

            -0.5f, -0.5f, -0.5f,  0.0f, -1.0f,  0.0f, // Bottom face
             0.5f, -0.5f, -0.5f,  0.0f, -1.0f,  0.0f,
             0.5f, -0.5f,  0.5f,  0.0f, -1.0f,  0.0f,
             0.5f, -0.5f,  0.5f,  0.0f, -1.0f,  0.0f,
            -0.5f, -0.5f,  0.5f,  0.0f, -1.0f,  0.0f,
            -0.5f, -0.5f, -0.5f,  0.0f, -1.0f,  0.0f,

            -0.5f,  0.5f, -0.5f,  0.0f,  1.0f,  0.0f, // Top face
             0.5f,  0.5f, -0.5f,  0.0f,  1.0f,  0.0f,
             0.5f,  0.5f,  0.5f,  0.0f,  1.0f,  0.0f,
             0.5f,  0.5f,  0.5f,  0.0f,  1.0f,  0.0f,
            -0.5f,  0.5f,  0.5f,  0.0f,  1.0f,  0.0f,
            -0.5f,  0.5f, -0.5f,  0.0f,  1.0f,  0.0f
        };

            Vertices.AddRange(vertices.Select(n => n / 2f));
            UpdateWindowLists();
        }

    }
}
