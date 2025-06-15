using System;
using UnityEngine;
using UnityEngine.InputSystem;
namespace InputSystem
{

    public class UIInputReader : ScriptableObject, Controls.IUIActions
    {

        public event Action OnEscEvent;

        private Controls _controls;

        private void OnEnable()
        {
            _controls = new Controls();
            _controls.UI.SetCallbacks(this);
            _controls.UI.Enable();
        }

        private void OnDisable()
        {
            _controls.UI.Disable();
        }

        public void OnEsc(InputAction.CallbackContext context)
        {
            if (context.performed)
                OnEscEvent?.Invoke();
        }
    }
}