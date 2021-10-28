using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;
using OpenTK;
namespace Engine
{
    public class Shader{
        int Handle;
        int vertexShader, fragmentShader;
        public Shader(string vertexsource, string fragmentsource){
            vertexShader = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(vertexShader, vertexsource);
            fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(fragmentShader, fragmentsource);

            GL.CompileShader(vertexShader);
            string infoLog = GL.GetShaderInfoLog(vertexShader);
            if (infoLog != String.Empty) Logger.Console(Log.Error, infoLog);
            GL.CompileShader(fragmentShader);
            infoLog = GL.GetShaderInfoLog(fragmentShader);
            if (infoLog != String.Empty) Logger.Console(Log.Error, infoLog);

            Handle = GL.CreateProgram();

            GL.AttachShader(Handle, vertexShader);
            GL.AttachShader(Handle, fragmentShader);
            GL.LinkProgram(Handle);

            GL.DetachShader(Handle, vertexShader);
            GL.DetachShader(Handle, fragmentShader);
            GL.DeleteShader(vertexShader);
            GL.DeleteShader(fragmentShader);
        }
        public int GetUniformLocation(string name) => GL.GetUniformLocation(Handle, name);
        public void SetVectorToUniform(string name, Vector3 vector){
            int location = GetUniformLocation(name);
            GL.Uniform3(location, vector);
        }
        public void SetVectorToUniform(string name, Vector4 vector){
            int location = GetUniformLocation(name);
            GL.Uniform4(location, vector);
        }
        public void SetMatrix4(ref Matrix4 matrix, string name){
            int location = GL.GetUniformLocation(Handle, name);
            GL.UniformMatrix4(location, false, ref matrix);
        }
        public void SetInt(string name, int value){
            int location = GL.GetUniformLocation(Handle, name);
            GL.Uniform1(location, value);
        }
        public void SetFloat(string name, float value){
            int location = GL.GetUniformLocation(Handle, name);
            GL.Uniform1(location, value);
        }
        public void Use() => GL.UseProgram(Handle);
    }
}
