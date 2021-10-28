using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Input;

namespace Engine
{
   public enum ProjectionStyle { Ortho,Perspective };
   public class Camera{
        float yaw = -90.0f, pitch = -14.0f;
        const float cameraSpeed = 8.5f;
        public Vector3 Forward;
        public Vector3 position;
        public Matrix4 view;
        public Matrix4 projection;
        public ProjectionStyle projStyle;
        public Camera(Vector3 position, ProjectionStyle projStyle){
            this.projStyle = projStyle;
            Forward = new Vector3(0, 0, -1);
            this.position = position;
          //  InitialYaw = goalYaw = yaw; InitialPitch = goalPitch = pitch;
           // InitialPosition = goalPos = position;
        }
        Key? currentKey; //Create a Input Class...
        public virtual void MoveCamera(KeyboardState input, float deltaTime){
            switch (KeyPressed(input)){
                case Key.Up: position += Forward * cameraSpeed * deltaTime; break;
                case Key.Down: position -= Forward * cameraSpeed * deltaTime; break;
                case Key.Left: position -= Vector3.Normalize(Vector3.Cross(Forward, Vector3.UnitY)) * cameraSpeed * deltaTime; break;
                case Key.Right: position += Vector3.Normalize(Vector3.Cross(Forward, Vector3.UnitY)) * cameraSpeed * deltaTime; break;
                case Key.LShift: position -= Vector3.UnitY * cameraSpeed * deltaTime; break;
                case Key.Space: position += Vector3.UnitY * cameraSpeed * deltaTime; break;
                case Key.Escape: Environment.Exit(0); break;
            }
        }
        private List<Key> availableKeys = new List<Key>() { 
            Key.A,Key.W,Key.D,Key.S,Key.Up,Key.Down,Key.Left,Key.Right,Key.LShift,Key.Space,Key.Escape
        };
        public void AddKey(Key key) => availableKeys.Add(key);
        public Key? KeyPressed(KeyboardState state){
            Key? key = null;
            void DownPressed(Key testkey) { if (state.IsKeyDown(testkey)) key = testkey; }
            if (!state.IsAnyKeyDown) return null;
            for (int i = 0; i < availableKeys.Count; i++)
                DownPressed(availableKeys[i]);
            //Logger.Console(Log.Info, "Key was pressed"+key.GetValueOrDefault().ToString());
            return currentKey = key;
        }
        public void Update(int GameWidth, int GameHeight, float zNear = 0.1f, float zFar = 140){
            Vector3 front = Vector3.Zero;
            if (pitch >= 89.0f) { goalPitch = pitch = 89.0f; };
            if (pitch <= -89.0f) { goalPitch = pitch = -89.0f; };
            front.X = (float)System.Math.Cos(MathHelper.DegreesToRadians(pitch)) * (float)System.Math.Cos(MathHelper.DegreesToRadians(yaw));
            front.Y = (float)System.Math.Sin(MathHelper.DegreesToRadians(pitch));
            front.Z = (float)System.Math.Cos(MathHelper.DegreesToRadians(pitch)) * (float)System.Math.Sin(MathHelper.DegreesToRadians(yaw));
            Forward = Vector3.Normalize(front);
            view = Matrix4.LookAt(position, position + Forward, Vector3.UnitY);
            if(projStyle == ProjectionStyle.Perspective)
                projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45.0f), (float)GameWidth / GameHeight, zNear, zFar);
            else { Matrix4.CreateOrthographic(GameWidth, GameHeight, zNear, zFar); }
        }

        bool firstMouse = true;
        float sensitivity = 0.05f;
        Vector2 lastMousePos = Vector2.Zero;
        public void Rotate(bool isFocused){
            Vector2 mouse = new Vector2(Mouse.GetCursorState().X, Mouse.GetCursorState().Y);
            if (!isFocused) return;
            if (firstMouse){
                lastMousePos = mouse;
                firstMouse = false;
            }
            float deltaX = mouse.X - lastMousePos.X;
            float deltaY = lastMousePos.Y - mouse.Y;
            lastMousePos = mouse;
            ChangeRotation(deltaY * sensitivity, deltaX * sensitivity);

        }
        void ChangeRotation(float pitch, float yaw){
            this.pitch += pitch;
            this.yaw += yaw;
            if (this.pitch >= 89.0f) this.pitch = 89.0f;
            if (this.pitch <= -89.0f) this.pitch = -89.0f;
        }
        #region Additional
            readonly Vector3 InitialPosition;
            readonly float InitialYaw, InitialPitch;
            float goalYaw, goalPitch = 0f;
            Vector3 goalPos = Vector3.Zero;
            public bool MotionDone { 
                get {
                    bool isExpectedVec(Vector3 vec, Vector3 expectedVec) {
                        return  (int)vec.X == (int)expectedVec.X &&
                                (int)vec.Y == (int)expectedVec.Y &&
                                (int)vec.Z == (int)expectedVec.Z;
                    }
                    return isExpectedVec(goalPos,position) && (int)yaw == (int)goalYaw && (int)pitch == (int)goalPitch; 
                } 
            }

            void PerformMotion(int frames){ //TimesPerFrame
                for (int i = 0; i < frames; i++){
                    if ((int)position.Y < (int)goalPos.Y) position.Y += 0.1f;
                    else if ((int)position.Y > (int)goalPos.Y) position.Y -= 0.1f;
                    if ((int)position.X < (int)goalPos.X) position.X += 0.1f;
                    else if ((int)position.X > (int)goalPos.X) position.X -= 0.1f;
                    if ((int)position.Z < (int)goalPos.Z) position.Z += 0.1f;
                    else if ((int)position.Z > (int)goalPos.Z) position.Z -= 0.1f;
                    if ((int)yaw < (int)goalYaw) yaw += 0.1f;
                    else if ((int)yaw > (int)goalYaw) yaw -= 0.1f;
                    if ((int)pitch < (int)goalPitch) pitch += 0.1f;
                    else if ((int)pitch > (int)goalPitch) pitch -= 0.1f;
                }
            }
        #endregion
    }
}