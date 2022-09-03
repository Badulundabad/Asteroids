using Asteroids.Model;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids.Services
{
    public class SpaceObjectMover
    {
        public static void MoveGroup<T>(List<T> objects) where T : SpaceObject
        {
            for (int i = 0; i < objects.Count; i++)
            {
                SpaceObject obj = objects[i];
                MoveSingle(obj);
            }
        }

        public static void MoveSingle(SpaceObject obj)
        {
            Vector2 nextPosition = obj.Position + (obj.Velocity * obj.Speed * Time.deltaTime);
            obj.SetPosition(nextPosition);
        }
    }
}