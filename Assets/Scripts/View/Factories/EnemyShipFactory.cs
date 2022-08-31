using Asteroids.Model;
using System;
using UnityEngine;

namespace Asteroids.View.Factories
{
    public class EnemyShipFactory : ISpaceObjectFactory<Enemy>
    {
        private GameObject prefab;
        private Enemy model;

        public EnemyShipFactory(Enemy model, GameObject prefab)
        {
            this.model = model;
            this.prefab = prefab;
        }

        public Enemy Create(Vector2 position, Vector2 direction, Quaternion rotation, Action<SpaceObjectView, GameObject> onCollision)
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
                return data;
            }

            GameObject.Destroy(instance);
            return null;
        }
    }
}