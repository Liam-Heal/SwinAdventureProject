using System;
using System.Security;

namespace SwinAdventure
{
    public abstract class Command : IdentifiableObject
    {

        public Command(string[] ids) : base(ids)
        {

        }

        public abstract string Execute(Player p, string[] text);
    }
}