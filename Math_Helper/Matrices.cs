using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics;

namespace Engine.Math
{
    public static class Matrices{
        public static Matrix4 Transformation(Vector3 rotation, Vector3 scale){
            Matrix4 result = Matrix4.Identity;
            Matrix4 rotate = Matrix4.CreateRotationX(MathHelper.DegreesToRadians(rotation.X)) *
                            Matrix4.CreateRotationY(MathHelper.DegreesToRadians(rotation.Y)) *
                            Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(rotation.Z));
            return result *= rotate * Matrix4.CreateScale(scale);
        }
        public static Matrix4 Transformation(Vector3 scale) => Transformation(Vector3.Zero,scale);
        public static Matrix4 Transformation(float scale) => Transformation(Vector3.Zero, new Vector3(scale,scale,scale));
        public static Matrix4 Transformation(Vector3 rotation,float scale) => Transformation(rotation, new Vector3(scale, scale, scale));
    }
}