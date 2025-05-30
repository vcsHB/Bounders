using System;
using UnityEngine;
using UnityEngine.Events;

namespace Players
{

    public class Health : MonoBehaviour
    {
        public event Action OnDieEvent;
        public event Action<float, float> OnHealthValueChangeEvent;
        public UnityEvent OnDieUnityEvent;

        [SerializeField] private float _currentHealth;
        [SerializeField] private float _maxHealth;

        public float MaxHealth => _maxHealth;
        private bool _isDead;

        public void Initialized()
        {
            _currentHealth = _maxHealth;
            _isDead = false;
        }

        public void ApplyDamage(float damage)
        {
            _currentHealth -= damage;
            OnHealthValueChangeEvent?.Invoke(_currentHealth, _maxHealth);
            CheckDie();

        }

        public void Restore(float amount)
        {
            _currentHealth += amount;
            OnHealthValueChangeEvent?.Invoke(_currentHealth, _maxHealth);
        }
        private void CheckDie()
        {
            if (_isDead) return;
            if (_currentHealth <= 0f)
            {
                _currentHealth = 0f;
                OnDieEvent?.Invoke();
                OnDieUnityEvent?.Invoke();
                _isDead = true;
            }
        }
    }

}