using System.Collections;
using Core.GameSystem;
using ObjectManage;
using UnityEngine;
using UnityEngine.Events;

namespace PongGameSystem
{

    public class EnvController : MonoBehaviour
    {
        [Header("Events Settings")]
        public UnityEvent OnBallCastedEvent;
        [Header("Agent Settings")]
        public GameObject Player1;
        public GameObject Player2;
        [SerializeField] private Ball _ball;

        [Header("Start Position Settings")]
        public Vector3 player1StartPos;
        public Vector3 player2StartPos;
        public Vector3 ballStartPos;

        private void Awake()
        {
            _ball.OnPlayerCastedEvent += HandlePlayerCasted;
        }

        private void HandlePlayerCasted()
        {
            OnBallCastedEvent?.Invoke();
            VFXPlayer vfx = PoolManager.Instance.Pop(ObjectPooling.PoolingType.BallDestroyVFX) as VFXPlayer;
            vfx.transform.position = _ball.transform.position;
            vfx.Play();
            _ball.transform.position = new Vector3(100f, 0f, 0f); // Out
            GameManager.Instance.GameStart(ResetGame);
        }


        public void ResetGame()
        {
            // 위치 초기화
            // Player1.transform.localPosition = player1StartPos;
            // Player2.transform.localPosition = player2StartPos;
            _ball.SetPosition(ballStartPos);
            // 속도 초기화
            _ball.StopImmediately();
            _ball.SetRandomVelocity();

            // 에이전트 리셋
            // Player1.GetComponent<Agent>().EndEpisode();
            // Player2.GetComponent<Agent>().EndEpisode();

            // 리셋용 타이머 초기화
        }

        



    }

}