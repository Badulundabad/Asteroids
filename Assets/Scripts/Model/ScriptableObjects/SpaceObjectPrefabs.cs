using UnityEngine;

namespace Asteroids.Model
{
    [CreateAssetMenu(fileName = "Space Object Prefabs", menuName = "Asteroids/Lists/Space Object Prefabs")]
    public class SpaceObjectPrefabs : ScriptableObject
    {
        [SerializeField] private GameObject asteroidBig;
        [SerializeField] private GameObject asteroidSmall;
        [SerializeField] private GameObject bullet;
        [SerializeField] private GameObject enenmyShip;
        [SerializeField] private GameObject laser;
        [SerializeField] private GameObject playerShip;
        [SerializeField] private GameObject enemyAmmo1;
        [SerializeField] private GameObject enemyAmmo2;
        [SerializeField] private GameObject enemyAmmo3;

        public GameObject AsteroidBig { get => asteroidBig; }
        public GameObject AsteroidSmall { get => asteroidSmall; }
        public GameObject Bullet { get => bullet; }
        public GameObject EnemyShip { get => enenmyShip; }
        public GameObject EnemyAmmo1 { get => enemyAmmo1; }
        public GameObject EnemyAmmo2 { get => enemyAmmo2; }
        public GameObject EnemyAmmo3 { get => enemyAmmo3; }
        public GameObject Laser { get => laser; }
        public GameObject PlayerShip { get => playerShip; }
    }
}
