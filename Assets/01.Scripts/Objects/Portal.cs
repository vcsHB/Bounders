using UnityEngine;
using UnityEngine.Events;

namespace ObjectManage
{
    public class Portal : MonoBehaviour
    {
        [SerializeField] private Portal _linkedPortal;
        [SerializeField] private float _exitOffset = 1.0f;
        [SerializeField] private float _cooldownDuration = 0.5f;

        private float _lastUsedTime = -Mathf.Infinity;
        private bool _isOnCooldown = false;

        public UnityEvent OnCooldownStart;
        public UnityEvent OnCooldownEnd;
        private readonly int _waveLevelHash = Shader.PropertyToID("_WaveLevel");
        private MeshRenderer _meshRenderer;

        private void Awake()
        {
            _meshRenderer = GetComponent<MeshRenderer>();
        }


        private void Update()
        {
            if (_isOnCooldown && Time.time - _lastUsedTime >= _cooldownDuration)
            {
                _isOnCooldown = false;
                OnCooldownEnd?.Invoke();
                SetPortalReady(true);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_linkedPortal == null || _isOnCooldown) return;

            Rigidbody rigid = other.GetComponent<Rigidbody>();
            if (rigid == null) return;

            _linkedPortal.ReceiveTeleport(rigid, rigid.transform, rigid.velocity);

            StartCooldown();
        }

        public void ReceiveTeleport(Rigidbody rigid, Transform teleportObject, Vector3 incomingVelocity)
        {
            Vector3 exitDirection = transform.forward;
            Vector3 exitPosition = transform.position + exitDirection * _exitOffset;

            teleportObject.position = exitPosition;
            rigid.velocity = incomingVelocity;

            StartCooldown();
        }

        private void StartCooldown()
        {
            _lastUsedTime = Time.time;
            _isOnCooldown = true;
            OnCooldownStart?.Invoke();
            SetPortalReady(false);
        }
        public void SetPortalReady(bool value)
        {
            _meshRenderer.material.SetFloat(_waveLevelHash, value ? 1f : 0f);

        }
    }
}
