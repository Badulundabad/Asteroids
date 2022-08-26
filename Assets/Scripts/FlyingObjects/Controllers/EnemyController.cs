using Asteroids.FlyingObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Asteroids.FlyingObjects.Controllers
{
    public class EnemyController : IFlyingObjectController
    {
        private float nextTimeSpawnShip;
        private EnemyShipModel model;
        private Dictionary<GameObject, EnemyShip> ships;
        public event Action OnDestroy;

        public EnemyController(EnemyShipModel model)
        {
            ships = new Dictionary<GameObject, EnemyShip>();
            this.model = model;
            this.model.OnCollision += (asteroid, collidedObj) => OnCollision(asteroid, collidedObj);
            nextTimeSpawnShip = UnityEngine.Random.Range(15f, 25f);
        }

        public void Tick()
        {
            if (ships.Count < 1 && Time.time > nextTimeSpawnShip)
            {
                SpawnShip();
            }
            for (int i = 0; i < ships.Count; i++)
            {
                GameObject view = ships.Keys.ElementAt(i);
                EnemyShip ship = ships[view];
                model.Tick(ship);
                if (view == null) continue;
                view.transform.position = ship.Position;
            }
        }

        public bool DestroyIfExist(GameObject obj)
        {
            return false;
        }

        public void SetTarget(PlayerShip obj)
        {
            model.SetTarget(obj);
        }

        private void SpawnShip()
        {
            var ship = model.CreateShip();
            var view = GameObject.Instantiate(model.Prefab);
            Collider2D collider = view.GetComponent<Collider2D>();
            ship.Collider = collider;
            view.transform.position = ship.Position;
            ships.Add(view, ship);
        }

        private void OnCollision(EnemyShip ship, GameObject collidedObj)
        {
            if (collidedObj.tag == Tags.ASTEROID)
            {
                DestroyShip(ship);
            }
            if (collidedObj.tag == Tags.SHIP && model.IsObjectATarget(collidedObj))
            {
                // Add request to PlayerController.DestroyShip(collidedObj)
                DestroyShip(ship);
            }
            if (collidedObj.tag == Tags.BULLET)
            {
                // Add request to AmmoController.DestroyAmmo(collidedObj)
                DestroyShip(ship);
            }
        }

        private void DestroyShip(EnemyShip ship)
        {
            GameObject view = ship.Collider.gameObject;
            ships.Remove(view);
            GameObject.Destroy(view);
            nextTimeSpawnShip += UnityEngine.Random.Range(15f, 25f);
        }
    }
}