using Asteroids.Input;
using UnityEngine;

namespace Asteroids.Controllers
{
    public class GameController : MonoBehaviour
    {
        private readonly float maxSpeed = 1f;
        private readonly float brakingSpeed = 0.3f;
        private readonly float rotationSpeed = 100f;
        private readonly float velocityChangeMax = 0.003f;
        private float velocityChange;
        private float speed = 0f;
        private PlayerInput input;
        private Vector2 inputVec;
        private Vector3 movementDirection;
        private Vector3 potentialMovementDirection;
        private Vector3 rotation;

        public GameObject player;

        void Start()
        {
            input = new PlayerInput();
            input.Player.Enable();
        }

        void Update()
        {
            if (input.Player.Move.IsPressed())
            {
                HandleInput();
            }
            else
            {
                DecreaseValues();
            }
            Move();
        }

        private void HandleInput()
        {
            inputVec = input.Player.Move.ReadValue<Vector2>();
            inputVec *= Time.deltaTime;

            player.transform.Rotate(Vector3.back, inputVec.x * rotationSpeed);
            rotation = player.transform.rotation * Vector3.up;

            if (inputVec.y > 0)
            {
                if (speed < maxSpeed)
                {
                    movementDirection = potentialMovementDirection;
                }
                else
                {
                    movementDirection = Vector3.Slerp(movementDirection, rotation, velocityChangeMax);
                }
                speed = maxSpeed;
            }
        }

        private void DecreaseValues()
        {
            float newSpeed = speed - (brakingSpeed * Time.deltaTime);
            speed = newSpeed < 0 ? 0 : newSpeed;
            velocityChange = velocityChangeMax / (speed + velocityChangeMax);
            potentialMovementDirection = Vector3.Slerp(movementDirection, rotation, velocityChange);
        }

        private void Move()
        {
            player.transform.position += movementDirection * speed * Time.deltaTime;
        }
    }
}
