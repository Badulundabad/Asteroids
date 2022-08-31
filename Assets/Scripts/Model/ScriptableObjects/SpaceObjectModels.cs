using Asteroids.Model.ScriptableObjects;
using UnityEngine;

namespace Asteroids.Model
{
    [CreateAssetMenu(fileName = "Space Object Models", menuName = "Asteroids/Lists/Space Object Models")]
    public class SpaceObjectModels : ScriptableObject
    {
        [SerializeField] private SpaceObjectData asteroidBig;
        [SerializeField] private SpaceObjectData asteroidSmall;
        [SerializeField] private AmmoData bullet;
        [SerializeField] private AmmoData enemyAmmo;
        [SerializeField] private ShipData enenmyShip;
        [SerializeField] private AmmoData laser;
        [SerializeField] private PlayerShipData playerShip;

        public SpaceObjectData AsteroidBig { get => asteroidBig; }
        public SpaceObjectData AsteroidSmall { get => asteroidSmall; }
        public AmmoData Bullet { get => bullet; }
        public AmmoData EnemyAmmo { get => enemyAmmo; }
        public ShipData EnemyShip { get => enenmyShip; }
        public AmmoData Laser { get => laser; }
        public PlayerShipData PlayerShip { get => playerShip; }
    }
}
