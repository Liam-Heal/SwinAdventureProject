using System.IO;

namespace SwinAdventure
{
    public class Player : GameObject
    {
        private readonly Inventory _inventory;

        public Player(string name, string desc)
            : base(new string[] { "me", "inventory" }, name, desc)
        {
            _inventory = new Inventory();
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
                return $"You are {Name} ({base.FullDescription})\n" +
                       "You are carrying:\n" +
                       _inventory.ItemList;
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

            System.Console.WriteLine("Player information");
            System.Console.WriteLine(Name);
            System.Console.WriteLine(ShortDescription);
            System.Console.WriteLine(itemDescriptionList);
        }
    }
}
