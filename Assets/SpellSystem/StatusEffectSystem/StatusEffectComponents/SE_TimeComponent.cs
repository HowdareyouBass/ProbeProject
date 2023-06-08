using System;
using System.Threading.Tasks;
using UnityEngine;

//TODO: might derive time component and count component from same class
[Serializable]
public abstract class SE_TimeComponent : StatusEffectComponent
{
    public event Action OnEffectApplied;
    public event Action OnEffectDeapplied;

    [SerializeField] private float m_DurationInSeconds;
    private float m_CurrentDuration = 0;

    public override void Init()
    {
        m_CurrentDuration = m_DurationInSeconds;
        statusEffect.effectTasks.Add(Timer());
    }
    public override void Destroy()
    {
    }

    public async Task Timer()
    {
        OnEffectApplied?.Invoke();
        while(!statusEffect.stopEffectToken.IsCancellationRequested && m_CurrentDuration > 0)
        {
            m_CurrentDuration -= Time.deltaTime;
            await Task.Yield();
        }
        OnEffectDeapplied?.Invoke();
    }
}