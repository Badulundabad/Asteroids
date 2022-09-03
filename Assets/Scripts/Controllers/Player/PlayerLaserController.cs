using Asteroids.Model;
using Asteroids.Services;
using Asteroids.View;
using UnityEngine;

namespace Asteroids.Controllers
{
    public class PlayerLaserController : BaseObjectController<Laser>
    {
        private SpaceObject owner;
        private AmmoLifeTimeChecker<Laser> lifeTimeChecker;

        public PlayerLaserController(ISpaceObjectFactory<Laser> factory) : base(factory)
        {
            lifeTimeChecker = new AmmoLifeTimeChecker<Laser>(objects);
            lifeTimeChecker.OnLifeTimeExpired += (laser) => Destroy(laser);
        }

        public override void Update()
        {
            if (!IsRunning) return;

            lifeTimeChecker.Check();

            if (owner == null) return;

            foreach (var laser in objects)
            {
                Vector2 rotation = owner.Rotation * Vector3.up;
                Vector2 position = owner.Position + (rotation * laser.Offset);
                laser.SetPosition(position);
                laser.SetRotation(owner.Rotation);
            }
        }

        public void Fire(SpaceActionEventArgs args)
        {
            var projectile = factory.Create(args.position, args.direction, args.rotation, OnCollision);
            projectile.SetLaunchTime(Time.time);
            objects.Add(projectile);
        }

        public void SetLaserOwner(SpaceObject owner)
        {
            this.owner = owner;
        }

        private void OnCollision(SpaceObjectView whp, GameObject withwhom)
        {

        }
    }
}
