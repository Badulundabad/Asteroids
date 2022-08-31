using Asteroids.Model;
using System;
using UnityEngine;

namespace Asteroids.View
{
    public interface ISpaceObjectFactory<T> where T : SpaceObject
    {
        T Create(Vector2 position, Quaternion rotation, Action<SpaceObjectView, GameObject> onCollision);
    }
}
