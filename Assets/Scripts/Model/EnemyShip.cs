using Asteroids.Model.ScriptableObjects;

namespace Asteroids.Model
{
    public sealed class EnemyShip: Ship
    {
        public SpaceObject Target { get; private set; }

        public EnemyShip(float speed, float maxSpeed, float acceleration, float rotationSpeed, float gunCooldown) 
            : base(speed, maxSpeed, acceleration, rotationSpeed, gunCooldown)
        {
        }

        public EnemyShip(ShipData data) : base(data)
        {
            
        }

        public void SetTarget(SpaceObject target)
        {
            Target = target;
        }
    }
}
