using Asteroids.Model;
using Asteroids.View;
using System;
namespace Asteroids.Controllers
{
    public class UIController : IController
    {
        private Ship model;
        private UIViewModel viewModel;

        public bool IsRunning { get; private set; }
        public event Action OnStartButtonClick;


        public UIController(UIView view)
        {
            viewModel = new UIViewModel();
            view.SetModel(viewModel);
            view.OnStartButtonClick += () => OnStartButtonClicked();
        }

        public void Start()
        {
            IsRunning = true;
        }

        private void OnStartButtonClicked()
        {
            OnStartButtonClick?.Invoke();
            viewModel.IsGameStarted = true;
        }

        public void Update()
        {
            if (model == null || !viewModel.IsGameStarted) return;

            viewModel.Position = model.Position.ToString();
            viewModel.Angle = model.Rotation.z.ToString();
            viewModel.Speed = model.Speed.ToString();
            viewModel.LaserChargeAmount = "0";
            viewModel.LaserChargeTimer = "0";
        }

        public void UpdateModel(Ship model)
        {
            this.model = model;
        }

        public void OnPlayerDestroy()
        {
            viewModel.IsGameStarted = false;
            viewModel.Position = String.Empty;
            viewModel.Angle = String.Empty;
            viewModel.Speed = String.Empty;
            viewModel.LaserChargeAmount = String.Empty;
            viewModel.LaserChargeTimer = String.Empty;
        }
    }
}
