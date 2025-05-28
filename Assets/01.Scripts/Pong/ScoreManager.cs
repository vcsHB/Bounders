using UnityEngine;
namespace Pong
{
    public enum PlayerType
    {
        Player1,
        Player2,
        AI
    }
    public class ScoreManager : MonoSingleton<ScoreManager>
    {
        [SerializeField] private int _player1Score;
        [SerializeField] private int _player2Score;
        [SerializeField] private int _goalScore;


        public void AddScore(PlayerType playerType, int amount)
        {
            switch (playerType)
            {
                case PlayerType.Player1:
                    _player1Score += amount;
                    break;
                case PlayerType.Player2:
                    _player2Score += amount;
                    break;
                case PlayerType.AI:
                    break;
            }
        }
    }
}