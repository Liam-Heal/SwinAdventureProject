using System;

namespace SwinAdventure
{
    public class Path : GameObject
    {
        private readonly Location _dest;

        public Path(string[] ids, string name, string desc, Location destination)
            : base(ids, name, desc)
        {
            _dest = destination;
        }

        public Location Destination
        {
            get { return _dest; }
        }
    }
}
