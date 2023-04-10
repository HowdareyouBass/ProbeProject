using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DebugManager))]
public class DebugCustomEditor : Editor
{
    private bool showEquipSpell = false;
    private string spellName = string.Empty;
    private int spellSlot;
    private bool isInPlaymode;

    public override void OnInspectorGUI()
    { 
        isInPlaymode = Application.IsPlaying(target);
        if (!isInPlaymode)
        {
            base.OnInspectorGUI();
            return;
        }

        DebugManager settings = target as DebugManager;

        settings.settingsSO.player = GameObject.Find("/Player").GetComponent<PlayerBehaviour>();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Player");
        settings.settingsSO.player = EditorGUILayout.ObjectField(settings.settingsSO.player, typeof(PlayerBehaviour), true) as PlayerBehaviour;
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("SpellDatabase");
        settings.settingsSO.spellDatabase = EditorGUILayout.ObjectField(settings.settingsSO.spellDatabase, typeof(SpellDatabase), false) as SpellDatabase;
        EditorGUILayout.EndHorizontal();

        if (GUILayout.Button("Equip Spell"))
        {
            if (!showEquipSpell)
            {
                showEquipSpell = true;
            }
            else
            {
                if (!string.IsNullOrEmpty(spellName))
                {
                    Spell spell = null;
                    foreach (Spell sp in settings.settingsSO.spellDatabase.spells)
                    {
                        if (sp.GetName() == spellName)
                        {
                            spell = sp;
                        }
                    }
                    foreach (Spell sp in settings.settingsSO.spellDatabase.spells)
                    {
                        if (sp.GetName().Contains(spellName))
                        {
                            spell = sp;
                        }
                    }
                    if (spell == null)
                    {
                        Debug.Log("Spell not found");
                    }
                    else
                    {
                        settings.settingsSO.player.EquipSpell(spell, spellSlot);
                    }
                }
                else
                {
                    Debug.Log("Name is empty");
                }
                showEquipSpell = false;
            }
        }
        if (showEquipSpell)
        {
            EditorGUI.indentLevel++;

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Spell Name");
            spellName = EditorGUILayout.TextField(spellName);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Spell Slot");
            spellSlot = EditorGUILayout.IntField(spellSlot);
            EditorGUILayout.EndHorizontal();

            EditorGUI.indentLevel--;
        }
    }
}
