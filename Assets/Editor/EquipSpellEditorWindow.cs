using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EquipSpellEditorWindow : ExtendedEditorWindow
{
    public enum Slot {first, second, third, fourth, fifth};
    private static SpellDatabase db;
    private static Player player;
    private Slot spellSlot;
    public static bool isInPlaymode = true;

    public static void Open(SpellDatabase _db, Player _player)
    {
        EquipSpellEditorWindow window = GetWindow<EquipSpellEditorWindow>("Equip Spell");
        db = _db;
        player = _player;
        window.serializedObject = new SerializedObject(db);
    }

    private void OnGUI()
    {
        if (!isInPlaymode)
        {
            Close();
        }
        currentProperty = serializedObject.FindProperty("spells");
        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.BeginVertical();
        spellSlot = (Slot)EditorGUILayout.EnumPopup("Spell Slot", spellSlot);
        DrawSidebar(currentProperty);
        EditorGUILayout.EndVertical();

        EditorGUILayout.EndHorizontal();
        EditorGUILayout.Space();
        EditorGUILayout.BeginHorizontal();

        if (GUILayout.Button("Equip"))
        {
            player.EquipSpell(db.spells[propertyIndex], (int)spellSlot);
            Debug.Log("<color=green>Spell Equiped Successfully</color>");
            Close();
        }
        EditorGUILayout.EndHorizontal();

    }
}
