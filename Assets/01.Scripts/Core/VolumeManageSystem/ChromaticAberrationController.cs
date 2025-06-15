using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
namespace Core.VolumeControlSystem
{

    public class ChromaticAberrationController : VolumeController
    {
        private ChromaticAberration _chromaticEffect;

        private bool _isChromaticEffectTweening;
        private readonly float _defualtChromaticAberrationLevel = 0f;

        public override void Initialize(Volume globalVolume)
        {
            globalVolume.profile.TryGet(out _chromaticEffect);

        }


        public void HandleEnableChromatic()
        {
            SetChromatic(0.13f);
        }

        public void HandleDisableChromatic()
        {
            SetChromatic(0f);
        }


        public void SetChromatic(float value)
        {
            _chromaticEffect.intensity.value = value;

        }
        public void SetChromatic(float intensity, float duration)
        {
            if (_isChromaticEffectTweening) return;
            StartCoroutine(ChromaticEffectCoroutine(intensity, duration));
        }

        private IEnumerator ChromaticEffectCoroutine(float intensity, float duration)
        {
            float currentTime = 0f;
            float defaultValue = _chromaticEffect.intensity.value;
            while (currentTime < duration)
            {
                currentTime += Time.deltaTime;
                float ratio = currentTime / duration;
                SetChromatic(Mathf.Lerp(defaultValue, intensity, ratio));
                yield return null;
            }
            SetChromatic(intensity);
            _isChromaticEffectTweening = false;
        }
    }
}