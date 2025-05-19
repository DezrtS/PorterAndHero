using UnityEngine;

namespace Systems.Movement
{
    public abstract class ForceController : MonoBehaviour
    {
        private bool _isKinematic;
        private bool _isDisabled;
        
        public bool IsKinematic
        {
            get => _isKinematic;
            set
            {
                _isKinematic = value;
                OnSetIsKinematic();
            }
        }

        public bool IsDisabled
        {
            get => _isDisabled;
            set
            {
                _isDisabled = value;
                OnSetIsDisabled();
            }
        }

        protected virtual void OnSetIsKinematic() {}
        protected virtual void OnSetIsDisabled() {}

        public abstract Vector2 GetVelocity();
        public void SetVelocity(Vector2 velocity)
        {
            if (_isDisabled) return;
            OnSetVelocity(velocity);
        }
        protected abstract void OnSetVelocity(Vector2 velocity);
        
        public virtual Quaternion GetRotation() { return transform.rotation; }

        public void SetRotation(Quaternion rotation)
        {
            if (_isDisabled) return;
            OnSetRotation(rotation);
        }
        protected virtual void OnSetRotation(Quaternion rotation) => transform.rotation = rotation;

        public void ApplyForce(Vector2 force, ForceMode2D forceMode)
        {
            if (_isDisabled) return;
            OnApplyForce(force, forceMode);
        }
        protected abstract void OnApplyForce(Vector2 force, ForceMode2D forceMode);

        public void ApplyTorque(float torque, ForceMode2D forceMode)
        {
            if (_isDisabled) return;
            OnApplyTorque(torque, forceMode);
        }
        protected abstract void OnApplyTorque(float torque, ForceMode2D forceMode);

        public void Teleport(Vector2 position)
        {
            if (_isDisabled) return;
            OnTeleport(position);
        }
        protected virtual void OnTeleport(Vector2 position) => transform.position = position;
    }
}
