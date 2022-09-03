using UnityEngine;

namespace Asteroids.Model.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Laser", menuName = "Asteroids/Models/Laser")]
    public class LaserData : AmmoData
    {
        [SerializeField] private float offset;

        public float Offset { get => offset; }
    }
}