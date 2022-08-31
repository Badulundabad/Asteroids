using Asteroids.Misc;
using Asteroids.Model;
using Asteroids.Model.Services;
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

        public event Action<Vector2, Vector2> OnDestroy;

        public SmallAsteroidController(ISpaceObjectFactory<SpaceObject> factory)
        {
            this.factory = factory;
            asteroids = new List<SpaceObject>();
        }

        public void Update()
        {
            SpaceObjectTeleporter.TeleportIfLeaveBoundsGroup(asteroids);
            SpaceObjectRotator.RotateGroup(asteroids);
            SpaceObjectMover.MoveGroup(asteroids);
        }

        // correct this to direction with small random deviation
        // and fix Quaternion.identity
        public void Spawn(Vector2 position, Vector2 direction)
        {
            for (int i = 0; i < 3; i++)
            {
                direction = BoundsHelper.GetRandomInBoundsDirection(position);
                var asteroid = factory.Create(position, Quaternion.identity, OnCollision);
                asteroids.Add(asteroid);
            }
        }

        private void OnCollision(SpaceObjectView view, GameObject obj)
        {
            if (obj.tag == Tags.PLAYER)
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
