using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DebugManager))]
public class DebugCustomEditor : Editor
{
    private bool isInPlaymode;
    private DebugManager debugManager;

    private void OnEnable()
    {
        debugManager = target as DebugManager;
        debugManager.settingsSO.player = GameObject.Find("/Player").GetComponent<Player>();
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
            EquipSpellEditorWindow.Open(debugManager.settingsSO.spellDatabase, debugManager.settingsSO.player);
        }

        if(GUILayout.Button("Equip Item"))
        {
            EquipItemEditorWindow.Open(debugManager.settingsSO.itemDatabase, debugManager.settingsSO.player);
        }
    }
}
