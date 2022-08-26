using UnityEngine;

namespace Asteroids.FlyingObjects.Models
{
    public class PlayerShip : FlyingObject
    {
        public bool IsImmortal { get; private set; }
        public int Lives { get; private set; }
        public Vector3 PotentialMovementDirection { get; set; }

        public PlayerShip(int lives)
        {
            Lives = lives;
            IsImmortal = false;
        }

        public void OnDestroy()
        {
            Lives--;
            IsImmortal = true;
            Speed = 0;
            Position = Vector3.zero;
            Rotation = Quaternion.identity;
            PotentialMovementDirection = Vector3.zero;
        }

        public void SetMortal()
        {
            IsImmortal = false;
        }
    }
}