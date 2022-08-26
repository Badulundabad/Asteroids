using Asteroids.FlyingObjects.Objects;
using System;
using UnityEngine;

namespace Asteroids.FlyingObjects.Models
{
    [CreateAssetMenu(fileName = "New Enemy Model", menuName = "Asteroids/Enemy Ship Model")]
    public class EnemyShipModel : FlyingObjectModel<EnemyShip>
    {
        [SerializeField] protected GameObject[] ammoPrefabs;
        private PlayerShip target;

        public override void Tick(EnemyShip ship)
        {
            if (target.IsImmortal)
            {
                if (ship.Position != Vector3.zero)
                {
                    ship.MovementDirection = (ship.Position - Vector3.zero).normalized;
                }
                else
                {
                    float x = UnityEngine.Random.Range(-1f, 1f);
                    float y = UnityEngine.Random.Range(-1f, 1f);
                    ship.MovementDirection = new Vector3(x, y);
                }
            }
            else
            {
                ship.MovementDirection = (target.Position - ship.Position).normalized;
            }
            base.Tick(ship);
        }

        public EnemyShip CreateShip()
        {
            Vector3 position = boundsHelper.GetInBoundsRandomPosition();
            return new EnemyShip(MaxSpeed, position);
        }

        public void SetTarget(PlayerShip target)
        {
            this.target = target;
        }

        public bool IsObjectATarget(GameObject obj)
        {
            return target != null && obj == target.Collider.gameObject;
        }
    }
}
