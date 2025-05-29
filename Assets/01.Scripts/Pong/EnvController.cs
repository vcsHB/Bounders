using Unity.MLAgents;
using UnityEngine;

namespace PongGameSystem
{

    public class EnvController : MonoBehaviour
    {
        [Header("Game Setting")]
        [SerializeField] private float _leftEndPoint;
        [SerializeField] private float _rightEndPoint;
        [Header("Agent Settings")]
        public GameObject Player1;
        public GameObject Player2;
        [SerializeField] private Ball _ball;
        private float stillTime = 0f;
        private const float stillThreshold = 1.5f; // 정지로 간주할 시간
        private const float minSpeed = 0.2f;

        [Header("Start Position Settings")]
        public Vector3 player1StartPos;
        public Vector3 player2StartPos;
        public Vector3 ballStartPos;

        void Update()
        {
            // 득점 처리 - x 좌표 기준
            if (_ball.transform.localPosition.x > _leftEndPoint)
            {
                Player2Scored(); // 왼쪽 끝 → Player2 점수
            }
            else if (_ball.transform.localPosition.x < _rightEndPoint)
            {
                Player1Scored(); // 오른쪽 끝 → Player1 점수
            }

            // 공이 정지하면 일정 시간 후 리셋
            CheckBallStillness();
        }


        void CheckBallStillness()
        {
            if (_ball.Velocity.magnitude < minSpeed)
            {
                stillTime += Time.deltaTime;
                if (stillTime > stillThreshold)
                {
                    ResetScene();
                    stillTime = 0f;
                }
            }
            else
            {
                stillTime = 0f;
            }
        }

        public void ResetScene()
        {
            // 위치 초기화
            Player1.transform.localPosition = player1StartPos;
            Player2.transform.localPosition = player2StartPos;
            _ball.transform.localPosition = ballStartPos;

            // 속도 초기화
            _ball.StopImmediately();
            _ball.SetRandomVelocity();

            // 에이전트 리셋
            Player1.GetComponent<Agent>().EndEpisode();
            Player2.GetComponent<Agent>().EndEpisode();

            // 리셋용 타이머 초기화
            stillTime = 0f;
        }

        public void Player1Scored()
        {
            ScoreManager.Instance.AddScore(PlayerType.Player1, 1);
            //scoreManager.Player1Scored();
            ResetScene();
        }

        public void Player2Scored()
        {
            ScoreManager.Instance.AddScore(PlayerType.Player2, 1);
            //scoreManager.Player2Scored();
            ResetScene();
        }
#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawLine(new Vector3(_leftEndPoint, 0f, 5f), new Vector3(_leftEndPoint, 0f, -5f));
            
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(new Vector3(_rightEndPoint, 0f, 5f), new Vector3(_rightEndPoint, 0f, -5f));
            
        }
#endif

    }

}