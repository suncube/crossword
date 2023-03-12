using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using WW.Models;
using WW.Signals;
using Zenject;

namespace WW.UI.Input
{
    public class InputPanelUI : MonoBehaviour
    {
        [SerializeField] private Transform _root;
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private Button _applyBtn;

        [Inject]
        private InputPanelItem.Factory _inputItemFactory;
        [Inject]
        private SignalBus _signalBus;

        public bool IsInited { get; private set; } = false;

        private WordBoardModel _board;
        private List<LetterInfo> _currentLetters;

        private void Awake()
        {
            _applyBtn.onClick.AddListener(OnApplyBtnClick);
            _signalBus.Subscribe<WrongWordSignal>(OnWrongWord);
            _signalBus.Subscribe<OpenWordSignal>(ResetInput);
        }

        private void OnDestroy()
        {
            _signalBus.Unsubscribe<WrongWordSignal>(OnWrongWord);
            _signalBus.Unsubscribe<OpenWordSignal>(ResetInput);
        }

        public void Initialize(WordBoardModel board)
        {
            _board = board;
            _text.text = "";
            _currentLetters = new List<LetterInfo>();
            CreateInputLeters(_board.LettersStat);

            IsInited = true;
        }

        private void CreateInputLeters(Dictionary<string, LetterInfo> lettersStat)
        {
            foreach(var letter in lettersStat)
            {
                var inputItem = _inputItemFactory.Create(letter.Value, _root);
                inputItem.OnClick += OnLetterSelected;
            }
        }

        private void OnLetterSelected(LetterInfo letter)
        {
            _text.text += letter.Value;

            _currentLetters.Add(letter);
            letter.UseLetter();// todo command
        }

        private void OnApplyBtnClick()
        {
            if (_currentLetters.Count == 0) return;

            _signalBus.Fire(new ApplyWordSignal(_currentLetters.ToArray()));
        }

        private void OnWrongWord()
        {
            for (var i = 0; i < _currentLetters.Count; i++)
            {
                _currentLetters[i].UnuseLetter();
            }

            ResetInput();
        }

        private void ResetInput()
        {
            _text.text = "";
            _currentLetters = new List<LetterInfo>();
        }

    }
}