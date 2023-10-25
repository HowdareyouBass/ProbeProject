using UnityEngine;

public class PlayerFollower : MonoBehaviour
{
    [SerializeField] private Transform m_PlayerTransform;
    [SerializeField] private Vector2 m_Offset;

    private void Update()
    {
        if (m_PlayerTransform != null)
        {
            Vector3 position = new Vector3(m_PlayerTransform.position.x + m_Offset.x, m_PlayerTransform.position.y + m_Offset.y, m_PlayerTransform.position.z + m_Offset.x);
            transform.position = position;
        }
    }
}