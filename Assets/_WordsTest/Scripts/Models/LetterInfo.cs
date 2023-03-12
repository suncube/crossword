using System;

namespace WW.Models
{
    public class LetterInfo
    {
        public string Value { get; private set; }
        public int Count { get; private set; }

        public Action<int> OnCountChanged;

        public LetterInfo(string value, int count = 1)
        {
            Value = value;
            Count = count;
        }

        public void IncrementCount()
        {
            Count++;
        }

        public void UseLetter()
        {
            Count--;
            OnCountChanged?.Invoke(Count);
        }

        public void UnuseLetter()
        {
            IncrementCount();
            OnCountChanged?.Invoke(Count);
        }

    }
}
