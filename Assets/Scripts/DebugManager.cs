using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class DebugManager : MonoBehaviour
{
    public PlayerBehaviour player;
    public GameObject debugUI;
    public List<GameObject> spellPrefabsList;

    private Spell GetSpellType(string type, string spellName)
    {
        if (type == "Projectile")
        {
            if (spellName == "Fireball") return new Spell();
        }
        Debug.Log("ll");
        return null;
    }
    public void AddSpell()
    {
        Transform spellPanel = debugUI.transform.Find("Add Spell Panel");

        TMP_InputField damageInput = spellPanel.Find("Damage").Find("Damage Input").GetComponent<TMP_InputField>();
        TMP_InputField slotInput = spellPanel.Find("Spell Slot").Find("Slot Input").GetComponent<TMP_InputField>();

        TMP_Dropdown spellType = spellPanel.Find("Spell Type").GetComponent<TMP_Dropdown>();
        TMP_Dropdown spellName = spellPanel.Find("Spell").GetComponent<TMP_Dropdown>();

        if (damageInput.text != "" &&
            slotInput.text != "")
        {
            Spell spell = GetSpellType(spellType.captionText.text, spellName.captionText.text); 
            player.EquipSpell(spell, Int16.Parse(slotInput.text));
        }
        else
        {
            Debug.Log("Bruh your input field is clean as hell");
        }
    }

    public void AddItem()
    {
        Transform itemPanel = debugUI.transform.Find("Add Item Panel");
        Transform  attackSpeedInput = itemPanel.Find("Attack Speed").Find("Attack Speed Input");
        TMP_InputField input = attackSpeedInput.GetComponent<TMP_InputField>();
        if (input.text != "")
        {
            Item item = new Item(Int16.Parse(input.text));
            player.EquipItem(item);
        }
        else
        {
            Debug.Log("Bruh your input field is clean as hell");
        }
        input.text = "0";
    }
}
