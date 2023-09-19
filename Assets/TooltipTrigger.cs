using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipTrigger : MonoBehaviour
{
    public string content;
    public string header;
    public string race;
    public string relations;
    public string stats;
    private bool mouseOver;

    public void OnMouseEnter()
    {
        Debug.Log("Pointer on");
        mouseOver = true;
    }

    public void OnMouseExit()
    {
        mouseOver = false;
        Debug.Log("Pointer off");
        TooltipScript.Hide();
    }

    public void Update()
    {
        if (mouseOver && Input.GetMouseButtonDown(0))
        {
            TooltipScript.Show(content, race, relations, stats, header);
        }
    }
}
