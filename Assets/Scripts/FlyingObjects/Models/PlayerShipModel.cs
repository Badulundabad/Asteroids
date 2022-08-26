using UnityEngine;

namespace Asteroids.FlyingObjects.Models
{
    [CreateAssetMenu(fileName = "New Player Ship Model", menuName = "Asteroids/Player Ship Model")]
    public sealed class PlayerShipModel : FlyingObjectModel<PlayerShip>
    {
        [SerializeField] private float brakingSpeed;
        [SerializeField] private float rotationSpeed;
        [SerializeField] private float velocityChangeMin;
        [SerializeField] private int maxLives;

        public int MaxLives { get => maxLives; }
        public Vector2 Input { get; set; }

        public override void Tick(PlayerShip player)
        {
            if (Input != Vector2.zero)
            {
                HandleInput(player);
            }
            else
            {
                DecreaseValues(player);
            }
            base.Tick(player);
        }

        public PlayerShip CreateShip()
        {
            return new PlayerShip(MaxLives);
        }

        public void OnShipDestroy(PlayerShip ship)
        {
            ship.OnDestroy();
        }

        private void HandleInput(PlayerShip player)
        {
            Input *= Time.deltaTime;
            Quaternion rotation = player.Rotation;
            rotation.eulerAngles += Quaternion.Euler(0, 0, -Input.x * rotationSpeed).eulerAngles;
            player.Rotation = rotation;
            if (Input.y > 0)
            {
                if (player.Speed < maxSpeed)
                {
                    player.MovementDirection = player.PotentialMovementDirection;
                }
                else
                {
                    player.MovementDirection = Vector3.Slerp(player.MovementDirection, rotation * Vector3.up, velocityChangeMin);
                }
                player.Speed = maxSpeed;
            }


            Input = Vector2.zero;
        }

        private void DecreaseValues(PlayerShip player)
        {
            float newSpeed = player.Speed - (brakingSpeed * Time.deltaTime);
            player.Speed = newSpeed < 0 ? 0 : newSpeed;
            float velocityChange = velocityChangeMin / ((player.Speed * 2) + velocityChangeMin);
            player.PotentialMovementDirection = Vector3.Slerp(player.MovementDirection, player.Rotation * Vector3.up, velocityChange);
        }
    }
}