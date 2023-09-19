using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode()]
public class Tooltip : MonoBehaviour
{
    public TextMeshProUGUI headerField;
    public TextMeshProUGUI raceField;
    public TextMeshProUGUI relationsField;
    public TextMeshProUGUI statsField;
    public TextMeshProUGUI contentField;
  
    public LayoutElement layoutElement;

    public int characterWrapLimit;

    public RectTransform rectTransform;

    private void Awake()
    {
       rectTransform = GetComponent<RectTransform>();
    }

    public void SetText(string content, string race, string relations, string stats, string header = "")
    {
        if (string.IsNullOrEmpty(header))
        {
            headerField.gameObject.SetActive(false);
        }
        else
        {
            headerField.gameObject.SetActive(true);
            headerField.text = header;
        }

        contentField.text = content;
        raceField.text = "Race: " + race;
        relationsField.text = "Relations: " + relations;
        statsField.text = "Statistics: " + stats;

        int headerLenght = headerField.text.Length;
        int contentLenght = contentField.text.Length;
        int raceLenght = raceField.text.Length;
        int relationsLenght = relationsField.text.Length;

        layoutElement.enabled = (headerLenght > characterWrapLimit ||
                                 contentLenght > characterWrapLimit ||
                                 raceLenght > characterWrapLimit ||
                                 relationsLenght > characterWrapLimit) ? true : false;

    }

    private void Update()
    {
        Vector2 position = Input.mousePosition;


        float pivotX = position.x / Screen.width;
        float pivotY = position.y / Screen.height;

        rectTransform.pivot = new Vector2(pivotX, pivotY);
        transform.position = position;
    }
}
