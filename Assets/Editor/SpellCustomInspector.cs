#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using System.Linq;
using System;
using System.Collections.Generic;
//using BinaryEgo.Editor.UI;

//2 inspectors cuz unity can't serialize generic types
//TODO:Should do 1 inspector and derive from it
//And try Unity UI Toolkit

[CustomEditor(typeof(Spell))]
[CanEditMultipleObjects]
public class SpellCustomInspector : Editor 
{
    private List<Type> m_DerivedList;
    private List<string> m_DerivedListNames;
    //private GenericMenuPopup m_Popup;
    private Spell m_Object;
    //For old UI
    private bool m_ShowPopup = false;
    private int m_Index = 0;

    public void OnEnable()
    {
        m_Object = ((Spell)target);
        //New UI
        // if (m_Popup == null)
        // {
        //     m_Popup = GenericMenuPopup.Get(GetTypeMenu(typeof(SpellComponent)), "Components");
        //     m_Popup.width = 220;
        //     m_Popup.showSearch = true;
        //     m_Popup.showTooltip = false;
        //     m_Popup.resizeToContent = true;
        // }
        if (m_DerivedList == null)
            m_DerivedList = GetAllDerived(typeof(SpellComponent)).ToList();
        if (m_DerivedListNames == null)
        {
            m_DerivedListNames = new List<string>();
            m_DerivedListNames.Add("None");
            foreach (Type type in m_DerivedList)
            {
                m_DerivedListNames.Add(type.ToString().Substring(2));
            }
        }
    }
    //FIXME: Doesn't update in realtime
    //TODO: Implement new UI for spells and status effects using UnityUI toolkit
    // public override VisualElement CreateInspectorGUI()
    // {
    //     VisualElement root = new VisualElement();

    //     SerializedProperty property = serializedObject.FindProperty("m_Components");
    //     PropertyField fl = new PropertyField(property);
    //     fl.RegisterValueChangeCallback((evt) => {});
    //     // root.Add(fl);

    //     // int i = 0;
    //     // foreach (SerializedProperty prop in property)
    //     // {
    //     //     //the 18 deletes Assembly CSharp before real type
    //     //     PropertyField propertyField = new PropertyField(prop, prop.managedReferenceFullTypename.Substring(18));
    //     //     // TODO: Delete component button
    //     //     // Button deleteButton = new Button(() => { DeleteComponent(i); });
    //     //     // deleteButton.text = "DeleteComponent";
    //     //     // propertyField.Add(deleteButton);
    //     //     root.Add(propertyField);
    //     //     i++;
    //     // }

    //     Button addComponentButton = new Button(ShowPopup) { text = "Add component" };
    //     Button deleteComponentButton = new Button(DeleteLastComponent) { text = "Delete last component" };
    //     root.Add(addComponentButton);
    //     root.Add(deleteComponentButton);
    //     root.MarkDirtyRepaint();
    //     return root;
    // }

    public override void OnInspectorGUI()
    {
        m_Object = ((Spell)target);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("m_SpellAnimation"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("m_SpellSound"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("m_SpellSoundVolume"));
        
        SerializedProperty property = serializedObject.FindProperty("m_Components");
        foreach (SerializedProperty prop in property)
        {
            //the 18 deletes Assembly CSharp before real type
            EditorGUILayout.PropertyField(prop, new GUIContent(prop.managedReferenceFullTypename.Substring(18)), true);
        }
        if (m_ShowPopup)
        {
            m_Index = EditorGUILayout.Popup(m_Index, m_DerivedListNames.ToArray());
            EditorGUILayout.Space();
            if (GUILayout.Button("Add"))
            {
                SpellComponent component = Activator.CreateInstance(m_DerivedList[m_Index - 1]) as SpellComponent;
                if (component == null)
                {
                    Debug.LogError("Something went wrong with component creation");
                }
                else
                {
                    m_Object.AddComponent(component);
                }
                m_ShowPopup = false;
            }
            if (GUILayout.Button("Cancel"))
            {
                m_ShowPopup = false;
            }
            EditorGUILayout.Space();
        }
        if (GUILayout.Button("Add Component"))
        {
            m_ShowPopup = !m_ShowPopup;
            //m_Popup.Show(Event.current.mousePosition);
        }
        if (GUILayout.Button("Delete"))
        {
            m_Object.DeleteComponent();
        }
        serializedObject.ApplyModifiedProperties();
        serializedObject.Update();
    }

    private void AddComponent(Type type)
    {
        SpellComponent component = Activator.CreateInstance(type) as SpellComponent;
        m_Object.AddComponent(component);
    }
    private void DeleteComponent(int index)
    {
        m_Object.components.RemoveAt(index);
    }
    private void DeleteLastComponent()
    {
        m_Object.DeleteComponent();
    }

    private IEnumerable<Type> GetAllDerived(Type _type)
    {
        return AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(assembly => assembly.GetTypes())
            .Where(type => type.IsSubclassOf(_type))
            .Where(type => !type.IsAbstract);
    }

    //New UI stuff
    // private GenericMenu GetTypeMenu(Type p_type)
    // {
    //     GenericMenu menu = new GenericMenu();
        
    //     Type[] types = GetAllDerived(p_type).ToArray();
    //     Array.Sort(types, (t1, t2) => t1.ToString().CompareTo(t2.ToString()));

    //     foreach (Type type in types)
    //     {
    //         string name = type.ToString();//.Substring(type.ToString().IndexOf(".") + 1);
    //         name = name.Replace('.', '/');
    //         //name = name.Substring(0, name.Length-4);
            
            
    //         menu.AddItem(new GUIContent(name, type.ToString()), false, () => { AddComponent(type); });
    //     }

    //     return menu;
    // }
}
#endif