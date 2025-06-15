using DG.Tweening;
using UIManage;
using UnityEngine;
namespace PongGameSystem
{

    public class GameViewPanel : UIPanel
    {
        public override void SetCanvasActive(bool value)
        {
            if (value)
                gameObject.SetActive(true);
            _canvasGroup.DOFade(value ? 1f : 0f, _activeDuration).SetUpdate(_useUnscaledTime).OnComplete(() => _isActive = value).OnComplete(() => gameObject.SetActive(value));
            //SetInteractable(value);
        }
    }
}