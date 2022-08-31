using Asteroids.Model;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids.Services
{
    public class TargetUpdater
    {
        private SpaceObject target;
        private List<SpaceObject> spaceObjects;

        public TargetUpdater()
        {
            spaceObjects = new List<SpaceObject>();
        }

        public void AddSpaceObject(SpaceObject obj)
        {
            spaceObjects.Add(obj);
        }

        public void SetTarget(SpaceObject target)
        {
            this.target = target;
        }

        public void Update()
        {
            for (int i = 0; i < spaceObjects.Count; i++)
            {
                SpaceObject enemy = spaceObjects[i];
                if (target != null)
                {
                    Vector2 velocity = target.Position - enemy.Position;
                    enemy.SetVelocity(velocity);
                }
            }
        }
    }
}