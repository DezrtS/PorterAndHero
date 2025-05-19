using Interfaces;
using Systems.Movement;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Systems.Player
{
    public abstract class PlayerController : MonoBehaviour, IMove
    {
        [SerializeField] private InputActionAsset inputActionAsset;
        private PlayerMovementController _playerMovementController;

        private InputActionMap _playerInputActionMap;
        private InputActionMap _movementInputActionMap;
        private InputAction _movementInputAction;

        private void Awake()
        {
            _playerMovementController = GetComponent<PlayerMovementController>();
            _playerMovementController.Initialize(this);
            AssignControls();
        }

        protected virtual void AssignControls()
        {
            _playerInputActionMap ??= inputActionAsset.FindActionMap("Player");
            _playerInputActionMap.Enable();
            
            _movementInputActionMap ??= inputActionAsset.FindActionMap("Movement");
            _movementInputActionMap.Enable();
            
            _movementInputAction ??= inputActionAsset.FindAction("Movement");
            
            var dodgeInputAction = _movementInputActionMap.FindAction("Dodge");
            dodgeInputAction.performed += OnDodge;
        }

        private void FixedUpdate()
        {
            _playerMovementController.Move();
        }

        private void Interact()
        {
            
        }

        private void OnDodge(InputAction.CallbackContext context)
        {
            _playerMovementController.Dodge(GetMoveInput());
        }

        public Vector2 GetMoveInput()
        {
            return _movementInputAction.ReadValue<Vector2>();
        }
    }
}
