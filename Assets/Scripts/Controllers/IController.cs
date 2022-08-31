namespace Asteroids.Controllers
{
    public interface IController
    {
        public bool IsRunning { get; }
        public void Start();
        public void Update();
    }
}
