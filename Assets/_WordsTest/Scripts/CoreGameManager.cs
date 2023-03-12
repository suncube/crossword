using Cysharp.Threading.Tasks;
using UnityEngine;
using WW.Models;
using WW.UI.Board;
using WW.UI.Input;
using WW.Signals;
using Zenject;
using WW.ConfigData;

public class CoreGameManager : MonoBehaviour
{
    private WordBoardModel _boardModel;
    private WordBoardData _boardData;
    private WordBoardUI _wordBoardView;
    private InputPanelUI _inputPanelView;
    private SignalBus _signalBus;

    [Inject]
    public void Initialize(
        WordBoardData boardData,
        WordBoardUI wordBoardView,
        InputPanelUI inputPanelView,
        SignalBus signalBus)
    {
        _signalBus = signalBus;
        _wordBoardView = wordBoardView;
        _boardData = boardData;
        _inputPanelView = inputPanelView;
    }

    private void Awake()
    {
        _signalBus.Subscribe<ApplyWordSignal>(OnTryApplyWord);
        _signalBus.Subscribe<OpenWordAnimateEndedSignal>(OnCheckEndGame);
    }

    private void OnDestroy()
    {
        _signalBus.Unsubscribe<ApplyWordSignal>(OnTryApplyWord);
        _signalBus.Unsubscribe<OpenWordAnimateEndedSignal>(OnCheckEndGame);
    }

    private async UniTask Start() 
    {
        _boardModel = await WordBoardModel.CreateAsync(_boardData);

        InitWordBoard().Forget();
        InitInputBoard().Forget();
    }

    private async UniTask InitWordBoard()
    {
        _wordBoardView.Initialize(_boardModel);
        await UniTask.WaitUntil(() => !_wordBoardView.IsInited);
    }
    private async UniTask InitInputBoard()
    {
        _inputPanelView.Initialize(_boardModel);
        await UniTask.WaitUntil(()=> !_inputPanelView.IsInited);
    }

    private void OnCheckEndGame()
    {
        if (_boardModel.IsGameAllWordComplete())
        {
            Debug.Log("[ALL WORDS OPENED]");
        }
    }

    private void OnTryApplyWord(ApplyWordSignal signal)
    {
        var result = _boardModel.GetWordByLetters(signal.CurrentLetters);

        if (result != null)
        {
            result.OpenWord();

            _signalBus.Fire(new OpenWordSignal(result));
        }
        else
        {
            _signalBus.Fire<WrongWordSignal>();
        }
    }
}

