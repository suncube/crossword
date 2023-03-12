using System;

namespace WW.ConfigData
{
    [Serializable]
    public class WordBoardData
    {
        public string[] Board;

        public WordBoardData() { }
        public WordBoardData(string[] board)
        {
            Board = board;
        }
    }

}



