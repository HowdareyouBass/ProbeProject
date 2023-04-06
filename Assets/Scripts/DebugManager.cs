using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class DebugManager : MonoBehaviour
{
    public PlayerBehaviour player;
    public GameObject addItemPanel;
    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    public void AddItem()
    {
        Item item = new Item(Int16.Parse(addItemPanel.GetComponentInChildren<TMP_InputField>().text));
        player.playerEquipment.EquipItem(item);
    }
}
