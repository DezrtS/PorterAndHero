using System.Collections;
using Interfaces;
using Scriptables.Items;
using UnityEngine;

namespace Systems.Items
{
    public abstract class Weapon : Item
    {
        private int _durability;
        private WeaponDatum _weaponDatum;
        
        private bool IsAttacking { get; set; }
        private bool IsCoolingDown { get; set; }

        protected override void Awake()
        {
            base.Awake();
            _weaponDatum = (WeaponDatum)ItemDatum;
            _durability = _weaponDatum.Durability;
        }

        protected override void OnUse(UseContext useContext)
        {
            BeginAttack(useContext);
        }

        private bool CanBeginAttack()
        {
            return !IsAttacking && !IsCoolingDown;
        }

        private void BeginAttack(UseContext useContext)
        {
            if (!CanBeginAttack()) return;
            IsAttacking = true;
            OnBeginAttack(IAim.GetAimInput(useContext.SourceGameObject));
        }

        protected abstract void OnBeginAttack(Vector2 direction);

        protected override void OnStopUsing(UseContext useContext)
        {
            FinishAttack(useContext);
        }

        private bool CanFinishAttack()
        {
            return IsAttacking;
        }

        private void FinishAttack(UseContext useContext)
        {
            if (!CanFinishAttack()) return;
            IsAttacking = false;
            OnFinishAttack(IAim.GetAimInput(useContext.SourceGameObject));
        }
        
        protected abstract void OnFinishAttack(Vector2 direction);

        protected void Damage(int damage)
        {
            _durability -= damage;
            if (_durability > 0) return;
            Drop();
            Destroy(gameObject);
        }

        protected IEnumerator CooldownRoutine()
        {
            IsCoolingDown = true;
            yield return new WaitForSeconds(_weaponDatum.CooldownDuration);
            IsCoolingDown = false;
        }
    }
}
