using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [Header("카메라가 바라볼 대상")] 
    public Transform target;
    public Transform targetChild;
    [Header("카메라가 이동할 속도")] 
    public float smoothSpeed = 5.0f;

    private void FixedUpdate()
    {
        Follow();
    }

    void Follow()
    {
        transform.position = Vector3.Lerp(transform.position, targetChild.position, Time.fixedDeltaTime * smoothSpeed);
        transform.LookAt(target);
    }
}
