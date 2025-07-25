using System.Collections;
using Cinemachine;
using UnityEngine;

namespace CameraControllers
{

    public class CameraShakeController : MonoBehaviour, ICameraControlable
    {
        private CinemachineVirtualCamera _virtualCamera;
        private CinemachineBasicMultiChannelPerlin _shaker;
        private bool _isShaking;


        public void Initialize(CinemachineVirtualCamera camera)
        {
            _virtualCamera = camera;
            _shaker = _virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        }
        public void HandleSmallShake()
        {
            Shake(10f, 0.07f);
        }

        public void HandleMiddleShake()
        {
            Shake(20f, 0.07f);
        }

        public void Shake(float power)
        {
            Shake(power, 0.1f);
        }

        public void Shake(float power, float duration)
        {
            if (_isShaking) return;
            _isShaking = true;
            StartCoroutine(ShakeCoroutine(power, duration));
        }

        private IEnumerator ShakeCoroutine(float power, float duration)
        {
            SetShake(power);
            yield return new WaitForSeconds(duration);
            SetShake(0);
            _isShaking = false;
        }

        public void StopShake()
        {
            _isShaking = false;

        }

        private void SetShake(float power)
        {
            _shaker.m_AmplitudeGain = power;
            _shaker.m_FrequencyGain = power;
        }

    }

}