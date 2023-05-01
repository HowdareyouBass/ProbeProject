using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ItemDatabaseEditorWindow : ExtendedEditorWindow
{
    private static ItemDatabase db;

    public static void Open(ItemDatabase _db)
    {
        ItemDatabaseEditorWindow window = GetWindow<ItemDatabaseEditorWindow>("Item Database Editor");
        db = _db;
        window.serializedObject = new SerializedObject(db);
    }

    private void OnGUI()
    {
        if (serializedObject == null)
        {
            db = Resources.Load<ItemDatabase>("ItemDatabase");
            serializedObject = new SerializedObject(db);
        }

        currentProperty = serializedObject.FindProperty("items");
        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.BeginVertical("box", GUILayout.MaxWidth(250), GUILayout.ExpandHeight(true));
        DrawSidebar(currentProperty);
        EditorGUILayout.EndVertical();

        EditorGUILayout.BeginVertical("box", GUILayout.ExpandHeight(true));
        if (selectedProperty != null)
        {
            DrawProperties(selectedProperty, true);
            if (GUILayout.Button("Delete Spell"))
            {
                db.items.RemoveAt(propertyIndex);
                serializedObject.Update();
            }
        }
        else
        {
            EditorGUILayout.LabelField("Select an item from the list");
        }
        EditorGUILayout.EndVertical();

        EditorGUILayout.EndHorizontal();

        serializedObject.ApplyModifiedProperties();
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Add"))
        {
            db.items.Add(new Item(""));
            serializedObject.Update();
        }
        EditorGUILayout.EndHorizontal();

        serializedObject.ApplyModifiedProperties();
    }

    
}
