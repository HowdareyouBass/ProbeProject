using UnityEditor;
using UnityEngine;

public class SpellDatabaseEditorWindow : ExtendedEditorWindow
{
    private static SpellDatabase db;
    private string searchName;

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
            db = Resources.Load<SpellDatabase>("SpellDatabase");
            serializedObject = new SerializedObject(db);
        }
        serializedObject.ApplyModifiedProperties();
        DrawProperties(serializedObject.FindProperty("spells"), true);
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Add"))
        {
            db.spells.Add(new Spell(""));
            serializedObject.Update();
        }
        EditorGUILayout.EndHorizontal();
    }

    private void DrawProperties(SerializedProperty prop, bool drawChildren)
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
                Spell.Types spellType = db.spells[spellIndex].GetSpellType();

                EditorGUI.indentLevel++;

                EditorGUILayout.PropertyField(p.FindPropertyRelative("m_Name"));
                EditorGUILayout.PropertyField(p.FindPropertyRelative("m_Type"));

                if (spellType == Spell.Types.passive)
                {
                    EditorGUILayout.PropertyField(p.FindPropertyRelative("m_PassiveStats"));
                    EditorGUILayout.PropertyField(p.FindPropertyRelative("m_Percents"));
                }

                if (spellType == Spell.Types.directedAtEnemy)
                {
                    EditorGUILayout.PropertyField(p.FindPropertyRelative("m_Effect"));
                    EditorGUILayout.PropertyField(p.FindPropertyRelative("m_SpellDamage"));
                }
                
                if (spellType == Spell.Types.projectile)
                {
                    EditorGUILayout.PropertyField(p.FindPropertyRelative("m_Effect"));
                    EditorGUILayout.PropertyField(p.FindPropertyRelative("m_SpellDamage"));
                    EditorGUILayout.PropertyField(p.FindPropertyRelative("m_EffectOnImpact"));
                    EditorGUILayout.PropertyField(p.FindPropertyRelative("m_SpeedOfProjectile"));
                }
                
                //Delete Spell Button
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
