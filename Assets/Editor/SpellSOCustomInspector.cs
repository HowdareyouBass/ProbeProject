#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using System.Linq;
using System;
using System.Collections.Generic;

namespace newSys
{
    [CustomEditor(typeof(Spell))]
    public class SpellSOCustomInspector : Editor
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
            //Debug.Log("Object: " + (scrip == null).ToString());
            //Debug.Log("Component: " + (comp == null).ToString());
            EditorUtility.SetDirty(target);
            Spell spell = ((Spell)target);
            spell.OnGUI();
            if (GUILayout.Button("Add Component"))
            {
                if (m_PopupShowed && m_Index != 0)
                {
                    SpellComponent component = Activator.CreateInstance(m_DerivedList[m_Index - 1]) as SpellComponent;
                    spell.AddComponent(component);
                }
                m_PopupShowed = !m_PopupShowed;
            }
            if (m_PopupShowed)
            {
                m_Index = EditorGUILayout.Popup(m_Index, m_DerivedListNames.ToArray());
            }
            if (GUILayout.Button("Delete"))
            {
                spell.DeleteComponent();
            }
            //serializedObject.Update();
            serializedObject.UpdateIfRequiredOrScript();
        }

        private IEnumerable<Type> GetAllDerived()
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => type.IsSubclassOf(typeof(SpellComponent)))
                .Where(type => !type.IsAbstract);
        }
    }
}
#endif