using System;
using System.Text;
using Core.GameSystem;
using TMPro;
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
        [SerializeField] private TextMeshProUGUI[] _secondPlayerNameTexts;
        private string _secondPlayerName;
        public void InitGameSet(GamePlayData data)
        {
            StringBuilder stringBuilder = new StringBuilder();
            if (data.gameType == GameTypeEnum.PVE)
            {
                SetAIMode(true);
                stringBuilder.Append("AI ");
            }
            else
            {
                SetAIMode(false);
                stringBuilder.Append("Player 2");

            }
            switch (data.detailSetting)
            {
                case GameDetailSettingEnum.Easy:
                    stringBuilder.Append(data.detailSetting);
                    break;
                case GameDetailSettingEnum.Normal:
                    GenerateObstacle();
                    stringBuilder.Append(data.detailSetting);
                    break;
                case GameDetailSettingEnum.Hard:
                    GenerateObstacle();
                    GeneratePortal();
                    stringBuilder.Append(data.detailSetting);
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
            _secondPlayerName = stringBuilder.ToString();
            for (int i = 0; i < _secondPlayerNameTexts.Length; i++)
            {
                _secondPlayerNameTexts[i].text = _secondPlayerName;
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
            ObstacleData randomData = _obstacles[UnityEngine.Random.Range(0, _obstacles.Length)];
            Instantiate(randomData.obstaclePrefab, _envTrm);
        }

        private void GeneratePortal()
        {
            PortalData randomData = _portals[UnityEngine.Random.Range(0, _portals.Length)];
            Instantiate(randomData.portalGroupPrefab, _envTrm);
        }
    }
}