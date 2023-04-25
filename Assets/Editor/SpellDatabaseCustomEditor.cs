using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

public class AssetHandler
{
    [OnOpenAsset()]
    public static bool OpenEditor(int instanceId, int line)
    {
        SpellDatabase spelldb = EditorUtility.InstanceIDToObject(instanceId) as SpellDatabase;
        ItemDatabase itemdb = EditorUtility.InstanceIDToObject(instanceId) as ItemDatabase;
        if (spelldb != null)
        {
            SpellDatabaseEditorWindow.Open(spelldb);
            return true;
        }
        if (itemdb != null)
        {   
            ItemDatabaseEditorWindow.Open(itemdb);
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
