using UnityEngine;

namespace Asteroids.Model.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Ship", menuName = "Asteroids/Models/Ship")]
    public class ShipData : SpaceObjectData
    {
        [SerializeField] private float maxSpeed;
        [SerializeField] private float acceleration;
        [SerializeField] private float gunCooldown;

        public float MaxSpeed { get => maxSpeed; }
        public float Acceleration { get => acceleration; }
        public float GunCooldown { get => gunCooldown; }
    }
}