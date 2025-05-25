using UnityEngine;

namespace Scriptables.Items
{
    [CreateAssetMenu(fileName = "MeleeWeaponDatum", menuName = "Scriptable Objects/Weapons/Melee/MeleeWeaponDatum")]
    public class MeleeWeaponDatum : WeaponDatum
    {
        [SerializeField] private int damage;
        [SerializeField] private float knockback;
        
        public int Damage => damage;
        public float Knockback => knockback;
    }
}
