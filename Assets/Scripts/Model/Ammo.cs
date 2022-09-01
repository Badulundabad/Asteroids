namespace Asteroids.Model
{
    public sealed class Ammo : SpaceObject
    {
        public float MaxLifeTime { get; private set; }
        public float LaunchTime { get; private set; }

        public Ammo(float spped, float rotationSpeed, float maxLifeTime)
            : base(spped, rotationSpeed)
        {
            MaxLifeTime = maxLifeTime;
        }

        public void SetLaunchTime(float time)
        {
            // Time can be set just once
            if (LaunchTime == 0f && time > 0f)
                LaunchTime = time;
        }
    }
}
