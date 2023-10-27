using System;
using System.Threading.Tasks;
using UnityEngine;

//TODO: might derive time component and count component from same class
//TODO: Rename all fields with Big first letter
[Serializable]
public abstract class SE_TimeComponent : StatusEffectComponent
{
    public event Action OnEffectApplied;
    public event Action OnEffectDeapplied;

    [field: SerializeField] public float DurationInSeconds { get; private set; }
    public float LeftTime { get; private set; } = 0;

    public override void Init()
    {
        LeftTime = DurationInSeconds;
        statusEffect.effectTasks.Add(Timer());
    }
    public override void Destroy()
    {
        
    }

    private async Task Timer()
    {
        float timer = 0;
        OnEffectApplied?.Invoke();
        while(!statusEffect.stopEffectToken.IsCancellationRequested && LeftTime > 0)
        {
            Debug.Log(timer);
            // PerMillisecond();
            timer += Time.deltaTime;
            if (timer >= 1)
            {
                PerSecond();
                timer = 0;
            }
            LeftTime -= Time.deltaTime;
            await Task.Yield();
        }
        // Only awake effect deapplied if effect was not canceled from another place
        if (!statusEffect.stopEffectToken.IsCancellationRequested)
        {
            OnEffectDeapplied?.Invoke();
        }
    }

    protected virtual void PerMillisecond()
    {
    }
    protected virtual void PerSecond()
    {
    }
}