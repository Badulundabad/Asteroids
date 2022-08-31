using Asteroids.Model;
using Asteroids.View;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids.Controllers
{
    public abstract class BaseObjectController<T> : IController where T : SpaceObject
    {
        protected List<T> objects;
        protected ISpaceObjectFactory<T> factory;

        public bool IsRunning { get; protected set; }


        public BaseObjectController(ISpaceObjectFactory<T> factory)
        {
            this.factory = factory;
            objects = new List<T>();
        }

        public abstract void Start();

        public void Stop()
        {
            IsRunning = false;
            factory.EraseAll();
            for (int i = 0; i < objects.Count; i++)
            {
                objects.Clear();
            }
            Debug.Log($"{objects.Count}");
        }

        public abstract void Update();

        protected void Destroy(T ammo)
        {
            objects.Remove(ammo);
            factory.Destroy(ammo);
        }
    }
}