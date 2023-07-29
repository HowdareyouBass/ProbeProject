using UnityEngine;
using System.Collections.Generic;

//TODO: Should derive from some component base class
[CreateAssetMenu(menuName = "Spell", fileName = "New Spell")]
public class Spell : ScriptableObject
{
    [SerializeReference] private List<SpellComponent> m_Components;
    
    [HideInInspector] public GameObject spellGameObject;
    public SpellEvents events { get; private set; }

    public List<SpellComponent> components { get => m_Components; }

    public void OnEnable()
    {
        if (m_Components == null)
            m_Components = new List<SpellComponent>();
        if (events == null)
            events = new SpellEvents();
    }

    public void Init(Transform caster)
    {
        foreach (SpellComponent component in m_Components)
        {
            component.caster = caster;
            component.spell = this;
            component.Init();
        }
        foreach (SpellComponent component1 in m_Components)
        {
            component1.LateInit();
        }
    }
    public void Cast(Target target)
    {
        foreach (SpellComponent component in m_Components)
        {
            component.target = target;
        }
        events.GetEvent(SpellEventName.OnTryCast).Trigger();
    }
#region Components
    //Everything related to components

    //In this method we give priority to exactly same type, if there is no such we take first of subclass
    //And if we have no subclass types we return default value wich for SpellComponent is null
    public T GetComponent<T>() where T : SpellComponent
    {
        bool isSubclassFound = false;
        T result = default(T);
        foreach (SpellComponent component in m_Components)
        {
            if (component.GetType() == typeof(T))
            {
                return (T)component;
            }
            if (component.GetType().IsSubclassOf(typeof(T)) && !isSubclassFound)
            {
                result = (T)component;
                isSubclassFound = true;
            }
        }
        return result;
    }
    public bool HasComponentOfType<T>() where T : SpellComponent
    {
        return GetComponent<T>() != null;
    }
    public bool TryGetComponent<T>(out T component) where T : SpellComponent
    {
        component = GetComponent<T>();
        return component != null;
    }
    public void AddComponent(SpellComponent component)
    {
        foreach (SpellComponent other in m_Components)
        {
            if (component.GetType() == other.GetType())
            {
                Debug.LogWarning("This component is already attached to this spell");
                return;
            }
        }
        m_Components.Add(component);
    }
    public void DeleteComponent()
    {
        if (m_Components.Count != 0)
            m_Components.RemoveAt(m_Components.Count - 1);
    }
#endregion
}