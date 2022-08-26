using Asteroids.Input;
using Asteroids.FlyingObjects.Models;
using UnityEngine;
using System;
using System.Threading.Tasks;

namespace Asteroids.FlyingObjects.Controllers
{
    public class PlayerController : IFlyingObjectController
    {
        private int immortalTicks;
        private float tickCounter;
        private float playerDestroyTime;
        private GameObject view;
        private PlayerInput input;
        private PlayerShip player;
        private PlayerShipModel model;
        public event Action OnDestroy;
        public event Action OnDefeat;

        public PlayerController(PlayerInput input, PlayerShipModel model)
        {
            this.input = input;
            input.Enable();
            this.model = model;
            model.OnCollision += (player, collidedObject) => OnCollision(player, collidedObject);
            SpawnShip();
        }

        public void Tick()
        {
            if (view == null)
            {
                if (Time.time - playerDestroyTime < 2f) return;
                SpawnShip();
            }

            UpdateModel();

            if (view != null)
                UpdateView();
        }

        public bool DestroyIfExist(GameObject obj)
        {
            if (obj != view) return false;
            DestroyShip();
            return true;
        }

        public PlayerShip GetPlayer()
        {
            return player;
        }

        private void SpawnShip()
        {
            if (player == null) player = model.CreateShip();
            view = GameObject.Instantiate(model.Prefab);
            player.Collider = view.GetComponent<Collider2D>();
        }

        private void UpdateModel()
        {
            if (input.Player.Move.IsPressed())
            {
                model.Input = input.Player.Move.ReadValue<Vector2>();
            }
            model.Tick(player);
        }

        private void UpdateView()
        {
            view.transform.position = player.Position;
            view.transform.rotation = player.Rotation;

            if (!player.IsImmortal) return;

            HandleImmortality();
        }

        private void HandleImmortality()
        {
            tickCounter += Time.deltaTime;
            if (tickCounter < 0.2f) return;

            tickCounter = 0f;
            immortalTicks++;
            if (immortalTicks > 9)
            {
                immortalTicks = 0;
                player.SetMortal();
            }

            ChangeTransparency();
        }

        private void ChangeTransparency()
        {
            var sprite = view.GetComponent<SpriteRenderer>();
            Color color = sprite.color;
            float alpha = color.a == 1f ? 0.5f : 1f;
            sprite.color = new Color(color.r, color.g, color.b, alpha);
        }

        private void OnCollision(PlayerShip player, GameObject collidedObj)
        {
            if (collidedObj.tag == Tags.LASER || collidedObj.tag == Tags.BULLET) return;

            if (collidedObj.tag == Tags.VEGETABLES)
            {
                // request AmmoController.DestroyAmmo(obj);
            }
            else if (collidedObj.tag == Tags.SHIP)
            {
                // request EnemyController.DestroyAmmo(obj);
            }
            if (player.IsImmortal) return;
            DestroyShip();
        }

        private void DestroyShip()
        {
            tickCounter = 0f;
            GameObject.Destroy(view);
            model.OnShipDestroy(player);
            playerDestroyTime = Time.time;
            OnDestroy?.Invoke();
        }
    }
}