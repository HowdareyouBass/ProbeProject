#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using System.Linq;
using System;
using System.Collections.Generic;

[CustomEditor(typeof(Spell))]
public class SpellSOCustomInspector : Editor
{
    private bool m_PopupShowed;
    private List<SpellComponent> m_DerivedList;
    private List<string> m_DerivedListNames;
    private int m_Index;

    public void OnEnable()
    {
        if (m_DerivedList == null)
            m_DerivedList = GetAllDerived().Select(comp => ScriptableObject.CreateInstance(comp) as SpellComponent).ToList();
        if (m_DerivedListNames == null)
        {
            m_DerivedListNames = new List<string>();
            foreach (SpellComponent component in m_DerivedList)
            {
                m_DerivedListNames.Add(component.GetType().ToString());
            }
        }
    }

    public override void OnInspectorGUI()
    {
        Spell spell = ((Spell)target);
        spell.OnGUI();
        if (GUILayout.Button("Add Component"))
        {
            if (m_PopupShowed)
            {
                m_DerivedList[m_Index].AddSelf(spell);
            }
            m_PopupShowed = !m_PopupShowed;
            //spell.AddComponent<ActiveSpellComponent>();
        }
        if (m_PopupShowed)
        {
            m_Index = EditorGUILayout.Popup(m_Index, m_DerivedListNames.ToArray());
        }
        if (GUILayout.Button("Delete"))
        {
            spell.DeleteComponent();
        }
        serializedObject.Update();
    }

    private IEnumerable<Type> GetAllDerived()
    {
        return AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(assembly => assembly.GetTypes())
            .Where(type => type.IsSubclassOf(typeof(SpellComponent)))
            .Where(type => !type.IsAbstract);
    }
}
#endif