using Asteroids.Model;
using System;
using System.Collections.Generic;

namespace Asteroids.Services
{
    public class GunLoadingChecker<T> where T : Ship
    {
        private List<T> ships;
        public event Action<T> OnGunLoaded;

        public GunLoadingChecker(List<T> ships)
        {
            this.ships = ships;
        }

        public void Update()
        {
            for (int i = 0; i < ships.Count; i++)
            {
                T ship = ships[i];
                if (ship.CanShoot())
                {
                    OnGunLoaded?.Invoke(ship);
                }
            }
        }
    }
}