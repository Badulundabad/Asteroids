using Asteroids.Model.ScriptableObjects;

namespace Asteroids.Model
{
    public class Ammo : SpaceObject
    {
        public float MaxLifeTime { get; protected set; }
        public float LaunchTime { get; protected set; }

        public Ammo(float speed, float rotationSpeed, float maxLifeTime)
            : base(speed, rotationSpeed)
        {
            MaxLifeTime = maxLifeTime;
        }

        public Ammo(AmmoData data) : base(data)
        {
            MaxLifeTime = data.MaxLifeTime;
        }

        public void SetLaunchTime(float time)
        {
            // Time can be set just once
            if (LaunchTime == 0f && time > 0f)
                LaunchTime = time;
        }
    }
}
