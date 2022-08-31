using Asteroids.Model;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids.Services
{
    public class AmmoLifeTimeChecker
    {
        private List<Ammo> ammos;
        public event Action<Ammo> OnLifeTimeExpired;

        public AmmoLifeTimeChecker(List<Ammo> ammos)
        {
            this.ammos = ammos;
        }

        public void Check()
        {
            for (int i = 0; i < ammos.Count; i++)
            {
                Ammo ammo = ammos[i];
                if (ammo.ShootTime < Time.time - ammo.MaxLifeTime)
                {
                    OnLifeTimeExpired?.Invoke(ammo);
                }
            }
        }
    }
}
