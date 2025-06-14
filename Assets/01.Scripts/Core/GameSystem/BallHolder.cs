using UnityEngine;
using UnityEngine.Events;
namespace PongGameSystem
{

    public class BallHolder : MonoBehaviour
    {
        public UnityEvent OnBallHoldEvent;
        public UnityEvent OnBallShootEvent;
        [SerializeField] private Ball _ball;
        [SerializeField] private Vector3 _holdOffset;
        private bool _isHolding;
        [SerializeField] private float _grabDuration = 3f;
        private float _currentTime;


        public void SetBallOwner()
        {
            _isHolding = true;

            _currentTime = 0f;
            OnBallHoldEvent?.Invoke();
        }

        private void FixedUpdate()
        {
            if (_isHolding)
            {
                _currentTime += Time.fixedDeltaTime;
                _ball.SetPosition(transform.position + _holdOffset);

                if (_currentTime > _grabDuration)
                {
                    ShootBlashBall();
                }
            }
        }

        private void ShootBlashBall()
        {
            Vector3 holderPosition = transform.position;
            Vector3 direction = (holderPosition + _holdOffset) - holderPosition;
            _isHolding = false;
            _ball.ShootBlast(direction);
            OnBallShootEvent?.Invoke();
        }


#if UNITY_EDITOR

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + _holdOffset);
        }

#endif



    }
}