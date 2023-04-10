using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

public class AssetHandler
{
    [OnOpenAsset()]
    public static bool OpenEditor(int instanceId, int line)
    {
        SpellDatabase obj = EditorUtility.InstanceIDToObject(instanceId) as SpellDatabase;
        if (obj != null)
        {
            SpellDatabaseEditorWindow.Open(obj);
            return true;
        }
        return false;
    }
}

[CustomEditor(typeof(SpellDatabase))]
public class SpellDatabaseCustomEditor : Editor
{
    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("Open Window"))
        {
            SpellDatabaseEditorWindow.Open((SpellDatabase)target);
        }
    }
}
