using Asteroids.Model;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids.Services
{
    public class SpaceObjectRotator
    {
        public static void RotateGroup<T>(List<T> objects) where T : SpaceObject
        {
            for (int i = 0; i < objects.Count; i++)
            {
                SpaceObject obj = objects[i];
                RotateSingle(obj);
            }
        }

        private static void RotateSingle(SpaceObject obj)
        {
            Quaternion rotation = obj.Rotation;
            rotation.eulerAngles += Quaternion.Euler(0, 0, obj.RotationSpeed * Time.deltaTime).eulerAngles;
            obj.SetRotation(rotation);
        }
    }
}