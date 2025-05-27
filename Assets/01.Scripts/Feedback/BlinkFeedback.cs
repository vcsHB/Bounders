using System.Collections;
using UnityEngine;
namespace FeedbackSystem
{

    public class BlinkFeedback : Feedback
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Material _blinkMaterial;
        [SerializeField] private float _blinkTime = 0.1f;
        private bool _isBlinking;
        private Material _defaultMaterial;

        private void Awake()
        {

            _defaultMaterial = _spriteRenderer.material;
        }

        public override void CreateFeedback()
        {
            if(_isBlinking || !gameObject.activeInHierarchy) return;
            _isBlinking = true;
            StartCoroutine(BlinkCoroutine());
        }

        private IEnumerator BlinkCoroutine()
        {
            _spriteRenderer.material = _blinkMaterial;
            yield return new WaitForSeconds(_blinkTime);
            _spriteRenderer.material = _defaultMaterial;
            _isBlinking = false;
        }

        public override void FinishFeedback()
        {
            StopAllCoroutines();
            _spriteRenderer.material = _defaultMaterial;
            _isBlinking = false;
        }
    }
}