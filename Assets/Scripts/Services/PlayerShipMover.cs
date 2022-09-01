using Asteroids.Model;
using UnityEngine;

namespace Asteroids.Services
{
    public class PlayerShipMover
    {
        private readonly float speedAttenuation = 1f;

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
                float rotationDif = Vector2.Dot(ship.Velocity.normalized, (ship.Rotation * Vector3.up).normalized);
                float speedDif = (ship.MaxSpeed * 1.05f) - ship.Speed;
                Vector3 velocity = Vector3.Slerp(ship.Velocity, ship.Rotation * Vector3.up, speedDif * 0.005f * Time.time);
                ship.SetVelocity(velocity);
                ship.SetSpeed(ship.Speed + (rotationDif * Time.deltaTime));
            }
            else
            {
                ship.SetSpeed(ship.Speed + (-speedAttenuation * Time.deltaTime));
            }

            Vector2 position = ship.Position + (ship.Velocity * ship.Speed * Time.deltaTime);
            ship.SetPosition(position);
        }

        public void SetShip(Ship ship)
        {
            this.ship = ship;
        }
    }
}