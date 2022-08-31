using Asteroids.Input;
using Asteroids.Misc;
using Asteroids.Model;
using Asteroids.View;
using System;
using UnityEngine;

namespace Asteroids.Controllers
{
    public class PlayerController : IController
    {
        private PlayerInput input;
        private PlayerShipMover shipMover;
        private Ship ship;
        private ISpaceObjectFactory<Ship> factory;

        public event Action<Ship> OnPlayerSpawned;
        public event Action<Vector2, Vector2> OnDestroy;
        public event Action<Vector2, Vector2> OnShootGun;
        public event Action<Vector2, Vector2> OnShootLaser;


        public PlayerController(ISpaceObjectFactory<Ship> factory)
        {
            this.factory = factory;
            input = new PlayerInput();
            input.Player.Fire1.performed += (context) => OnShootGun?.Invoke(ship.Position, ship.Velocity);
            input.Player.Fire2.performed += (context) => OnShootLaser?.Invoke(ship.Position, ship.Velocity);
            input.Enable();
            shipMover = new PlayerShipMover();
        }

        public void Start()
        {
            ship = factory.Create(Vector2.zero, Quaternion.identity, OnCollision);
            shipMover.SetShip(ship);
            OnPlayerSpawned?.Invoke(ship);
        }

        public void Update()
        {
            if (input.Player.Move.IsPressed())
                shipMover.Move(input.Player.Move.ReadValue<Vector2>());
            else
                shipMover.Move(Vector3.zero);
        }

        private void OnCollision(SpaceObjectView who, GameObject withWhom)
        {
            if (withWhom.tag == Tags.PLAYER) return;
            Vector2 position = who.model.Position;
            Vector2 velocity = who.model.Velocity;            
            GameObject.Destroy(who.gameObject);
            GameObject.Destroy(ship);
            OnDestroy?.Invoke(position, velocity);
        }
    }
}