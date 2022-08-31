using UnityEngine;

namespace Asteroids.Model
{
    public class Ship : SpaceObject
    {
        public float MaxSpeed { get; protected set; }
        public float Acceleration { get; protected set; }
        public float BulletFiringRate { get; protected set; }

        public Ship(float speed, float maxSpeed, float acceleration, float rotationSpeed, float bulletFiringRate) 
            : base(speed, rotationSpeed)
        {
            MaxSpeed = maxSpeed;
            Acceleration = acceleration;
            BulletFiringRate = bulletFiringRate;
        }

        public void SetSpeed(float speed)
        {
            Speed = Mathf.Clamp(speed, 0, MaxSpeed);
        }
    }
}
