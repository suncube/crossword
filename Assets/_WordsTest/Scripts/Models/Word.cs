using System.Text;

namespace WW.Models
{
    public class Word
    {
        public Letter[] Letters { get; private set; }
        public bool IsOpened { get; private set; }
        public string Value { get; private set; }

        public Word(Letter[] letters)
        {
            Letters = letters;

            StringBuilder value = new StringBuilder();
            foreach (var let in Letters)
            {
                value.Append(let.Value);
            }

            Value = value.ToString();
        }

        public void OpenWord()
        {
            IsOpened = true;
        }
    }
}
