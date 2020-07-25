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
        private List<uint> Indices = new List<uint>();

        private WaveSimWindow Window;

        public PrimitiveHelper(WaveSimWindow window)
        {
            Window = window;
        }
        private void UpdateWindowLists()
        {
            Window.Vertices = Vertices;
            Window.Indices = Indices;
        }

        public void ClearPrimitiveLists()
        {
            Vertices.Clear();
            Indices.Clear();
            UpdateWindowLists();
        }

        public void AppendTriangle(List<float> vertices)
        {
            Indices.AddRange(new List<uint>() { 0, 1, 2 }.Select((n) => n + (uint)Vertices.Count / 3));
            Vertices.AddRange(vertices);

            UpdateWindowLists();
        }

        public void AppendSquare(float x, float y, float w, float h)
        {

            List<float> vertices = new List<float>() {
                x, y, 0,
                x + w, y, 0,
                x, y + h, 0,
                x + w, y + h, 0
            };

            Indices.AddRange(new List<uint>() { 0, 1, 2, 1, 2, 3 }.Select(n => n + (uint)Vertices.Count / 3));
            Vertices.AddRange(vertices);
            UpdateWindowLists();
        }

        public void AppendRectPrism(float x, float y, float z, float w, float h, float d)
        {
            List<float> vertices = new List<float>()
            {
                x, y, z,
                x + w, y, z,
                x, y + h, z,
                x + w, y + h, z,
                x, y, z + d,
                x + w, y, z + d,
                x, y + h, z + d,
                x + w, y + h, z + d
            };

            List<uint> indices = new List<uint>()
            {
                0, 1, 2,    // front
                1, 2, 3,
                4, 5, 6,    // back
                5, 6, 7,
                0, 2, 4,    // left
                2, 5, 6,
                1, 3, 5,    // right
                3, 5, 7,
                2, 3, 6,    // top
                3, 6, 7,
                0, 1, 4,    // bottom
                1, 4, 5
            };

            Indices.AddRange(indices.Select(n => n + (uint)Vertices.Count / 3));
            Vertices.AddRange(vertices);
            UpdateWindowLists();
        }

    }
}
