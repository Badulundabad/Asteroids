using Asteroids.Input;
using System;
using System.Collections.Generic;
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
        private float speed;
        private PlayerInput input;
        private Vector2 inputVec;
        private Vector3 movementDirection;
        private Vector3 potentialMovementDirection;
        private Vector3 rotation;


        private readonly float maxAsteroids = 10f;
        private float lastAsteroidTime;
        private Dictionary<GameObject, Vector3> asteroids;
        private float xBound, yBound;
        private float asteroidSpeed = 0.25f;

        public Camera mainCamera;
        public GameObject player;
        public GameObject asteroidPrefab;

        void Start()
        {
            input = new PlayerInput();
            input.Player.Enable();

            asteroids = new Dictionary<GameObject, Vector3>();

            yBound = Camera.main.orthographicSize;
            xBound = yBound * Screen.width / Screen.height;
            lastAsteroidTime = Time.time;
            HandleAsteroids();
        }

        void Update()
        {
            HandlePlayer();
            foreach (var kvp in asteroids)
            {
                Vector3 pos = kvp.Key.transform.position;
                pos += kvp.Value * asteroidSpeed * Time.deltaTime;
                kvp.Key.transform.position = pos;
            }
        }

        private void HandleAsteroids()
        {
            if (asteroids.Count < maxAsteroids)
            {
                GameObject asteroid = Instantiate(asteroidPrefab);

                Vector3 position = GetBoundRandomPositionByVariant();
                asteroid.transform.position = position;
                Vector3 direction = GetRandomVectorToCenterByVariant(position);

                asteroids.Add(asteroid, direction);
            }
        }

        private float offsetFromBound = 0.5f;
        private float offsetFromCorner = 1f;

        private Vector3 GetRandomVectorToCenterByVariant(Vector3 position)
        {
            float x = UnityEngine.Random.Range(-xBound + offsetFromBound, xBound - offsetFromBound);
            float y = UnityEngine.Random.Range(-yBound + offsetFromBound, yBound - offsetFromBound);

            Vector3 toCenter = new Vector3(x, y, -1) - position;

            return toCenter;
        }

        private Vector3 GetBoundRandomPositionByVariant()
        {
            int num = UnityEngine.Random.Range(0, 3);
            float x, y;
            if (num < 2) // top or bottom variants
            {
                x = UnityEngine.Random.Range(-xBound + offsetFromCorner, xBound - offsetFromCorner);
                y = num == 0 ? yBound + offsetFromBound : -yBound - offsetFromBound;
            }
            else // right or left variants
            {
                y = UnityEngine.Random.Range(-yBound + offsetFromCorner, yBound - offsetFromCorner);
                x = num == 2 ? xBound + offsetFromBound : -xBound - offsetFromBound;
            }
            return new Vector3(x, y, -1f);
        }

        private void HandlePlayer()
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
