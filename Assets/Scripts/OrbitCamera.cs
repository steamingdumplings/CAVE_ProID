using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitCamera : MonoBehaviour
{
    public Transform target;
    public float speed = 20f;
    public bool orbiting = true;
    void Update()
    {
        if (!orbiting) return;
        transform.RotateAround(target.position,
          Vector3.up, speed * Time.deltaTime);
        transform.LookAt(target.position);
    }
}
