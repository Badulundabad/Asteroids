using Asteroids.Model;
using Asteroids.Model.ScriptableObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Asteroids.View.Factories
{
    public class AmmoFactory : ISpaceObjectFactory<Ammo>
    {
        private List<GameObject> prefabs;
        private AmmoData model;
        private Dictionary<Ammo, SpaceObjectView> objects;
        private GameObject root;

        public AmmoFactory(AmmoData model, GameObject prefab1, GameObject prefab2 = null, GameObject prefab3 = null)
        {
            this.model = model;
            root = new GameObject("AmmosRoot");
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
            int rnd = UnityEngine.Random.Range(0, prefabs.Count);
            GameObject instance = GameObject.Instantiate(prefabs[rnd], position, rotation, root.transform);
            SpaceObjectView view = null;
            if (instance.TryGetComponent<SpaceObjectView>(out view))
            {
                var ammo = new Ammo(model.Speed, model.RotationSpeed, model.MaxLifeTime); 
                ammo.SetPosition(position);
                ammo.SetRotation(rotation);
                ammo.SetVelocity(direction);

                view.SetData(ammo);
                view.OnCollision += (obj) => onCollision.Invoke(view, obj);
                if (objects.TryAdd(ammo, view))
                    return ammo;
            }

            GameObject.Destroy(instance);
            return null;
        }

        public bool TryGetView(Ammo model, out SpaceObjectView view)
        {
            return objects.TryGetValue(model, out view);
        }

        public void Destroy(Ammo obj)
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
            root = new GameObject("AmmosRoot");
        }
    }
}