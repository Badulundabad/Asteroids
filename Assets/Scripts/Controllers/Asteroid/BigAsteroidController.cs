using Asteroids.Misc;
using Asteroids.Model;
using Asteroids.Model.Services;
using Asteroids.View;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids.Controllers
{
    public class BigAsteroidController : IController
    {
        private readonly float asteroidFrequency = 3f;

        private float lastTimeAsteroidSpawned;
        private ISpaceObjectFactory<SpaceObject> factory;
        private List<SpaceObject> asteroids;

        public bool IsRunning { get; private set; }
        public event Action<Vector2, Vector2> OnDestroy;


        public BigAsteroidController(ISpaceObjectFactory<SpaceObject> factory)
        {
            this.factory = factory;
            asteroids = new List<SpaceObject>();
        }

        public void Start()
        {
            IsRunning = true;
        }

        public void Update()
        {
            if (!IsRunning) return;

            if (lastTimeAsteroidSpawned < Time.time - asteroidFrequency && asteroids.Count < 6f)
            {
                SpawnAsteroid();
                lastTimeAsteroidSpawned = Time.time;
            }
            SpaceObjectTeleporter.TeleportIfLeaveBoundsGroup(asteroids);
            SpaceObjectRotator.RotateGroup(asteroids);
            SpaceObjectMover.MoveGroup(asteroids);
        }

        // fix this Quaternion.identity
        private void SpawnAsteroid()
        {
            Vector2 position = BoundsHelper.GetInBoundsRandomPosition();
            Vector2 direction = BoundsHelper.GetRandomInBoundsDirection(position);
            var asteroid = factory.Create(position, Quaternion.identity, OnCollision);
            asteroids.Add(asteroid);
        }

        private void OnCollision(SpaceObjectView view, GameObject obj)
        {
            if (obj.tag == Tags.PLAYERAMMO || obj.tag == Tags.ENEMYAMMO)
            {
                Vector2 position = view.model.Position;
                Vector2 direction = view.model.Velocity;
                asteroids.Remove(view.model);
                GameObject.Destroy(view.gameObject);
                OnDestroy?.Invoke(position, direction);
            }
        }
    }
}
