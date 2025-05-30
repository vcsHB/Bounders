using UIManage.GameScene;
using UnityEngine;

namespace Core.GameSystem
{
    public class GameManager : MonoSingleton<GameManager>
    {
        [SerializeField] private ParticleSystem[] _defeatVFX;
        [SerializeField] private BattlePanel[] _winnerPanel;
        [SerializeField] private bool _isGameOver;

        public void HandlePlayerDie(int index)
        {
            if (_isGameOver) return;
            _isGameOver = true;
            _defeatVFX[index].Play();
            _winnerPanel[1 - index].Open();
        }
    }

}