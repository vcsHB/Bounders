using System;
using System.Collections;
using Core.DataManage;
using Core.InputSystem;
using PongGameSystem;
using UIManage.GameScene;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core.GameSystem
{
    public enum GameTypeEnum
    {
        PVE,
        PVP
    }
    public enum GameDetailSettingEnum
    {
        Easy,
        Normal,
        Hard,
        Original,
        Hell,
        Obstacle,
        Portal

    }
    public class GameManager : MonoSingleton<GameManager>
    {
        [SerializeField] private PlayerInputReader[] _inputReaders;
        [SerializeField] private GameTypeEnum _currentGameType;
        [SerializeField] private ParticleSystem[] _defeatVFX;
        [SerializeField] private BattlePanel[] _winnerPanel;
        [SerializeField] private GameUIController _gameUIController;
        [SerializeField] private GameModeSetter _modeSetter;
        [SerializeField] private AttackOrderController _orderController;
        [SerializeField] private bool _isGameOver;
        [SerializeField] private float _waitTerm = 2f;
        private GamePlayData _gameData;
        private int _currentOrderId;
        protected override void Awake()
        {
            base.Awake();
            _gameData = DBManager.GetGameData();
            _modeSetter.InitGameSet(_gameData);
            _orderController.SetGameMode(_gameData.gameType);
        }
        private void Start()
        {
            SetGameSetting(_gameData);

        }

        public void HandlePlayerDie(int index)
        {
            if (_isGameOver) return;
            _isGameOver = true;
            _defeatVFX[index].Play();
            _winnerPanel[1 - index].Open();
        }


        private void SetGameSetting(GamePlayData playData)
        {
            _gameUIController.SetGameView(playData.gameType);

        }

        public void GameStart(int orderId)
        {
            _currentOrderId = orderId;
            StartCoroutine(GameTermCoroutine(HandleSetPlayerBall));
        }
        private void HandleSetPlayerBall()
        {
            _orderController.SetBallOwner(_currentOrderId);
        }

        private IEnumerator GameTermCoroutine(Action resetEvent)
        {
            yield return new WaitForSeconds(_waitTerm);
            if (!_isGameOver)
                resetEvent?.Invoke();

        }

        public void MoveToTitle()
        {
            SceneManager.LoadScene("TitleScene");
        }

    }

}