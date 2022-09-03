using Asteroids.Model;
using Asteroids.View;
using System;
using UnityEngine;

namespace Asteroids.Controllers
{
    public class UIController : IController
    {
        private PlayerShip model;
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

        public void Stop()
        {
            IsRunning = false;
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
            viewModel.Angle = Mathf.RoundToInt(model.Rotation.eulerAngles.z).ToString();

            viewModel.Speed =  MathF.Round(model.Speed, 1).ToString();
            viewModel.LaserCharges = model.CurrentLaserCharges.ToString();
            viewModel.LaserChargingTimer = MathF.Round(model.CurrentLaserChargingTimer, 1).ToString();
        }

        public void UpdateModel(PlayerShip model)
        {
            this.model = model;
        }

        public void OnPlayerDestroy()
        {
            viewModel.IsGameStarted = false;
            viewModel.Position = String.Empty;
            viewModel.Angle = String.Empty;
            viewModel.Speed = String.Empty;
            viewModel.LaserCharges = String.Empty;
            viewModel.LaserChargingTimer = String.Empty;
        }
    }
}
