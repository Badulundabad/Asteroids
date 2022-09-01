using Asteroids.Model;
using Asteroids.View;
using System.Collections.Generic;

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

        /// <summary>
        /// Running the controller.
        /// Warning! Please don't remove base implementation when overriding this method 
        /// or make sure you set IsRunning on true.
        /// </summary>
        public virtual void Start()
        {
            IsRunning = true;
        }

        /// <summary>
        /// Stops working of the controller, erases all views in factory and clears list of mdoel objects.
        /// Warning! Please don't remove base implementation when overriding this method.
        /// </summary>
        public virtual void Stop()
        {
            IsRunning = false;
            factory.EraseAll();
            objects.Clear();
        }

        public abstract void Update();

        protected void Destroy(T ammo)
        {
            objects.Remove(ammo);
            factory.Destroy(ammo);
        }
    }
}