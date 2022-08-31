using Asteroids.Model;
using Asteroids.Model.Services;
using Asteroids.View;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids.Controllers
{
    public abstract class BaseGunController : IController
    {
        protected List<Ammo> projectiles;
        protected ISpaceObjectFactory<Ammo> factory;

        public bool IsRunning { get; private set; }


        public BaseGunController(ISpaceObjectFactory<Ammo> factory)
        {
            this.factory = factory;
            projectiles = new List<Ammo>();
        }

        public void Start()
        {
            IsRunning = true;
        }

        public virtual void Update()
        {
            SpaceObjectTeleporter.TeleportIfLeaveBoundsGroup(projectiles);
            SpaceObjectMover.MoveGroup(projectiles);
        }

        // fix Quaternion
        public void OnShoot(SpaceActionEventArgs args)
        {
            var projectile = factory.Create(args.position, args.direction, args.rotation, OnCollision);
            projectiles.Add(projectile);
        }

        protected abstract void OnCollision(SpaceObjectView who, GameObject withWhom);
    }
}
