namespace Asteroids.Model
{
    public class EnemyShip: Ship
    {
        public SpaceObject Target { get; private set; }

        public EnemyShip(float speed, float maxSpeed, float acceleration, float rotationSpeed, float bulletFiringRate) 
            : base(speed, maxSpeed, acceleration, rotationSpeed, bulletFiringRate)
        {
        }

        public void SetTarget(SpaceObject target)
        {
            Target = target;
        }
    }
}
