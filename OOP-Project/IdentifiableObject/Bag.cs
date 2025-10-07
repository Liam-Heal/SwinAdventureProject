using System;

namespace SwinAdventure
{
    public class Bag : Item, IHaveInventory
    {   

        // Define inventory 
        private Inventory _inventory;


        // Construct the bag and put an inventory in it.
        public Bag(string[] idents, string name, string desc) : base(idents, name, desc)
        {
            _inventory = new Inventory();
        }

        public Inventory Inventory
        {
            get { return _inventory; }
        }


        // Locate the objects using methods 
        public GameObject? Locate(string id)
        {
            if (AreYou(id)) return this;
            return _inventory.Fetch(id);
        }

        public override string FullDescription
        {
            get { return $"In this {Name} you can see:\n{_inventory.ItemList}"; }
        }
    }
}
