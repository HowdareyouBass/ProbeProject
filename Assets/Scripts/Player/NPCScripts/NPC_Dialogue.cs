using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC_Dialogue : MonoBehaviour
{
    [Header("TypingSound")]
    public AudioClip[] typingSound;
    public AudioSource typingSoundSource;

    [Header("Game Objects")]
    public GameObject dialoguePanel;
    public Image NPCPhoto;
    public Text NPCName;
    public Text dialogueText;
    public GameObject contButton;
    public GameObject skipButton;

    [Header("Character description")]
    public string NPCNameText;
    public string[] dialogue;
    public string[] badRepDialogue;
    public Sprite NPCPhotoSprite;

    [Header("Typing speed")]
    public float wordSpeed;

    [HideInInspector] private int index;
    [HideInInspector] public bool playerIsClose;
    [HideInInspector] public int respectPoints = 10;

    void Update()
    {
       if(Input.GetKeyDown(KeyCode.E) && playerIsClose)
        {
            if(dialoguePanel.activeInHierarchy)
            {
                SkipTyping();
                respectPoints -= 1;
                zeroText();
            }
            else
            {
                NPCPhoto.sprite = NPCPhotoSprite;
                NPCName.text = NPCNameText;
                zeroText();
                dialoguePanel.SetActive(true);

                if (respectPoints <= 0)
                {
                    dialogue = badRepDialogue;
                }

                StartCoroutine(Typing());
            }
        }

       if (dialogueText.text == dialogue[index])
        {
            contButton.SetActive(true);
            skipButton.SetActive(false);
        }
    }

    public void zeroText()
    {
        dialogueText.text = "";
        index = 0;
        dialoguePanel.SetActive(false);
    }


    IEnumerator Typing()
    {
        int randNum = Random.Range(0, typingSound.Length);

        typingSoundSource.PlayOneShot(typingSound[randNum]);

        foreach (char letter in dialogue[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
    }

    public void SkipTyping()
    {
        StopAllCoroutines();
        respectPoints -= 1;
        dialogueText.text = dialogue[index];
    }

    public void NextLine()
    {
        contButton.SetActive(false);
        skipButton.SetActive(true);

        if(index < dialogue.Length - 1)
        {
            ++index;
            dialogueText.text = "";
            StartCoroutine(Typing());
        }
        else
        {
            zeroText();
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("Player is near");

        if (collision.gameObject.tag == "Player")
        {
            playerIsClose = true;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (index < dialogue.Length - 1 || dialogueText.text != dialogue[index])
            {
                respectPoints -= 1;
            }

            playerIsClose = false;
            zeroText();
        }
    }
}
