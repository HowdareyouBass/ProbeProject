using UnityEngine;

public class DestroyOnEndOfAnimation : MonoBehaviour
{
    public void DestroyGO()
    {
        Destroy(gameObject, 0.1f);
    }
}