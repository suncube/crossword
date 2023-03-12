using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WW.ConfigData;
using WW.UI.Board;
using Zenject;

namespace WW.Installers
{
    [CreateAssetMenu(fileName = "GameConfigInstaller", menuName = "WordWorld/Installers/GameConfigInstaller")]
    public class GameConfigInstaller : ScriptableObjectInstaller<GameConfigInstaller>
    {
        public BoardData BoardDataJson;
        public GameSettingsData SettingsDataJson;
        public BoardConfig BoardSettings;

        public override void InstallBindings()
        {
            Container.BindInstance(BoardDataJson.GetWordBoardData());//WordBoardData
            Container.BindInstance(SettingsDataJson.GetGameSetting());//GameSettings

            Container.BindInstance(BoardSettings);
        }

    }
}
