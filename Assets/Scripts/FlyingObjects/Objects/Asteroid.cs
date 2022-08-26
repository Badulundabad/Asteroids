using UnityEngine;

namespace Asteroids.FlyingObjects.Models
{
    public class Asteroid : FlyingObject
    {
        public AsteroidType Type { get; private set; }

        public Asteroid(AsteroidType type, float speed, Vector3 pos, Vector3 dir, Quaternion rot) 
                    : base(speed, pos, dir, rot)
        {
            Type = type;
        }
    }

    public enum AsteroidType
    {
        big,
        small
    }
}