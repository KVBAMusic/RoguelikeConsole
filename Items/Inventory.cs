using System;

namespace Roguelike.Items
{
    public class Inventory
    {
        [Serializable]
        struct InventoryItem
        {
            public IItem Item;
            public uint Count;

            public string Name => Item.Name;

            public InventoryItem(IItem item, uint count)
            {
                Item = item;
                Count = count;
            }
        }

        private InventoryItem[] _items;
        private ItemType _invItemType;
        private uint _invSize;


        public Inventory(uint size, ItemType? type)
        {
            _items = new InventoryItem[size];
            _invSize = size;
            _invItemType = type ?? ItemType.All;
        }

        public bool AddItem(IItem item)
        {
            // return true if can add item
            // else return false
            if (item.Type != _invItemType) return false;
            // find first free slot
            bool hasFreeSlot = false;
            int freeSlot = -1;
            for (int i = 0; i < _invSize; i++)
            {
                if (_items[i].Item == null)
                {
                    hasFreeSlot = true;
                    freeSlot = i;
                    break;
                }
            }
            if (!hasFreeSlot) return false;
            if (item.isStackable)
            {
                // find first occurence
                // if there's no occurence of the item, add it to the free slot
                for(int i = 0; i < _invSize; i++)
                {
                    if (_items[i].Name == item.Name)
                    {
                        _items[i].Count++;
                        return true;
                    }
                }
            }
            _items[freeSlot] = new(item, 1);
            return true;
        }
    }
}
