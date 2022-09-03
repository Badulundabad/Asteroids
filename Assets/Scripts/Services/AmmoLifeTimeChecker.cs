using Asteroids.Model;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids.Services
{
    public class AmmoLifeTimeChecker<T> where T : Ammo
    {
        private List<T> ammos;
        public event Action<T> OnLifeTimeExpired;

        public AmmoLifeTimeChecker(List<T> ammos)
        {
            this.ammos = ammos;
        }

        public void Check()
        {
            for (int i = 0; i < ammos.Count; i++)
            {
                T ammo = ammos[i];
                if (ammo.LaunchTime < Time.time - ammo.MaxLifeTime)
                {
                    OnLifeTimeExpired?.Invoke(ammo);
                }
            }
        }
    }
}
