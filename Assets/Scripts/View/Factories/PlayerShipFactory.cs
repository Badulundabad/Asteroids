using Asteroids.Model;
using Asteroids.Model.ScriptableObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Asteroids.View.Factories
{
    public class PlayerShipFactory : ISpaceObjectFactory<Ship>
    {
        private GameObject prefab;
        private PlayerShipData model;
        private GameObject root;
        private Dictionary<Ship, SpaceObjectView> objects;

        public PlayerShipFactory(PlayerShipData model, GameObject prefab)
        {
            this.model = model;
            this.prefab = prefab;
            root = new GameObject("PlayerShipRoot");
            objects = new Dictionary<Ship, SpaceObjectView>();
        }

        public Ship Create(Vector2 position, Vector2 direction, Quaternion rotation, Action<SpaceObjectView, GameObject> onCollision)
        {
            GameObject instance = GameObject.Instantiate(prefab, position, rotation, root.transform);
            SpaceObjectView view = null;
            if (instance.TryGetComponent<SpaceObjectView>(out view))
            {
                var data = new Ship(model.Speed, model.MaxSpeed, model.Acceleration, model.RotationSpeed, model.BulletFiringRate);
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

        public bool TryGetView(Ship model, out SpaceObjectView view)
        {
            return objects.TryGetValue(model, out view);
        }

        public void Destroy(Ship obj)
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
            root = new GameObject("PlayerShipRoot");
        }
    }
}