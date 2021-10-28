using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Graphics;

namespace Engine
{
    public abstract class Game:GameWindow{
        public static int HEIGHT, WIDTH;
        public static Render render = new Render();
        public Render.Renderer GetRenderer{
            get { return render.renderer; }
            set { render.renderer = value; }
        }
        public Game(int width, int height, GraphicsMode mode, string title) : base(width, height, mode, title) { HEIGHT = height; WIDTH = width; }
        public static Camera camera;
       // protected virtual void base.OnLoad(EventArgs e) { }
        public bool isFocused = true;
        protected override void OnFocusedChanged(EventArgs e){
            isFocused = !isFocused;
            base.OnFocusedChanged(e);
        }
       /* protected virtual void OnUpdateFrame(FrameEventArgs e){
            base.OnUpdateFrame(e);
        }*/
        /* protected virtual void OnRenderFrame(FrameEventArgs e){
             base.OnRenderFrame(e);
         }*/
 //protected virtual void OnClosed(EventArgs e){
 //    base.OnClosed(e);
 //}
        protected override void OnResize(EventArgs e){
            GL.Viewport(0, 0, Width, Height);
            WIDTH = Width; HEIGHT = Height;
            base.OnResize(e);
        }
    }
}