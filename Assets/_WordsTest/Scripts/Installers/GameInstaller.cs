using UnityEngine;
using WW.Models;
using WW.Service;
using WW.Signals;
using WW.UI.Board;
using WW.UI.Input;
using Zenject;

namespace WW.Installers
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private WordBoardUI _wordBoardView;
        [SerializeField] private WordBoardItem _wordBoardItem;

        [SerializeField] private InputPanelUI _imputPanelView;
        [SerializeField] private InputPanelItem _inputPanelItem;

        [SerializeField] private SoundPlayer _SoundPlayer;

        public override void InstallBindings()
        {
            Container.Bind<SoundPlayer>().FromInstance(_SoundPlayer).AsSingle();
            Container.Bind<WordBoardUI>().FromInstance(_wordBoardView).AsSingle();
            Container.Bind<InputPanelUI>().FromInstance(_imputPanelView).AsSingle();

            Container.BindFactory<Transform, WordBoardItem, WordBoardItem.Factory>().FromComponentInNewPrefab(_wordBoardItem);
            Container.BindFactory<LetterInfo, Transform, InputPanelItem, InputPanelItem.Factory>().FromComponentInNewPrefab(_inputPanelItem);

            SignalBusInstaller.Install(Container);

            Container.DeclareSignal<ApplyWordSignal>();
            Container.DeclareSignal<WrongWordSignal>();
            Container.DeclareSignal<OpenWordSignal>();
            Container.DeclareSignal<OpenWordAnimateEndedSignal>();

        }
    }
}

