using Asteroids.FlyingObjects.Controllers;
using Asteroids.FlyingObjects.Misc;
using Asteroids.FlyingObjects.Models;
using Asteroids.Game.Controllers;
using Asteroids.Input;
using UnityEngine;
using Zenject;

namespace Asteroids.Installers
{
    public class GameSceneInstaller : MonoInstaller
    {
        [SerializeField] private PlayerShipModel playerShipModel;
        [SerializeField] private AsteroidModel asteroidModel;
        [SerializeField] private EnemyShipModel enemyModel;
        private BoundsHelper boundsHelper;

        public override void InstallBindings()
        {
            boundsHelper = new BoundsHelper();
            playerShipModel.SetBoundsHelper(boundsHelper);
            asteroidModel.SetBoundsHelper(boundsHelper);
            enemyModel.SetBoundsHelper(boundsHelper);

            Container.Bind<PlayerShipModel>().FromInstance(playerShipModel).AsSingle();
            Container.Bind<AsteroidModel>().FromInstance(asteroidModel).AsSingle();
            Container.Bind<EnemyShipModel>().FromInstance(enemyModel).AsSingle();
            Container.Bind<PlayerInput>().AsSingle();
            Container.Bind<PlayerController>().AsSingle();
            Container.Bind<AsteroidController>().AsSingle();
            Container.Bind<EnemyController>().AsSingle();
            Container.Bind<ITickable>().To<GameController>().AsSingle().NonLazy();
        }
    }
}