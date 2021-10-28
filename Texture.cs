using System;
using System.Collections.Generic;
using System.Text;
using OpenTK;
using OpenTK.Graphics.OpenGL4;
using System.IO;
using System.Drawing;
using System.Diagnostics;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System.Drawing.Imaging;


namespace Engine{


    public class Texture // dont know why this doesnt work....
    {
        int? Handle;
        public Texture(string path) { Create(path); }
        public Texture() { }
        public void Create(string path)
        {
            Handle = GL.GenTexture();
            Use();
            SixLabors.ImageSharp.Image<Rgba32> image = (SixLabors.ImageSharp.Image<Rgba32>)SixLabors.ImageSharp.Image.Load(path);
            image.Mutate(x => x.Flip(FlipMode.Vertical));
            Rgba32[] tempPixels = new Rgba32[image.Width * image.Height];
            for (int i = 0; i < image.Height; i++)
                image.GetPixelRowSpan(i);   //Rgba32[] tempPixels = image.GetPixelSpan().ToArray();
           // Rgba32[] tempPixels = image.GetPixelSpan().ToArray();
            List<byte> pixels = new List<byte>();
            foreach (Rgba32 p in tempPixels)
            {
                pixels.Add(p.R);
                pixels.Add(p.G);
                pixels.Add(p.B);
                pixels.Add(p.A);
            }
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.ClampToEdge);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.ClampToEdge);
            float[] borderColor = { 1.0f, 1.0f, 0.0f, 1.0f };
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureBorderColor, borderColor);
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, image.Width, image.Height, 0, OpenTK.Graphics.OpenGL4.PixelFormat.Rgba, PixelType.UnsignedByte, pixels.ToArray());
            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.NearestMipmapNearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);
        }
        public void Create(Bitmap bitmap)
        {
            Handle = GL.GenTexture();
            //Use();

            GL.Hint(HintTarget.PerspectiveCorrectionHint, HintMode.Nicest);
            BitmapData data = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height),
                ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0,
                OpenTK.Graphics.OpenGL4.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);
            bitmap.UnlockBits(data);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);
        }
        public void Use(TextureUnit unit = TextureUnit.Texture0)
        {
            GL.ActiveTexture(unit);
            GL.BindTexture(TextureTarget.Texture2D, (int)Handle);
        }
        public bool isNull() => Handle == null;
    }
    /*public struct Texture{
            int? Handle;
            public Bitmap image;
            public Texture(string path) { image = null; Handle = GL.GenTexture(); Create(path); }
            public Texture(Bitmap bitmap) { image = bitmap; Handle = GL.GenTexture(); Create(bitmap); }
        void Create(string path) {
            Use();
            SixLabors.ImageSharp.Image<Rgba32> image = (SixLabors.ImageSharp.Image<Rgba32>)SixLabors.ImageSharp.Image.Load(path);
            image.Mutate(x => x.Flip(FlipMode.Vertical));
            Rgba32[] tempPixels = new Rgba32[image.Width * image.Height];
                for (int i = 0; i< image.Height; i++)
                    image.GetPixelRowSpan(i);   //Rgba32[] tempPixels = image.GetPixelSpan().ToArray();
                 List<byte> pixels = new List<byte>();
                foreach (Rgba32 p in tempPixels){
                    pixels.Add(p.R);
                    pixels.Add(p.G);
                    pixels.Add(p.B);
                    pixels.Add(p.A);
                }

                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.ClampToEdge);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.ClampToEdge);
                float[] borderColor = { 1.0f, 1.0f, 0.0f, 1.0f };
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureBorderColor, borderColor);
                GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, image.Width, image.Height, 0, OpenTK.Graphics.OpenGL.PixelFormat.Rgba, PixelType.UnsignedByte, pixels.ToArray());
                GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
            }
            public void Create(Bitmap bitmap){
                Use();
                GL.Hint(HintTarget.PerspectiveCorrectionHint, HintMode.Nicest);
                BitmapData data = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height),
                    ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0,
                    OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);
                bitmap.UnlockBits(data);

                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);
            }
            public bool isNull() => Handle == null;
            public void Use(TextureUnit unit = TextureUnit.Texture0){
                GL.ActiveTexture(unit);
                GL.BindTexture(TextureTarget.Texture2D, (int)Handle);
            }
        }*/
}
