using Asteroids.Model;
using Asteroids.Model.ScriptableObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Asteroids.View.Factories
{
    public class LaserFactory : ISpaceObjectFactory<Laser>
    {
        private List<GameObject> prefabs;
        private LaserData model;
        private Dictionary<Laser, SpaceObjectView> objects;
        private GameObject root;

        public LaserFactory(LaserData model, GameObject prefab1, GameObject prefab2 = null, GameObject prefab3 = null)
        {
            this.model = model;
            root = new GameObject("LaserRoot");
            objects = new Dictionary<Laser, SpaceObjectView>();
            prefabs = new List<GameObject>();
            if (prefab1 != null)
                prefabs.Add(prefab1);
            if (prefab2 != null)
                prefabs.Add(prefab2);
            if (prefab3 != null)
                prefabs.Add(prefab3);
        }

        public Laser Create(Vector2 position, Vector2 direction, Quaternion rotation, Action<SpaceObjectView, GameObject> onCollision)
        {
            int rnd = UnityEngine.Random.Range(0, prefabs.Count);
            GameObject instance = GameObject.Instantiate(prefabs[rnd], position, rotation, root.transform);
            SpaceObjectView view = null;
            if (instance.TryGetComponent<SpaceObjectView>(out view))
            {
                var laser = new Laser(model); 
                laser.SetPosition(position);
                laser.SetRotation(rotation);
                laser.SetVelocity(direction);

                view.SetData(laser);
                view.OnCollision += (obj) => onCollision.Invoke(view, obj);
                if (objects.TryAdd(laser, view))
                    return laser;
            }

            GameObject.Destroy(instance);
            return null;
        }

        public bool TryGetView(Laser model, out SpaceObjectView view)
        {
            return objects.TryGetValue(model, out view);
        }

        public void Destroy(Laser obj)
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
            root = new GameObject("LaserRoot");
        }
    }
}