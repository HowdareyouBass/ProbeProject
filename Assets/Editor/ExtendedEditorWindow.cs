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
        string lastPropPath = string.Empty;
        if (prop.isArray && prop.propertyType == SerializedPropertyType.Generic)
        {
            EditorGUILayout.BeginHorizontal();
            prop.isExpanded = EditorGUILayout.Foldout(prop.isExpanded, prop.displayName);
            EditorGUILayout.EndHorizontal();

            if (prop.isExpanded)
            {
                EditorGUI.indentLevel++;
                DrawProperties(prop, drawChildren);
                EditorGUI.indentLevel--;
            }
        }
        else
        {
            if (!string.IsNullOrEmpty(lastPropPath) && prop.propertyPath.Contains(lastPropPath)) { return; }
            lastPropPath = prop.propertyPath;
            EditorGUILayout.PropertyField(prop, drawChildren);
        }
    }

    protected void DrawPropertiesFromList(SerializedProperty property, List<string> list, bool drawChildren)
    {
        foreach(SerializedProperty p in GetChildren(property))
        {
            if (list.Contains(p.displayName))
            {
                DrawProperties(p, drawChildren);
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
                    propertyIndex = GetIndexFromPropertyPath(selectedPropertyPath);
                }
                GUI.backgroundColor = new Color(1f, 1f, 1f);
            }
            else
            {
                if (GUILayout.Button(p.displayName))
                {
                    selectedPropertyPath = p.propertyPath;
                    propertyIndex = GetIndexFromPropertyPath(selectedPropertyPath);
                }
            }
        }
        if (!string.IsNullOrEmpty(selectedPropertyPath))
        {
            selectedProperty = serializedObject.FindProperty(selectedPropertyPath);
        }
    }

    private int GetIndexFromPropertyPath(string path)
    {
        int digits = 0;
        int i = 0;
        //Get index when first number met
        for (i = 1; !char.IsDigit(path[i]); i++);
        //Get length of number
        for (; char.IsDigit(path[i]); digits++, i++);
        //Just to use same index
        i -= digits;
        //Returning parsed number
        return Int32.Parse(path.Substring(i, digits));
    }

    private IEnumerable<SerializedProperty> GetChildren(SerializedProperty serializedProperty)
{
    SerializedProperty currentProperty = serializedProperty.Copy();
    SerializedProperty nextSiblingProperty = serializedProperty.Copy();
    {
        nextSiblingProperty.Next(false);
    }
 
    if (currentProperty.Next(true))
    {
        do
        {
            if (SerializedProperty.EqualContents(currentProperty, nextSiblingProperty))
                break;
 
            yield return currentProperty;
        }
        while (currentProperty.Next(false));
    }
}
}
