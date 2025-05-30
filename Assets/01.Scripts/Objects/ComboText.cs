using DG.Tweening;
using ObjectPooling;
using TMPro;
using UnityEngine;

namespace ObjectManage
{

    public class ComboText : MonoBehaviour, IPoolable
    {
        [field: SerializeField] public PoolingType type { get; set; }
        [SerializeField] Vector3 _offset;

        public GameObject ObjectPrefab => gameObject;
        [SerializeField] private TextMeshPro _comboDisplayText;
        private readonly string _comboTextFormat = "<size=5><color=#A045D4>COMBO </color></size>";
        [SerializeField] private float _lifeTime = 0.4f;


        public void SetComboText(Vector3 position, int combo)
        {
            _comboDisplayText.alpha = 1f;
            transform.position = position + _offset;;
            _comboDisplayText.text = $"{_comboTextFormat}{combo}";
            _comboDisplayText.DOFade(0f, _lifeTime).OnComplete(() => PoolManager.Instance.Push(this));
        }

        public void ResetItem()
        {

        }
    }

}