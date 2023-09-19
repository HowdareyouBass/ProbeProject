using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipScript : MonoBehaviour
{
    private static TooltipScript current;

    public Tooltip tooltip;

    public void Awake()
    {
        current = this;
    }

    public static void Show(string content, string race, string relations, string stats, string header = "")
    {
        current.tooltip.SetText(content, race, relations, stats, header);
        current.tooltip.gameObject.SetActive(true);
    }

    public static void Hide()
    {
        current.tooltip.gameObject.SetActive(false);
    }

}
