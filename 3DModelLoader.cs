using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;
using OpenTK;
using ObjLoader;
using ObjLoader.Loader.Loaders;
using System.IO;
using ObjLoader.Loader.Data.VertexData;
using ObjLoader.Loader.Data.Elements;

namespace Engine
{
    public class ModelLoader{
        public List<float> vertices = new List<float>();
        public List<float> textures = new List<float>();
        public List<float> normals = new List<float>();
        public List<uint> indices = new List<uint>();
        string modelName;
        string[] Lines;
        public ModelLoader(string modelName) {
            this.modelName = modelName;
            Lines = File.ReadAllLines(modelName);
            for(int i = 0; i < Lines.Length;i++){
                // if (Lines[i].StartsWith("vn ")) { Vertex(i, normals); }
                //  else if (Lines[i].StartsWith("vt ")) { Vertex(i,textures);  }
                /*else*/ if(Lines[i].StartsWith("v ")) { Vertex(i,vertices); }
                else if (Lines[i].StartsWith("f ")) { Face(i); }
            }
        }
        
        public void Face(int LineIndex){
            char[] lineArray = Lines[LineIndex].ToCharArray();
            char currentChar = ' ';
            string currentNumber = "";
            int vertexCount = 0;
            for(int i = 0; i< lineArray.Length;i++){
                currentChar = lineArray[i];
                if (Char.IsDigit(currentChar)) currentNumber+=currentChar;
                else if(!currentNumber.isNull()) {
                    if (vertexCount == 0) {
                        indices.Add(UInt32.Parse(currentNumber)); vertexCount++;
                    }
                    else if (vertexCount == 1) { 
                        indices.Add(UInt32.Parse(currentNumber)); vertexCount++;
                    }
                    else if (vertexCount == 2) { indices.Add(UInt32.Parse(currentNumber)); vertexCount = 0; }
                    currentNumber = "";
                }
            }
            indices.Add(UInt32.Parse(currentNumber));
        }
        public void Vertex(int LineIndex,List<float> Vertex){
            char[] lineArray = Lines[LineIndex].ToCharArray();
            char currentChar = ' ';
            string currentNumber = "";

            for(int i=0; i< lineArray.Length; i++){
                currentChar = lineArray[i];
                if (Char.IsDigit(currentChar) || Char.IsPunctuation(currentChar)) currentNumber += currentChar;
                else if (!currentNumber.isNull()){
                    Vertex.Add(float.Parse(currentNumber));
                    currentNumber = "";
                }
            }
            Vertex.Add(float.Parse(currentNumber));
        }
        public string ModelName { set { modelName = value; } }
    }
    public static class StringHelper{
        public static bool isNull(this string s) => s == "" || s == null;
    }
}