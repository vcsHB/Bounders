using Core.GameSystem;
using UnityEngine;
namespace PongGameSystem
{

    public class GameModeSetter : MonoBehaviour
    {
        [SerializeField] private ObstacleData[] _obstacles;
        [SerializeField] private PortalData[] _portals;
        [SerializeField] private Transform _secondPlayerTrm;
        [SerializeField] private Transform _AIPlayerTrm;
        [SerializeField] private float _enablePosition = -9.5f;
        [SerializeField] private float _disablePosition = -20f;
        [SerializeField] private Transform _envTrm;
        public void InitGameSet(GamePlayData data)
        {
            if (data.gameType == GameTypeEnum.PVE)
                SetAIMode(true);
            else
            {
                SetAIMode(false);

            }
            switch (data.detailSetting)
            {
                case GameDetailSettingEnum.Easy:
                    break;
                case GameDetailSettingEnum.Normal:
                    GenerateObstacle();
                    break;
                case GameDetailSettingEnum.Hard:
                    GenerateObstacle();
                    GeneratePortal();
                    break;
                case GameDetailSettingEnum.Original:
                    // DO nothing
                    break;
                case GameDetailSettingEnum.Hell:
                    GenerateObstacle();
                    GeneratePortal();
                    break;
                case GameDetailSettingEnum.Obstacle:
                    GenerateObstacle();
                    break;
                case GameDetailSettingEnum.Portal:
                    GeneratePortal();
                    break;
            }
        }

        private void SetAIMode(bool value)
        {
            if (value)
            {
                _AIPlayerTrm.position = new Vector3(_enablePosition, 0.5f, 0f);
                _secondPlayerTrm.position = new Vector3(_disablePosition, 0.5f, 0f);
            }
            else
            {
                _AIPlayerTrm.position = new Vector3(_disablePosition, 0.5f, 0f);
                _secondPlayerTrm.position = new Vector3(_enablePosition, 0.5f, 0f);
            }
        }
        private void GenerateObstacle()
        {
            ObstacleData randomData = _obstacles[Random.Range(0, _obstacles.Length)];
            Instantiate(randomData.obstaclePrefab, _envTrm);
        }

        private void GeneratePortal()
        {
            PortalData randomData = _portals[Random.Range(0, _portals.Length)];
            Instantiate(randomData.portalGroupPrefab, _envTrm);
        }
    }
}