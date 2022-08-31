using Asteroids.Model;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids.Services
{
    public class SpaceObjectTeleporter
    {
        public static void TeleportIfLeaveBoundsGroup<T>(List<T> objects) where T : SpaceObject
        {
            for (int i = 0; i < objects.Count; i++)
            {
                SpaceObject obj = objects[i];
                TeleportIfLeaveBoundsSingle(obj);
            }
        }

        public static void TeleportIfLeaveBoundsSingle(SpaceObject obj)
        {
            if (BoundsHelper.IsOutsideBounds(obj.Position))
            {
                Vector2 newPosition = BoundsHelper.GetPositionForTeleportation(obj.Position);
                obj.SetPosition(newPosition);
            }
        }
    }
}