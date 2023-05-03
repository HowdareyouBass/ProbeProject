using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ItemDatabase))]
public class ItemDatabaseCustomEditor : Editor
{
    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("Open Window"))
        {
            ItemDatabaseEditorWindow.Open((ItemDatabase)target);
        }
    }
}

