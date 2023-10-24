using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts
{
    public class GameInput : MonoBehaviour
    {

        public event EventHandler OnInteractAction;
        public event EventHandler OnInteractAlternateAction;

        private PlayerInputActions _playerInputActions;

        private void Start()
        {
            _playerInputActions = new PlayerInputActions();
            _playerInputActions.Player.Enable();

            _playerInputActions.Player.Interact.performed += Interact_performed;
            _playerInputActions.Player.InteractAlternate.performed += InteractAlternate_performed;
        }

        private void Interact_performed(InputAction.CallbackContext obj)
        {
            OnInteractAction?.Invoke(this, EventArgs.Empty);
        }
        
        private void InteractAlternate_performed(InputAction.CallbackContext obj)
        {
            OnInteractAlternateAction?.Invoke(this, EventArgs.Empty);
        }

        public Vector2 GetMovementVectorNormalized()
        {
            Vector2 inputVector = _playerInputActions.Player.Move.ReadValue<Vector2>();

            inputVector = inputVector.normalized;
        
            return inputVector;
        }
    }
}
