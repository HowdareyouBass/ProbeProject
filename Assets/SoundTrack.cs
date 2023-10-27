using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTrack : MonoBehaviour
{
    private void Start()
    {
        GetComponent<AudioManager>().Play("SoundTrack");
    }
}
