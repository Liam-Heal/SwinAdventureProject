using System;

namespace SwinAdventure
{
    public class Bag : Item, IHaveInventory
    {
        private Inventory _inventory;

        public Bag(string[] idents, string name, string desc) : base(idents, name, desc)
        {
            _inventory = new Inventory();
        }

        public Inventory Inventory
        {
            get { return _inventory; }
        }

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
