namespace SwinAdventure
{
    public class Item : IdentifiableObject
    {
        private string _name;
        private string _description;

        public Item(string[] idents, string name, string desc) : base(idents)
        {
            _name = name;
            _description = desc;
        }

        public string Name => _name;

        public string ShortDescription => $"a {_name} ({FirstId})";

        public string LongDescription => _description;

        // For the verification test
        public int GetIdentifierCount()
        {
            return IdentifierCount;
        }
    }
}
