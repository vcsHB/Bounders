using System.Collections;
using UnityEngine;

namespace ObjectManage
{

    public class WallObject : MonoBehaviour
    {
        private MeshRenderer _meshRenderer;
        [SerializeField] private float _blinkDuration;
        private readonly int _waveLevelHash = Shader.PropertyToID("_WaveLevel");
        private readonly int _waveHitPointHash = Shader.PropertyToID("_HitPosition");
        private Coroutine _currentWaveBlinkCoroutine;
        private void Awake()
        {
            _meshRenderer = GetComponent<MeshRenderer>();
        }

        public void StartHitBlink(Vector3 hitPosition)
        {
            _meshRenderer.material.SetVector(_waveHitPointHash, hitPosition);
            if (_currentWaveBlinkCoroutine != null)
                StopCoroutine(_currentWaveBlinkCoroutine);
            _currentWaveBlinkCoroutine = StartCoroutine(WaveEffectRoutine());
        }

        private IEnumerator WaveEffectRoutine()
        {
            float currentTime = 0f;
            while (currentTime < _blinkDuration)
            {
                currentTime += Time.deltaTime;
                float ratio = currentTime / _blinkDuration;
                SetWaveLevel(Mathf.Sin(ratio * Mathf.PI));
                yield return null;
            }
            SetWaveLevel(0f);
            _currentWaveBlinkCoroutine = null;
        }

        public void SetWaveLevel(float value)
        {
            _meshRenderer.material.SetFloat(_waveLevelHash, value);

        }
    }

}