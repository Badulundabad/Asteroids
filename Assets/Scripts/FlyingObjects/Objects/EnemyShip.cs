using UnityEngine;

namespace Asteroids.FlyingObjects.Models
{
    public class EnemyShip : FlyingObject
    {
        protected float lastTimeShoot;

        public EnemyShip(float speed, Vector3 position)
        {
            Speed = speed;
            Position = position;
        }

        public void Shoot()
        {
            lastTimeShoot = Time.time;
        }
    }
}