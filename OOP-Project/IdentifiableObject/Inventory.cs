using System.Collections.Generic;

namespace SwinAdventure
{
    public class Inventory
    {
        private readonly List<Item> _items;

        public Inventory()
        {
            _items = new List<Item>();
        }

        // Returns true if any item in the inventory matches id (case-insensitive via AreYou)
        public bool HasItem(string id)
        {
            foreach (Item i in _items)
            {
                if (i.AreYou(id)) return true;   // AreYou already lowercases
            }
            return false;
        }

        // Put: add an item to the inventory
        public void Put(Item itm)
        {
            _items.Add(itm);
        }

        // Fetch: locate but DO NOT remove. Return the item or null if not found.
        public Item? Fetch(string id)
        {
            foreach (Item i in _items)
            {
                if (i.AreYou(id)) return i;
            }
            return null;
        }

        // Take: remove and return the item with matching id (or null)
        public Item? Take(string id)
        {
            for (int idx = 0; idx < _items.Count; idx++)
            {
                if (_items[idx].AreYou(id))
                {
                    Item found = _items[idx];
                    _items.RemoveAt(idx);
                    return found;
                }
            }
            return null;
        }
        public bool RemoveItem(Item itm)
        {
            // Try to remove the given object instance from the list, rather than removing based on ID like the "Take" method.
            return _items.Remove(itm);
        }
        public string ItemList
        {
            get
            {
                var lines = new System.Text.StringBuilder();
                foreach (Item i in _items)
                {
                    lines.Append('\t')
                         .Append(i.ShortDescription)
                         .Append('\n');
                }
                return lines.ToString();
            }
        }
    }
}
