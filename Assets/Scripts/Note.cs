using UnityEngine;
using System.Collections;

public class Note : MonoBehaviour
{
    public Transform targetTransform;
    public float speed = 10.0f;
    private float perfectRange = 0.1f;
    private float greatRange = 0.2f;
    private float arrivalTime;
    private bool isArrived = false;
    private NoteSpawner spawner;  // 添加对 NoteSpawner 的引用

    void Start()
    {
        arrivalTime = Time.time + Vector3.Distance(transform.position, targetTransform.position) / speed;
    }

    public void Initialize(Transform target, NoteSpawner spawner)
    {
        targetTransform = target;
        this.spawner = spawner;
    }

    void Update()
    {
        if (targetTransform != null && !isArrived)
        {
            MoveTowardsTarget();
        }
    }

    private void MoveTowardsTarget()
    {
        Vector3 localTargetPosition = transform.parent.InverseTransformPoint(targetTransform.position);
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, localTargetPosition, speed * Time.deltaTime);

        if (Vector3.Distance(transform.localPosition, localTargetPosition) < 0.1f)
        {
            isArrived = true;
            StartCoroutine(ListenForClicks());
        }
    }

    private IEnumerator ListenForClicks()
    {
        float clickTimeWindow = greatRange;
        while (clickTimeWindow > 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                EvaluateClick(Time.time - arrivalTime);
                break;
            }
            clickTimeWindow -= Time.deltaTime;
            yield return null;
        }
        if (clickTimeWindow <= 0)
        {
            EvaluateClick(float.MaxValue);  // Miss due to timeout
        }
    }

    private void EvaluateClick(float timeDifference)
    {
        string result;
        int points = 0;

        if (Mathf.Abs(timeDifference) <= perfectRange)
        {
            result = "Perfect";
            points = 100;
        }
        else if (Mathf.Abs(timeDifference) <= greatRange)
        {
            result = "Great";
            points = 50;
        }
        else
        {
            result = "Miss";
        }

        spawner.RegisterHit(result); // 此方法中应该记录点击结果
        UIManager.Instance.ShowFeedback(result, targetTransform.position);
        UIManager.Instance.PlaySound();
        Destroy(gameObject);
    }
}
