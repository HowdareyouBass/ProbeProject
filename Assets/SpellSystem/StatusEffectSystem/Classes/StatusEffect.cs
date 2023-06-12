using UnityEngine;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System;

//TODO: Rethink about tasks and everything
//Didn't add component base because Unity doesn't support generic classes serializaiton
[CreateAssetMenu(menuName = "Status Effect", fileName = "New Status Effect")]
public class StatusEffect : ScriptableObject
{
    [SerializeField] private float m_DefaultDuration = 3;
    private CancellationTokenSource m_StopEffectSource;
    public List<Task> effectTasks { get; private set; }

    public CancellationToken stopEffectToken { get; private set; }
    public float defaultDuration { get => m_DefaultDuration; }

    public event Action<StatusEffect> OnEffectEnd;

    public void OnEnable()
    {
        effectTasks = new List<Task>();
        m_StopEffectSource = new CancellationTokenSource();
        stopEffectToken = m_StopEffectSource.Token;
        if (m_Components == null)
        {
            m_Components = new List<StatusEffectComponent>();
        }
    }
    public void Init(Transform target)
    {
        foreach (StatusEffectComponent component in m_Components)
        {
            component.statusEffect = this;
            component.target = target;
            component.Init();
        }
    }
    public void StopEffect()
    {
        m_StopEffectSource.Cancel();
    }

    public async void StartEffect()
    {
        await Task.WhenAll(effectTasks);
        //Idk can't debug target ????? FIXME:
        foreach (StatusEffectComponent component in m_Components)
        {
            component.Destroy();
        }
        OnEffectEnd.Invoke(this);
    }
#region Components
    [SerializeReference] private List<StatusEffectComponent> m_Components;
    public List<StatusEffectComponent> components { get => m_Components; }
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
#endregion
}
