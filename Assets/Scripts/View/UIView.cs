using Asteroids.Model;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Asteroids.View
{
    public class UIView : MonoBehaviour
    {
        [SerializeField] private Button startButton;
        [SerializeField] private GameObject menu;
        [SerializeField] private GameObject shipParams;
        [SerializeField] private TextMeshProUGUI position;
        [SerializeField] private TextMeshProUGUI angle;
        [SerializeField] private TextMeshProUGUI speed;
        [SerializeField] private TextMeshProUGUI laserChargeAmount;
        [SerializeField] private TextMeshProUGUI laserChargeTimer;

        private UIViewModel model;

        public event Action OnStartButtonClick;

        public void SetModel(UIViewModel model)
        {
            this.model = model;
        }

        private void Start()
        {
            startButton.onClick.AddListener(OnStartButtonClicked);
            menu.gameObject.SetActive(true);
            shipParams.SetActive(false);
        }

        private void Update()
        {
            if (model == null) return;

            shipParams.SetActive(model.IsGameStarted);

            menu.gameObject.SetActive(!model.IsGameStarted);

            if (model.IsGameStarted)
            {
                position.text = model.Position;
                angle.text = model.Angle;
                speed.text = model.Speed;
                laserChargeAmount.text = model.LaserCharges;
                laserChargeTimer.text = model.LaserChargingTimer;
            }
        }

        private void OnStartButtonClicked()
        {
            OnStartButtonClick?.Invoke();
        }
    }
}
