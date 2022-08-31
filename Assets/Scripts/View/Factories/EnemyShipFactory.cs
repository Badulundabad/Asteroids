using Asteroids.Model;
using Asteroids.Model.ScriptableObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Asteroids.View.Factories
{
    public class EnemyShipFactory : ISpaceObjectFactory<EnemyShip>
    {
        private GameObject prefab;
        private ShipData model;
        private Dictionary<EnemyShip, SpaceObjectView> objects;
        private GameObject root;

        public EnemyShipFactory(ShipData model, GameObject prefab)
        {
            this.model = model;
            this.prefab = prefab;
            root = new GameObject("EnemyShipsRoot");
            objects = new Dictionary<EnemyShip, SpaceObjectView>();
        }

        public EnemyShip Create(Vector2 position, Vector2 direction, Quaternion rotation, Action<SpaceObjectView, GameObject> onCollision)
        {
            GameObject instance = GameObject.Instantiate(prefab, position, rotation, root.transform);
            SpaceObjectView view = null;
            if (instance.TryGetComponent<SpaceObjectView>(out view))
            {
                var ship = new EnemyShip(model.Speed, model.MaxSpeed, model.Acceleration, model.RotationSpeed, model.BulletFiringRate);
                ship.SetPosition(position);
                ship.SetRotation(rotation);
                ship.SetVelocity(direction);

                view.SetData(ship);
                view.OnCollision += (obj) => onCollision.Invoke(view, obj);
                if (objects.TryAdd(ship, view))
                    return ship;
            }

            GameObject.Destroy(instance);
            return null;
        }

        public bool TryGetView(EnemyShip ship, out SpaceObjectView view)
        {
            return objects.TryGetValue(ship, out view);
        }

        public void Destroy(EnemyShip obj)
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
            root = new GameObject("EnemyShipsRoot");
        }
    }
}