using UnityEngine;
using UnityEngine.InputSystem;

namespace Core.InputSystem
{
    [CreateAssetMenu(menuName = "SO/SecondPlayerInput")]
    public class SecondPlayerInputReader : PlayerInputReader, Controls.ISecondPlayerActions
    {
        private Controls _controls;

        private void OnEnable()
        {
            _controls = new Controls();
            _controls.SecondPlayer.SetCallbacks(this);
            _controls.SecondPlayer.Enable();
        }

        private void OnDisable()
        {
            _controls.SecondPlayer.Disable();
        }


        public void OnMove(InputAction.CallbackContext context)
        {
            if (!CanControl) return;
            if (context.performed)
                CurrentMoveDirection = context.ReadValue<Vector2>();
            else if (context.canceled)
                CurrentMoveDirection = Vector2.zero;
        }

    }

}