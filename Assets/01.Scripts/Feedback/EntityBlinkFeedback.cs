using System.Collections;
using UnityEngine;
namespace FeedbackSystem
{

    public class EntityBlinkFeedback : Feedback
    {
        [SerializeField] private SpriteRenderer _ownerRenderer;
        [SerializeField] private float _blinkDuration;
        [SerializeField, Range(0f, 1f)] private float _blinkLevel = 1f;
        private Material _material;
        private bool _isBlinking;
        private readonly int _blinkHash = Shader.PropertyToID("_BlinkLevel");
        private float _defualtBlinkValue;
        private void Awake()
        {
            _material = _ownerRenderer.material;
            _defualtBlinkValue = _material.GetFloat(_blinkHash);
        }
        public override void CreateFeedback()
        {
            if (_isBlinking || !gameObject.activeInHierarchy) return;
            _isBlinking = true;
            StartCoroutine(BlinkCoroutine());
        }

        private IEnumerator BlinkCoroutine()
        {
            _material.SetFloat(_blinkHash, _blinkLevel);
            yield return new WaitForSeconds(_blinkDuration);
            _material.SetFloat(_blinkHash, _defualtBlinkValue);
            _isBlinking = false;
        }

        public override void FinishFeedback()
        {
            StopAllCoroutines();
            _material.SetFloat(_blinkHash, _defualtBlinkValue);
            _isBlinking = false;
        }
    }
}