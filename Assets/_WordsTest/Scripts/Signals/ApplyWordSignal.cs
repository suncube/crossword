using WW.Models;

namespace WW.Signals
{
    public class ApplyWordSignal
    {
        public LetterInfo[] CurrentLetters { get; private set; }

        public ApplyWordSignal(LetterInfo[] currentLetters)
        {
            CurrentLetters = currentLetters;
        }
    }
}