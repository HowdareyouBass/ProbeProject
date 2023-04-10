using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnEndOfAnimation : MonoBehaviour
{
    public void DestroyGO()
    {
        Destroy(transform.gameObject, 0.1f);
    }
}
