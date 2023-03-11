using UnityEngine;

public class PlayerFollower : MonoBehaviour
{
    public Transform PlayerTransform;
    public Vector3 Offset;

    void Update()
    {
        this.transform.position = PlayerTransform.position + Offset;
    }
}
