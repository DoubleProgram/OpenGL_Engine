using System;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace Engine
{
    public class VAO { //maybe consider struct?!
        public int vaoID { get; private set; }
        int attribCount;
        public VAO(int? _vaoID = null) {
            if (_vaoID.HasValue) vaoID = (int)_vaoID; 
            else vaoID = GenerateVAO();
            Render.vaos.Add(vaoID);
            attribCount = 0;
        }
        public int GenerateVAO() => GL.GenVertexArray();
        public void Bind() => GL.BindVertexArray(vaoID);
        public void Unbind() => GL.BindVertexArray(0);
        public void Remove(){
            GL.DeleteVertexArray(vaoID);
            vaoID = 0;
            Render.vbos.Remove(vaoID);
        }
        public void StoreData(VBO vbo){
            vbo.Bind();
            GL.EnableVertexAttribArray(vbo.attrib.HasValue ? (int)vbo.attrib : attribCount);
            GL.VertexAttribPointer(attribCount, vbo.SingledataLength , VertexAttribPointerType.Float,
                            false, vbo.SingledataLength * sizeof(float), vbo.offset * sizeof(float));
            attribCount++;
            vbo.Unbind();
        }
    }
    public class VBO { //maybe consider struct?!
        int vboID;
        public int? attrib;
        public float[] data { get; private set; }
        public int SingledataLength { get; private set; }
        public int offset;
        public VBO(float[] data,int SingledataLength,int offset = 0,int? attrib = null){
            vboID = GL.GenBuffer();
            this.attrib = attrib;
            this.data = data;
            this.offset = offset;
            this.SingledataLength = SingledataLength;
            Render.vbos.Add(vboID);
        }
        public void Bind() {
            GL.BindBuffer(BufferTarget.ArrayBuffer, vboID);
            GL.BufferData(BufferTarget.ArrayBuffer, data.Length * sizeof(float), data, BufferUsageHint.StaticDraw);
        }
        public void Unbind() => GL.BindBuffer(BufferTarget.ArrayBuffer,0);
        public void Remove(){
            GL.DeleteVertexArray(vboID); 
            vboID = 0;
            Render.vbos.Remove(vboID);
        }
    }
    public class EBO { //maybe consider struct?!
        int eboID;
        public uint[] indices;
        public EBO(uint[] indices){
            eboID = GL.GenBuffer();
            this.indices = indices;
            Bind();
            GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(uint), indices, BufferUsageHint.StaticDraw);
        }
        public void Bind() { GL.BindBuffer(BufferTarget.ElementArrayBuffer, eboID); }
        public void Unbind() { GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0); }

        //HAVE TO INCLUDE REMOVE() FUNC....
    }
}