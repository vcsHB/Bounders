using System;
using UnityEngine;
using UnityEngine.Events;
namespace Players
{

    public class Player : MonoBehaviour
    {
        public UnityEvent OnPlayerDieEvent;
        public Health HealthCompo { get; private set; }

        protected virtual void Awake()
        {
            HealthCompo = GetComponent<Health>();
            HealthCompo.OnDieUnityEvent.AddListener(HandlePlayerDie);
        }

        protected virtual void HandlePlayerDie()
        {
            OnPlayerDieEvent?.Invoke();
        }
    }
}