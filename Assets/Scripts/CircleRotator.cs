using UnityEngine;

public class CircleRotator : MonoBehaviour
{
    public Collider2D rotationHandle;  // Assign this variable in the inspector. 在检查器中分配这个变量
    private bool isDragging;
    private float angleOffset;  // The offset for the rotation. 旋转偏移量

    void Update()
    {
        // Handle touch input for mobile devices. 处理移动设备的触摸输入
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            touchPosition.z = 0;  // Ensure the z-position is zero. 确保z位置为零

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    if (rotationHandle.OverlapPoint(touchPosition))
                    {
                        StartRotation(touchPosition);
                    }
                    break;
                case TouchPhase.Moved:
                    if (isDragging)
                    {
                        ContinueRotation(touchPosition);
                    }
                    break;
                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                    EndRotation();
                    break;
            }
        }
    }

    private void StartRotation(Vector3 position)
    {
        // Calculate the offset at the start of the rotation. 计算起始旋转时的偏移量
        Vector3 dir = position - rotationHandle.transform.position;
        angleOffset = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - rotationHandle.transform.eulerAngles.z;
        isDragging = true;
    }

    private void ContinueRotation(Vector3 position)
    {
        if (!isDragging) return;

        // Calculate the current angle and apply it to the rotation object. 计算当前旋转角度，并应用到旋转对象
        Vector3 dir = position - rotationHandle.transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - angleOffset;
        rotationHandle.transform.eulerAngles = new Vector3(0, 0, angle);
    }

    private void EndRotation()
    {
        isDragging = false;
    }
}
