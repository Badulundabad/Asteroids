using Asteroids.Model;
using Asteroids.Services;
using Asteroids.View;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids.Controllers
{
    public abstract class BaseGunController : IController
    {
        private AmmoLifeTimeChecker ammoLifeTimeChecker;
        protected List<Ammo> projectiles;
        protected ISpaceObjectFactory<Ammo> factory;

        public bool IsRunning { get; private set; }


        public BaseGunController(ISpaceObjectFactory<Ammo> factory)
        {
            this.factory = factory;
            projectiles = new List<Ammo>();
            ammoLifeTimeChecker = new AmmoLifeTimeChecker(projectiles);
            ammoLifeTimeChecker.OnLifeTimeExpired += (ammo) => Destroy(ammo);
        }

        public void Start()
        {
            IsRunning = true;
        }

        public virtual void Update()
        {
            if (!IsRunning) return;

            ammoLifeTimeChecker.Check();
            SpaceObjectTeleporter.TeleportIfLeaveBoundsGroup(projectiles);
            SpaceObjectMover.MoveGroup(projectiles);
        }

        public void OnShoot(SpaceActionEventArgs args)
        {
            var projectile = factory.Create(args.position, args.direction, args.rotation, OnCollision);
            projectiles.Add(projectile);
        }

        protected abstract void OnCollision(SpaceObjectView who, GameObject withWhom);

        private void Destroy(Ammo ammo)
        {
            SpaceObjectView view;
            if (factory.TryGetView(ammo, out view))
            {
                GameObject.Destroy(view.gameObject);
                projectiles.Remove(ammo);
            }
        }
    }
}
