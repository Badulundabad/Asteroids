using UnityEngine;

namespace Asteroids.Model.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Player Ship", menuName = "Asteroids/Models/Player Ship")]
    public class PlayerShipData : ShipData
    {
        [SerializeField] private int laserCharges;
        [SerializeField] private int maxLaserCharges;
        [SerializeField] private float laserChargingTime;

        public int LaserCharges { get => laserCharges; }
        public int MaxLaserCharges { get => maxLaserCharges; }
        public float LaserChargingTime { get => laserChargingTime; }
    }
}