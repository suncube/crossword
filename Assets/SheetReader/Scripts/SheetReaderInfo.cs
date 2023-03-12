using System;
using UnityEngine;

namespace SC.SheetReader
{
    [Serializable]
    public class SheetReaderInfo
    {
        public TextAsset Credentials;
        public string SpreadsheetId = "1IH6bBbCAUyKA4PMi-YWKZHAzdPQrgNQsoBE9dB3NdEA";
        public SheetInfo[] Sheets;
    }
}