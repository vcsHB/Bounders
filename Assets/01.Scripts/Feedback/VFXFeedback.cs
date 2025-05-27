using UnityEngine;

namespace FeedbackSystem
{

    public class VFXFeedback : Feedback
    {
        [SerializeField] private ParticleSystem _vfx;

        public override void CreateFeedback()
        {
            _vfx.Play();
        }

        public override void FinishFeedback()
        {
            _vfx.Stop();
        }
    }

}