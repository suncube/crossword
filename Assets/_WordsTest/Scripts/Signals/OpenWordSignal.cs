using WW.Models;

namespace WW.Signals
{
    public class OpenWordSignal
    {
        public Word OpenWord { get; private set; }

        public OpenWordSignal(Word openWord)
        {
            OpenWord = openWord;
        }
    }
}