using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public PlayerBehaviour player;
    public GameObject debugUI;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            player.CastSpell(0);
        }
        if (Input.GetKeyDown(KeyCode.F12))
        {
            if(debugUI.activeSelf)
            {
                debugUI.SetActive(false);
            }
            else
            {
                debugUI.SetActive(true);
            }
        }
    }
}
