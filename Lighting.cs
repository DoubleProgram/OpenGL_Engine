using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace Engine
{
     public struct Material{
        public readonly Vector3 ambient;
        public readonly Vector3 diffuse;
        public readonly Vector3 specular;
        public readonly float shininess;
        public Material(Vector3 ambient, Vector3 diffuse, Vector3 specular, float shininess){
            this.ambient = ambient;
            this.diffuse = diffuse;
            this.specular = specular;
            this.shininess = shininess;
        }
    }
    public enum LightType { Point, Direction, SpotLight }
    public abstract class Light{
        public abstract LightType lightType { get; }
        public readonly Vector3 diffuse;
        public readonly Vector3 ambient;
        public readonly Vector3 specular;
        public Light(Vector3 ambient, Vector3 diffuse, Vector3 specular) {
            this.ambient = ambient;
            this.diffuse = diffuse;
            this.specular = specular;
        }
    }
    public class PointLight:Light{
        public readonly Vector3 position;
        public readonly float constant;
        public readonly float linear;
        public readonly float quadratic;

        public override LightType lightType => LightType.Point;

        public PointLight(Vector3 ambient, Vector3 diffuse, Vector3 specular,Vector3 position,
            float constant,float linear, float quadratic):base(ambient,diffuse,specular){
            this.position = position;
            this.constant = constant;
            this.linear = linear;
            this.quadratic = quadratic;
        }
        public PointLight(Vector3 ambient, Vector3 diffuse, Vector3 specular, Vector3 position) : base(ambient, diffuse, specular){
            this.position = position;
        }
    }
    public class DirLight:Light{
        public readonly Vector3 direction;
        public override LightType lightType => LightType.Direction;
        public DirLight(Vector3 ambient, Vector3 diffuse, Vector3 specular,Vector3 direction) : base(ambient,diffuse,specular){
            this.direction = direction;
        }
    }
    public class SpotLight:Light{
        public readonly Vector3 position;
        public readonly Vector3 direction;
        public readonly float constant;
        public readonly float linear;
        public readonly float quadratic;
        public readonly float cutOff;
        public override LightType lightType => LightType.SpotLight;
        public SpotLight(Vector3 ambient, Vector3 diffuse, Vector3 specular,Vector3 position,Vector3 direction,
            float constant,float linear, float quadratic):base(ambient,diffuse,specular){
            this.direction = direction;
            this.position = position;
            this.constant = constant;
            this.linear = linear;
            this.quadratic = quadratic;
        }
        public SpotLight(Vector3 ambient, Vector3 diffuse, Vector3 specular, Vector3 position, Vector3 direction) 
            : base(ambient, diffuse, specular){
            this.position = position;
            this.direction = direction;
        }
    }
}
