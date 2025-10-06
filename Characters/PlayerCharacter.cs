using UnityEngine;

namespace Characters
{
    public class PlayerCharacter : Character
    {
        private PlayerControls controls;
        private Vector2 moveInput;

        public Vector2 MoveInput => moveInput;

        protected override void Awake()
        {
            base.Awake();
            controls = new PlayerControls();
        }

        private void OnEnable()
        {
            controls.Enable();
        }

        protected override void Update()
        {
            base.Update();

            moveInput = controls.Player.Move.ReadValue<Vector2>();
        }

        private void OnDisable()
        {
            controls.Disable();
        }
    }
}
