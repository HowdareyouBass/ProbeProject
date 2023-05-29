using UnityEngine;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(menuName = "Spell", fileName = "New Spell")]
[System.Serializable]
public class Spell : ScriptableObject
{
    [HideInInspector] public GameObject spellGameObject;
    [SerializeReference] private List<SpellComponent> m_Components;
    public List<SpellComponent> components { get => m_Components; }
    public SpellEvents events { get; private set; }

    public void OnEnable()
    {
        //Debug.Log(m_Components == null);
        if (events == null)
            events = new SpellEvents();
        if (components == null)
            m_Components = new List<SpellComponent>();
        //hideFlags = HideFlags.HideAndDontSave;
    }

    public void Init(Transform caster)
    {
        foreach (SpellComponent component in m_Components)
        {
            component.caster = caster;
            //component.spell = this;
        }
    }
    public void Cast(Transform target)
    {
        foreach (SpellComponent component in m_Components)
        {
            component.target = target;
        }
        foreach (ActiveSpellComponent active in m_Components)
        {
            active.TryCast();
        }
    }
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
        return null;
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