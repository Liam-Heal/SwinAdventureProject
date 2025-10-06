using System.IO;

namespace SwinAdventure
{
    public class Player : GameObject, IHaveInventory
    {
        private readonly Inventory _inventory;

        public Player(string name, string desc)
            : base(new string[] { "me", "inventory" }, name, desc)
        {
            _inventory = new Inventory();
            _studentID = "105923500";
            _studentName = "Liam Healey";
        }

        public Inventory Inventory => _inventory;

        public GameObject? Locate(string id)
        {
            if (AreYou(id)) return this;
            return _inventory.Fetch(id);
        }

        public override string FullDescription
        {
            get
            {
                return $"You are {Name} ({base.FullDescription})\nYou are carrying:\n{_inventory.ItemList}";
            }
        }

        public override void SaveTo(StreamWriter writer)
        {
            base.SaveTo(writer);
            writer.WriteLine(_inventory.ItemListCsv);
        }

        public override void LoadFrom(StreamReader reader)
        {
            base.LoadFrom(reader);
            string itemDescriptionList = reader.ReadLine() ?? "";
            System.Console.WriteLine(Name);
            System.Console.WriteLine(_description);
            System.Console.WriteLine(StudentID);
            System.Console.WriteLine(StudentName);
            System.Console.WriteLine(itemDescriptionList);
        }
    }
}
