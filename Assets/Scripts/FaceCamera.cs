using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    private Transform m_MainCameraTransform;
    private void Start()
    {
        m_MainCameraTransform = Camera.main.transform;
    }
    private void Update()
    {
        transform.LookAt(m_MainCameraTransform);
        transform.forward = -transform.forward;
    }
}