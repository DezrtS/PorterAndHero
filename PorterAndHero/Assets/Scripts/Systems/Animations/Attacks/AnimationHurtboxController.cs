using System;
using System.Collections.Generic;
using UnityEngine;

namespace Systems.Animations
{
    public class AnimationHurtboxController : MonoBehaviour
    {
        private static readonly int Activate1 = Animator.StringToHash("Activate");

        [Serializable]
        public class HurtboxProfile
        {
            public Collider2D hurtboxCollider;
            public Vector2 knockbackDirection;
        }
        
        public delegate void HitEventHandler(HurtboxProfile hurtboxProfile, IEnumerable<Collider2D> colliders);
        public event HitEventHandler OnHit;
        
        [SerializeField] private HurtboxProfile[] hurtboxProfiles;
        
        private Animator _animator;
        private readonly HashSet<GameObject> _hitGameObjects = new HashSet<GameObject>();

        private bool IsActive { get; set; }

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }
        
        private void Update()
        {
            if (!IsActive) return;
            
            foreach (var profile in hurtboxProfiles)
            {
                if (profile.hurtboxCollider.enabled)
                {
                    CheckHurtbox(profile);
                }
            }
        }

        public void Activate(float duration)
        {
            IsActive = true;
            _animator.SetTrigger(Activate1);
        }

        public void Deactivate()
        {
            IsActive = false;
            _hitGameObjects.Clear();
            foreach (var hurtboxProfile in hurtboxProfiles)
            {
                hurtboxProfile.hurtboxCollider.enabled = false;
            }
        }

        public void EnableHurtbox(int profileIndex)
        {
            if (profileIndex < 0 || profileIndex >= hurtboxProfiles.Length) return;
            hurtboxProfiles[profileIndex].hurtboxCollider.enabled = true;
        }

        public void DisableHurtbox(int profileIndex)
        {
            if (profileIndex < 0 || profileIndex >= hurtboxProfiles.Length) return;
            hurtboxProfiles[profileIndex].hurtboxCollider.enabled = false;
        }

        private void CheckHurtbox(HurtboxProfile profile)
        {
            var filter = new ContactFilter2D();
            // filter.layerMask = _targetLayers;
            // filter.useLayerMask = true;

            var results = new Collider2D[5];
            var count = profile.hurtboxCollider.Overlap(filter, results);
            var colliders = new List<Collider2D>();

            for (var i = 0; i < count; i++)
            {
                var target = results[i].gameObject;
                if (!_hitGameObjects.Add(target)) continue;
                colliders.Add(results[i]);
            }

            if (colliders.Count <= 0) return; 
            OnHit?.Invoke(profile, colliders);
        }
    }
}
