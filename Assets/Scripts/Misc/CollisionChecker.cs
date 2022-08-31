using System;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class CollisionChecker
    {
        private Collider2D collider;
        private Collider2D[] colliders;
        public event Action<GameObject> OnCollision;

        public CollisionChecker(Collider2D collider)
        {
            this.collider = collider;
            colliders = new Collider2D[1];
        }

        public void Tick()
        {
            if (collider.IsTouchingLayers())
            {
                collider.GetContacts(colliders);
                OnCollision?.Invoke(colliders[0].gameObject);
            }
        }
    }
}