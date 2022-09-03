using Asteroids.Model;
using System;
using UnityEngine;

namespace Asteroids.View
{
    public class SpaceObjectView : MonoBehaviour
    {
        public SpaceObject model { get; private set; }
        public event Action<GameObject> OnCollision;

        public void SetData(SpaceObject model)
        {
            this.model = model;
        }

        private void LateUpdate()
        {
            transform.position = model.Position;
            transform.rotation = model.Rotation;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            OnCollision?.Invoke(collision.gameObject);
        }
    }
}
