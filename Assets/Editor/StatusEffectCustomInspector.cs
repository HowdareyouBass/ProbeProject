#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using System.Linq;
using System;
using System.Collections.Generic;

//2 inspectors cuz unity can't serialize generic types
//TODO: Learn about Json serialization
[CustomEditor(typeof(StatusEffect))]
[CanEditMultipleObjects]
public class StatusEffectCustomInspector : Editor
{
    private bool m_PopupShowed;
    private List<Type> m_DerivedList;
    private List<string> m_DerivedListNames;
    private int m_Index;

    public void OnEnable()
    {
        if (m_DerivedList == null)
            m_DerivedList = GetAllDerived().ToList();
        if (m_DerivedListNames == null)
        {
            m_DerivedListNames = new List<string>();
            m_DerivedListNames.Add("None");
            foreach (Type type in m_DerivedList)
            {
                m_DerivedListNames.Add(type.ToString());
            }
        }
    }

    public override void OnInspectorGUI()
    {
        StatusEffect obj = ((StatusEffect)target);
        SerializedProperty property = serializedObject.FindProperty("m_Components");
        foreach (SerializedProperty prop in property)
        {
            //the 15 deletes Assembly CSharp before real type
            EditorGUILayout.PropertyField(prop, new GUIContent(prop.managedReferenceFullTypename.Substring(15)), true);
        }
        if (GUILayout.Button("Add Component"))
        {
            if (m_PopupShowed && m_Index != 0)
            {
                StatusEffectComponent component = Activator.CreateInstance(m_DerivedList[m_Index - 1]) as StatusEffectComponent;
                obj.AddComponent(component);
            }
            m_PopupShowed = !m_PopupShowed;
        }
        if (m_PopupShowed)
        {
            m_Index = EditorGUILayout.Popup(m_Index, m_DerivedListNames.ToArray());
        }
        if (GUILayout.Button("Delete"))
        {
            obj.DeleteComponent();
        }
        serializedObject.ApplyModifiedProperties();
        serializedObject.Update();
    }

    private IEnumerable<Type> GetAllDerived()
    {
        return AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(assembly => assembly.GetTypes())
            .Where(type => type.IsSubclassOf(typeof(StatusEffectComponent)))
            .Where(type => !type.IsAbstract);
    }
}
#endif