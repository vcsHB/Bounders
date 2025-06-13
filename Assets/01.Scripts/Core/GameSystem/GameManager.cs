using PongGameSystem;
using UIManage.GameScene;
using UnityEngine;

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

        [SerializeField] private GameTypeEnum _currentGameType;
        [SerializeField] private ParticleSystem[] _defeatVFX;
        [SerializeField] private BattlePanel[] _winnerPanel;
        [SerializeField] private GameUIController _gameUIController;
        [SerializeField] private bool _isGameOver;

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
    }

}