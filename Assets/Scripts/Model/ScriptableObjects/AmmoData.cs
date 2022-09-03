using UnityEngine;

namespace Asteroids.Model.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Ammo", menuName = "Asteroids/Models/Ammo")]
    public class AmmoData : SpaceObjectData
    {
        [SerializeField] private float maxLifeTime;

        public float MaxLifeTime { get => maxLifeTime; }
    }
}