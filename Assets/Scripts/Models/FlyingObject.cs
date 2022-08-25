using UnityEngine;

namespace Asteroids.Models
{
    public abstract class FlyingObject
    {
        public float speed { get; protected set; }
        public float maxSpeed { get; protected set; }
        public Vector3 position { get; protected set; }
        public Vector3 movementDirection { get; protected set; }
        public Vector3 rotation { get; protected set; }

        public FlyingObject(float speed, float maxSpeed, Vector3 position, Vector3 movementDirection, Vector3 rotation)
        {
            this.speed = speed;
            this.maxSpeed = maxSpeed;
            this.position = position;
            this.movementDirection = movementDirection;
            this.rotation = rotation;
        }

        public void SetSpeed(float speed)
        {
            this.speed = Mathf.Clamp(speed, 0f, maxSpeed);
        }

        public void SetMaxSpeed(float speed)
        {
            maxSpeed = speed;
        }

        public void SetPosition(Vector3 position)
        {
            this.position = position;
        }

        public void SetMovementDirection(Vector3 vector)
        {
            movementDirection = vector;
            movementDirection.Normalize();
        }

        public void SetRotation(Vector3 vector)
        {
            rotation = vector;
            rotation.Normalize();
        }
    }
}