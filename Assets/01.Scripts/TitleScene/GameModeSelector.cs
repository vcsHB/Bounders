using Core.DataManage;
using PongGameSystem;
using UIManage;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TitleScene
{

    public class GameModeSelector : MonoSingleton<GameModeSelector>
    {
        [SerializeField] private SceneExitPanel _sceneExitPanel;
        [SerializeField] private float _sceneMovementTerm = 2f;
        [SerializeField] private string _gameSceneName = "PongGameScene";
        
        public void SaveGameModeData(GamePlayData data)
        {
            DBManager.SavePlayData(data);
            _sceneExitPanel.Open();
            Invoke(nameof(HandleMoveToGameScene), _sceneMovementTerm);

        }

        private void HandleMoveToGameScene()
        {
            SceneManager.LoadScene(_gameSceneName);
        }
    }

}