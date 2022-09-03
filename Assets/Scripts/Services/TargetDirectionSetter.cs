using Asteroids.Model;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids.Services
{
    public class TargetDirectionSetter<T> where T : SpaceObject
    {
        private SpaceObject target;
        private List<T> spaceObjects;

        public TargetDirectionSetter(List<T> objects)
        {
            spaceObjects = objects;
        }

        public void SetTarget(SpaceObject target)
        {
            this.target = target;
        }

        public void Update()
        {
            if (target == null) return;

            for (int i = 0; i < spaceObjects.Count; i++)
            {
                SpaceObject enemy = spaceObjects[i];
                Vector2 velocity = target.Position - enemy.Position;
                enemy.SetVelocity(velocity);
            }
        }
    }
}