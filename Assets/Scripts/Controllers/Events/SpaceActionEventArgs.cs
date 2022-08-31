using UnityEngine;

namespace Asteroids.Controllers
{
    public class SpaceActionEventArgs
    {
        public readonly Vector2 position;       
        public readonly Vector2 direction;       
        public readonly Quaternion rotation;    
        
        public SpaceActionEventArgs(Vector2 position, Vector2 direction, Quaternion rotation)
        {
            this.position = position;
            this.direction = direction;
            this.rotation = rotation;
        }
    }
}
