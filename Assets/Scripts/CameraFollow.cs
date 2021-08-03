using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal sealed class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Transform cameraRotationCenter;
    public float smoothSpeed;

    private void FixedUpdate()
    {
        cameraRotationCenter.position = Vector3.Lerp(cameraRotationCenter.position, target.position, smoothSpeed);
        cameraRotationCenter.rotation = Quaternion.Lerp(cameraRotationCenter.rotation, target.rotation, smoothSpeed);
    }

}
