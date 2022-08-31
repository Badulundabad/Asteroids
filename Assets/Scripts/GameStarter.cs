using Asteroids.Controllers;
using Asteroids.Model;
using Asteroids.View;
using Asteroids.View.Factories;
using System;
using System.Collections.Generic;
using UnityEngine;

public class GameStarter : MonoBehaviour
{
    // fix angle in view
    // shooting
    // fix space movement
    // check all event subscriptions
    [SerializeField] private UIView uiView;
    [SerializeField] private SpaceObjectModels models;
    [SerializeField] private SpaceObjectPrefabs prefabs;

    private UIController uiController;
    private PlayerController playerController;
    private BaseGunController playerGunController;
    private PlayerLaserController playerLaserController;
    private BigAsteroidController bigAsteroidController;
    private SmallAsteroidController smallAsteroidController;
    private EnemyController enemyController;
    private EnemyGunController enemyAmmoController;

    private List<IController> controllers;

    private void Start()
    {
        InitializeControllers();
        AddControllersToList();
        SubscribeControllers();
    }

    private void InitializeControllers()
    {
        PlayerShipFactory shipFactory = new PlayerShipFactory(models.PlayerShip, prefabs.PlayerShip);
        AmmoFactory playerBulletFactory = new AmmoFactory(models.Bullet, prefabs.Bullet);

        EnemyShipFactory enemyFactory = new EnemyShipFactory(models.EnemyShip, prefabs.EnemyShip);
        AmmoFactory enemyAmmoFactory = new AmmoFactory(models.EnemyAmmo, prefabs.EnemyAmmo1, prefabs.EnemyAmmo2, prefabs.EnemyAmmo3);

        SpaceObjectFactory bigAsteroidFactory = new SpaceObjectFactory(models.AsteroidBig, prefabs.AsteroidBig);
        SpaceObjectFactory smallAsteroidFactory = new SpaceObjectFactory(models.AsteroidSmall, prefabs.AsteroidSmall);

        playerController = new PlayerController(shipFactory);
        playerGunController = new PlayerGunController(playerBulletFactory);
        //playerLaserController = new PlayerLaserController(laserFactory);
        bigAsteroidController = new BigAsteroidController(bigAsteroidFactory);
        smallAsteroidController = new SmallAsteroidController(smallAsteroidFactory);
        enemyController = new EnemyController(enemyFactory);
        enemyAmmoController = new EnemyGunController(enemyAmmoFactory);

        uiController = new UIController(uiView);
    }

    private void AddControllersToList()
    {
        controllers = new List<IController>();
        controllers.Add(uiController);
        controllers.Add(playerController);
        controllers.Add(playerGunController);
        //controllers.Add(playerLaserController);
        controllers.Add(bigAsteroidController);
        controllers.Add(smallAsteroidController);
        controllers.Add(enemyController);
        controllers.Add(enemyAmmoController);
    }

    private void SubscribeControllers()
    {
        playerController.OnPlayerSpawned += (ship) => enemyController.SetTarget(ship);
        playerController.OnPlayerSpawned += (ship) => uiController.UpdateModel(ship);
        playerController.OnDestroy += (position, direction) => uiController.OnPlayerDestroy();
        uiController.OnStartButtonClick += () => OnStartButtonClick();
    }

    private void OnStartButtonClick()
    {
        foreach (var controller in controllers)
        {
            controller.Start();
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
