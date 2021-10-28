using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;
using OpenTK;

namespace Engine
{
    public struct VertexData {
        public string vertexSource;
        public string fragmentSource;
        public float[] vectors;
        public float[] colors;
        public float[] normals;
        public float[] texturecoords;

        public VertexData(float[] vectors){
            this.vectors = vectors;
            colors = null;
            normals = null;
            texturecoords = null;
            vertexSource = "";
            fragmentSource = "";
        }
        public VertexData(float[] vectors, float[] colors):this(vectors) => this.colors = colors;
        public VertexData(float[] vectors, float[] colors, float[] normals) : this(vectors,colors) => this.normals = normals;
        public VertexData(float[] vectors, float[] colors, float[] normals, float[] texturecoords):this(vectors,colors,normals) =>
            this.texturecoords = texturecoords;
        

        /*QUESTIONABLE...*/
        public bool HasVectors { get { return vectors != null; } }
        public bool HasColors { get { return colors != null; } }
        public bool HasNormals { get { return normals != null; } }
        public bool HasTextureCoords { get { return texturecoords != null; } }
    }
}