using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Engine
{
    public enum SimpleShape { Line,Triangle,Rectangle,Cube,Complex } //Sphere is coming later
    public abstract class Object : IRenderable {
        public PrimitiveType type;
        public Shader shader;
        public VertexData vertexdata;
        public VAO vao;
        public VBO[] vbo;
        public EBO ebo;
        public Vector3 position;
        public SimpleShape simpleShape;
        public abstract Light light { get; }
        public Material material;
        public float size { get; set; } = 1.0f;
        public abstract Camera camera { get; set; }
        public Object ShallowCopy() => (Object)MemberwiseClone();
       // public Object(SimpleShape simpleShape) { this.simpleShape = simpleShape; }
        public VertexData GetSimpleShape(){
            if (!isSimpleObj) { Logger.Console(Log.Warning, "shape is set to COMPLEX"); return new VertexData(); }
                return getSimpleShape();
        }
        public bool isSimpleObj { get { return simpleShape != SimpleShape.Complex; } }
        public virtual bool isRenderable { get => true; set => throw new NotImplementedException(); }
        public abstract void Update(float deltaTime);
        public abstract void Render(Object obj = default);

        private VertexData getSimpleShape(){
            type = PrimitiveType.Triangles;
            float[] vectors(){
                switch (simpleShape){
                    case SimpleShape.Cube:
                        return new float[] {
                        -0.5f, -0.5f, -0.5f,
                         0.5f, -0.5f, -0.5f,
                         0.5f,  0.5f, -0.5f,
                         0.5f,  0.5f, -0.5f,
                        -0.5f,  0.5f, -0.5f,
                        -0.5f, -0.5f, -0.5f,

                        -0.5f, -0.5f,  0.5f,
                         0.5f, -0.5f,  0.5f,
                         0.5f,  0.5f,  0.5f,
                         0.5f,  0.5f,  0.5f,
                        -0.5f,  0.5f,  0.5f,
                        -0.5f, -0.5f,  0.5f,

                        -0.5f,  0.5f,  0.5f,
                        -0.5f,  0.5f, -0.5f,
                        -0.5f, -0.5f, -0.5f,
                        -0.5f, -0.5f, -0.5f,
                        -0.5f, -0.5f,  0.5f,
                        -0.5f,  0.5f,  0.5f,

                         0.5f,  0.5f,  0.5f,
                         0.5f,  0.5f, -0.5f,
                         0.5f, -0.5f, -0.5f,
                         0.5f, -0.5f, -0.5f,
                         0.5f, -0.5f,  0.5f,
                         0.5f,  0.5f,  0.5f,

                        -0.5f, -0.5f, -0.5f,
                         0.5f, -0.5f, -0.5f,
                         0.5f, -0.5f,  0.5f,
                         0.5f, -0.5f,  0.5f,
                        -0.5f, -0.5f,  0.5f,
                        -0.5f, -0.5f, -0.5f,

                        -0.5f,  0.5f, -0.5f,
                         0.5f,  0.5f, -0.5f,
                         0.5f,  0.5f,  0.5f,
                         0.5f,  0.5f,  0.5f,
                        -0.5f,  0.5f,  0.5f,
                        -0.5f,  0.5f, -0.5f };

                    case SimpleShape.Line:
                        type = PrimitiveType.Lines;
                        return new float[] {
                        0.0f, 1.0f, 0.0f,
                        0.0f, -1.0f, 0.0f, }; 
                    case SimpleShape.Triangle:
                        return new float[] {
                        -0.5f, -0.5f, 0.0f,
                        0.5f, -0.5f, 0.0f,
                        0.0f,  0.5f, 0.0f };
                    case SimpleShape.Rectangle:
                        return new float[] {
                            -0.5f, -0.5f, -0.5f,
                             0.5f, -0.5f, -0.5f,
                             0.5f,  0.5f, -0.5f,
                             0.5f,  0.5f, -0.5f,
                            -0.5f,  0.5f, -0.5f,
                            -0.5f, -0.5f, -0.5f,
                        // 0.5f,  0.5f, 0.0f, 
                        // 0.5f, -0.5f, 0.0f, 
                        //-0.5f,  0.5f, 0.0f, 
                        // 0.5f, -0.5f, 0.0f, 
                        //-0.5f, -0.5f, 0.0f, 
                        //-0.5f,  0.5f, 0.0f  
                        };
                    default: return null;
                }
            }
            return new VertexData(vectors());
        }
    }
}