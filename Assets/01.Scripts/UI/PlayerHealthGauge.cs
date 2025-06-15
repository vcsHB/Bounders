using Players;
using UnityEngine;
using UnityEngine.UI;

namespace UIManage.GameScene
{
    public class PlayerHealthGauge : MonoBehaviour
    {
        [SerializeField] private Health _owner;
        [SerializeField] private Slider _slider;

        private void Awake()
        {
            _owner.OnHealthValueChangeEvent += HandleHealthChange;
            HandleHealthChange(_owner.CurrentHealth, _owner.MaxHealth);
        }

        private void HandleHealthChange(float currentValue, float maxValue)
        {
            float ratio = currentValue / maxValue;
            _slider.value = ratio;
        }
    }


}