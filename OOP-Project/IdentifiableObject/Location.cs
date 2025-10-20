using System;
using System.Reflection.Metadata.Ecma335;

namespace SwinAdventure
{
    public class Location : GameObject, IHaveInventory
    {

        private Inventory _inventory;

        private List<Path> _paths = [];

        public Location(string[] ids, string name, string desc) : base(ids, name, desc)
        {
            _inventory = new Inventory();

            _paths = new List<Path>();
        }
        public Inventory Inventory
        {
            get { return _inventory; }
        }
        public GameObject Locate(string id)
        {
            if (AreYou(id))
            {
                return this;
            }
            else
            {
                return _inventory.Fetch(id);
            }
        }

        public void AddPath(Path pth)
        {
            _paths.Add(pth);
        }

        public Path? Fetch(string id)
        {
            foreach (var p in _paths)
            {
                if (p.AreYou(id))
                {
                    return p;
                }
            }
            return null;
        }
        public override string FullDescription
        {
            get
            {
                string nameDescription;
                string inventoryDescription;

                if (Name != null && Name != "")
                {
                    nameDescription = Name;
                }
                else
                {
                    nameDescription = "an unknown location.";
                }
                if (_inventory != null && _inventory.ItemList != null)
                {
                    inventoryDescription = _inventory.ItemList;
                }
                else
                {
                    inventoryDescription = " there is no items.";
                }

                return " You are in " + nameDescription + inventoryDescription + " \n base information " 
                + base.LongDescription;
            }
        }
    }
}