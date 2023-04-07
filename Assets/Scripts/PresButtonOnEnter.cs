using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PresButtonOnEnter : MonoBehaviour
{
    public Button buttonToPress;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            buttonToPress.onClick.Invoke();
        }
    }
}
