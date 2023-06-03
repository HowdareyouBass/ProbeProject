using UnityEngine;
using System.Collections.Generic;

//Didn't add component base because Unity doesn't support generic classes serializaiton
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
    }
    public void Cast(Target target)
    {
        foreach (SpellComponent component in m_Components)
        {
            component.target = target;
        }
        events.GetEvent(SpellEventName.OnCast).Trigger();
    }
#region Components
    //Everything related to components

    //Opana kostyli
    //TODO: Refactor this dude it looks so fucking bad
    public T GetComponent<T>() where T : SpellComponent
    {
        foreach (SpellComponent component in m_Components)
        {
            if (component.GetType() == typeof(T))
                return (T)component;
        }
        foreach (SpellComponent component1 in m_Components)
        {
            if (component1.GetType().IsSubclassOf(typeof(T)))
                return (T)component1;
        }
        return default(T);
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