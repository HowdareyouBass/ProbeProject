using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    private Transform camTransform;
    void Start()
    {
        camTransform = Camera.main.transform;
    }
    void Update()
    {
        transform.LookAt(camTransform);
        transform.forward = -transform.forward;
    }
}
