using UnityEngine;

namespace Scriptables.Items
{
    public abstract class WeaponDatum : ItemDatum
    {
        [SerializeField] private int durability;
        [SerializeField] private float cooldownDuration;

        public int Durability => durability;
        public float CooldownDuration => cooldownDuration;
    }
}
