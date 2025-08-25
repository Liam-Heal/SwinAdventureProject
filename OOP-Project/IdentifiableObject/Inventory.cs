namespace SwinAdventure
{

    public class Inventory
    {
        private List<Item> _items;

        public Inventory()
        {
            _items = new List<Item>();
        }

        public bool HasItem(string id)
        {
            foreach (Item i in _items)
            {
                if (i.AreYou(id.ToLower) == true)
                    return true;
            }
            return false;
        }

        // Put
        public void Put(Item itm)
        {
            _items.Add(itm);
        }
        // Take

        // Fetch

        public string ItemList
        {
            get
            {
                string result = "\n";
                foreach (Item i in _items)
                {
                    result += "\t " +
                    i.ShortDescription +
                    "\n";
                }

                return result;
            }
        }

    }
}