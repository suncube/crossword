using System;
using UnityEngine;

namespace WW.ConfigData
{
    [Serializable]
    public class GameSettingsData
    {
        public TextAsset SettingsJson;

        public GameSettings GetGameSetting()
        {
            var settings = new GameSettings();
            JsonUtility.FromJsonOverwrite(SettingsJson.text, settings);
            return settings;
        }
    }
}