using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
namespace UIManage
{

    public class PlayOrderSelectButton : UIPanel
    {
        [SerializeField] private int _id;
        [SerializeField] private Image _xMarkImage;
        [SerializeField] private float _xMarkTweenDuration = 0.3f;

        public event Action<int> OnPlayerSelectEvent;

        private Button _button;

        protected override void Awake()
        {
            base.Awake();
            _button = GetComponent<Button>();
            _button.onClick.AddListener(HandleButtonClick);
        }

        private void HandleButtonClick()
        {
            OnPlayerSelectEvent?.Invoke(_id);
        }

        public void OpenXMark()
        {
            SetInteractable(false);
            _xMarkImage.enabled = true;
            _xMarkImage.transform.DOScale(1f, _xMarkTweenDuration);
        }

    }

}