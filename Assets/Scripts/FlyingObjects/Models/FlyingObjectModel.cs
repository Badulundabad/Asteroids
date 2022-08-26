using System;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids.FlyingObjects.Models
{
    public abstract class FlyingObjectModel<T> : ScriptableObject where T : FlyingObject
    {
        [SerializeField] protected float maxSpeed;
        [SerializeField] protected GameObject prefab;
        protected BoundsHelper boundsHelper;

        public float MaxSpeed { get => maxSpeed; }
        public GameObject Prefab { get => prefab; }
        public event Action<T, GameObject> OnCollision;
        public event Action<T> OnLeaveBounds;

        public virtual void Tick(T obj)
        {
            obj.Position += obj.MovementDirection * obj.Speed * Time.deltaTime;
            CheckCollision(obj);
            CheckPosition(obj);
        }

        public void SetBoundsHelper(BoundsHelper boundsHelper)
        {
            this.boundsHelper = boundsHelper;
        }

        private void CheckCollision(T obj)
        {
            if (obj.Collider.IsTouchingLayers())
            {
                List<Collider2D> colliders = new List<Collider2D>();
                int count = obj.Collider.GetContacts(colliders);
                for (int i = 0; i < count; i++)
                {
                    Collider2D collider = colliders[i];
                    OnCollision?.Invoke(obj, collider.gameObject);
                }
            }
        }

        private void CheckPosition(T obj)
        {
            if (boundsHelper.IsPositionInBounds(obj.Position)) return;

            OnLeaveBounds?.Invoke(obj);
        }
    }
}