using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ExtendedEditorWindow : EditorWindow
{
    protected SerializedProperty currentProperty;
    protected SerializedObject serializedObject;

    private int IntPow(int x, uint pow)
    {
        int ret = 1;
        while ( pow != 0 )
        {
            if ( (pow & 1) == 1 )
                ret *= x;
            x *= x;
            pow >>= 1;
        }
        return ret;
    }

    protected void DrawProperties(SerializedProperty prop, bool drawChildren, SpellDatabase db)
    {
        //string lastPropPath = string.Empty;
        int spellIndex = 0;
        foreach(SerializedProperty p in prop)
        {
            EditorGUILayout.BeginHorizontal();
            p.isExpanded = EditorGUILayout.Foldout(p.isExpanded, p.displayName);
            EditorGUILayout.EndHorizontal();

            //if (!string.IsNullOrEmpty(lastPropPath) && p.propertyPath.Contains(lastPropPath)) { continue; }
            //lastPropPath = p.propertyPath;
            
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
