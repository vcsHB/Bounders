using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ObjectManage
{

    public class CrownObject : MonoBehaviour
    {
        [SerializeField] private float _rotationSpeed;
        [SerializeField] private bool _isRotationMode;

        private MeshRenderer _crownRenderer;
        [SerializeField] private Material[] _materials;
        private float _currentRotation;

        private void Awake()
        {
            _crownRenderer = GetComponent<MeshRenderer>();
        }
        public void StartRotation(int materialIndex)
        {
            _crownRenderer.material = _materials[materialIndex];
            _currentRotation = 0f;
        }

        private void Update()
        {
            if (_isRotationMode)
            {
                Rotate();
            }
        }

        private void Rotate()
        {
            _currentRotation += _rotationSpeed;
            transform.localRotation = Quaternion.Euler(0f, _currentRotation, 0f);
        }

    }

}