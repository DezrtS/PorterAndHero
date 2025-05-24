using UnityEngine;

namespace Systems.Movement
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class RigidbodyForceController : ForceController
    {
        private Rigidbody2D _rig;

        private void Awake()
        {
            _rig = GetComponent<Rigidbody2D>();
        }

        protected override void OnSetIsKinematic()
        {
            if (!IsKinematic) SetVelocity(Vector2.zero);
            _rig.bodyType = IsKinematic ? RigidbodyType2D.Kinematic : RigidbodyType2D.Dynamic;
        }

        protected override void OnApplyForce(Vector2 force, ForceMode2D forceMode)
        {
            _rig.AddForce(force, forceMode);
        }

        protected override void OnApplyTorque(float torque, ForceMode2D forceMode)
        {
            _rig.AddTorque(torque, forceMode);
        }

        public override Vector2 GetVelocity()
        {
            return _rig.linearVelocity;
        }

        protected override void OnSetVelocity(Vector2 velocity)
        {
            _rig.linearVelocity = velocity;
        }
    }
}