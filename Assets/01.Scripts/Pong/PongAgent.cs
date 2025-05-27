using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;


public class PongAgent : Agent
{
    public GameObject enemy;
    public GameObject ball;
    private Rigidbody RbAgent;
    private Rigidbody RbBall;

    private const int Stay = 0;
    private const int Up = 1;
    private const int Down = 2;

    private Vector3 ResetPosAgent;
    [SerializeField] private bool _isLearningMode;

    public override void Initialize()
    {
        ResetPosAgent = transform.localPosition;
        RbAgent = GetComponent<Rigidbody>();
        RbBall = ball.GetComponent<Rigidbody>();

        if (_isLearningMode)
            Academy.Instance.AgentPreStep += WaitTimeInference;

    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.localPosition.z);
        sensor.AddObservation(enemy.transform.localPosition.z);
        sensor.AddObservation(ball.transform.localPosition.x);
        sensor.AddObservation(ball.transform.localPosition.z);
        sensor.AddObservation(RbBall.velocity.x);
        sensor.AddObservation(RbBall.velocity.z);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        Vector3 ballPos = ball.transform.localPosition;
        Vector3 ballVel = RbBall.velocity;
        Vector3 agentPos = transform.localPosition;

        // 공이 나에게 오는 방향인지 확인
        bool ballApproaching = (transform.localPosition.x < 0 && ballVel.x < 0) ||
                               (transform.localPosition.x > 0 && ballVel.x > 0);

        float predictedZ = ballPos.z;

        // 공이 일정 속도 이상이고 나에게 오고 있다면 예측
        if (ballApproaching && Mathf.Abs(ballVel.x) > 0.1f)
        {
            // 단순 직선 예측 (벽 반사 없이)
            float timeToReach = Mathf.Abs((agentPos.x - ballPos.x) / ballVel.x);
            predictedZ = ballPos.z + ballVel.z * timeToReach;

            // 벽에 부딪혔을 때 반사 고려 (3.7 높이 기준)
            while (predictedZ > 3.7f || predictedZ < -3.7f)
            {
                if (predictedZ > 3.7f)
                    predictedZ = 3.7f - (predictedZ - 3.7f);
                else if (predictedZ < -3.7f)
                    predictedZ = -3.7f + (-3.7f - predictedZ);
            }
        }

        // 현재 z좌표와 예측 z좌표 비교
        float diff = predictedZ - agentPos.z;

        float moveThreshold = 0.1f;

        if (Mathf.Abs(diff) < moveThreshold)
        {
            // Stay
            transform.localPosition = transform.localPosition;
        }
        else if (diff > 0)
        {
            // Move Up
            transform.Translate(Vector3.forward * Time.fixedDeltaTime * 30f);
        }
        else
        {
            // Move Down
            transform.Translate(Vector3.back * Time.fixedDeltaTime * 30f);
        }

        // Clamp z축 이동
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y,
            Mathf.Clamp(transform.localPosition.z, -3.7f, 3.7f));
    }

    public override void OnEpisodeBegin()
    {
        transform.localPosition = ResetPosAgent;
        RbAgent.velocity = Vector3.zero;
        RbAgent.angularVelocity = Vector3.zero;
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var discreteActionsOut = actionsOut.DiscreteActions;
        if (Input.GetKey(KeyCode.W))
        {
            discreteActionsOut[0] = 1;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            discreteActionsOut[0] = 2;
        }
        else
        {
            discreteActionsOut[0] = 0;
        }
    }

    float DecisionWaitingTime = 0.05f;
    float m_currentTime = 0f;
    // mlagents-learn D:\_UnitySave\GitHub\Bounders\ml-agents-release_20\config\poca\Pong.yaml --run-id=pong_run1 --force
    public void WaitTimeInference(int action)
    {
        if (Academy.Instance.IsCommunicatorOn)
        {
            RequestDecision();
        }
        else
        {
            if (m_currentTime >= DecisionWaitingTime)
            {
                m_currentTime = 0;
                RequestDecision();
            }

            else
            {
                m_currentTime += Time.fixedDeltaTime;
            }
        }

    }





}
