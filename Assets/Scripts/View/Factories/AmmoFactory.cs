using Asteroids.Model;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids.View.Factories
{
    public class AmmoFactory : ISpaceObjectFactory<Ammo>
    {
        private List<GameObject> prefabs;
        private Ammo model;
        private Dictionary<Ammo, SpaceObjectView> objects;

        public AmmoFactory(Ammo model, GameObject prefab1, GameObject prefab2 = null, GameObject prefab3 = null)
        {
            this.model = model;
            objects = new Dictionary<Ammo, SpaceObjectView>();
            prefabs = new List<GameObject>();
            if (prefab1 != null)
                prefabs.Add(prefab1);
            if (prefab2 != null)
                prefabs.Add(prefab2);
            if (prefab3 != null)
                prefabs.Add(prefab3);
        }

        public Ammo Create(Vector2 position, Vector2 direction, Quaternion rotation, Action<SpaceObjectView, GameObject> onCollision)
        {
            int rnd = UnityEngine.Random.Range(0, prefabs.Count - 1);
            GameObject instance = GameObject.Instantiate(prefabs[rnd], position, rotation);
            SpaceObjectView view = null;
            if (instance.TryGetComponent<SpaceObjectView>(out view))
            {
                var data = GameObject.Instantiate(model);
                data.SetPosition(position);
                data.SetRotation(rotation);
                data.SetVelocity(direction);
                data.RegisterShootTime();

                view.SetData(data);
                view.OnCollision += (obj) => onCollision.Invoke(view, obj);
                if (objects.TryAdd(data, view))
                    return data;

                GameObject.Destroy(data);
            }

            GameObject.Destroy(instance);
            return null;
        }

        public bool TryGetView(Ammo model, out SpaceObjectView view)
        {
            return objects.TryGetValue(model, out view);
        }
    }
}