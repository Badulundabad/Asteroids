using UnityEngine;

namespace Asteroids.Model
{
    [CreateAssetMenu(fileName = "Space Object Models", menuName = "Asteroids/Lists/Space Object Models")]
    public class SpaceObjectModels : ScriptableObject
    {
        [SerializeField] private SpaceObject asteroidBig;
        [SerializeField] private SpaceObject asteroidSmall;
        [SerializeField] private Ammo bullet;
        [SerializeField] private Ammo enemyAmmo;
        [SerializeField] private Enemy enenmyShip;
        [SerializeField] private Ammo laser;
        [SerializeField] private Ship playerShip;

        public SpaceObject AsteroidBig { get => asteroidBig; }
        public SpaceObject AsteroidSmall { get => asteroidSmall; }
        public Ammo Bullet { get => bullet; }
        public Ammo EnemyAmmo { get => enemyAmmo; }
        public Enemy EnemyShip { get => enenmyShip; }
        public Ammo Laser { get => laser; }
        public Ship PlayerShip { get => playerShip; }
    }
}
