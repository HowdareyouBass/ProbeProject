using UnityEngine;

public class DestroyOnEndOfParticle : MonoBehaviour
{
    public float particleDuration;

    private void Start()
    {
        Destroy(gameObject, particleDuration);
    }
}