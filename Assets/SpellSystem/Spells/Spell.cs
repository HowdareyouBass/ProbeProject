using UnityEngine;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(menuName = "Spell", fileName = "New Spell")]
public class Spell : ScriptableObject
{
    [HideInInspector] public GameObject spellGameObject;
    [SerializeField] private List<SpellComponent> m_Components;
    public List<SpellComponent> components { get => m_Components; }
    public SpellEvents events { get; private set; }

    public void OnEnable()
    {
        if (events == null)
            events = new SpellEvents();
        if (components == null)
            m_Components = new List<SpellComponent>();
    }

    public void Init(Transform caster)
    {
        foreach (SpellComponent component in m_Components)
        {
            component.caster = caster;
        }
    }
    public void Cast(Transform target)
    {
        foreach (SpellComponent component in m_Components)
        {
            component.target = target;
            component.Init();
        }
        foreach (ActiveSpellComponent active in m_Components)
        {
            active.TryCast();
        }
    }
    public T GetComponent<T>() where T : SpellComponent
    {
        foreach (SpellComponent component in m_Components)
        {
            if (component.GetType() == typeof(T))
                return (T)component;
        }
        return null;
    }
    public void AddComponent<T>() where T : SpellComponent
    {
        m_Components.Add(ScriptableObject.CreateInstance<T>());
    }
    public void AddComponent(SpellComponent component)
    {
        m_Components.Add(component);
    }
    public void DeleteComponent()
    {
        if (m_Components.Count != 0)
            m_Components.RemoveAt(m_Components.Count - 1);
    }
    #if UNITY_EDITOR
    public void OnGUI()
    {
        foreach (SpellComponent component in m_Components)
        {
            component.OnGUI();
            EditorGUILayout.Space();
        }
    }
    #endif
}