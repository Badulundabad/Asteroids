using Asteroids.Misc;
using Asteroids.Model;
using Asteroids.View;
using System;
using UnityEngine;

namespace Asteroids.Controllers
{
    public class EnemyGunController : BaseGunController
    {
        public event Action<SpaceActionEventArgs> OnDestroy;


        public EnemyGunController(ISpaceObjectFactory<Ammo> factory) : base(factory) { }

        protected override void OnCollision(SpaceObjectView who, GameObject withWhom)
        {
            if (withWhom.tag == Tags.PLAYER || withWhom.tag == Tags.NEUTRAL)
            {
                Vector2 position = who.model.Position;
                Vector2 direction = who.model.Velocity;
                Destroy(who.model as Ammo);
                OnDestroy?.Invoke(new SpaceActionEventArgs(position, direction, Quaternion.identity));
            }
        }
    }
}
