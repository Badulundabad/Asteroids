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

        public BaseGunController(ISpaceObjectFactory<Ammo> factory)
        {
            this.factory = factory;
            projectiles = new List<Ammo>();
        }

        public virtual void Update()
        {
            SpaceObjectTeleporter.TeleportIfLeaveBoundsGroup(projectiles);
            SpaceObjectMover.MoveGroup(projectiles);
        }
        
        // fix Quaternion
        public void Shoot(Vector2 position, Vector2 direction)
        {
            var projectile = factory.Create(position, Quaternion.identity, OnCollision);
            projectiles.Add(projectile);
        }

        protected abstract void OnCollision(SpaceObjectView who, GameObject withWhom);
    }
}
