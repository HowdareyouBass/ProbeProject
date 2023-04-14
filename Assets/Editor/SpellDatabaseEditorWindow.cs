using UnityEditor;
using UnityEngine;

public class SpellDatabaseEditorWindow : EditorWindow
{
    private static SpellDatabase db;
    private SerializedObject serializedObject;
    private string searchName = string.Empty;
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
        DrawProperty(serializedObject.FindProperty("spells"), true);
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Add"))
        {
            db.spells.Add(new Spell(""));
            serializedObject.Update();
        }
        EditorGUILayout.EndHorizontal();
    }

    private void DrawProperty(SerializedProperty prop, bool drawChildren)
    {
        int spellIndex = 0;
        EditorGUILayout.LabelField("Search");
        searchName = EditorGUILayout.TextField(searchName);
        foreach(SerializedProperty p in prop)
        {
            //if spell doesn't found in database then we go to next
            if (!db.spells[spellIndex].GetName().ToLower().Contains(searchName.ToLower()) && !string.IsNullOrEmpty(searchName))
            {
                spellIndex++;
                continue;
            }
            EditorGUILayout.BeginHorizontal();
            p.isExpanded = EditorGUILayout.Foldout(p.isExpanded, p.displayName);
            EditorGUILayout.EndHorizontal();
            
            if (p.isExpanded)
            {
                EditorGUI.indentLevel++;

                EditorGUILayout.PropertyField(p.FindPropertyRelative("m_Name"));
                EditorGUILayout.PropertyField(p.FindPropertyRelative("m_Effect"));
                EditorGUILayout.PropertyField(p.FindPropertyRelative("m_SpellDamage"));
                EditorGUILayout.PropertyField(p.FindPropertyRelative("m_Type"));
                
                
                if (db.spells[spellIndex].GetSpellType() == Spell.Types.projectile)
                {
                    EditorGUILayout.PropertyField(p.FindPropertyRelative("m_EffectOnImpact"));
                    EditorGUILayout.PropertyField(p.FindPropertyRelative("m_SpeedOfProjectile"));
                }
                
                
                EditorGUILayout.BeginHorizontal();
                GUILayout.Space(20);
                if (GUILayout.Button("Delete Spell"))
                {
                    db.spells.RemoveAt(spellIndex);
                    p.isExpanded = false;
                    serializedObject.ApplyModifiedProperties();
                    serializedObject.Update();
                }
                EditorGUILayout.EndHorizontal();

                EditorGUI.indentLevel--;
            }
            spellIndex++;
        }
    }
}
