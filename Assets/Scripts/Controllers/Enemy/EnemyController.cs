using Asteroids.Misc;
using Asteroids.Model;
using Asteroids.Services;
using Asteroids.View;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids.Controllers
{
    public class EnemyController : BaseObjectController<EnemyShip>
    {
        private readonly float enemyFrequency = 20f;

        private float lastTimeEnemySpawned;
        private TargetDirectionSetter<EnemyShip> destinationUpdater;
        private GunLoadingChecker<EnemyShip> gunLoadingChecker;
        private SpaceObject target;

        public event Action<SpaceActionEventArgs> OnDestroy;
        public event Action<SpaceActionEventArgs> OnShoot;

        public EnemyController(ISpaceObjectFactory<EnemyShip> factory) : base(factory)
        {
            destinationUpdater = new TargetDirectionSetter<EnemyShip>(objects);
            gunLoadingChecker = new GunLoadingChecker<EnemyShip>(objects);
            gunLoadingChecker.OnGunLoaded += (ship) => OnGunLoaded(ship);
        }

        private void OnGunLoaded(EnemyShip ship)
        {
            Vector2 direction = (target.Position - ship.Position).normalized;
            ship.SetShotTimeNow();
            OnShoot?.Invoke(new SpaceActionEventArgs(ship.Position, direction, Quaternion.identity));
        }

        public override void Start()
        {
            IsRunning = true;
        }

        public override void Update()
        {
            if (!IsRunning) return;

            if (lastTimeEnemySpawned < Time.time - enemyFrequency && objects.Count < 2f)
            {
                SpawnRandomEnemy();
                lastTimeEnemySpawned = Time.time;
            }
            destinationUpdater.Update();
            gunLoadingChecker.Update();
            SpaceObjectTeleporter.TeleportIfLeaveBoundsGroup(objects);
            SpaceObjectMover.MoveGroup(objects);
        }

        public void SetTarget(SpaceObject target)
        {
            this.target = target;
            destinationUpdater.SetTarget(target);
        }

        private void SpawnRandomEnemy()
        {
            Vector2 position = BoundsHelper.GetInBoundsRandomPosition();
            var enemy = factory.Create(position, Vector2.zero, Quaternion.identity, OnCollision);
            objects.Add(enemy);
        }

        private void OnCollision(SpaceObjectView who, GameObject withWhom)
        {
            if (withWhom.tag == Tags.PLAYER || withWhom.tag == Tags.PLAYERAMMO)
            {
                Vector2 position = who.model.Position;
                Vector2 direction = who.model.Velocity;
                Destroy(who.model as EnemyShip);
                OnDestroy?.Invoke(new SpaceActionEventArgs(position, direction, Quaternion.identity));
            }
        }
    }
}