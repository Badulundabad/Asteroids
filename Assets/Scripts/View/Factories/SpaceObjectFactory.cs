using Asteroids.Model;
using Asteroids.Model.ScriptableObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Asteroids.View.Factories
{
    public class SpaceObjectFactory : ISpaceObjectFactory<SpaceObject>
    {
        private GameObject prefab;
        private SpaceObjectData model;
        private Dictionary<SpaceObject, SpaceObjectView> objects;
        private GameObject root;

        public SpaceObjectFactory(SpaceObjectData model, GameObject prefab)
        {
            this.model = model;
            this.prefab = prefab;
            root = new GameObject("AsteroidsRoot");
            objects = new Dictionary<SpaceObject, SpaceObjectView>();
        }

        public SpaceObject Create(Vector2 position, Vector2 direction, Quaternion rotation, Action<SpaceObjectView, GameObject> onCollision)
        {
            GameObject instance = GameObject.Instantiate(prefab, position, rotation, root.transform);
            SpaceObjectView view = null;
            if (instance.TryGetComponent<SpaceObjectView>(out view))
            {
                var data = new SpaceObject(model.Speed, model.RotationSpeed);
                data.SetPosition(position);
                data.SetRotation(rotation);
                data.SetVelocity(direction);

                view.SetData(data);
                view.OnCollision += (obj) => onCollision.Invoke(view, obj);
                if (objects.TryAdd(data, view))
                    return data;
            }

            GameObject.Destroy(instance);
            return null;
        }

        public bool TryGetView(SpaceObject model, out SpaceObjectView view)
        {
            return objects.TryGetValue(model, out view);
        }

        public void Destroy(SpaceObject obj)
        {
            SpaceObjectView view;
            if (objects.TryGetValue(obj, out view))
            {
                objects.Remove(obj);
                GameObject.Destroy(view.gameObject);
            }
        }

        public void EraseAll()
        {
            for (int i = 0; i < objects.Count; i++)
            {
                var model = objects.Keys.ElementAt(i);
                objects.Remove(model);
            }
            GameObject.Destroy(root);
            root = new GameObject("AsteroidsRoot");
        }
    }
}