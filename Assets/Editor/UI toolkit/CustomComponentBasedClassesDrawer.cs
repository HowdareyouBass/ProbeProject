using UnityEditor;
using UnityEngine;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using System.Collections.Generic;
using System;
using System.Linq;

//TODO: Generic Editor for all component based classes
//FIXME: Doesn't automatically update :(

//[CustomEditor(typeof(Spell), true)]
public class CustomComponentBasedClassesDrawer : Editor
{
    private Spell m_Spell;
    private VisualElement m_Root;
    private PopupField<SpellComponent> m_Popup;
    private Button m_ApplyButton;
    private List<SpellComponent> m_AllSpellComponents;

    public void OnEnable()
    {
        m_Spell ??= (Spell)target;
        m_ApplyButton ??= new Button(AddComponent);
        m_ApplyButton.text = "Apply";
        m_AllSpellComponents = GetAllDerived(typeof(SpellComponent)).Select(t => (SpellComponent)Activator.CreateInstance(t)).ToList();
        m_Popup ??= new PopupField<SpellComponent>(m_AllSpellComponents, m_AllSpellComponents[0], PopupCallback1, PopupCallback2);
    }

    public override VisualElement CreateInspectorGUI()
    {
        m_Root = new VisualElement();
        foreach(SerializedProperty property in serializedObject.FindProperty("m_Components"))
        {
            m_Root.Add(new PropertyField(property, property.type.Substring(16)));
        }
        Button addButton = new Button(ShowPopup);
        addButton.text = "Add";
        m_Root.Add(addButton);
        return m_Root;
    }
    private string PopupCallback1(SpellComponent spellComponent)
    {
        Debug.Log("Callback #1");
        return spellComponent.GetType().ToString();
    }
    private string PopupCallback2(SpellComponent spellComponent)
    {
        Debug.Log("Callback #2");
        return spellComponent.GetType().ToString();
    }
    private void ShowPopup()
    {
        m_Root.Add(m_Popup);
        m_Root.Add(m_ApplyButton);
    }
    private void AddComponent()
    {
        m_Spell.AddComponent(m_Popup.value);
        m_Root.Remove(m_Popup);
        m_Root.Remove(m_ApplyButton);
        m_Root.Add(new PropertyField(serializedObject.FindProperty("m_Components").GetArrayElementAtIndex(m_Spell.components.Count - 1)));
    }
    private IEnumerable<Type> GetAllDerived(Type _type)
    {
        return AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(assembly => assembly.GetTypes())
            .Where(type => type.IsSubclassOf(_type))
            .Where(type => !type.IsAbstract);
    }
}
