using Asteroids.Misc;
using Asteroids.Model;
using Asteroids.Services;
using Asteroids.View;
using System;
using UnityEngine;

namespace Asteroids.Controllers
{
    public class BigAsteroidController : BaseObjectController<SpaceObject>
    {
        private readonly float asteroidFrequency = 3f;

        private float lastTimeAsteroidSpawned;
        public event Action<SpaceActionEventArgs> OnDestroy;


        public BigAsteroidController(ISpaceObjectFactory<SpaceObject> factory) : base(factory) { }

        public override void Start()
        {
            IsRunning = true;
        }

        public override void Update()
        {
            if (!IsRunning) return;

            if (lastTimeAsteroidSpawned < Time.time - asteroidFrequency && objects.Count < 6f)
            {
                SpawnAsteroid();
                lastTimeAsteroidSpawned = Time.time;
            }
            SpaceObjectTeleporter.TeleportIfLeaveBoundsGroup(objects);
            SpaceObjectRotator.RotateGroup(objects);
            SpaceObjectMover.MoveGroup(objects);
        }

        private void SpawnAsteroid()
        {
            Vector2 position = BoundsHelper.GetInBoundsRandomPosition();
            Vector2 direction = BoundsHelper.GetRandomInBoundsDirection(position);
            var asteroid = factory.Create(position, direction, Quaternion.identity, OnCollision);
            objects.Add(asteroid);
        }

        private void OnCollision(SpaceObjectView who, GameObject withWhom)
        {
            if (withWhom.tag == Tags.PLAYERAMMO || withWhom.tag == Tags.ENEMYAMMO)
            {
                Vector2 position = who.model.Position;
                Vector2 direction = who.model.Velocity;
                Destroy(who.model);
                OnDestroy?.Invoke(new SpaceActionEventArgs(position, direction, Quaternion.identity));
            }
        }
    }
}
