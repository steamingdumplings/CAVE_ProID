using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLook : MonoBehaviour
{
    float rotX, rotY;
    public float sensitivity = 2f;
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            rotX -= Input.GetAxis("Mouse Y") * sensitivity;
            rotY += Input.GetAxis("Mouse X") * sensitivity;
            rotX = Mathf.Clamp(rotX, -80f, 80f);
            transform.localRotation = Quaternion.Euler(rotX, rotY, 0);
        }
    }
}
