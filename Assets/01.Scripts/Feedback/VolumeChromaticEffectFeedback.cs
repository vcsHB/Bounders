using System.Collections;
using Core;
using Core.VolumeControlSystem;
using UnityEngine;
namespace FeedbackSystem
{

    public class VolumeChromaticEffectFeedback : Feedback
    {
        [SerializeField] private float _level = 0.1f;
        [SerializeField] private float duration = 0.1f;
        private ChromaticAberrationController _controller;
        private void Start()
        {
            _controller = VolumeManager.Instance.GetCompo<ChromaticAberrationController>();
        }

        public override void CreateFeedback()
        {
            _controller.SetChromatic(_level);
            _controller.SetChromatic(0, duration);
        }

        public override void FinishFeedback()
        {
            _controller.HandleDisableChromatic();
        }
    }
}