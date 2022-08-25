using Asteroids.Input;
using System.Collections.Generic;
using System.Linq;
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
        private GameObject player;
        private GameObject flyingPlate;


        private readonly float maxAsteroids = 10f;
        private float lastAsteroidTime;
        private Dictionary<GameObject, Vector3> asteroids;
        private float xBound, yBound;
        private float asteroidSpeed = 0.5f;

        public Camera mainCamera;
        public GameObject playerPrefab;
        public GameObject asteroidPrefab;
        public GameObject flyingPlatePrefab;


        void Start()
        {
            player = Instantiate(playerPrefab);
            flyingPlate = Instantiate(asteroidPrefab);

            input = new PlayerInput();
            input.Player.Enable();

            asteroids = new Dictionary<GameObject, Vector3>();

            yBound = Camera.main.orthographicSize;
            xBound = yBound * Screen.width / Screen.height;
        }

        void Update()
        {
            HandlePlayer();
            MovePlate();
            if (asteroids.Count < maxAsteroids && Time.time - lastAsteroidTime > 6)
            {
                lastAsteroidTime = Time.time;
                CreateAsteroid();
            }

            MoveAsteroids();
            CheckAsteroidPositions();
        }

        private void MovePlate()
        {
            Vector3 dir = (player.transform.position - flyingPlate.transform.position).normalized;
            flyingPlate.transform.position += dir * maxSpeed * Time.deltaTime;
        }

        private void MoveAsteroids()
        {
            foreach (var kvp in asteroids)
            {
                Vector3 pos = kvp.Key.transform.position;
                pos += kvp.Value * asteroidSpeed * Time.deltaTime;
                kvp.Key.transform.position = pos;
            }
        }

        private void CheckAsteroidPositions()
        {
            for (int i = 0; i < asteroids.Count; i++)
            {
                GameObject asteroid = asteroids.ElementAt(i).Key;
                float x = asteroid.transform.position.x;
                float y = asteroid.transform.position.y;
                bool canBeDestroied = x > xBound + offsetFromBound + 1 ||
                                      x < -xBound - offsetFromBound - 1 ||
                                      y > yBound + offsetFromBound + 1 ||
                                      y < -yBound - offsetFromBound - 1;
                if (canBeDestroied)
                {
                    asteroids.Remove(asteroid);
                    Destroy(asteroid);
                }
            }
        }

        private GameObject CreateAsteroid()
        {
            GameObject asteroid = Instantiate(asteroidPrefab);

            Vector3 position = GetBoundRandomPosition();
            asteroid.transform.position = position;
            Vector3 direction = GetRandomVectorFromPosition(position);

            asteroids.Add(asteroid, direction);
            return asteroid;
        }

        private float offsetFromBound = 1f;
        private float offsetFromCorner = 1f;

        private Vector3 GetRandomVectorFromPosition(Vector3 position)
        {
            float x = Random.Range(-xBound + offsetFromBound, xBound - offsetFromBound);
            float y = Random.Range(-yBound + offsetFromBound, yBound - offsetFromBound);

            return (new Vector3(x, y, -1) - position).normalized;
        }

        private Vector3 GetBoundRandomPosition()
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
            velocityChange = velocityChangeMax / ((speed * 2) + velocityChangeMax);
            potentialMovementDirection = Vector3.Slerp(movementDirection, rotation, velocityChange);
        }

        private void Move()
        {
            player.transform.position += movementDirection * speed * Time.deltaTime;
        }
    }
}
