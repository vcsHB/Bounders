using Core.GameSystem;
using UnityEngine;
namespace PongGameSystem
{

    public class AttackOrderController : MonoBehaviour
    {
        [SerializeField] private BallHolder[] _ballHolders;
        [SerializeField] private BallHolder[] _enabledHolders = new BallHolder[2];

        public void SetGameMode(GameTypeEnum gameType) // Init Setting
        {
            _enabledHolders[0] = _ballHolders[0];

            if (gameType == GameTypeEnum.PVE)
                _enabledHolders[1] = _ballHolders[1];
            else
            {
                _enabledHolders[1] = _ballHolders[2];

            }
        }

        public void SetBallOwner(int index)
        {
            _enabledHolders[index].SetBallOwner();
            
        }
    }
}