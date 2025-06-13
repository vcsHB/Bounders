using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UIManage
{

    public class SceneExitPanel : MonoBehaviour, IWindowPanel
    {
        public UnityEvent OnOpenEvent;
        public UnityEvent OnCloseEvent;
        [SerializeField] private Image[] _fadePanels;
        [SerializeField] private float _fadeTweenDuration = 0.45f;


        public void Open()
        {
            for (int i = 0; i < _fadePanels.Length; i++)
            {
                _fadePanels[i].DOFillAmount(1f, _fadeTweenDuration);
            }
            OnOpenEvent?.Invoke();

        }

        public void Close()
        {
            for (int i = 0; i < _fadePanels.Length; i++)
            {
                _fadePanels[i].DOFillAmount(0f, _fadeTweenDuration);
            }
            OnCloseEvent?.Invoke();

        }
    }

}