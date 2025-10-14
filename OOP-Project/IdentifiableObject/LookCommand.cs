using System;
using System.IO;

namespace SwinAdventure
{
    public class LookCommand : Command
    {
        public LookCommand(string[] ids) : base(ids) { }

        public override string Execute(Player p, string[] text)
        {
            string raw = (text.Length == 1) ? text[0] : string.Join("-", text);
            string[] parts = raw.Split('-', StringSplitOptions.RemoveEmptyEntries);

            IHaveInventory? container = p;

            if (parts.Length != 3 && parts.Length != 5)
                return "I don't know how to look like that";

            if (parts[0].ToLower() != "look")
                return "Error in look input";

            if (parts[1].ToLower() != "at")
                return "What do you want to look at?";

            if (parts.Length == 5)
            {
                if (parts[3].ToLower() != "in")
                    return "What do you want to look in?";

                string containerId = parts[4].ToLower();
                container = FetchContainer(p, containerId);
                if (container == null)
                    return $"I cannot find the {containerId}";
            }

            string thingId = parts[2].ToLower();
            string result = LookAtIn(thingId, container!, parts.Length == 5);

            if (result != "I don't know how to look like that" &&
                result != "Error in look input" &&
                result != "What do you want to look at?" &&
                result != "What do you want to look in?")
            {
                LogLookCommand(NormalizeLook(parts));
            }

            return result;

            /*
            IHaveInventory? containerSpace = p;

            if (text.Length != 3 && text.Length != 5)
                return "I don't know how to look like that";

            if (text[0].ToLower() != "look")
                return "Error in look input";

            if (text[1].ToLower() != "at")
                return "What do you want to look at?";

            if (text.Length == 5)
            {
                if (text[3].ToLower() != "in")
                    return "What do you want to look in?";

                string containerIdSpace = text[4].ToLower();
                containerSpace = FetchContainer(p, containerIdSpace);
                if (containerSpace == null)
                    return $"I cannot find the {containerIdSpace}";
            }

            return LookAtIn(text[2].ToLower(), containerSpace!, text.Length == 5);
            */
        }

        private IHaveInventory? FetchContainer(Player p, string containerId)
        {
            GameObject? obj = p.Locate(containerId);
            if (obj == null) return null;
            return obj as IHaveInventory;
        }

        private string LookAtIn(string thingId, IHaveInventory container, bool includeContainerName)
        {
            GameObject? found = container.Locate(thingId);
            if (found == null)
            {
                if (includeContainerName)
                    return $"I cannot find the {thingId} in the {container.Name}";
                return $"I cannot find the {thingId}";
            }
            return found.FullDescription;
        }

        private void LogLookCommand(string normalizedHyphenCommand)
        {
            try
            {
                using var w = new StreamWriter("LookHistory.txt", append: true);
                w.WriteLine(normalizedHyphenCommand);
            }
            catch { }
        }

        private string NormalizeLook(string[] parts)
        {
            if (parts.Length == 3)
                return $"look-at-{parts[2].ToLower()}";
            else
                return $"look-at-{parts[2].ToLower()}-in-{parts[4].ToLower()}";
        }
    }
}
