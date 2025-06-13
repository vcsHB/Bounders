using Core.GameSystem;
using UnityEngine;
namespace PongGameSystem
{

    public class GameUIController : MonoBehaviour
    {
        // TODO : Player View UI Control
        [SerializeField] private GameViewPanel[] _gameViews;
        

        public void SetGameView(GameTypeEnum gameType)
        {
            switch (gameType)
            {
                case GameTypeEnum.PVE:
                    _gameViews[0].Open();
                    _gameViews[1].Close();
                    break;

                case GameTypeEnum.PVP:
                    _gameViews[0].Open();
                    _gameViews[1].Open();
                    break;
            }
        }
    }
}