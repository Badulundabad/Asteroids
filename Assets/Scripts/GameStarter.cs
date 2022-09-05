using Asteroids.Audio;
using Asteroids.Controllers;
using Asteroids.Model;
using Asteroids.View;
using Asteroids.View.Factories;
using System.Collections.Generic;
using UnityEngine;

public class GameStarter : MonoBehaviour
{
    [SerializeField] private UIView uiView;
    [SerializeField] private AudioPlayer audioPlayer;
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
        playerController.OnPlayerSpawned -= (ship) => playerLaserController.SetLaserOwner(ship);
        playerController.OnDestroy -= (eventArgs) => uiController.OnPlayerDestroy();
        playerController.OnDestroy -= (eventArgs) => StopAllControllers();
        playerController.OnGunFire -= (eventArgs) => playerGunController.Fire(eventArgs);
        playerController.OnGunFire -= (eventArgs) => audioPlayer.OnGunShot();
        playerController.OnLaserFire -= (eventArgs) => playerLaserController.Fire(eventArgs);

        enemyController.OnGunFire -= (eventArgs) => enemyGunController.Fire(eventArgs);
        enemyController.OnDestroy -= (eventArgs) => audioPlayer.OnExplosion();

        bigAsteroidController.OnDestroy -= (eventArgs) => smallAsteroidController.SpawnAsteroids(eventArgs.position);
        bigAsteroidController.OnDestroy -= (eventArgs) => audioPlayer.OnExplosion();

        smallAsteroidController.OnDestroy -= (eventArgs) => audioPlayer.OnExplosion();

        uiController.OnStartButtonClick -= () => StartAllControllers();
        uiController.OnStartButtonClick -= () => audioPlayer.OnButtonClick();
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
        playerController.OnDestroy += (eventArgs) => StopAllControllers();
        playerController.OnGunFire += (eventArgs) => playerGunController.Fire(eventArgs);
        playerController.OnGunFire += (eventArgs) => audioPlayer.OnGunShot();
        playerController.OnLaserFire += (eventArgs) => playerLaserController.Fire(eventArgs);
        playerController.OnLaserFire += (eventArgs) => audioPlayer.OnLaserShot();

        enemyController.OnGunFire += (eventArgs) => enemyGunController.Fire(eventArgs);
        enemyController.OnDestroy += (eventArgs) => audioPlayer.OnExplosion();

        bigAsteroidController.OnDestroy += (eventArgs) => smallAsteroidController.SpawnAsteroids(eventArgs.position);
        bigAsteroidController.OnDestroy += (eventArgs) => audioPlayer.OnExplosion();

        smallAsteroidController.OnDestroy += (eventArgs) => audioPlayer.OnExplosion();

        uiController.OnStartButtonClick += () => StartAllControllers();
        uiController.OnStartButtonClick += () => audioPlayer.OnButtonClick();
    }

    private void StartAllControllers()
    {
        foreach (var controller in controllers)
        {
            controller.Start();
        }
    }

    private void StopAllControllers()
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
