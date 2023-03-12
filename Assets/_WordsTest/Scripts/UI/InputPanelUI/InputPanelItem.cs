using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using WW.Models;
using Zenject;

namespace WW.UI.Input
{
    public class InputPanelItem : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private TextMeshProUGUI _count;
        [SerializeField] private Button _clickBtn;

        public Action<LetterInfo> OnClick;

        private LetterInfo _levelStat;

        [Inject]
        public void Initialize(LetterInfo levelStat, Transform root)
        {
            transform.SetParent(root);
            transform.localScale = Vector3.one;
            _levelStat = levelStat;
            _text.text = _levelStat.Value;
            _count.text = _levelStat.Count.ToString();

            _levelStat.OnCountChanged += OnCountChanged;

            gameObject.SetActive(true);
        }

        private void Awake()
        {
            _clickBtn.onClick.AddListener(OnClickBtnClick);
        }

        private void OnDestroy()
        {
            if(_levelStat != null) //???????
                _levelStat.OnCountChanged -= OnCountChanged;
        }

        private void OnClickBtnClick()
        {
            OnClick?.Invoke(_levelStat);
        }

        private void OnCountChanged(int count)
        {
            _count.text = count.ToString();
            gameObject.SetActive(count > 0);
        }

        public class Factory : PlaceholderFactory<LetterInfo, Transform,InputPanelItem> { }
    }
}