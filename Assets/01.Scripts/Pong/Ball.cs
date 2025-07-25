using System;
using ObjectManage;
using UnityEngine;
using UnityEngine.Events;

using Random = UnityEngine.Random;

namespace PongGameSystem
{

    public class Ball : MonoBehaviour
    {
        public UnityEvent OnBlastModeEnableEvent;
        public UnityEvent OnBlastModeDisableEvent;
        public Action OnPlayerCastedEvent;
        public UnityEvent OnWallCollisionEvent;
        [Header("Ball Settings")]
        public float min_ball_speed = 1.0f;
        public float max_ball_speed = 3.0f;

        private Rigidbody _rigidCompo;
        public Vector2 Velocity => _rigidCompo.velocity;
        private BallComboCounter _comboCounter;
        private BallDamageCaster _caster;
        public bool isBallEnable;
        private bool _isBlastBall;

        private void Awake()
        {
            _comboCounter = GetComponent<BallComboCounter>();
            _caster = GetComponentInChildren<BallDamageCaster>();
            _rigidCompo = GetComponent<Rigidbody>();
        }

        public void StopImmediately()
        {
            _rigidCompo.velocity = Vector2.zero;

        }

        private void FixedUpdate()
        {
            if (!isBallEnable) return;


            if (_caster.CastDamage())
            {
                OnPlayerCastedEvent?.Invoke();
                isBallEnable = false;
            }
            // 최소 속도 유지
            if (_rigidCompo.velocity.magnitude < 0.1f)
            {
                ForceResetBall(); // 공이 멈췄을 경우 방향 재지정
            }
            else if (Mathf.Abs(_rigidCompo.velocity.x) < min_ball_speed * 0.5f)
            {
                float dirX = _rigidCompo.velocity.x >= 0 ? 1 : -1;
                Vector3 newVel = new Vector3(dirX * min_ball_speed, 0, _rigidCompo.velocity.z);
                _rigidCompo.velocity = newVel;
            }

            // 벽에 평행하게 계속 튕기는 현상 방지
            if (!_isBlastBall && Mathf.Abs(_rigidCompo.velocity.z) < 0.3f)
            {
                float randZ = Random.Range(0, 2) == 0 ? -1 : 1;
                _rigidCompo.velocity = new Vector3(_rigidCompo.velocity.x, 0, randZ * min_ball_speed);
            }

            // 코너에서 계속 멈춰 있을 때 강제 리셋
            if (Mathf.Abs(transform.localPosition.z) > 3.6f && Mathf.Abs(_rigidCompo.velocity.x) < 0.2f)
            {
                ForceResetBall();
            }

        }


        public void SetRandomVelocity()
        {
            float xSpeed = Random.Range(min_ball_speed, max_ball_speed);
            float zSpeed = Random.Range(min_ball_speed * 0.3f, max_ball_speed * 0.7f);

            float xSign = Random.Range(0, 2) == 0 ? -1 : 1;
            float zSign = Random.Range(0, 2) == 0 ? -1 : 1;
            _comboCounter.ResetCombo();
            _rigidCompo.velocity = new Vector3(xSign * xSpeed, 0f, zSign * zSpeed);
        }

        private void ForceResetBall()
        {
            // 멈춘 공에 강제로 방향 적용
            float randX = Random.Range(0, 2) == 0 ? -1f : 1f;
            float randZ = Random.Range(-1f, 1f);
            Vector3 newDir = new Vector3(randX, 0, randZ).normalized;
            _rigidCompo.velocity = newDir * min_ball_speed;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (_isBlastBall)
            {
                _isBlastBall = false;
                OnBlastModeDisableEvent?.Invoke();
            }
            if (collision.transform.CompareTag("Wall"))
            {
                OnWallCollisionEvent?.Invoke();
                if (collision.transform.TryGetComponent(out WallObject wall))
                {
                    wall.StartHitBlink(collision.GetContact(0).point);
                    _comboCounter.AddCombo();
                }
            }
            else if (collision.transform.CompareTag("PlayerPanel"))
            {
                //_comboCounter.ResetCombo();

            }
        }

        public void ShootBlast(Vector2 direction)
        {
            direction.Normalize();
            _isBlastBall = true;
            isBallEnable = true;
            _rigidCompo.velocity = direction * max_ball_speed;
            OnBlastModeEnableEvent?.Invoke();
        }
        public void SetPosition(Vector3 newPosition)
        {
            transform.position = newPosition;
        }
    }

}