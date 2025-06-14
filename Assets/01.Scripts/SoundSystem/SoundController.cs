using Core.DataManage;
using UIManage.Other;
using UnityEngine;
using UnityEngine.Audio;
namespace SoundManage
{

    public class SoundController : MonoBehaviour
    {
        [SerializeField] private AudioMixer _audioMixer;
        private GameSetting _settingData;
        [SerializeField] private SettingPanel _settingPanel;

        private void Awake()
        {
            Load(DBManager.GetGameSetting());
        }

        public void Load(GameSetting data)
        {
            _settingData = data;

            if (_settingPanel != null)
            {
                _settingPanel.InitailizeSettingSlider(_settingData.bgmVolume, _settingData.sfxVolume);
                _settingPanel.OnBGMVolumeChangedEvent += SetBGMVolume;
                _settingPanel.OnSFXVolumeChangedEvent += SetSFXVolume;
            }

            _audioMixer.SetFloat("BGM_Volume", _settingData.bgmVolume);
            _audioMixer.SetFloat("SFX_Volume", _settingData.sfxVolume);
        }

        public void Save()
        {
            DBManager.SaveGameSetting(_settingData);
        }


        public void SetBGMVolume(float volume)
        {
            if (Mathf.Approximately(volume, -40f))
                volume = -80f;
            _settingData.bgmVolume = volume;
            _audioMixer.SetFloat("BGM_Volume", volume);
        }

        public void SetSFXVolume(float volume)
        {
            if (Mathf.Approximately(volume, -40f))
                volume = -80f;
            _settingData.sfxVolume = volume;
            _audioMixer.SetFloat("SFX_Volume", volume);
        }

    }
}