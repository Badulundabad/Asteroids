using Asteroids.Input;
using System;
using UnityEngine;

namespace Assets.Scripts.Input
{
    public class InputChecker
    {
        private PlayerInput input;
        public event Action<Vector2> OnMove;

        public InputChecker(PlayerInput input)
        {
            this.input = input;
            input.Enable();
        }

        public void Tick()
        {
            if (input.Player.Move.IsPressed())
            {
                Vector3 vec = input.Player.Move.ReadValue<Vector2>();
                OnMove?.Invoke(vec);
            }
        }
    }
}