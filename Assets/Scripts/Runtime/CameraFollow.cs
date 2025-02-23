using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private Transform target;

    [SerializeField]
    private Vector3 offset;

    [SerializeField]
    private float smoothTime = 0.5f;

    private void Update()
    {
        transform.position = Vector3.Lerp(
            transform.position,
            target.position + offset,
            smoothTime * Time.deltaTime
        );
        transform.LookAt(target);
    }
}
