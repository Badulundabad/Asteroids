using Asteroids.FlyingObjects.Models;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Asteroids.FlyingObjects.Controllers
{
    public class AsteroidController : IFlyingObjectController
    {
        private float lastAsteroidSpawnedTime;
        private AsteroidModel model;
        private Dictionary<GameObject, Asteroid> asteroids;

        public AsteroidController(AsteroidModel model)
        {
            asteroids = new Dictionary<GameObject, Asteroid>();
            this.model = model;
            this.model.OnCollision += (asteroid, collidedObj) => OnCollision(asteroid, collidedObj);
            this.model.OnLeaveBounds += (asteroid) => DestroyAsteroid(asteroid);
        }

        public void Tick()
        {
            if (asteroids.Count == 0 || Time.time - lastAsteroidSpawnedTime > 4f)
            {
                SpawnAsteroid(AsteroidType.big);
            }
            for (int i = 0; i < asteroids.Count; i++)
            {
                GameObject view = asteroids.Keys.ElementAt(i);
                Asteroid asteroid = asteroids[view];
                model.Tick(asteroid);
                if (view == null) continue;
                view.transform.position = asteroid.Position;
            }
        }

        public bool DestroyIfExist(GameObject obj)
        {
            bool isExisting = asteroids.ContainsKey(obj);
            if (!isExisting) return false;

            Asteroid asteroid = asteroids[obj];
            DestroyAsteroid(asteroid);
            if(asteroid.Type == AsteroidType.big)
            {
                for (int i = 0; i < 3; i++)
                {
                    SpawnAsteroid(AsteroidType.small);
                }
            }
            return true;
        }

        private void SpawnAsteroid(AsteroidType type)
        {
            var obj = model.CreateAsteroid(type);
            var view = type == AsteroidType.big ? GameObject.Instantiate(model.Prefab)
                                                 : GameObject.Instantiate(model.SmallAsteroidPrefab);

            Collider2D collider = view.GetComponent<Collider2D>();
            obj.Collider = collider;
            view.transform.position = obj.Position;
            asteroids.Add(view, obj);
            lastAsteroidSpawnedTime = Time.time;
        }

        private void OnCollision(Asteroid asteroid, GameObject collidedObj)
        {
            if (collidedObj.tag == Tags.ASTEROID) return;

            if (collidedObj.tag == Tags.SHIP)
            {
                // Add request to ShipController.DestroyShip(collidedObj) if collidedObj is Bullet
            }

            if (collidedObj.tag == Tags.BULLET)
            {
                // Add request to AmmoController.DestroyAmmo(collidedObj) if collidedObj is Bullet
                DestroyAsteroid(asteroid);
                for (int i = 0; i < 3; i++)
                {
                    SpawnAsteroid(AsteroidType.small);
                }
            }
        }

        private void DestroyAsteroid(Asteroid asteroid)
        {
            GameObject view = asteroid.Collider.gameObject;
            asteroids.Remove(view);
            GameObject.Destroy(view);
        }
    }
}