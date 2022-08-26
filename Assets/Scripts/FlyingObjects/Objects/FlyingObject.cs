using UnityEngine;

namespace Asteroids.FlyingObjects.Objects
{
    public class FlyingObject
    {
        public float Speed { get; set; }
        public Vector3 Position { get; set; }
        public Vector3 MovementDirection { get; set; }
        public Quaternion Rotation { get; set; }
        public Collider2D Collider { get; set; }

        public FlyingObject() { }

        public FlyingObject(float speed, Vector3 position, Vector3 moveDir, Quaternion rotation)
        {
            Speed = speed;
            Position = position;
            MovementDirection = moveDir;
            Rotation = rotation;
        }
    }
}