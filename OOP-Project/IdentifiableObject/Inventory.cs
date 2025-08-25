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

        // ItemList: multi-line string, each line is tab-indented ShortDescription
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
