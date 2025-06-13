using System;
using PongGameSystem;
using UIManage;
using UnityEngine;
using UnityEngine.UI;
namespace TitleScene
{

    public class GameSelectionCheckPanel : UIPanel
    {
        [SerializeField] private GamePlayData _gamePlayData;
        private Button _parentButton;
        private Button _confirmButton;

        protected override void Awake()
        {
            base.Awake();
            _parentButton = transform.parent.GetComponent<Button>();
            _confirmButton = GetComponentInChildren<Button>();

            _parentButton.onClick.AddListener(Toggle);
            _confirmButton.onClick.AddListener(HandleGameSelect);
        }

        public void Toggle()
        {
            if (_isActive)
                Close();
            else
                Open();
        }

        private void HandleGameSelect()
        {
            GameModeSelector.Instance.SaveGameModeData(_gamePlayData);
        }
    }
}