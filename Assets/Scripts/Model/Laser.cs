
using Asteroids.Model.ScriptableObjects;

namespace Asteroids.Model
{
    public sealed class Laser : Ammo
    {
        public float Offset { get; private set; }

        public Laser(float offset, float speed, float rotationSpeed, float maxLifeTime) 
            : base(speed, rotationSpeed, maxLifeTime)
        {
            Offset = offset;
        }
    
        public Laser(LaserData data) : base(data)
        {
            Offset = data.Offset;
        }
    }
}