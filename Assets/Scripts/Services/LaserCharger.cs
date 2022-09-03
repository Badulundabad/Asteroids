using Asteroids.Model;
using UnityEngine;

namespace Asteroids.Services
{
    public class LaserCharger
    {
        private PlayerShip ship;

        public LaserCharger(PlayerShip ship)
        {
            this.ship = ship;
        }

        public void Update()
        {
            if (ship.CurrentLaserCharges >= ship.MaxLaserCharges) return;

            float currentTime = ship.CurrentLaserChargingTimer;
            if (ship.CurrentLaserChargingTimer == 0f)
            {
                currentTime = ship.LaserChargingTime;
            }
            else
            {
                currentTime -= Time.deltaTime;
                if (currentTime < 0f)
                {
                    ship.SetLaserCharges(ship.CurrentLaserCharges + 1);
                    currentTime = 0f;
                }
            }

            ship.UpdateLaserChargingTime(currentTime);
        }
    }
}