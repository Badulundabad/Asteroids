using UnityEngine;

namespace Asteroids.Model
{
    [CreateAssetMenu(fileName = "New Ship", menuName = "Asteroids/Models/Ship")]
    public class Ship : SpaceObject
    {
        [SerializeField] private float maxSpeed;
        [SerializeField] private float acceleration;
        [SerializeField] private float bulletFiringRate;
        [SerializeField] private float laserChargeAmount;
        [SerializeField] private float laserChargeTime;
        private float currentLaserLoadingTime;

        public float MaxSpeed { get => maxSpeed; }
        public float Acceleration { get => acceleration; }
        public float BulletFiringRate { get => bulletFiringRate; }
        public float LaserChargeAmount { get => laserChargeAmount; }
        public float LaserChargeTime { get => laserChargeTime; }
        public float CurrentLaserLoadingTime { get => currentLaserLoadingTime; }

        public void SetSpeed(float speed)
        {
            Speed = Mathf.Clamp(speed, 0, MaxSpeed);
        }

        public void UpdateLaserLoadingTime(float value)
        {
            currentLaserLoadingTime = Mathf.Clamp(value, 0, laserChargeTime);
        }
    }
}
