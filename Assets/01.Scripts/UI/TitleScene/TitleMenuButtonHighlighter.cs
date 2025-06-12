using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
namespace UIManage.TitleScene
{

    public class TitleMenuButtonHighlighter : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public UnityEvent OnHoverEnterEvent;
        public UnityEvent OnHoverExitEvent;
        [SerializeField] private Image _gradientPanel;
        [SerializeField] private Image _selectionPanel;
        [SerializeField] private TextMeshProUGUI _optionShadowText;
        [SerializeField] private float _tweenDuration;


        public void OnPointerEnter(PointerEventData eventData)
        {
            _gradientPanel.DOFade(1f, _tweenDuration);
            _optionShadowText.DOFade(1f, _tweenDuration);
            _selectionPanel.transform.DOScale(1f, _tweenDuration);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _gradientPanel.DOFade(0f, _tweenDuration);
            _optionShadowText.DOFade(0f, _tweenDuration);
            _selectionPanel.transform.DOScale(0f, _tweenDuration);
        }
    }
}