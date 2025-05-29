using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace UIManage.GameScene
{

    public class BattlePanel : MonoBehaviour, IWindowPanel
    {
        [SerializeField] private BattleProfilePanel _profilePanel;
        [SerializeField] private float _enabledSize;
        [SerializeField] private Image _backgroundPanel;
        [SerializeField] private float _backgroundTweenDuration = 0.2f;
        [SerializeField] private Image _backgroundCoverPanel1;
        [SerializeField] private float _backgroundCover1TweenDuration = 0.2f;
        [SerializeField] private Image _backgroundCoverPanel2;
        [SerializeField] private float _backgroundCover2TweenDuration = 0.2f;
        [SerializeField] private float _tweenInterval = 0.08f;
        private CanvasGroup _canvasGroup;


        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        [ContextMenu("DebugClose")]
        public void Close()
        {
            _profilePanel.Close();

            _backgroundPanel
                .DOFillAmount(0f, _backgroundTweenDuration)
                .SetDelay(0f);

            _backgroundCoverPanel1
                .DOFillAmount(0f, _backgroundCover1TweenDuration)
                .SetDelay(_tweenInterval);

            _backgroundCoverPanel2
                .DOFillAmount(0f, _backgroundCover2TweenDuration)
                .SetDelay(_tweenInterval * 2)
                .OnComplete(() =>
                {
                    _canvasGroup.alpha = 0f;
                });
        }

        [ContextMenu("DebugOpen")]
        public void Open()
        {
            _canvasGroup.alpha = 1f;

            _backgroundPanel
                .DOFillAmount(1f, _backgroundTweenDuration)
                .SetDelay(0f);

            _backgroundCoverPanel1
                .DOFillAmount(1f, _backgroundCover1TweenDuration)
                .SetDelay(_tweenInterval);

            _backgroundCoverPanel2
                .DOFillAmount(1f, _backgroundCover2TweenDuration)
                .SetDelay(_tweenInterval * 2)
                .OnComplete(() =>
                {
                    _profilePanel.Open();
                });
        }



    }

}