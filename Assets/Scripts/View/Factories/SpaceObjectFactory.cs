using Asteroids.Model;
using System;
using UnityEngine;

namespace Asteroids.View.Factories
{
    public class SpaceObjectFactory : ISpaceObjectFactory<SpaceObject>
    {
        private GameObject prefab;
        private SpaceObject model;

        public SpaceObjectFactory(SpaceObject model, GameObject prefab)
        {
            this.model = model;
            this.prefab = prefab;
        }

        public SpaceObject Create(Vector2 position, Quaternion rotation, Action<SpaceObjectView, GameObject> onCollision)
        {
            GameObject instance = GameObject.Instantiate(prefab, position, rotation);
            SpaceObjectView view = null;
            if (instance.TryGetComponent<SpaceObjectView>(out view))
            {
                var data = GameObject.Instantiate(model);
                data.SetPosition(position);
                data.SetRotation(rotation);
                data.SetVelocity(rotation * Vector2.up);

                view.SetData(data);
                view.OnCollision += (obj) => onCollision.Invoke(view, obj);
                return data;
            }

            GameObject.Destroy(instance);
            return null;
        }
    }
}