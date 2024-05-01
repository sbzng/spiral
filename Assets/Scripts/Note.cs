using UnityEngine;
using System.Collections;

public class Note : MonoBehaviour
{
    public Transform targetTransform;  // The target position the note should move towards. 笔记应该移动向的目标位置。
    public float speed = 10.0f;  // Movement speed of the note. 笔记的移动速度。
    private float perfectRange = 0.1f;  // Range for a 'perfect' hit. 完美击中的范围。
    private float greatRange = 0.2f;  // Range for a 'great' hit. 很好的击中范围。
    private float arrivalTime;  // Time when the note should arrive at the target. 笔记应到达目标的时间。
    private bool isArrived = false;  // Flag to check if the note has arrived at the target. 检查笔记是否已到达目标的标志。
    private NoteSpawner spawner;  // Reference to the NoteSpawner. 对NoteSpawner的引用。

    void Start()
    {
        // Calculate the time it will take for the note to reach the target. 计算笔记到达目标所需的时间。
        arrivalTime = Time.time + Vector3.Distance(transform.position, targetTransform.position) / speed;
    }

    public void Initialize(Transform target, NoteSpawner spawner)
    {
        targetTransform = target;
        this.spawner = spawner;
    }

    void Update()
    {
        // Move the note towards the target if it hasn't arrived yet. 如果笔记还没有到达，将其移向目标。
        if (targetTransform != null && !isArrived)
        {
            MoveTowardsTarget();
        }
    }

    private void MoveTowardsTarget()
    {
        // Calculate the local position of the target and move the note towards it. 计算目标的局部位置并将笔记移向它。
        Vector3 localTargetPosition = transform.parent.InverseTransformPoint(targetTransform.position);
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, localTargetPosition, speed * Time.deltaTime);

        // Check if the note has reached the target and start listening for clicks. 检查笔记是否已达到目标并开始监听点击。
        if (Vector3.Distance(transform.localPosition, localTargetPosition) < 0.1f)
        {
            isArrived = true;
            StartCoroutine(ListenForClicks());
        }
    }

    private IEnumerator ListenForClicks()
    {
        // Set the time window for registering clicks and wait for a click or timeout. 设置注册点击的时间窗口，并等待点击或超时。
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
            EvaluateClick(float.MaxValue);  // Miss due to timeout. 由于超时而错过。
        }
    }

    private void EvaluateClick(float timeDifference)
    {
        // Determine the result of the click based on the timing difference. 根据时间差确定点击结果。
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

        spawner.RegisterHit(result); // Record the result of the click. 记录点击结果。
        UIManager.Instance.ShowFeedback(result, targetTransform.position); // Show feedback on UI. 在UI上显示反馈。
        UIManager.Instance.PlaySound(); // Play a sound effect. 播放声效。
        Destroy(gameObject); // Destroy the note object. 销毁笔记对象。
    }
}
