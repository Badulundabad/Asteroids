using UnityEngine;

namespace Asteroids.Model.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Player Ship", menuName = "Asteroids/Models/Player Ship")]
    public class PlayerShipData : ShipData
    {
        [SerializeField] private float laserChargeAmount;
        [SerializeField] private float laserChargeTime;

        public float LaserChargeAmount { get => laserChargeAmount; }
        public float LaserChargeTime { get => laserChargeTime; }
    }
}