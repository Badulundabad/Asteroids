using Asteroids.Input;
using Asteroids.Misc;
using Asteroids.Model;
using Asteroids.Services;
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

        public bool IsRunning { get; private set; }
        public event Action<Ship> OnPlayerSpawned;
        public event Action<SpaceActionEventArgs> OnDestroy;
        public event Action<SpaceActionEventArgs> OnFireSlot1;
        public event Action<SpaceActionEventArgs> OnFireSlot2;


        public PlayerController(ISpaceObjectFactory<Ship> factory)
        {
            this.factory = factory;
            input = new PlayerInput();
            input.Player.Fire1.performed += (context) => OnFireSlot1?.Invoke(new SpaceActionEventArgs(ship.Position, ship.Rotation * Vector2.up, ship.Rotation));
            input.Player.Fire2.performed += (context) => OnFireSlot2?.Invoke(new SpaceActionEventArgs(ship.Position, ship.Rotation * Vector2.up, Quaternion.identity));
            input.Enable();
            shipMover = new PlayerShipMover();
        }

        public void Start()
        {
            ship = factory.Create(Vector2.zero, Vector2.up, new Quaternion(0, 0, 0, 1), OnCollision);
            shipMover.SetShip(ship);
            OnPlayerSpawned?.Invoke(ship);
            IsRunning = true;
        }

        public void Update()
        {
            if (!IsRunning) return;

            if (input.Player.Move.IsPressed())
                shipMover.Move(input.Player.Move.ReadValue<Vector2>());
            else
                shipMover.Move(Vector3.zero);
        }

        private void OnCollision(SpaceObjectView who, GameObject withWhom)
        {
            if (withWhom.tag == Tags.PLAYERAMMO) return;

            IsRunning = false;
            Vector2 position = who.model.Position;
            Vector2 direction = who.model.Velocity;
            GameObject.Destroy(who.gameObject);
            GameObject.Destroy(ship);
            OnDestroy?.Invoke(new SpaceActionEventArgs(position, direction, Quaternion.identity));
        }
    }
}