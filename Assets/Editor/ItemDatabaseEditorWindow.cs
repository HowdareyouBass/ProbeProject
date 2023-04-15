using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ItemDatabaseEditorWindow : ExtendedEditorWindow
{
    private static ItemDatabase db;
    private string searchName;


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
            if (GUILayout.Button("File open"))
            {
                db = Resources.Load<ItemDatabase>("ItemDatabase");
                serializedObject = new SerializedObject(db);
            }
            return;
        }
        serializedObject.ApplyModifiedProperties();
        if (serializedObject.FindProperty("items") == null)
        {
            Debug.Log("BUFH");
        }

        DrawProperties(serializedObject.FindProperty("items"), true);
        
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Add"))
        {
            db.items.Add(new Item(""));
            serializedObject.Update();
        }
        EditorGUILayout.EndHorizontal();
    }

    private void DrawProperties(SerializedProperty prop, bool drawChildren)
    {
        int index = 0;
        EditorGUILayout.LabelField("Search");
        searchName = EditorGUILayout.TextField(searchName);
        foreach(SerializedProperty p in prop)
        {
            //if spell doesn't found in database then we go to next
            if (!db.items[index].GetName().ToLower().Contains(searchName.ToLower()) && !string.IsNullOrEmpty(searchName))
            {
                index++;
                continue;
            }

            EditorGUILayout.BeginHorizontal();
            p.isExpanded = EditorGUILayout.Foldout(p.isExpanded, p.displayName);
            EditorGUILayout.EndHorizontal();
            
            if (p.isExpanded)
            {
                EditorGUI.indentLevel++;

                EditorGUILayout.PropertyField(p.FindPropertyRelative("m_Name"));
                EditorGUILayout.PropertyField(p.FindPropertyRelative("m_AttackSpeed"));
                
                EditorGUILayout.BeginHorizontal();
                GUILayout.Space(20);
                if (GUILayout.Button("Delete Item"))
                {
                    db.items.RemoveAt(index);
                    p.isExpanded = false;
                    serializedObject.ApplyModifiedProperties();
                    serializedObject.Update();
                }
                EditorGUILayout.EndHorizontal();

                EditorGUI.indentLevel--;
            }
            index++;
        }
    }
}
