using Asteroids.Misc;
using Asteroids.Model;
using Asteroids.Model.Services;
using Asteroids.View;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids.Controllers
{
    public class EnemyController : IController
    {
        private readonly float enemyFrequency = 20f;

        private float lastTimeEnemySpawned;
        private List<Enemy> enemies;
        private ISpaceObjectFactory<Enemy> factory;
        private TargetUpdater targetUpdater;

        public bool IsRunning { get; private set; }
        public event Action<SpaceActionEventArgs> OnDestroy;


        public EnemyController(ISpaceObjectFactory<Enemy> factory)
        {
            this.factory = factory;
            enemies = new List<Enemy>();
            targetUpdater = new TargetUpdater();
        }

        public void Start()
        {
            IsRunning = true;
        }

        public void Update()
        {
            if (lastTimeEnemySpawned < Time.time - enemyFrequency && enemies.Count < 6f)
            {
                SpawnRandomEnemy();
                lastTimeEnemySpawned = Time.time;
            }

            SpaceObjectTeleporter.TeleportIfLeaveBoundsGroup(enemies);
            SpaceObjectMover.MoveGroup(enemies);
        }

        public void SetTarget(SpaceObject target)
        {
            targetUpdater.SetTarget(target);
        }

        private void SpawnRandomEnemy()
        {
            Vector2 position = BoundsHelper.GetInBoundsRandomPosition();
            var enemy = factory.Create(position, Vector2.zero, Quaternion.identity, OnCollision);
            enemies.Add(enemy);
            targetUpdater.AddSpaceObject(enemy);
        }

        private void OnCollision(SpaceObjectView view, GameObject obj)
        {
            if (obj.tag == Tags.PLAYER || obj.tag == Tags.PLAYERAMMO)
            {
                Vector2 position = view.model.Position;
                Vector2 direction = view.model.Velocity;
                enemies.Remove(view.model as Enemy);
                GameObject.Destroy(view.gameObject);
                OnDestroy?.Invoke(new SpaceActionEventArgs(position, direction, Quaternion.identity));
            }
        }
    }
}