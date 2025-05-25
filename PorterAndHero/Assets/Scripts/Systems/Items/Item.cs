using System;
using Interfaces;
using Scriptables.Items;
using Systems.Movement;
using UnityEngine;

namespace Systems.Items
{
    [RequireComponent(typeof(RigidbodyForceController))]
    public abstract class Item : MonoBehaviour, IUsable
    {
        public event IUsable.UsedEventHandler Used;
        public delegate void PickedUpEventHandler(Item item, bool isPickedUp);
        public event PickedUpEventHandler PickedUp;
        
        [SerializeField] private ItemDatum itemDatum;
        
        private RigidbodyForceController _rigidbodyForceController;
        private CircleCollider2D _col;
        
        private UseContext _useContext;
        
        public bool IsUsing { get; private set;  }
        public bool IsDisabled { get; private set; }
        public ItemDatum ItemDatum => itemDatum;
        public bool IsPickedUp { get; private set; }

        protected virtual void Awake()
        {
            _rigidbodyForceController = GetComponent<RigidbodyForceController>();
            _col = GetComponent<CircleCollider2D>();
        }

        public bool CanUse()
        {
            return !IsUsing;
        }

        public bool CanStopUsing()
        {
            return IsUsing;
        }

        public void Use(UseContext useContext)
        {
            if (!CanUse()) return;
            IsUsing = true;
            _useContext = useContext;
            OnUse(useContext);
            Used?.Invoke(this, IsUsing, useContext);
        }
        
        protected abstract void OnUse(UseContext useContext);

        public void StopUsing(UseContext useContext)
        {
            if (!CanStopUsing()) return;
            IsUsing = false;
            _useContext = useContext;
            OnStopUsing(useContext);
            Used?.Invoke(this, IsUsing, useContext);
        }
        
        protected abstract void OnStopUsing(UseContext useContext);

        public bool CanPickUp()
        {
            return !IsPickedUp;    
        }

        public void PickUp(Transform parent, bool resetPosition = true, bool resetRotation = true)
        {
            if (IsPickedUp) Drop();
            IsPickedUp = true;
            OnPickUp(parent, resetPosition, resetRotation);
            PickedUp?.Invoke(this, IsPickedUp);
        }

        protected virtual void OnPickUp(Transform parent, bool resetPosition, bool resetRotation)
        {
            _rigidbodyForceController.IsKinematic = true;
            //_col.enabled = false;
            transform.SetParent(parent);
            if (resetPosition) transform.localPosition = Vector3.zero;
            if (resetRotation) transform.localRotation = Quaternion.identity;
        }

        private bool CanDrop()
        {
            return IsPickedUp;
        }

        public void Drop()
        {
            if (!CanDrop()) return;
            if (IsUsing) StopUsing(_useContext);
            IsPickedUp = false;
            OnDrop();
            PickedUp?.Invoke(this, IsPickedUp);
        }
        
        protected virtual void OnDrop()
        {
            _rigidbodyForceController.IsKinematic = false;
            //_col.enabled = true;
            transform.SetParent(null);
        }

        protected void Throw(Vector2 direction)
        {
            if (IsPickedUp) Drop();
            _rigidbodyForceController.ApplyForce(direction * itemDatum.ThrowSpeed, ForceMode2D.Impulse);
        }

        public bool CanCombineWith(Item item)
        {
            return false;
        }

        public void CombineWith(Item item)
        {
            OnCombineWith(item);
        }
        
        protected abstract void OnCombineWith(Item item);
    }
}
