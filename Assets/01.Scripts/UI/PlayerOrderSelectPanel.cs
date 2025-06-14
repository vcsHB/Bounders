using System;
using Core.GameSystem;
using TMPro;
using UnityEngine;
namespace UIManage.GameScene
{

    public class PlayerOrderSelectPanel : UIPanel
    {
        [SerializeField] private TextMeshProUGUI _playerSelectText;
        [SerializeField] private Color[] _playerColor;
        [SerializeField] private PlayOrderSelectButton[] _orderSelectButtons;
        [SerializeField] private float _disableTerm = 3f;
        private int _selectedOrderID;

        protected override void Awake()
        {
            base.Awake();
            for (int i = 0; i < _orderSelectButtons.Length; i++)
            {
                _orderSelectButtons[i].OnPlayerSelectEvent += HandleSelectButton;
            }
        }

        private void HandleSelectButton(int id)
        {
            _selectedOrderID = id;
            _orderSelectButtons[id].Close();
            _orderSelectButtons[1 - id].OpenXMark();
            _playerSelectText.color = _playerColor[id];
            _playerSelectText.text = $"Player{id + 1}";
            Invoke(nameof(HandleSelectOrder), _disableTerm);
        }

        private void HandleSelectOrder()
        {
            Close();
            GameManager.Instance.GameStart(_selectedOrderID);
        }
    }
}