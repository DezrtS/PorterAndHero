using UnityEngine;

namespace Scriptables.Items
{
    [CreateAssetMenu(fileName = "MeleeWeaponDatum", menuName = "Scriptable Objects/Weapons/Melee/MeleeWeaponDatum")]
    public class MeleeWeaponDatum : WeaponDatum
    {
        [SerializeField] private int damage;
        [SerializeField] private int range;
        [SerializeField] private float knockback;
        
        public int Damage => damage;
        public int Range => range;
        public float Knockback => knockback;
    }
}
