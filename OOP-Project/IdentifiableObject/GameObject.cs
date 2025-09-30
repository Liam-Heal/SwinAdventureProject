using System.IO;

namespace SwinAdventure
{
    public abstract class GameObject : IdentifiableObject
    {
        protected string _name;
        protected string _description;

        protected GameObject(string[] ids, string name, string desc) : base(ids)
        {
            _name = name;
            _description = desc;
        }

        public string Name => _name;
        public string ShortDescription => $"a {Name} ({FirstId})";

        public virtual string LongDescription => _description;
        public virtual string FullDescription => LongDescription;

        public virtual void SaveTo(StreamWriter writer)
        {
            writer.WriteLine(_name);
            writer.WriteLine(_description);
        }

        public virtual void LoadFrom(StreamReader reader)
        {
            _name = reader.ReadLine() ?? "";
            _description = reader.ReadLine() ?? "";
        }
    }
}
