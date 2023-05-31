using UnityEngine;
using System.Collections.Generic;

//Didn't add component base because Unity doesn't support generic classes serializaiton
[CreateAssetMenu(menuName = "Status Effect", fileName = "New Status Effect")]
public class StatusEffect : ScriptableObject
{
    [SerializeReference] private List<StatusEffectComponent> m_Components;
    public List<StatusEffectComponent> components { get => m_Components; }
    public void OnEnable()
    {
        if (m_Components == null)
        {
            //Debug.Log("m_Component is null");
            //Debug.Log(typeof(StatusEffectComponent));
            m_Components = new List<StatusEffectComponent>();
        }
    }
    //Opana kostyli
    //TODO: Refactor this dude it looks so fucking bad
    public T GetComponent<T>() where T : StatusEffectComponent
    {
        foreach (StatusEffectComponent component in m_Components)
        {
            if (component.GetType() == typeof(T))
                return (T)component;
        }
        foreach (StatusEffectComponent component1 in m_Components)
        {
            if (component1.GetType().IsSubclassOf(typeof(T)))
                return (T)component1;
        }
        return default(T);
    }
    public bool TryGetComponent<T>(out T component) where T : StatusEffectComponent
    {
        component = GetComponent<T>();
        return component != null;
    }
    public void AddComponent(StatusEffectComponent component)
    {
        foreach (StatusEffectComponent other in m_Components)
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
}
