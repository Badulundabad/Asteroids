using Asteroids.Controllers;
using Asteroids.Model;
using Asteroids.View;
using Asteroids.View.Factories;
using System.Collections.Generic;
using UnityEngine;

public class GameStarter : MonoBehaviour
{
    [SerializeField] private UIView uiView;
    [SerializeField] private SpaceObjectModels models;
    [SerializeField] private SpaceObjectPrefabs prefabs;

    private UIController uiController;
    private PlayerController playerController;
    private BaseBulletsController playerGunController;
    private PlayerLaserController playerLaserController;
    private BigAsteroidController bigAsteroidController;
    private SmallAsteroidController smallAsteroidController;
    private EnemyController enemyController;
    private EnemyBulletsController enemyGunController;

    private List<IController> controllers;

    private void Start()
    {
        InitializeControllers();
        AddControllersToList();
        SubscribeControllers();
    }


    private void OnDestroy()
    {
        playerController.OnPlayerSpawned -= (ship) => enemyController.SetTarget(ship);
        playerController.OnPlayerSpawned -= (ship) => uiController.UpdateModel(ship);
        playerController.OnDestroy -= (eventArgs) => uiController.OnPlayerDestroy();
        playerController.OnDestroy -= (eventArgs) => OnPlayerDestroy();
        playerController.OnGunFire -= (eventArgs) => playerGunController.Fire(eventArgs);
        enemyController.OnGunFire -= (eventArgs) => enemyGunController.Fire(eventArgs);
        bigAsteroidController.OnDestroy -= (eventArgs) => smallAsteroidController.SpawnAsteroids(eventArgs.position);
        uiController.OnStartButtonClick -= () => OnStartButtonClick();
    }

    private void InitializeControllers()
    {
        PlayerShipFactory shipFactory = new PlayerShipFactory(models.PlayerShip, prefabs.PlayerShip);
        AmmoFactory playerBulletFactory = new AmmoFactory(models.Bullet, prefabs.Bullet);
        LaserFactory laserFactory = new LaserFactory(models.Laser, prefabs.Laser);

        EnemyShipFactory enemyFactory = new EnemyShipFactory(models.EnemyShip, prefabs.EnemyShip);
        AmmoFactory enemyAmmoFactory = new AmmoFactory(models.EnemyAmmo, prefabs.EnemyAmmo1, prefabs.EnemyAmmo2, prefabs.EnemyAmmo3);

        SpaceObjectFactory bigAsteroidFactory = new SpaceObjectFactory(models.AsteroidBig, prefabs.AsteroidBig);
        SpaceObjectFactory smallAsteroidFactory = new SpaceObjectFactory(models.AsteroidSmall, prefabs.AsteroidSmall);

        playerController = new PlayerController(shipFactory);
        playerGunController = new PlayerBulletsController(playerBulletFactory);
        playerLaserController = new PlayerLaserController(laserFactory);
        bigAsteroidController = new BigAsteroidController(bigAsteroidFactory);
        smallAsteroidController = new SmallAsteroidController(smallAsteroidFactory);
        enemyController = new EnemyController(enemyFactory);
        enemyGunController = new EnemyBulletsController(enemyAmmoFactory);

        uiController = new UIController(uiView);
    }

    private void AddControllersToList()
    {
        controllers = new List<IController>();
        controllers.Add(uiController);
        controllers.Add(playerController);
        controllers.Add(playerGunController);
        controllers.Add(playerLaserController);
        controllers.Add(bigAsteroidController);
        controllers.Add(smallAsteroidController);
        controllers.Add(enemyController);
        controllers.Add(enemyGunController);
    }

    private void SubscribeControllers()
    {
        playerController.OnPlayerSpawned += (ship) => enemyController.SetTarget(ship);
        playerController.OnPlayerSpawned += (ship) => uiController.UpdateModel(ship);
        playerController.OnPlayerSpawned += (ship) => playerLaserController.SetLaserOwner(ship);
        playerController.OnDestroy += (eventArgs) => uiController.OnPlayerDestroy();
        playerController.OnDestroy += (eventArgs) => OnPlayerDestroy();
        playerController.OnGunFire += (eventArgs) => playerGunController.Fire(eventArgs);
        playerController.OnLaserFire += (eventArgs) => playerLaserController.Fire(eventArgs);
        enemyController.OnGunFire += (eventArgs) => enemyGunController.Fire(eventArgs);
        bigAsteroidController.OnDestroy += (eventArgs) => smallAsteroidController.SpawnAsteroids(eventArgs.position);
        uiController.OnStartButtonClick += () => OnStartButtonClick();
    }

    private void OnStartButtonClick()
    {
        foreach (var controller in controllers)
        {
            controller.Start();
        }
    }

    private void OnPlayerDestroy()
    {
        foreach (var controller in controllers)
        {
            controller.Stop();
        }
    }

    private void Update()
    {
        foreach (var controller in controllers)
        {
            controller.Update();
        }
    }
}
