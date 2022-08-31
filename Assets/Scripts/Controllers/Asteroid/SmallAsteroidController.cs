using Asteroids.Misc;
using Asteroids.Model;
using Asteroids.Services;
using Asteroids.View;
using System;
using UnityEngine;

namespace Asteroids.Controllers
{
    public class SmallAsteroidController : BaseObjectController<SpaceObject>
    {
        public event Action<SpaceActionEventArgs> OnDestroy;

        public SmallAsteroidController(ISpaceObjectFactory<SpaceObject> factory) : base(factory) { }

        public override void Start()
        {
            IsRunning = true;
        }

        public override void Update()
        {
            SpaceObjectTeleporter.TeleportIfLeaveBoundsGroup(objects);
            SpaceObjectRotator.RotateGroup(objects);
            SpaceObjectMover.MoveGroup(objects);
        }

        public void SpawnAsteroids(Vector2 position)
        {
            for (int i = 0; i < 3; i++)
            {
                Vector2 direction = BoundsHelper.GetRandomInBoundsDirection(position);
                var asteroid = factory.Create(position, direction, Quaternion.identity, OnCollision);
                objects.Add(asteroid);
            }
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
