using Asteroids.Input;
using Asteroids.Misc;
using Asteroids.Model;
using Asteroids.Services;
using Asteroids.View;
using System;
using UnityEngine;

namespace Asteroids.Controllers
{
    public class PlayerController : BaseObjectController<PlayerShip>
    {
        private PlayerInput input;
        private PlayerShipMover shipMover;
        private PlayerShip ship;
        private LaserCharger laserCharger;

        public event Action<PlayerShip> OnPlayerSpawned;
        public event Action<SpaceActionEventArgs> OnDestroy;
        public event Action<SpaceActionEventArgs> OnGunFire;
        public event Action<SpaceActionEventArgs> OnLaserFire;


        public PlayerController(ISpaceObjectFactory<PlayerShip> factory) : base(factory)
        {
            input = new PlayerInput();
            input.Player.Fire1.performed += (context) => OnGunSlotFire();
            input.Player.Fire2.performed += (context) => OnLaserSlotFire();
            input.Enable();
            shipMover = new PlayerShipMover();
        }

        public override void Start()
        {
            ship = factory.Create(Vector2.zero, Vector2.up, new Quaternion(0, 0, 0, 1), OnCollision);
            objects.Add(ship);
            shipMover.SetShip(ship);
            laserCharger = new LaserCharger(ship);
            OnPlayerSpawned?.Invoke(ship);
            IsRunning = true;
        }

        public override void Update()
        {
            if (!IsRunning) return;

            SpaceObjectTeleporter.TeleportIfLeaveBoundsSingle(ship);

            if (input.Player.Move.IsPressed())
                shipMover.Move(input.Player.Move.ReadValue<Vector2>());
            else
                shipMover.Move(Vector3.zero);

            laserCharger.Update();
        }

        private void OnGunSlotFire()
        {
            if (!ship.CanShoot()) return;

            ship.SetShotTimeNow();
            OnGunFire?.Invoke(new SpaceActionEventArgs(ship.Position, ship.Rotation * Vector2.up, ship.Rotation));
        }

        private void OnLaserSlotFire()
        {
            if (ship.CurrentLaserCharges == 0f) return;

            int charges = ship.CurrentLaserCharges - 1;
            ship.SetLaserCharges(charges);
            OnLaserFire?.Invoke(new SpaceActionEventArgs(ship.Position, ship.Rotation * Vector2.up, ship.Rotation));
        }

        private void OnCollision(SpaceObjectView who, GameObject withWhom)
        {
            if (withWhom.tag == Tags.PLAYERAMMO) return;

            IsRunning = false;
            Vector2 position = who.model.Position;
            Vector2 direction = who.model.Velocity;
            Destroy(ship);
            OnDestroy?.Invoke(new SpaceActionEventArgs(position, direction, Quaternion.identity));
        }
    }
}