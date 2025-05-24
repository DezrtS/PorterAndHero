using Interfaces;
using Scriptables.Items;
using UnityEngine;

namespace Systems.Items
{
    public class MeleeWeapon : Weapon
    {
        private MeleeWeaponDatum _meleeWeaponDatum;

        protected override void Awake()
        {
            base.Awake();
            _meleeWeaponDatum = (MeleeWeaponDatum)ItemDatum;
        }

        protected override void OnCombineWith(Item item)
        {
            throw new System.NotImplementedException();
        }

        protected override void OnBeginAttack(Vector2 direction)
        {
            Debug.Log("Attacked");
            Damage(1);
            StartCoroutine(CooldownRoutine());
        }

        protected override void OnFinishAttack(Vector2 direction)
        {
            
        }
    }
}
