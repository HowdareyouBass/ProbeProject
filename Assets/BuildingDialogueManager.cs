using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class BuildingDialogueManager : MonoBehaviour
{
    public TextMeshProUGUI dialogueWindow;
    public Image buildingImage;
   
    public string welcomeText;
    public Sprite buildingImageSprite;
   


    public void printWelcomeText()
    {
        buildingImage.sprite = buildingImageSprite;
        dialogueWindow.text = welcomeText;
    }

    public void printTextButton(string text)
    {
        dialogueWindow.text = text;
    }
}
