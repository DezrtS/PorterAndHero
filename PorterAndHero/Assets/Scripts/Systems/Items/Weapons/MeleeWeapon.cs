using System.Collections.Generic;
using Scriptables.Items;
using Systems.Animations;
using Systems.Entities;
using Systems.Movement;
using UnityEngine;

namespace Systems.Items
{
    public class MeleeWeapon : Weapon
    {
        [SerializeField] private AnimationHurtboxController animationHurtboxController;
        
        private MeleeWeaponDatum _meleeWeaponDatum;

        protected override void Awake()
        {
            base.Awake();
            _meleeWeaponDatum = (MeleeWeaponDatum)ItemDatum;
            animationHurtboxController.OnHit += AnimationHurtboxControllerOnHit;
        }

        protected override void OnCombineWith(Item item)
        {
            throw new System.NotImplementedException();
        }

        protected override void OnBeginAttack(Vector2 direction)
        {
            Debug.Log("Attacked");
            animationHurtboxController.Activate(1);
            StartCoroutine(CooldownRoutine());
        }

        protected override void OnFinishAttack(Vector2 direction)
        {
            
        }

        protected override void OnCooldown()
        {
            base.OnCooldown();
            animationHurtboxController.Deactivate();
        }

        private void AnimationHurtboxControllerOnHit(AnimationHurtboxController.HurtboxProfile hurtboxProfile, IEnumerable<Collider2D> colliders)
        {
            OnHit(colliders, hurtboxProfile.knockbackDirection);
        }

        protected void OnHit(IEnumerable<Collider2D> colliders, Vector2 knockbackDirection)
        {
            var hasHit = false;
            foreach (var hitCollider in colliders)
            {
                if (!hitCollider.TryGetComponent(out Health health)) continue;
                if (!health.CanDamage()) continue;
                hasHit = true;
                health.Damage(_meleeWeaponDatum.Damage);
                if (!hitCollider.TryGetComponent(out ForceController forceController)) continue;
                forceController.ApplyForce(transform.rotation * knockbackDirection * _meleeWeaponDatum.Knockback, ForceMode2D.Force);
            }
            
            if (hasHit) Damage(1);
        }
    }
}
