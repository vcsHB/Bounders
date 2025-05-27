using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using UnityEngine;

public class EnvController : MonoBehaviour
{
    [Header("Agent Settings")]
    public GameObject Player1;
    public GameObject Player2;

    [Header("Ball Settings")]
    public GameObject Ball;
    public Rigidbody RbBall;
    public float min_ball_speed = 1.0f;
    public float max_ball_speed = 3.0f;
    private float stillTime = 0f;
    private const float stillThreshold = 1.5f; // 정지로 간주할 시간
    private const float minSpeed = 0.2f;

    [Header("Start Position Settings")]
    public Vector3 player1StartPos;
    public Vector3 player2StartPos;
    public Vector3 ballStartPos;

    // [Header("Score UI")]
    // public SceneManager scoreManager;

    void Update()
    {
        // 득점 처리 - x 좌표 기준
        if (Ball.transform.localPosition.x < -12f)
        {
            Player2Scored(); // 왼쪽 끝 → Player2 점수
        }
        else if (Ball.transform.localPosition.x > 12f)
        {
            Player1Scored(); // 오른쪽 끝 → Player1 점수
        }

        // 공이 정지하면 일정 시간 후 리셋
        CheckBallStillness();
    }

    void FixedUpdate()
    {
        // 최소 속도 유지
        if (RbBall.velocity.magnitude < 0.1f)
        {
            ForceResetBall(); // 공이 멈췄을 경우 방향 재지정
        }
        else if (Mathf.Abs(RbBall.velocity.x) < min_ball_speed * 0.5f)
        {
            float dirX = RbBall.velocity.x >= 0 ? 1 : -1;
            Vector3 newVel = new Vector3(dirX * min_ball_speed, 0, RbBall.velocity.z);
            RbBall.velocity = newVel;
        }

        // 벽에 평행하게 계속 튕기는 현상 방지
        if (Mathf.Abs(RbBall.velocity.x) < 0.1f)
        {
            float randX = Random.Range(0, 2) == 0 ? -1 : 1;
            RbBall.velocity = new Vector3(randX * min_ball_speed, 0, RbBall.velocity.z);
        }

        // 코너에서 계속 멈춰 있을 때 강제 리셋
        if (Mathf.Abs(Ball.transform.localPosition.z) > 3.6f && Mathf.Abs(RbBall.velocity.x) < 0.2f)
        {
            ForceResetBall();
        }
    }

    void CheckBallStillness()
    {
        if (RbBall.velocity.magnitude < minSpeed)
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
        Ball.transform.localPosition = ballStartPos;

        // 속도 초기화
        RbBall.velocity = Vector3.zero;

        // 방향 랜덤
        float xSpeed = Random.Range(min_ball_speed, max_ball_speed);
        float zSpeed = Random.Range(min_ball_speed * 0.3f, max_ball_speed * 0.7f);

        float xSign = Random.Range(0, 2) == 0 ? -1 : 1;
        float zSign = Random.Range(0, 2) == 0 ? -1 : 1;

        RbBall.velocity = new Vector3(xSign * xSpeed, 0f, zSign * zSpeed);

        // 에이전트 리셋
        Player1.GetComponent<Agent>().EndEpisode();
        Player2.GetComponent<Agent>().EndEpisode();

        // 리셋용 타이머 초기화
        stillTime = 0f;
    }

    public void Player1Scored()
    {
        //scoreManager.Player1Scored();
        ResetScene();
    }

    public void Player2Scored()
    {
        //scoreManager.Player2Scored();
        ResetScene();
    }

    private void ForceResetBall()
    {
        // 멈춘 공에 강제로 방향 적용
        float randX = Random.Range(0, 2) == 0 ? -1f : 1f;
        float randZ = Random.Range(-1f, 1f);
        Vector3 newDir = new Vector3(randX, 0, randZ).normalized;
        RbBall.velocity = newDir * min_ball_speed;
    }
}
