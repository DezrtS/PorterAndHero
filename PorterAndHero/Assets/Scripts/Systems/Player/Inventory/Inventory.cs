using System;
using System.Collections.Generic;
using Systems.Items;
using UnityEngine;

namespace Systems.Player
{
    public class Inventory : MonoBehaviour
    {
        public delegate void InventoryValueChangedEventHandler(int oldValue, int newValue);
        public event InventoryValueChangedEventHandler GoldChanged;
        
        public delegate void InventoryItemEventHandler(int index, Item item);
        public event InventoryItemEventHandler ItemAdded;
        public event InventoryItemEventHandler ItemRemoved;
        
        public delegate void InventorySwitchItemEventHandler(int oldIndex, int newIndex, Item oldItem, Item newItem);
        public event InventorySwitchItemEventHandler SwitchedItems;
        
        [SerializeField] private Transform activeTransform;
        [SerializeField] private int itemCapacity = 1;
        [SerializeField] private int goldCapacity = 1;
        
        private List<Item> _items;
        private int _selectedIndex;
        private int _gold;

        private void Awake()
        {
            _items = new List<Item>(itemCapacity);
            for (var i = 0; i < itemCapacity; i++) _items.Add(null);
        }

        public void SwitchItem(int delta)
        {
            _selectedIndex = (_selectedIndex + delta + itemCapacity) % itemCapacity;
        }

        public Item GetItem()
        {
            return _items[_selectedIndex];
        }

        public void AddItem(Item item)
        {
            if (!item.CanPickUp()) return;
            
            var currentItem = GetItem();
            if (currentItem)
            {
                if (currentItem.CanCombineWith(item))
                {
                    currentItem.CombineWith(item);
                    return;
                }
                currentItem.Drop();
            }

            _items[_selectedIndex] = item;
            item.PickedUp += ItemOnPickedUp;
            item.PickUp(activeTransform);
        }

        private void ItemOnPickedUp(Item item, bool isPickedUp)
        {
            if (isPickedUp) return;
            item.PickedUp -= ItemOnPickedUp;
            var index = _items.IndexOf(item);
            _items[index] = null;
        }

        public void ConsumeItem(Item item)
        {
            if (_gold >= goldCapacity) return;
            _gold = Mathf.Min(_gold + item.ItemDatum.ItemValue, goldCapacity);
            item.Drop();
            item.gameObject.SetActive(false);
        }
    }
}
