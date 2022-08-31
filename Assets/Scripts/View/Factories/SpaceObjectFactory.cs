using Asteroids.Model;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids.View.Factories
{
    public class SpaceObjectFactory : ISpaceObjectFactory<SpaceObject>
    {
        private GameObject prefab;
        private SpaceObject model;
        private Dictionary<SpaceObject, SpaceObjectView> objects;

        public SpaceObjectFactory(SpaceObject model, GameObject prefab)
        {
            this.model = model;
            this.prefab = prefab;
            objects = new Dictionary<SpaceObject, SpaceObjectView>();
        }

        public SpaceObject Create(Vector2 position, Vector2 direction, Quaternion rotation, Action<SpaceObjectView, GameObject> onCollision)
        {
            GameObject instance = GameObject.Instantiate(prefab, position, rotation);
            SpaceObjectView view = null;
            if (instance.TryGetComponent<SpaceObjectView>(out view))
            {
                var data = GameObject.Instantiate(model);
                data.SetPosition(position);
                data.SetRotation(rotation);
                data.SetVelocity(direction);

                view.SetData(data);
                view.OnCollision += (obj) => onCollision.Invoke(view, obj);
                if (objects.TryAdd(data, view))
                    return data;

                GameObject.Destroy(data);
            }

            GameObject.Destroy(instance);
            return null;
        }

        public bool TryGetView(SpaceObject model, out SpaceObjectView view)
        {
            return objects.TryGetValue(model, out view);
        }
    }
}