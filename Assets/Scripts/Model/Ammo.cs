namespace Asteroids.Model
{
    public class Ammo : SpaceObject
    {
        public float MaxLifeTime { get; private set; }
        public float LaunchTime { get; private set; }

        public Ammo(float spped, float rotationSpeed, float maxLifeTime, float launchTime) 
            : base(spped, rotationSpeed)
        {
            MaxLifeTime = maxLifeTime;
            LaunchTime = launchTime;
        }
    }
}
