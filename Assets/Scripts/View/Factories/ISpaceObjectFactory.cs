using Asteroids.Model;
using System;
using System.Collections;
using UnityEngine;

namespace Asteroids.View
{
    public interface ISpaceObjectFactory<T> where T : SpaceObject
    {
        T Create(Vector2 position, Vector2 direction, Quaternion rotation, Action<SpaceObjectView, GameObject> onCollision);
        bool TryGetView(T model, out SpaceObjectView view);
        void Destroy(T model);
        void EraseAll();
    }
}
