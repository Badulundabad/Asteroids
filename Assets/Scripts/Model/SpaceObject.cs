using UnityEngine;

namespace Asteroids.Model
{
    [CreateAssetMenu(fileName = "New Space Object", menuName = "Asteroids/Models/Space Object")]
    public class SpaceObject : ScriptableObject
    {
        [SerializeField] private float speed;
        [SerializeField] private float rotationSpeed;

        public float Speed { get => speed; protected set => speed = value; }
        public float RotationSpeed { get => rotationSpeed; }
        public Vector2 Position { get; protected set; }
        public Vector2 Velocity { get; protected set; }
        public Quaternion Rotation { get; protected set; }

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