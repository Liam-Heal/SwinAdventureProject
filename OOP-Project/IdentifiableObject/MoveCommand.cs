// MoveCommand.cs
using System;

namespace SwinAdventure
{
    public class MoveCommand : Command
    {
        public MoveCommand(string[] ids) : base(ids) { }

        public override string Execute(Player p, string[] text)
        {
            // Normalize: accept one hyphenated token OR an array -> "move-north"
            string raw = (text.Length == 1) ? text[0] : string.Join("-", text);
            string[] parts = raw.Split('-', StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length != 2)
            {
                return "I don't know how to move like that";
            }

            string verb = parts[0].ToLower();
            if (verb != "move" && verb != "go" && verb != "head" && verb != "leave")
            {
                return "Error in move input";
            }

            if (p.Location == null)
            {
                return "You are nowhere. You cannot move.";
            }

            string dirId = parts[1].ToLower();

            // Ask the current location for a path with that identifier
            Path? path = p.Location.Fetch(dirId);
            if (path == null)
            {
                return $"There is no path to '{dirId}'.";
            }

            // Move!
            p.Location = path.Destination;

            // Simple feedback + show new location description
            return $"You move {dirId}.\n{p.Location.FullDescription}";
        }
    }
}
