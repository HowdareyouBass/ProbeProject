using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public PlayerBehaviour player;
    public PlayerController playerController;
    public GameObject debugUI;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            player.CastSpell(0);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            playerController.StopAction();
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
