using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class MummyGoAgent : Agent
{
    public Material goodMaterial;
    public Material badMaterial;
    

    private Material originMaterial;
    private Renderer floorRenderer;

    public Transform targetTransform;
    private new Rigidbody rigidbody;

   
    public override void Initialize()
    {
        rigidbody = GetComponent<Rigidbody>();

        floorRenderer = transform.parent.Find("Floor").GetComponent<Renderer>();
       
        originMaterial = floorRenderer.material;
        
    }

    public override void OnEpisodeBegin()
    {
        rigidbody.velocity = Vector3.zero;
        rigidbody.angularVelocity = Vector3.zero;
       

        transform.localPosition = new Vector3(Random.Range(-4f, 4f), 0.05f, Random.Range(-4f, 4f));
        targetTransform.localPosition = new Vector3(Random.Range(-4f, 4f), 0.55f, Random.Range(-4f, 4f));
    

        StartCoroutine(RecoverFloor());
        //RecoverFloor() �ڷ�ƾ�� ȣ���Ͽ� �ٴ� ������ ������� ����.
    }

    private IEnumerator RecoverFloor()
    {
        yield return new WaitForSeconds(0.2f);
        //0.2�� �Ŀ� �ٴ� ������ ���� ������ ����.
        floorRenderer.material = originMaterial;
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        // ���� ���� 8�� 
        sensor.AddObservation(targetTransform.localPosition);
        sensor.AddObservation(transform.localPosition);
        sensor.AddObservation(rigidbody.velocity.x);
        sensor.AddObservation(rigidbody.velocity.z);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {

        var ContinuousActions = actions.ContinuousActions;
        //������ �ൿ(Continuous Actions)** �̹ǷΡ�

        // �̵� ���� ���� ����
        Vector3 moveDir = new Vector3(ContinuousActions[1], 0, ContinuousActions[0]).normalized;

        // Rigidbody�� �ӵ��� ���� ���� 
        rigidbody.velocity = moveDir * 3f;

        // �Ÿ� ��� ���� �߰�
        float distance = Vector3.Distance(transform.localPosition, targetTransform.localPosition); SetReward(-distance * 0.001f); // ��ǥ�� ����������� ���Ƽ �����Ͽ� ��ǥ�� ������ ����
    }

  
    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var ContinuousActionsOut = actionsOut.ContinuousActions;
        ContinuousActionsOut[0] = Input.GetAxis("Vertical");
        ContinuousActionsOut[1] = Input.GetAxis("Horizontal");
    }

   
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("WALL"))
        {
            floorRenderer.material = badMaterial;
            SetReward(-0.3f);
            EndEpisode();
        }

       


        if (collision.collider.CompareTag("TARGET"))
        {
            floorRenderer.material = goodMaterial;
            SetReward(2f);
            EndEpisode();
        }
      

    }
}