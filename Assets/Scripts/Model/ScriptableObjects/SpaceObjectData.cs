using UnityEngine;

namespace Asteroids.Model.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Space Object", menuName = "Asteroids/Models/Space Object")]
    public class SpaceObjectData : ScriptableObject
    {
        [SerializeField] private float speed;
        [SerializeField] private float rotationSpeed;

        public float Speed { get => speed; }
        public float RotationSpeed { get => rotationSpeed; }
    }
}