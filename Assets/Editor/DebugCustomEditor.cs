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

    private void OnEnable()
    {
        settings = target as DebugManager;
        settings.settingsSO.player = GameObject.Find("/Player").GetComponent<PlayerBehaviour>();
    }

    public override void OnInspectorGUI()
    {        
        isInPlaymode = Application.IsPlaying(target);
        if (!isInPlaymode)
        {
            EquipSpellEditorWindow.isInPlaymode = false;
            base.OnInspectorGUI();
            return;
        }        

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
            EquipItemEditorWindow.Open(settings.settingsSO.itemDatabase, settings.settingsSO.player);
            //OnEquipItemClick();
        }
        if (showEquipItem)
        {
            ShowEquipItem();
        }
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
    
    private void ShowEquipItem()
    {
        EditorGUI.indentLevel++;

        EditorGUILayout.BeginHorizontal();

        itemName = EditorGUILayout.TextField("Item Name", itemName);
        EditorGUILayout.EndHorizontal();

        EditorGUI.indentLevel--;
    }
}
