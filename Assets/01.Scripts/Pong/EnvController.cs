using System;
using Unity.MLAgents;
using UnityEngine;

namespace PongGameSystem
{

    public class EnvController : MonoBehaviour
    {
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
            ResetGame();
        }



        public void ResetGame()
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
        }


#if UNITY_EDITOR
        private void OnDrawGizmos()
        {


        }
#endif

    }

}