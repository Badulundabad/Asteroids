using Asteroids.FlyingObjects.Controllers;
using System.Collections.Generic;
using Zenject;

namespace Asteroids.Game.Controllers
{
    public class GameController : ITickable
    {
        private List<IFlyingObjectController> controllers;

        public GameController(PlayerController player, AsteroidController asteroid, EnemyController enemy)
        {
            controllers = new List<IFlyingObjectController>();            
            controllers.Add(player);
            controllers.Add(asteroid);
            controllers.Add(enemy);
            var target = player.GetPlayer();
            enemy.SetTarget(target);
        }

        public void Tick()
        {
            foreach (var controller in controllers)
            {
                controller.Tick();
            }
        }
    }
}
