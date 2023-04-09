using UnityEditor;
using UnityEngine;

public class SpellDatabaseEditorWindow : ExtendedEditorWindow
{
    static SpellDatabase db;
    public static void Open(SpellDatabase _db)
    {

        SpellDatabaseEditorWindow window = GetWindow<SpellDatabaseEditorWindow>("Spell Database Editor");
        db = _db;
        window.serializedObject = new SerializedObject(db);
    }

    private void OnGUI()
    {
        if (serializedObject == null)
        {
            if (GUILayout.Button("File open"))
            {
                db = Resources.Load<SpellDatabase>("SpellDatabase");
                serializedObject = new SerializedObject(db);
            }
            return;
        }
        serializedObject.ApplyModifiedProperties();
        currentProperty = serializedObject.FindProperty("spells");
        //EditorGUILayout.PropertyField(currentProperty);
        DrawProperties(currentProperty, true, db);
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Add"))
        {
            db.spells.Add(new Spell());
            serializedObject.Update();
        }
        EditorGUILayout.EndHorizontal();
    }
}
