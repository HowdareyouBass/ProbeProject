using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnEndOfParticle : MonoBehaviour
{
    public float particleDuration;

    void Start()
    {
        Destroy(gameObject, particleDuration);
    }
}
