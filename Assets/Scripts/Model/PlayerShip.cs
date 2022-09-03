using Asteroids.Model.ScriptableObjects;
using UnityEngine;

namespace Asteroids.Model
{
    public sealed class PlayerShip : Ship
    {
        public int MaxLaserCharges { get; private set; }
        public int CurrentLaserCharges { get; private set; }
        public float LaserChargingTime { get; private set; }
        public float CurrentLaserChargingTimer { get; private set; }

        public PlayerShip(float speed, float maxSpeed, float acceleration, float rotationSpeed, float gunCooldown, int laserCharges, int maxLaserCharges,float laserChargingTime) 
                    : base(speed, maxSpeed, acceleration, rotationSpeed, gunCooldown)
        {
            CurrentLaserCharges = laserCharges;
            MaxLaserCharges = maxLaserCharges;
            LaserChargingTime = laserChargingTime;
        }

        public PlayerShip(PlayerShipData data) : base(data)
        {
            CurrentLaserCharges = data.LaserCharges;
            MaxLaserCharges = data.MaxLaserCharges;
            LaserChargingTime = data.LaserChargingTime;
        }

        public void UpdateLaserChargingTime(float value)
        {
            CurrentLaserChargingTimer = Mathf.Clamp(value, 0, LaserChargingTime);
        }

        public void SetLaserCharges(int value)
        {
            CurrentLaserCharges = Mathf.Clamp(value, 0, MaxLaserCharges);
        }
    }
}