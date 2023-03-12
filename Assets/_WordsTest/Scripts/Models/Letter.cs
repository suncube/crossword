namespace WW.Models
{
    public class Letter: LinkedLetter
    {
        public (int, int) Position { get; private set; }
        public string Value { get; private set; }

        public Letter(string value, (int, int) position)
        {
            Value = value;
            Position = position;
            IsEmpty = string.IsNullOrEmpty(value);
        }
    }
}
