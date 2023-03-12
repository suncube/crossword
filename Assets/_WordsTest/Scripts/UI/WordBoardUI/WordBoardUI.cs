using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using WW.ConfigData;
using WW.Models;
using WW.Service;
using WW.Signals;
using Zenject;

namespace WW.UI.Board
{
    public class WordBoardUI : MonoBehaviour
    {
        [SerializeField] private GridLayoutGroup _board;

        [Inject]
        private WordBoardItem.Factory _boardItemFactory;
        [Inject]
        private GameSettings _gameSettings;
        [Inject]
        private SoundPlayer _soundPlayer;
        [Inject]
        private SignalBus _signalBus;

        public bool IsInited { get; private set; } = false;

        private WordBoardItem[,] _grid;

        private void Awake()
        {
            _signalBus.Subscribe<OpenWordSignal>(OnOpenWordSignal);
            _signalBus.Subscribe<WrongWordSignal>(OnWrongWordSignal);
        }
        private void OnDestroy()
        {
            _signalBus.Unsubscribe<OpenWordSignal>(OnOpenWordSignal);
            _signalBus.Unsubscribe<WrongWordSignal>(OnWrongWordSignal);
        }

        private void OnWrongWordSignal()
        {
            AnimateWrongReactForApply();
        }

        private void OnOpenWordSignal(OpenWordSignal signal)
        {
            OpenWordOnBoard(signal.OpenWord).Forget();
        }

        public void Initialize(WordBoardModel board)
        {
            CreateGrid(board.Width, board.Height);
            InitGrid(board);
            IsInited = true;
        }

        private void CreateGrid(int width, int height)
        {
            _board.constraintCount = width;
            _grid = new WordBoardItem[width, height];

            var root = _board.transform;

            for (var i = 0; i < width; i++)
            {
                for (var j = 0; j < height; j++)
                {
                    var item = _boardItemFactory.Create(root);

                    _grid[i, j] = item;
                }
            }
        }

        private void InitGrid(WordBoardModel board)
        {
            for (var i = 0; i < board.Words.Count; i++)
            {
                var word = board.Words[i];

                for (var j = 0; j < word.Letters.Length; j++)
                {
                    var letter = word.Letters[j];
                    _grid[letter.Position.Item1, letter.Position.Item2].Initialize(letter);
                }
                    
            }
        }

        private async UniTask OpenWordOnBoard(Word word)
        {
            for (var i = 0; i < word.Letters.Length; i++)
            {
                var letter = word.Letters[i];
                var item = _grid[letter.Position.Item1, letter.Position.Item2];

                if (item.IsOpened) continue;

                await item.AnimateOpenLetter();
                await UniTask.Delay(500);
            }
            _signalBus.Fire<OpenWordAnimateEndedSignal>();
        }

        private void AnimateWrongReactForApply()
        {
            if (_gameSettings.AlarmBehaviorType == AlarmBehaviorType.Shake)
            {
                transform.DOKill(true);
                transform.DOShakeRotation(1, 0.5f, randomness: 0);
            }
            else
            if (_gameSettings.AlarmBehaviorType == AlarmBehaviorType.Sound)
            {
                _soundPlayer.PlayWrongWord();
            }

        }
    }
}