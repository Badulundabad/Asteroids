using Asteroids.Misc;
using Asteroids.Model;
using Asteroids.Services;
using Asteroids.View;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids.Controllers
{
    public class SmallAsteroidController : IController
    {
        private List<SpaceObject> asteroids;
        private ISpaceObjectFactory<SpaceObject> factory;

        public bool IsRunning { get; private set; }
        public event Action<SpaceActionEventArgs> OnDestroy;

        public SmallAsteroidController(ISpaceObjectFactory<SpaceObject> factory)
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
            SpaceObjectTeleporter.TeleportIfLeaveBoundsGroup(asteroids);
            SpaceObjectRotator.RotateGroup(asteroids);
            SpaceObjectMover.MoveGroup(asteroids);
        }

        public void SpawnAsteroids(Vector2 position)
        {
            for (int i = 0; i < 3; i++)
            {
                Vector2 direction = BoundsHelper.GetRandomInBoundsDirection(position);
                var asteroid = factory.Create(position, direction, Quaternion.identity, OnCollision);
                asteroids.Add(asteroid);
            }
        }

        private void OnCollision(SpaceObjectView view, GameObject obj)
        {
            if (obj.tag == Tags.PLAYERAMMO || obj.tag == Tags.ENEMYAMMO)
            {
                Vector2 position = view.model.Position;
                Vector2 direction = view.model.Velocity;
                asteroids.Remove(view.model);
                GameObject.Destroy(view.gameObject);
                OnDestroy?.Invoke(new SpaceActionEventArgs(position, direction, Quaternion.identity));
            }
        }
    }
}
