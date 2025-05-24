using UnityEngine;

namespace Scriptables.Items
{
    [CreateAssetMenu(fileName = "ItemDatum", menuName = "Scriptable Objects/Items/ItemDatum")]
    public class ItemDatum : ScriptableObject
    {
        [SerializeField] private string itemName;
        [TextArea]
        [SerializeField] private string itemDescription;
        [SerializeField] private int itemValue;
        [Space] 
        [SerializeField] private float throwSpeed;
        
        public string ItemName => itemName;
        public string ItemDescription => itemDescription;
        public int ItemValue => itemValue;
        public float ThrowSpeed => throwSpeed;
    }
}
