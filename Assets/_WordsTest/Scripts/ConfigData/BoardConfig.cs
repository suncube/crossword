using System;
using UnityEngine;
using WW.UI.Board;

namespace WW.ConfigData
{
    [Serializable]
    public class BoardConfig
    {
        public Color DefaultColor;
        public Color ActiveColor;
        public Color OpenColor;

        public Color GetStateColor(BoardItemStatus boardItemStatus)
        {
            switch (boardItemStatus)
            {
                case BoardItemStatus.Default:
                    return DefaultColor;
                case BoardItemStatus.Active:
                    return ActiveColor;
                case BoardItemStatus.Open:
                    return OpenColor;
            }

            return DefaultColor;
        }
    }
}