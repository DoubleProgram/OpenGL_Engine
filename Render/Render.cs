using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK.Graphics.OpenGL;
using System.Threading.Tasks;

namespace Engine
{
    public class Render { //I guess the render should be static
        public delegate void Renderer(Object obj = default);
        public Renderer renderer;
        public static List<int> vaos = new List<int>();
        public static List<int> vbos = new List<int>();
        public void RemoveAll(){
            GL.DeleteVertexArrays(vaos.Count, vaos.ToArray());
            GL.DeleteBuffers(vbos.Count, vbos.ToArray());
            vaos = new List<int>();
            vbos = new List<int>();
        }
    }
}
