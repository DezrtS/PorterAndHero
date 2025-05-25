using System;
using System.Collections.Generic;
using Interfaces;
using Systems.Items;
using UnityEngine;

namespace Systems.Player
{
    public class Inventory : MonoBehaviour
    {
        public delegate void ValueChangedEventHandler(int oldValue, int newValue);
        public event ValueChangedEventHandler GoldChanged;
        
        public delegate void InventoryItemEventHandler(int index, Item item);
        public event InventoryItemEventHandler ItemAdded;
        public event InventoryItemEventHandler ItemUpdated;
        public event InventoryItemEventHandler ItemRemoved;
        
        public delegate void SwitchedItemEventHandler(int oldIndex, int newIndex, Item oldItem, Item newItem);
        public event SwitchedItemEventHandler SwitchedItems;

        [SerializeField] private Transform rotationTransform;
        [SerializeField] private Transform activeTransform;
        [SerializeField] private int itemCapacity = 1;
        [SerializeField] private int goldCapacity = 1;
        
        private List<Item> _items;
        private int _selectedIndex;
        private int _gold;
        private IAim _aim;

        private void Awake()
        {
            _items = new List<Item>(itemCapacity);
            for (var i = 0; i < itemCapacity; i++) _items.Add(null);
            TryGetComponent(out _aim);
        }

        private void FixedUpdate()
        {
            if (_aim == null) return;
            var input = _aim.GetAimInput();
            var angle = Mathf.Atan2(input.y, input.x) * Mathf.Rad2Deg;
            rotationTransform.rotation = Quaternion.Euler(0, 0, angle);
        }

        public void SwitchItem(int delta)
        {
            var newIndex = (_selectedIndex + delta + itemCapacity) % itemCapacity;
            SwitchedItems?.Invoke(_selectedIndex, newIndex, _items[_selectedIndex], _items[newIndex]);
            _selectedIndex = newIndex;
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
                    ItemUpdated?.Invoke(_selectedIndex, currentItem);
                    return;
                }
                currentItem.Drop();
            }

            _items[_selectedIndex] = item;
            item.PickedUp += ItemOnPickedUp;
            item.PickUp(activeTransform);
            ItemAdded?.Invoke(_selectedIndex, currentItem);
        }

        private void ItemOnPickedUp(Item item, bool isPickedUp)
        {
            if (isPickedUp) return;
            item.PickedUp -= ItemOnPickedUp;
            var index = _items.IndexOf(item);
            _items[index] = null;
            ItemRemoved?.Invoke(index, item);
        }

        public void ConsumeItem(Item item)
        {
            if (_gold >= goldCapacity) return;
            var newValue = Mathf.Min(_gold + item.ItemDatum.ItemValue, goldCapacity);
            GoldChanged?.Invoke(_gold, newValue);
            _gold = newValue;
            item.Drop();
            Destroy(item.gameObject);
        }
    }
}
