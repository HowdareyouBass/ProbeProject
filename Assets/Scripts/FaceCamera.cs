using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    public Transform camTransform;
    void Update()
    {
        transform.LookAt(camTransform);
        transform.forward = -transform.forward;
    }
}
