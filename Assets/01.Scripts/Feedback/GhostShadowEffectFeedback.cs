using System.Collections;
using Agents;
using ObjectManage;
using ObjectPooling;
using UnityEngine;

namespace FeedbackSystem
{
    public class GhostShadowEffectFeedback : Feedback
    {
        [SerializeField] private SpriteRenderer _ownerRenderer;
        [SerializeField] private Gradient _colorGradient;
        [SerializeField] private float _duration = 1f;
        [SerializeField] private float _generateTerm = 0.4f;
        [SerializeField] private float _shadowLifeTime = 0.1f;
        private Coroutine _effectCoroutine;
        private bool _isFrameGenerate;

        public override void CreateFeedback()
        {
            _isFrameGenerate = Mathf.Approximately(_generateTerm, 0f);
            _effectCoroutine = StartCoroutine(PlayEffectCoroutine());

        }

        private IEnumerator PlayEffectCoroutine()
        {
            float currentTime = 0f;
            float currentGenerateTime = 0f;
            while (currentTime < _duration)
            {
                currentTime += Time.deltaTime;
                currentGenerateTime += Time.deltaTime;
                //float ratio = currentTime / _duration;
                if (_isFrameGenerate)
                {
                    GenerateGhostEffect();
                }
                else if (currentGenerateTime > _generateTerm)
                {
                    currentGenerateTime = 0f;
                    GenerateGhostEffect();

                }
                yield return null;
            }
            _effectCoroutine = null;

        }

        public override void FinishFeedback()
        {
            if (_effectCoroutine == null) return;
            StopCoroutine(_effectCoroutine);
            _effectCoroutine = null;
        }

        private void GenerateGhostEffect()
        {
            GhostShadowVFXPlayer effect = PoolManager.Instance.Pop(PoolingType.GhostShadowVFX, transform.position, Quaternion.identity) as GhostShadowVFXPlayer;
            effect.Initialize(
                transform.position,
                _shadowLifeTime,
                _colorGradient,
                _ownerRenderer.sprite
            );
            effect.Play();
        }
    }
}