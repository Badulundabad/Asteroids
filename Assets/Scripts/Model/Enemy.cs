using UnityEngine;

namespace Asteroids.Model
{
    [CreateAssetMenu(fileName = "New Enemy", menuName = "Asteroids/Models/Enemy")]
    public class Enemy : SpaceObject
    {
        public SpaceObject Target { get; private set; }

        public void SetTarget(SpaceObject target)
        {
            Target = target;
        }
    }
}
