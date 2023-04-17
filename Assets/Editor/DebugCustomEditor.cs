using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DebugManager))]
public class DebugCustomEditor : Editor
{
    private enum Slot { first, second, third, fourth, fifth }
    private bool showEquipSpell = false;
    private bool showEquipItem = false;
    private Slot spellSlot;
    private bool isInPlaymode;
    private string spellName = string.Empty;
    private string itemName = string.Empty;
    private DebugManager settings;

    public override void OnInspectorGUI()
    { 
        //Debug.Log("Spell Name: " + spellName);
        //Debug.Log("Item Name: " + itemName);

        isInPlaymode = Application.IsPlaying(target);
        if (!isInPlaymode)
        {
            EquipSpellEditorWindow.isInPlaymode = false;
            base.OnInspectorGUI();
            return;
        }

        settings = target as DebugManager;

        settings.settingsSO.player = GameObject.Find("/Player").GetComponent<PlayerBehaviour>();

        if (GUILayout.Button("Equip Spell"))
        {
            EquipSpellEditorWindow.Open(settings.settingsSO.spellDatabase, settings.settingsSO.player);
            //OnEquipSpellClick();
        }
        if (showEquipSpell)
        {
            ShowEquipSpell();
        }

        if(GUILayout.Button("Equip Item"))
        {
            OnEquipItemClick();
        }
        if (showEquipItem)
        {
            ShowEquipItem();
        }
    }

    private void OnEquipSpellClick()
    {
        if (showEquipSpell)
        {
            if (string.IsNullOrEmpty(spellName))
            {
                Debug.LogWarning("Name is empty");
                showEquipSpell = !showEquipSpell;
                return;
            }
            Spell spell = FindByName<Spell>(settings.settingsSO.spellDatabase.spells);

            if (spell == null)
            {
                Debug.LogWarning("Spell not found");
                showEquipSpell = !showEquipSpell;
                return;
            }
            spellName = string.Empty;
            settings.settingsSO.player.EquipSpell(spell, (int)spellSlot);
            Debug.Log("<color=green>Spell Successfully Equiped</color>");
        }
        showEquipSpell = !showEquipSpell;
    }

    private void ShowEquipSpell()
    {
        EditorGUI.indentLevel++;

        EditorGUILayout.BeginHorizontal();
        spellName = EditorGUILayout.TextField("Spell Name", spellName);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        spellSlot = (Slot)EditorGUILayout.EnumPopup("Spell Slot", spellSlot);
        EditorGUILayout.EndHorizontal();

        EditorGUI.indentLevel--;
    }

    private void OnEquipItemClick()
    {
        if (showEquipItem)
        {
            if (string.IsNullOrEmpty(itemName))
            {
                Debug.LogWarning("Name is empty");
                showEquipItem = !showEquipItem;
                return;
            }
            Item item = FindByName<Item>(settings.settingsSO.itemDatabase.items);

            if (item == null)
            {
                Debug.LogWarning("Item not found");
                showEquipItem = !showEquipItem;
                return;
            }
            itemName = string.Empty;

            Debug.Log("<color=green>Item Successfully Equiped</color>");
            
            settings.settingsSO.player.EquipItem(item);
        }
        showEquipItem = !showEquipItem;
    }

    private void ShowEquipItem()
    {
        EditorGUI.indentLevel++;

        EditorGUILayout.BeginHorizontal();

        itemName = EditorGUILayout.TextField("Item Name", itemName);
        EditorGUILayout.EndHorizontal();

        EditorGUI.indentLevel--;
    }

    private T FindByName<T>(List<T> list) where T : DatabaseItem
    {
        T item = null;

        foreach (T sp in list)
        {
            if (sp.GetName() == spellName)
            {
                item = sp;
                break;
            }
            if (sp.GetName().ToLower() == spellName.ToLower())
            {
                item = sp;
                break;
            }
            if (sp.GetName().Contains(spellName))
            {
                item = sp;
                break;
            }
            if (sp.GetName().Contains(spellName.ToLower()))
            {
                item = sp;
                break;
            }
        }
        return item;
    }
}
