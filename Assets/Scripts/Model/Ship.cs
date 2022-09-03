using Asteroids.Model.ScriptableObjects;
using UnityEngine;

namespace Asteroids.Model
{
    public class Ship : SpaceObject
    {
        public float MaxSpeed { get; protected set; }
        public float Acceleration { get; protected set; }
        public float GunCooldown { get; protected set; }
        public float LastShotTime { get; protected set; }

        public Ship(float speed, float maxSpeed, float acceleration, float rotationSpeed, float gunCooldown)
            : base(speed, rotationSpeed)
        {
            MaxSpeed = maxSpeed;
            Acceleration = acceleration;
            GunCooldown = gunCooldown;
        }

        public Ship(ShipData data) : base(data)
        {
            MaxSpeed = data.MaxSpeed;
            Acceleration = data.Acceleration;
            GunCooldown = data.GunCooldown;
        }

        public void SetSpeed(float speed)
        {
            Speed = Mathf.Clamp(speed, 0, MaxSpeed);
        }

        public bool CanShoot()
        {
            return LastShotTime < Time.time - GunCooldown;
        } 
    
        public void SetShotTimeNow()
        {
            LastShotTime = Time.time;
        }
    }
}
