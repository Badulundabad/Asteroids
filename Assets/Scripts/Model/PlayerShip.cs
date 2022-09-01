using UnityEngine;

namespace Asteroids.Model
{
    public sealed class PlayerShip : Ship
    {
        public float LaserChargeAmount { get; private set; }
        public float LaserChargeTime { get; private set; }
        public float CurrentLaserChargeTime { get; private set; }

        public PlayerShip(float speed, float maxSpeed, float acceleration, float rotationSpeed, float gunCooldown, float laserChargeAmount, float laserChargeTime) 
                    : base(speed, maxSpeed, acceleration, rotationSpeed, gunCooldown)
        {
            LaserChargeAmount = laserChargeAmount;
            LaserChargeTime = laserChargeTime;
        }

        public void UpdateLaserChargeTime(float value)
        {
            CurrentLaserChargeTime = Mathf.Clamp(value, 0, LaserChargeTime);
        }
    }
}