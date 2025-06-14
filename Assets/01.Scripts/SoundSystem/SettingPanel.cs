using System;
using UnityEngine;
using UnityEngine.UI;
namespace UIManage.Other
{

    public class SettingPanel : MonoBehaviour
    {
        public event Action<float> OnBGMVolumeChangedEvent;
        public event Action<float> OnSFXVolumeChangedEvent;
        [SerializeField] private Slider _bgmSlider;
        [SerializeField] private Slider _sfxSlider;

        private void Start()
        {
            _bgmSlider.onValueChanged.AddListener(HandleBGMChanged);
            _sfxSlider.onValueChanged.AddListener(HandleSFXChanged);
        }


        private void HandleBGMChanged(float value)
        {
            OnBGMVolumeChangedEvent?.Invoke(value);
        }
        private void HandleSFXChanged(float value)
        {
            OnSFXVolumeChangedEvent?.Invoke(value);
        }

        public void InitailizeSettingSlider(float bgmVolume, float sfxVolume)
        {
            _bgmSlider.value = bgmVolume;
            _sfxSlider.value = sfxVolume;
        }
    }
}