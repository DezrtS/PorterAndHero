using System;
using Interfaces;
using Scriptables.Entities;
using Systems.Items;
using Systems.Movement;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Systems.Player
{
    public class PlayerController : MonoBehaviour, IEntity, IMove, IAim
    {
        public enum Type
        {
            Hero,
            Porter
        }
        
        [SerializeField] private EntityDatum entityDatum;
        [SerializeField] private Type type;
        
        private PlayerMovementController _playerMovementController;
        private PlayerInputActions _playerInputActions;
        private InputAction _movementInputAction;
        private InputAction _aimInputAction;

        private Vector2 _previousAimInput;
        
        public EntityDatum EntityDatum => entityDatum;
        public Type PlayerType => type;
        public Inventory Inventory { get; private set; }

        private void Awake()
        {
            _playerMovementController = GetComponent<PlayerMovementController>();
            _playerMovementController.Initialize(this);
            AssignControls();
            
            Inventory = GetComponent<Inventory>();
        }

        private void AssignControls()
        {
            _playerInputActions ??= new PlayerInputActions();

            _movementInputAction ??= _playerInputActions.Movement.Movement;
            _movementInputAction.Enable();
            
            var dodgeInputAction = _playerInputActions.Movement.Dodge;
            dodgeInputAction.performed += OnDodge;
            dodgeInputAction.Enable();
            
            _aimInputAction ??= _playerInputActions.Aim.Aim;
            _aimInputAction.performed += OnAim;
            _aimInputAction.Enable();

            var primaryInputAction = _playerInputActions.Player.Use;
            primaryInputAction.performed += OnPrimaryAction;
            primaryInputAction.canceled += OnPrimaryAction;
            primaryInputAction.Enable();
            
            var dropInputAction = _playerInputActions.Player.Drop;
            dropInputAction.performed += OnDrop;
            dropInputAction.Enable();
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

        private void OnAim(InputAction.CallbackContext context)
        {
            var input = context.ReadValue<Vector2>().normalized;
            if (input == Vector2.zero) return;
            _previousAimInput = input;
        }

        private void OnPrimaryAction(InputAction.CallbackContext context)
        {
            var item = Inventory.GetItem();
            if (!item) return;
            if (context.performed) item.Use(UseContext.Construct(gameObject, this));
            if (context.canceled) item.StopUsing(UseContext.Construct(gameObject, this));
        }

        private void OnDrop(InputAction.CallbackContext context)
        {
            Inventory.GetItem().Drop();
        }

        public Vector2 GetMoveInput()
        {
            return _movementInputAction.ReadValue<Vector2>();
        }

        public Vector2 GetAimInput()
        {
            return _previousAimInput;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Item")) return;
            var item = other.GetComponent<Item>();
            Inventory.AddItem(item);
        }
    }
}
