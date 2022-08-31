using UnityEngine;

namespace Asteroids.Model
{
    [CreateAssetMenu(fileName = "New Ammo", menuName = "Asteroids/Models/Ammo")]
    public class Ammo : SpaceObject
    {
        [SerializeField] private float maxLifeTime;

        public float MaxLifeTime { get => maxLifeTime; }
        public float ShootTime { get; private set; }

        public void Init()
        {
            if (ShootTime == 0)
                ShootTime = Time.time;
        }
    }
}
