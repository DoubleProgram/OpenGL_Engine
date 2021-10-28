using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace Engine
{
    public class ParticleSystem{
        float range;
        float lifeTime = 1f;
        float MaxSize;
        float MinSize;
        uint amount;
        Matrix3 mat3;
        Object[] Particles;
        Vector3[] destinations;
        Game game;
      //  Vector3 TargetRotation;
        public ParticleSystem(float range,uint amount,float lifeTime,Object Initial,Object ParticleInstance, Game game) { //ADD THE ABSTRACT GAME CLASS , or just class... 
            this.range = range;
            this.game = game;
            this.amount = amount;
            this.lifeTime = lifeTime;
            ParticleInstance.position = Initial.position;
            mat3 = new Matrix3(new Vector3(range, 0, 0), new Vector3(0, range, 0), new Vector3(0, 0, range));
            Particles = new Object[amount];
            destinations = new Vector3[amount];
            Particles[0] = ParticleInstance;
           /* for (int i = 0; i < amount; i++) { Particles[i] = ParticleInstance.ShallowCopy();*/ game.GetRenderer += Particles[0].Render; /*}*/
            MaxSize = MinSize = ParticleInstance.size;
        }
        public void SetSize(float minSize, float maxSize){
            MaxSize = maxSize;
            MinSize = minSize;
        }
       // public void SetRotation(Vector3 rotation) { }
        public void Prepare(){
            Random rnd = new Random();
            for (int i = 0; i < amount; i++) {
                Particles[i].size = (float)Math_Helper.Helper.GetRandomNumber(MinSize, MaxSize);
                destinations[i] = new Vector3((float)Math_Helper.Helper.GetRandomNumber(-range, range), (float)Math_Helper.Helper.GetRandomNumber(-range, range),
                    (float)Math_Helper.Helper.GetRandomNumber(-range, range));
                destinations[i] += Particles[i].position;
            }
        }
        float timePassed = 0f;
        public void Update(float delta){
            timePassed += delta;
            for (int i = 0; i < amount; i++) { 
                PerformMotion(2,i);
                Logger.Console(Log.Info,timePassed.ToString());
                if (/*MotionDone(i)||*/  timePassed >= lifeTime) /*Particles[i].isRenderable = false;*/ 
                    game.GetRenderer -= Particles[i].Render; }
        }
        public void Stop() => timePassed = lifeTime;
        
        void PerformMotion(int frames, int index) {
            for (int i = 0; i < frames; i++){
                if ((int)Particles[index].position.Y < (int)destinations[index].Y) 
                    Particles[index].position.Y += 0.1f;
                else if ((int)Particles[index].position.Y > (int)destinations[index].Y) 
                    Particles[index].position.Y -= 0.1f;
                if ((int)Particles[index].position.X < (int)destinations[index].X) 
                    Particles[index].position.X += 0.1f;
                else if ((int)Particles[index].position.X > (int)destinations[index].X) 
                    Particles[index].position.X -= 0.1f;
                if ((int)Particles[index].position.Z < (int)destinations[index].Z) 
                    Particles[index].position.Z += 0.1f;
                else if ((int)Particles[index].position.Z > (int)destinations[index].Z) 
                    Particles[index].position.Z -= 0.1f;
            }
        }   
        public bool MotionDone(int index) { 
            bool isExpectedVec(Vector3 vec, Vector3 expectedVec) {
                return  (int)vec.X == (int)expectedVec.X &&
                        (int)vec.Y == (int)expectedVec.Y &&
                        (int)vec.Z == (int)expectedVec.Z;
            }
            return isExpectedVec(Particles[index].position,destinations[index]); 
        }
    }
}