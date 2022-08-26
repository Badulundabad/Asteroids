using UnityEngine;

namespace Asteroids.FlyingObjects.Controllers
{
    public interface IFlyingObjectController
    {
        void Tick();
        bool DestroyIfExist(GameObject obj);
    }
}
