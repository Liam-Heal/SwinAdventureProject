using System.IO;

namespace SwinAdventure
{
    public abstract class GameObject : IdentifiableObject
    {
        protected string _name;
        protected string _description;
        protected string _studentID = "";
        protected string _studentName = "";
        public new int IdentifierCount => base.IdentifierCount;
        public string Name => _name;
        public string ShortDescription => $"a {Name} ({FirstId})";
        public virtual string LongDescription => _description;
        public virtual string FullDescription => LongDescription;

        public string StudentID
        {
            get => _studentID;
            set => _studentID = value ?? "";
        }
        public string StudentName
        {
            get => _studentName;
            set => _studentName = value ?? "";
        }

        protected GameObject(string[] ids, string name, string desc) : base(ids)
        {
            _name = name;
            _description = desc;
        }

        public virtual void SaveTo(StreamWriter writer)
        {
            writer.WriteLine(_name);
            writer.WriteLine(_description);
            writer.WriteLine(_studentID);
            writer.WriteLine(_studentName);
        }

        public virtual void LoadFrom(StreamReader reader)
        {
            _name = reader.ReadLine() ?? "";
            _description = reader.ReadLine() ?? "";
            _studentID = reader.ReadLine() ?? "";
            _studentName = reader.ReadLine() ?? "";
        }
    }
}
