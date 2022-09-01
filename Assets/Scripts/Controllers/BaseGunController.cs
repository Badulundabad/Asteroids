using Asteroids.Model;
using Asteroids.Services;
using Asteroids.View;
using UnityEngine;

namespace Asteroids.Controllers
{
    public abstract class BaseGunController : BaseObjectController<Ammo>
    {
        private AmmoLifeTimeChecker ammoLifeTimeChecker;


        public BaseGunController(ISpaceObjectFactory<Ammo> factory) : base(factory)
        {
            ammoLifeTimeChecker = new AmmoLifeTimeChecker(objects);
            ammoLifeTimeChecker.OnLifeTimeExpired += (ammo) => Destroy(ammo);
        }

        public override void Start()
        {
            IsRunning = true;
        }

        public override void Update()
        {
            if (!IsRunning) return;

            ammoLifeTimeChecker.Check();
            SpaceObjectTeleporter.TeleportIfLeaveBoundsGroup(objects);
            SpaceObjectMover.MoveGroup(objects);
        }

        public void OnShot(SpaceActionEventArgs args)
        {
            var projectile = factory.Create(args.position, args.direction, args.rotation, OnCollision);
            projectile.SetLaunchTime(Time.time);
            objects.Add(projectile);
        }

        protected abstract void OnCollision(SpaceObjectView who, GameObject withWhom);
    }
}
