using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarterDialogue : MonoBehaviour
{
    [Header("TypingSound")]
    public AudioClip[] _typingSound;
    public AudioSource _typingSoundSource;

    [Header("Game Objects")]
    public GameObject _dialoguePanel;
    public Image _NPCPhoto;
    public Text _NPCName;
    public Text _dialogueText;
    public GameObject _contButton;
    public GameObject _skipButton;
    public Text _contButtonText;

    [Header("Character description")]
    public string[] _NPCNameText;
    public string[] _dialogue;
    public string[] _badRepDialogue;
    public Sprite[] _NPCPhotoSprite;
    public string[] _characterLines;

    [Header("Character statistics")]
    public float _wordSpeed;
    public int _respectPoints = 10;

    [HideInInspector] private int _index;
    [HideInInspector] public bool _playerIsClose;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && _playerIsClose)
        {
            if (_dialoguePanel.activeInHierarchy)
            {
                _SkipTyping();
                _respectPoints -= 1;
                _zeroText();
            }
            else
            {
                _index = 0;
                _NPCPhoto.sprite = _NPCPhotoSprite[_index];
                _NPCName.text = _NPCNameText[_index];
                _zeroText();
                _dialoguePanel.SetActive(true);

                if (_respectPoints <= 0)
                {
                    _dialogue = _badRepDialogue;
                }

                StartCoroutine(_Typing());
            }
        }

        if (_dialogueText.text == _dialogue[_index])
        {
            _contButton.SetActive(true);
            _skipButton.SetActive(false);
        }
    }

    public void _zeroText()
    {
        _dialogueText.text = "";
        _index = 0;
        _dialoguePanel.SetActive(false);
    }


    IEnumerator _Typing()
    {
        int randNum = Random.Range(0, _typingSound.Length);

        _typingSoundSource.PlayOneShot(_typingSound[randNum]);

        foreach (char letter in _dialogue[_index].ToCharArray())
        {
            _dialogueText.text += letter;
            yield return new WaitForSeconds(_wordSpeed);
        }
    }

    public void _SkipTyping()
    {
        StopAllCoroutines();
        _respectPoints -= 1;
        _dialogueText.text = _dialogue[_index];
    }

    public void _NextLine()
    {
        _contButton.SetActive(false);
        _skipButton.SetActive(true);

        if (_index < _dialogue.Length - 1)
        {
            ++_index;
            _dialogueText.text = "";
            _NPCPhoto.sprite = _NPCPhotoSprite[_index];
            _NPCName.text = _NPCNameText[_index];

            if (_characterLines[_index] == "") 
            {
                _contButtonText.text = "Continue";
            }
            else
            {
                _contButtonText.text = _characterLines[_index];
            }

            StartCoroutine(_Typing());
        }
        else
        {
            _zeroText();
        }
    }

    private void OnTriggerEnter(Collider _collision)
    {
        Debug.Log("Player is near");

        if (_collision.gameObject.tag == "Player")
        {
            _playerIsClose = true;
        }
    }

    private void OnTriggerExit(Collider _collision)
    {
        if (_collision.gameObject.tag == "Player")
        {
            if (_index < _dialogue.Length - 1 || _dialogueText.text != _dialogue[_index])
            {
                _respectPoints -= 1;
            }

            _playerIsClose = false;
            _zeroText();
        }
    }
}
