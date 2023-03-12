using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using WW.ConfigData;
using WW.Models;
using Zenject;

namespace WW.UI.Board
{
    //todo refact
    public class WordBoardItem : MonoBehaviour
    {
        [SerializeField] private Image _background;
        [SerializeField] private TextMeshProUGUI _text;

        [Inject]
        private BoardConfig _boardConfig;

        public bool IsOpened => _viewStatus == BoardItemStatus.Open;

        private BoardItemStatus _viewStatus = BoardItemStatus.Default;
        private Letter _letter;

        [Inject]
        public void SetParent(Transform root)
        {
            transform.SetParent(root);
            transform.localScale = Vector3.one;
        }

        private void Awake()
        {
            UpdateState();
        }

        public void Initialize(Letter letter)
        {
            _text.text = letter.Value.ToString();
            _letter = letter;
            _viewStatus = BoardItemStatus.Active;

            UpdateState();
        }

        public async UniTask AnimateOpenLetter()
        {
            _viewStatus = BoardItemStatus.Open;
            var sequence = DOTween.Sequence();
            sequence.Append(transform.DORotate(new Vector3(0,90,0), 0.5f));
            sequence.AppendCallback(()=>{
                UpdateState();
            });
            sequence.Append(transform.DORotate(Vector3.zero, 0.5f));
            sequence.AppendCallback(() => {
            });

            await UniTask.WaitUntil(() => sequence.IsComplete() == false);
           
        }

        private void UpdateState()
        {
            _background.color = _boardConfig.GetStateColor(_viewStatus);
            _text.gameObject.SetActive(_viewStatus == BoardItemStatus.Open);
        }

        public class Factory : PlaceholderFactory<Transform,WordBoardItem> { }
    }

    public enum BoardItemStatus
    {
        Default,
        Open,
        Active,
    }

}