using UnityEngine;

public class CircleRotator : MonoBehaviour
{
    private bool isDragging;
    private Vector3 startMousePos;
    private Vector3 currentMousePos;
    private float angleOffset;

    void Update()
    {
        // For testing in the Unity editor
        if (Input.GetMouseButtonDown(0))
        {
            StartRotation(Input.mousePosition);
        }
        if (Input.GetMouseButton(0) && isDragging)
        {
            ContinueRotation(Input.mousePosition);
        }
        if (Input.GetMouseButtonUp(0))
        {
            EndRotation();
        }


    }

    private void StartRotation(Vector3 position)
    {
        startMousePos = Camera.main.ScreenToWorldPoint(position);
        startMousePos.z = 0;
        Vector3 dir = startMousePos - transform.position;
        angleOffset = (Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg) - transform.eulerAngles.z;
        isDragging = true;
    }

    private void ContinueRotation(Vector3 position)
    {
        currentMousePos = Camera.main.ScreenToWorldPoint(position);
        currentMousePos.z = 0;
        Vector3 dir = currentMousePos - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - angleOffset;
        transform.eulerAngles = new Vector3(0, 0, angle);
    }

    private void EndRotation()
    {
        isDragging = false;
    }
}
        // For touch input (uncomment for building to mobile)
        /*
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                StartRotation(touch.position);
            }
            else if (touch.phase == TouchPhase.Moved && isDragging)
            {
                ContinueRotation(touch.position);
            }
            else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                EndRotation();
            }
        }
        */