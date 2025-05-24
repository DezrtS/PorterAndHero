using System;
using System.Collections;
using UnityEngine;

namespace Systems.Movement
{
    public class PlayerMovementController : RigidbodyMovementController
    {
        [SerializeField] private float dodgeSpeed = 5f;
        [SerializeField] private AnimationCurve dodgeCurve;
        [SerializeField] private float dodgeDuration = 0.5f;
        
        private bool _isDodging;
        private Vector3 _dodgeDirection;
        
        public void Dodge(Vector3 input)
        {
            if (_isDodging) return;
            _isDodging = true;
            IsDisabled = true;
            _dodgeDirection = input;
            StartCoroutine(DodgeRoutine());
        }

        private void FixedUpdate()
        {
            if (!_isDodging) return;
            ForceController.SetVelocity(_dodgeDirection * dodgeSpeed);
        }

        private IEnumerator DodgeRoutine()
        {
            yield return new WaitForSeconds(dodgeDuration);
            _isDodging = false;
            IsDisabled = false;
            _dodgeDirection = Vector3.zero;
        }
    }
}
