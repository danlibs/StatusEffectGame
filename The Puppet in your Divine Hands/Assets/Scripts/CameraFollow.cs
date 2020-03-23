using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform Target;
    public Vector3 Offset;
    public float SmoothSpeed;

    private void FixedUpdate()
    {
        Vector3 desiredPosition = Target.transform.position + Offset;
        Vector3 smoothPosition = Vector3.Lerp(this.transform.position, desiredPosition, SmoothSpeed * Time.deltaTime);
        this.transform.position = smoothPosition;
    }
}
