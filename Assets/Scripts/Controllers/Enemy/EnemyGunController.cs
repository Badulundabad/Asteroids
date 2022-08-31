using Asteroids.Misc;
using Asteroids.Model;
using Asteroids.View;
using System;
using UnityEngine;

namespace Asteroids.Controllers
{
    public class EnemyGunController : BaseGunController
    {
        public event Action<Vector2, Vector2> OnDestroy;


        public EnemyGunController(ISpaceObjectFactory<Ammo> factory) : base(factory) { }

        protected override void OnCollision(SpaceObjectView view, GameObject obj)
        {
            if (obj.tag == Tags.PLAYER)
            {
                Vector2 position = view.model.Position;
                Vector2 direction = view.model.Velocity;
                projectiles.Remove(view.model as Ammo);
                GameObject.Destroy(view.gameObject);
                OnDestroy?.Invoke(position, direction);
            }
        }
    }
}
