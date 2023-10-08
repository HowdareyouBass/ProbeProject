using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildingDialogueManager : MonoBehaviour
{
    public TextMeshProUGUI dialogueWindow;
    public Image buildingImage;

    public string welcomeText;
    public Sprite buildingImageSprite;
    public float typingSpeed;

    public IEnumerator PrintText(string text)
    {
        dialogueWindow.text = "";

        foreach (char character in text)
        {
            dialogueWindow.text += character;

            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public void printTextButton(string text)
    {
        StopAllCoroutines();
        StartCoroutine(PrintText(text));
    }


    void Start()
    {
        buildingImage.sprite = buildingImageSprite;

        StartCoroutine(PrintText(welcomeText));
    }
}
