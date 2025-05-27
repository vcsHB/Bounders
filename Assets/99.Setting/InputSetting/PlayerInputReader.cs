using UnityEngine;
using UnityEngine.InputSystem;

namespace Core.InputSystem
{
    [CreateAssetMenu(menuName = "SO/PlayerInput")]
    public class PlayerInputReader : ScriptableObject, Controls.IPlayerActions
    {
        public Vector2 CurrentMoveDirection { get; private set; }
        private Controls _controls;

        private void OnEnable()
        {
            _controls = new Controls();
            _controls.Player.SetCallbacks(this);
            _controls.Player.Enable();
        }

        private void OnDisable()
        {
            _controls.Player.Disable();
        }


        public void OnMove(InputAction.CallbackContext context)
        {
            if (context.performed)
                CurrentMoveDirection = context.ReadValue<Vector2>();
            else if (context.canceled)
                CurrentMoveDirection = Vector2.zero;
        }

    }

}