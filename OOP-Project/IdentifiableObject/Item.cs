namespace SwinAdventure
{
    public class Item : GameObject
    {
        public Item(string[] idents, string name, string desc)
            : base(idents, name, desc) { }

        // For the verification test (kept from your earlier work)
        public int GetIdentifierCount() => IdentifierCount;
    }
}
