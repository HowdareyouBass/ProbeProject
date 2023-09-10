using UnityEngine;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System;

//TODO: Rethink about tasks and everything
//TODO: Component base
[CreateAssetMenu(menuName = "Status Effect", fileName = "New Status Effect")]
public class StatusEffect : ScriptableObject
{
    //[SerializeField] private string m_Name;
    [SerializeField] private float m_DefaultDuration = 3;
    private CancellationTokenSource m_StopEffectSource;
    public List<Task> effectTasks { get; private set; }

    public CancellationToken stopEffectToken { get; private set; }
    public float defaultDuration { get => m_DefaultDuration; }
    /// <summary>
    /// Returns formatted name of StatusEffect
    /// </summary>
    public string Name
    { 
        get
        {
            string result = name;
            if (result.LastIndexOf("(Clone)") != -1)
            {
                result = result.Substring(0, result.LastIndexOf("(Clone)"));
            }
            if (result.LastIndexOf(".effect") != -1)
            {
                result = result.Substring(0, result.LastIndexOf(".effect"));
            }
            return result;
        }
    }

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
    // Unity cannot be used in asyng task methods because unity api only works on 1 thread
    public async void StartEffect()
    {
        await Task.WhenAll(effectTasks);
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
