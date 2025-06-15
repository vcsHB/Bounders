using System;
using ObjectManage;
using UnityEngine;
namespace PongGameSystem
{

    public class BallComboCounter : MonoBehaviour
    {
        public event Action<int> OnComboValueChangedEvent;
        [SerializeField] private float _defaultDamage;
        [SerializeField] private AnimationCurve _comboToDamageGraph;
        [SerializeField] private int _currentCombo;

        public float GetComboDamage => _defaultDamage + _comboToDamageGraph.Evaluate(_currentCombo);

        public void AddCombo(int amount = 1)
        {
            _currentCombo += amount;
            ComboText comboText = PoolManager.Instance.Pop(ObjectPooling.PoolingType.ComboText) as ComboText;
            comboText.SetComboText(transform.position, _currentCombo);
            OnComboValueChangedEvent?.Invoke(_currentCombo);

        }

        public void ResetCombo()
        {
            _currentCombo = 0;
            OnComboValueChangedEvent?.Invoke(_currentCombo);
        }



        
    }
}