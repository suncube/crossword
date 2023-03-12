using System;

namespace SC.SheetReader
{
    [Serializable]
    public class SheetInfo
    {
        public string Name = "Board1";
        public string From = "A";
        public string To = "J";

        public string GetRequest => $"{Name}!{From}:{To}";
    }
}