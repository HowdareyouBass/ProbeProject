using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DebugManager))]
public class DebugCustomEditor : Editor
{
    private enum Slot { first, second, third, fourth, fifth }
    private bool showEquipSpell = false;
    private Slot spellSlot;
    private bool isInPlaymode;
    private string spellName = string.Empty;


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

        if (GUILayout.Button("Equip Spell"))
        {
            OnEquipSpellClick(settings);
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
            spellSlot = (Slot)EditorGUILayout.EnumFlagsField(spellSlot);
            EditorGUILayout.EndHorizontal();

            EditorGUI.indentLevel--;
        }
    }

    private void OnEquipSpellClick(DebugManager settings)
    {
        if (showEquipSpell)
        {
            if (string.IsNullOrEmpty(spellName))
            {
                Debug.LogWarning("Name is empty");
                return;
            }
            Spell spell = FindSpellByName(settings.settingsSO.spellDatabase);

            if (spell == null)
            {
                Debug.LogWarning("Spell not found");
                return;
            }
            settings.settingsSO.player.EquipSpell(spell, (int)spellSlot);
        }
        showEquipSpell = !showEquipSpell;
    }

    private Spell FindSpellByName(SpellDatabase db)
    {
       Spell spell = null;

        foreach (Spell sp in db.spells)
        {
            if (sp.GetName() == spellName)
            {
                spell = sp;
            }
        }
        foreach (Spell sp in db.spells)
        {
            if (sp.GetName().Contains(spellName))
            {
                spell = sp;
            }
        }
        return spell;
    }
}
