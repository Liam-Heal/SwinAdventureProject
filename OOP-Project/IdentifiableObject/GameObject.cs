using System;

namespace SwinAdventure
{
    // <<abstract>> GameObject : "anything" the player can interact with
    // Inherits identifiers so every GameObject is identifiable.
    public abstract class GameObject : IdentifiableObject
    {
        protected string _name;
        protected string _description;

        // + GameObject(string[] ids, string name, string desc)
        protected GameObject(string[] ids, string name, string desc)
            : base(ids)
        {
            _name = name;
            _description = desc;
        }

        // + Name : string <<readonly, property>>
        public string Name => _name;

        // + ShortDescription : string <<readonly, property>>
        // "a {name} ({first id})"
        public string ShortDescription => $"a {Name} ({FirstId})";

        // By default just returns the longer textual description; child classes may extend.
        public virtual string LongDescription => _description;
    }
}
