using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenTK_tutorial.letraU
{
    public static class VertexData
    {
        public static readonly float[] Vertices = {
        -0.4f, -0.4f, 0.0f,
         0.4f, -0.4f, 0.0f,
         0.4f, -0.2f, 0.0f,
        -0.4f, -0.2f, 0.0f,

        -0.4f, -0.2f, 0.0f,
        -0.4f,  0.4f, 0.0f,
        -0.2f,  0.4f, 0.0f,
        -0.2f, -0.2f, 0.0f,

         0.2f, -0.2f, 0.0f,
         0.2f,  0.4f, 0.0f,
         0.4f,  0.4f, 0.0f,
         0.4f, -0.2f, 0.0f
    };

        public static readonly int[] Indices = {
        0, 1, 2, 2, 3, 0,
        4, 5, 6, 6, 7, 4,
        8, 9, 10, 10, 11, 8
    };
    }

}
