using Asteroids.Model.ScriptableObjects;
using UnityEngine;

namespace Asteroids.Model
{    
    public class SpaceObject
    {       
        public float Speed { get; protected set; }
        public float RotationSpeed { get; protected set; }
        public Vector2 Position { get; protected set; }
        public Vector2 Velocity { get; protected set; }
        public Quaternion Rotation { get; protected set; }
        
        public SpaceObject(float speed, float rotationSpeed)
        {
            Speed = speed;
            RotationSpeed = rotationSpeed;
        }

        public SpaceObject(SpaceObjectData data)
        {
            Speed = data.Speed;
            RotationSpeed = data.RotationSpeed;
        }

        public void SetPosition(Vector2 position)
        {
            Position = position;
        }

        public void SetVelocity(Vector2 velocity)
        {
            Velocity = velocity.normalized;
        }

        public void SetRotation(Quaternion rotation)
        {
            Rotation = rotation;
        }
    }
}