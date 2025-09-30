namespace SwinAdventure
{
    public class IdentifiableObject
    {
        private List<string> _identifiers = [];

        public IdentifiableObject(string[] identifiers)
        {
            foreach (string i in identifiers)
            {
                _identifiers.Add(i.ToLower());
            }
        }

        public bool AreYou(string id)
        {
            return _identifiers.Contains(id.ToLower());
        }

        public string FirstId
        {
            get
            {
                if (_identifiers.Count == 0) return "";
                return _identifiers[0];
            }
        }

        // Block duplicates
        public void AddIdentifier(string id)
        {
            string key = id.ToLower();
            if (!_identifiers.Contains(key))
            {
                _identifiers.Add(key);
            }
        }

        // Return true if something was removed, false otherwise
        public bool RemoveIdentifier(string id)
        {
            return _identifiers.Remove(id.ToLower());
        }

        public void PrivilegeEscalation(string pin)
        {
            if (pin == "3500")
            {
                _identifiers[0] = "Class tuesday afternoon";
            }
        }
        protected int IdentifierCount => _identifiers.Count;
    }
}
