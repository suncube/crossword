using System.Collections.Generic;
using System.Text;
using Cysharp.Threading.Tasks;
using UnityEngine;
using WW.ConfigData;

namespace WW.Models
{
    public class WordBoardModel
    {
        public int Width { get; private set; }
        public int Height { get; private set; }

        public List<Word> Words { get; private set; }
        public Dictionary<string, LetterInfo> LettersStat { get; private set; } // for input model
        private bool _isInited;

        public static async UniTask<WordBoardModel> CreateAsync(WordBoardData boardData)
        {
            var result = new WordBoardModel();

            await result.Initialize(boardData);

            return result;
        }

        private async UniTask Initialize(WordBoardData boardData)
        {
            Words = new List<Word>();
            LettersStat = new Dictionary<string, LetterInfo>();

            // create letters
            var first = CreateLetters(boardData);

            ProcessingWords(first);

            await UniTask.WaitUntil(() => _isInited);

            PrintFoundWords();
        }

        public bool IsGameAllWordComplete()
        {
            foreach (var word in Words)
            {
                if (!word.IsOpened)
                    return false;
            }

            return true;
        }


        public Word GetWordByLetters(LetterInfo[] letters)
        {
            var input = new StringBuilder(letters.Length);
            foreach (var l in letters)
            {
                input.Append(l.Value);
            }

            var compare = input.ToString();

            foreach (var word in Words)
            {
                if (word.Value == compare)
                    return word;
            }

            return null;
        }

#region Parce Methods

        private void PrintFoundWords()
        {
            Debug.Log("Find words");
            foreach (var word in Words)
            {
                Debug.Log(word.Value);
            }
        }

        private Letter CreateLetters(WordBoardData data)
        {
            //
            Height = data.Board.Length;
            //
            Letter firstLetter = null;

            var previousLine = new Queue<Letter>();

            for (var row = data.Board.Length-1; row >= 0; row--)
            {
                var line = data.Board[row].Split(",");
                Width = line.Length;
                var curLine = new Queue<Letter>(Width);

                Letter previous = null;

                for (var col = line.Length-1; col >= 0; col--)
                {
                    var curLetter = new Letter(line[col], (row,col));
                    firstLetter = curLetter;

                    curLetter.SetRightLink(previous);
                    previous = curLetter;

                    if(previousLine.Count != 0)
                        curLetter.SetDownLink(previousLine.Dequeue());

                    curLine.Enqueue(curLetter);
                }

                previousLine = curLine;
            }

            return firstLetter;
        }

        private void ProcessingWords(Letter firstLetter)
        {
            var current  = firstLetter;
            var firstInLine = firstLetter;

            while (firstInLine != null)
            {
                while (current != null)
                {
                    TryFindWord(current);
              
                    current = current.Right;
                }

                firstInLine = firstInLine.Down;
                current = firstInLine;
            }
            _isInited = true;
        }

        private void TryFindWord(Letter letter)
        {
            if(letter.IsEmpty) return;

            if(!letter.IsRightUsed && !letter.IsRightEmpty)// horizontal
            {
                Words.Add(CreateWord(letter, DirectionType.Rigth));
            }

            if (!letter.IsDownUsed && !letter.IsDownEmpty)// vertical
            {
                Words.Add(CreateWord(letter, DirectionType.Down));
            }
        }

        private Word CreateWord(Letter letter,DirectionType direction)
        {
            Letter current = letter;

            var word = new Queue<Letter>();
            while (current != null)
            {

                AddLetterToStat(current.Value);

                word.Enqueue(current);
                current.MarkerUsedDirection(direction);//

                current = current.GetNextIfCanMove(direction);
            }

            return new Word(word.ToArray());
        }

        private void AddLetterToStat(string letter)
        {
            if (LettersStat.ContainsKey(letter))
            {
                LettersStat[letter].IncrementCount();
            }
            else
            {
                LettersStat.Add(letter, new LetterInfo(letter));
            }
        }

#endregion Parce Methods

    }
}
