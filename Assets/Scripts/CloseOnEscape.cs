using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseOnEscape : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && gameObject.activeSelf)
        {
            gameObject.SetActive(false);
        }
    }
}
