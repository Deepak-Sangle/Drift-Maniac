using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform Target;
    private Vector3 offset, followPos;
    private float y;
    public float speedFlow=10f;
    void Start()
    {
        offset = transform.position;
    }
    void LateUpdate()
    {
        followPos.z = Target.position.z + offset.z;
        followPos.y = offset.y;
        followPos.x = offset.x;
        transform.position = followPos;
    }

}
