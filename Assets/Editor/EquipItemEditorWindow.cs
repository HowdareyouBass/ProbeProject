using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EquipItemEditorWindow : ExtendedEditorWindow
{
    private static ItemDatabase db;
    private static PlayerBehaviour player;
    public static bool isInPlaymode = true;

    public static void Open(ItemDatabase _db, PlayerBehaviour _player)
    {
        EquipItemEditorWindow window = GetWindow<EquipItemEditorWindow>("Equip Item");
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
        currentProperty = serializedObject.FindProperty("items");
        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.BeginVertical();
        DrawSidebar(currentProperty);
        EditorGUILayout.EndVertical();

        EditorGUILayout.EndHorizontal();
        EditorGUILayout.Space();
        EditorGUILayout.BeginHorizontal();

        if (GUILayout.Button("Equip"))
        {
            player.EquipItem(db.items[propertyIndex]);
            Debug.Log("<color=green>Item Equiped Successfully</color>");
            Close();
        }
        EditorGUILayout.EndHorizontal();

    }
}
