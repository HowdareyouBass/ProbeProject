using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class ExtendedEditorWindow : EditorWindow
{
    protected SerializedObject serializedObject;
    protected SerializedProperty currentProperty;

    private string selectedPropertyPath;
    protected SerializedProperty selectedProperty;
    protected int propertyIndex = 0;
    private string searchName = string.Empty;


    protected void DrawProperties(SerializedProperty prop, bool drawChildren)
    {
        foreach(SerializedProperty p in prop)
        {
            EditorGUILayout.PropertyField(prop, drawChildren);
        }
    }

    protected void DrawPropertiesFromList(SerializedProperty prop, List<string> list, bool drawChildren)
    {
        foreach(SerializedProperty p in prop)
        {
            if (list.Contains(p.displayName))
            {
                EditorGUILayout.PropertyField(prop, drawChildren);
            }
        }
    }

    protected void DrawSidebar(SerializedProperty prop)
    {

        EditorGUILayout.LabelField("Search");
        searchName = EditorGUILayout.TextField(searchName);
        foreach(SerializedProperty p in prop)
        {
            if (!p.displayName.ToLower().Contains(searchName.ToLower()) && !string.IsNullOrEmpty(searchName))
            {
                continue;
            }

            if (p.propertyPath == selectedPropertyPath)
            {
                GUI.backgroundColor = new Color(0.32f, 0.32f, 0.32f);
                if (GUILayout.Button(p.displayName))
                {
                    selectedPropertyPath = p.propertyPath;
                    propertyIndex = Int32.Parse(selectedPropertyPath[18].ToString());
                }
                GUI.backgroundColor = new Color(1f, 1f, 1f);
            }
            else
            {
                if (GUILayout.Button(p.displayName))
                {
                    selectedPropertyPath = p.propertyPath;
                    propertyIndex = Int32.Parse(selectedPropertyPath[18].ToString());
                }
            }
        }
        if (!string.IsNullOrEmpty(selectedPropertyPath))
        {
            selectedProperty = serializedObject.FindProperty(selectedPropertyPath);
        }
    }
}
