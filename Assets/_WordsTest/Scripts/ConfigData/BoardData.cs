using System;
using UnityEngine;

namespace WW.ConfigData
{
    [Serializable]
    public class BoardData
    {
        public TextAsset BoardJson;

        public WordBoardData GetWordBoardData()
        {
            var data = new WordBoardData();
            JsonUtility.FromJsonOverwrite(BoardJson.text, data);
            return data;
        }
    }
}