using System.IO;
using PongGameSystem;
using UnityEngine;

namespace Core.DataManage
{

    public static class DBManager
    {
        private static string LOCALPATH = Application.dataPath + "/SaveData";
        private static string PlayDataSaveFileName = "PlayData.json";
        private static string GameSettingFileName = "GameSetting.json";


        public static GameSetting GetGameSetting()
        {
            string path = Path.Combine(LOCALPATH, GameSettingFileName);
            CheckLocalPath();
            if (File.Exists(path))
            {
                string data = File.ReadAllText(path);
                return JsonUtility.FromJson<GameSetting>(data);
            }

            GameSetting newSetting = new GameSetting();
            SaveGameSetting(newSetting);
            return newSetting;
        }

        public static void SaveGameSetting(GameSetting gameSetting)
        {
            CheckLocalPath();
            string json = JsonUtility.ToJson(gameSetting, true);
            string path = Path.Combine(LOCALPATH, GameSettingFileName);
            File.WriteAllText(path, json);

        }

        public static GamePlayData GetGameData()
        {
            CheckLocalPath();
            string path = Path.Combine(LOCALPATH, PlayDataSaveFileName);
            if (File.Exists(path))
            {
                string data = File.ReadAllText(path);
                return JsonUtility.FromJson<GamePlayData>(data);
            }

            GamePlayData newData = new GamePlayData();
            SavePlayData(newData);
            return newData;
        }

        public static void SavePlayData(GamePlayData data)
        {
            CheckLocalPath();
            string json = JsonUtility.ToJson(data, true);
            string path = Path.Combine(LOCALPATH, PlayDataSaveFileName);
            File.WriteAllText(path, json);

        }

        private static void CheckLocalPath()
        {
            if (!Directory.Exists(LOCALPATH))
            {
                Debug.Log("폴더가 존재하지 않습니다.");
                Debug.Log("폴더를 생성합니다.");
                Directory.CreateDirectory(LOCALPATH);
            }
        }


    }
}