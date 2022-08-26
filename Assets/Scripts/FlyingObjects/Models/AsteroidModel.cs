using UnityEngine;
using Zenject;

namespace Asteroids.FlyingObjects.Models
{
    [CreateAssetMenu(fileName = "New Asteroid Model", menuName = "Asteroids/Asteroid Model")]
    public class AsteroidModel : FlyingObjectModel<Asteroid>
    {
        [SerializeField] private GameObject smallAsteroidPrefab;

        public GameObject SmallAsteroidPrefab { get => smallAsteroidPrefab; }

        public override void Tick(Asteroid obj)
        {
            base.Tick(obj);
        }

        public Asteroid CreateAsteroid(AsteroidType type)
        {
            Vector3 position = boundsHelper.GetInBoundsRandomPosition();
            Vector3 direction = boundsHelper.GetRandomVectorFromPosition(position);
            float speed = type == AsteroidType.big ? maxSpeed * 0.75f : maxSpeed;
            return new Asteroid(type, speed, position, direction, Quaternion.identity);
        }
    }
}
