using Asteroids.Input;
using UnityEngine;

namespace Asteroids.Controllers
{
    public class GameController : MonoBehaviour
    {
        private PlayerInput input;
        public float speed = 0f;
        public float maxSpeed = 1f;
        public float brakingSpeed = 0.3f;
        public float rotationSpeed = 100f;
        public float test;
        public Vector2 inputVec;
        public Vector3 movementDirection;
        public Vector3 rotation;
        public GameObject player;

        void Start ()
        {
            input = new PlayerInput();
            input.Player.Enable();
        }

        void Update()
        {
            if (input.Player.Move.IsPressed())
            {
                ChangeForce();
            }
            Move();
        }

        private void Move()
        {
            float speedDiff = speed - (brakingSpeed * Time.deltaTime);
            speed = Mathf.Clamp(speedDiff, 0, speedDiff);

            test = Mathf.Clamp(test, 0.3f, 1);
            movementDirection = Vector3.Slerp(movementDirection, rotation, test);

            player.transform.position += movementDirection * speed * Time.deltaTime;
        }

        private void ChangeForce()
        {
            inputVec = input.Player.Move.ReadValue<Vector2>();
            inputVec *= Time.deltaTime;

            player.transform.Rotate(Vector3.back, inputVec.x * rotationSpeed);
            rotation = player.transform.rotation * Vector3.up;

            if (inputVec.y > 0)
            {
                movementDirection = Vector3.Slerp(movementDirection, rotation, test);
                speed = maxSpeed;
            }
        }
    }
}
