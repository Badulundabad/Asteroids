using Asteroids.Model;
using UnityEngine;

namespace Asteroids.Services
{
    public class PlayerShipMover
    {
        private readonly float speedAttenuation = 0.5f;
        private readonly float angleModifier = 0.3f;

        private Ship ship;

        public void Move(Vector3 input)
        {
            if (ship == null) return;

            input *= Time.deltaTime;
            if (input.x != 0)
            {
                Quaternion rotation = ship.Rotation;
                rotation.eulerAngles += Quaternion.Euler(0, 0, -input.x * ship.RotationSpeed).eulerAngles;
                ship.SetRotation(rotation);
            }

            if (input.y > 0)
            {
                float delta = (ship.MaxSpeed - ship.Speed + angleModifier) * Time.deltaTime;
                Vector3 velocity = Vector3.Slerp(ship.Velocity, ship.Rotation * Vector3.up, delta);
                ship.SetVelocity(velocity);
            }

            float speedDelta = input.y > 0 ? input.y * ship.Acceleration : -speedAttenuation;
            speedDelta *= Time.deltaTime;
            ship.SetSpeed(ship.Speed + speedDelta);

            Vector2 position = ship.Position + (ship.Velocity * ship.Speed * Time.deltaTime);
            ship.SetPosition(position);
        }

        public void SetShip(Ship ship)
        {
            this.ship = ship;
        }
    }
}